using KDABackendLibrary.Models;
using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.Helpers
{
    public static class WrongDataFixer
    {
        static List<FileInfo> GetOrderedFileList(string dirPath)
        {
            string[] datafiles = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
            List<FileInfo> allFiles = new List<FileInfo>();
            foreach (var path in datafiles)
            {
                FileInfo f = new FileInfo(path);
                allFiles.Add(f);
            }
            allFiles = allFiles.OrderBy(o => o.LastWriteTime).ToList();
            return allFiles;
        }
        static List<DateTime> GetDatesOfFileList(List<FileInfo> allFiles)
        {
            var dates = new List<DateTime>();
            foreach (var file in allFiles)
            {
                string date = file.Directory.ToString().Split('\\').Last();
                var dateParts = date.Split('-');
                var time = file.Name;
                string b = string.Empty;
                for (int i = 0; i < time.Length; i++)
                {
                    if (Char.IsDigit(time[i]))
                        b += time[i];
                }
                DateTime d = new DateTime(Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[0]), int.Parse(b), 0, 0);
                dates.Add(d);
            }
            return dates;
        }
        static List<KeystrokeData[]> GetDataFromFileList(List<FileInfo> allFiles)
        {
            var oldData = new List<KeystrokeData[]>();
            foreach (var file in allFiles)
            {
                var AllKeystrokeData = BinaryConnector.StaticLoad<KeystrokeData[]>(file.FullName);
                oldData.Add(AllKeystrokeData);
            }
            return oldData;
        }
        static List<KeystrokeData[]> GetFixedDataFromWrongDataList(List<KeystrokeData[]> oldData)
        {
            var newData = new List<KeystrokeData[]>();
            int[] holdIndexes = new int[FileHelper.GetEnumCount<KeysList>()];
            int[,] seekIndexes = new int[FileHelper.GetEnumCount<KeysList>(), FileHelper.GetEnumCount<KeysList>()];
            for (int s = 0; s < oldData.Count; s++)
            {
                KeystrokeData[] newKeyData = new KeystrokeData[oldData[s].Length];
                for (int k = 0; k < oldData[s].Length; k++)
                {
                    if (oldData[s][k] != null)
                    {
                        int keyIndex = oldData[s][k].Key.KeyIndex;
                        if (s != 0 && oldData[s - 1][k] != null)
                        {
                            newKeyData[k] = new KeystrokeData();
                            newKeyData[k].Key = oldData[s][k].Key;
                            newKeyData[k].HoldTimes = oldData[s][k].HoldTimes.GetRange(holdIndexes[keyIndex], oldData[s][k].HoldTimes.Count - oldData[s - 1][k].HoldTimes.Count);
                            holdIndexes[keyIndex] += newKeyData[k].HoldTimes.Count;
                            for (int sk = 0; sk < oldData[s][k].SeekTimes.Length; sk++)
                            {
                                if (oldData[s][k].SeekTimes[sk] != null)
                                {
                                    if (oldData[s - 1][k].SeekTimes[sk] != null)
                                    {
                                        newKeyData[k].SeekTimes[sk] = oldData[s][k].SeekTimes[sk].GetRange(seekIndexes[keyIndex, sk], oldData[s][k].SeekTimes[sk].Count - oldData[s - 1][k].SeekTimes[sk].Count);
                                        seekIndexes[k, sk] += newKeyData[k].SeekTimes[sk].Count;
                                    }
                                }
                            }
                        }
                        else
                        {
                            holdIndexes[keyIndex] += oldData[s][k].HoldTimes.Count;
                            for (int sk = 0; sk < oldData[s][k].SeekTimes.Length; sk++)
                            {
                                if (oldData[s][k].SeekTimes[sk] != null)
                                {
                                    seekIndexes[k, sk] += oldData[s][k].SeekTimes[sk].Count;
                                }
                            }
                            newKeyData[k] = oldData[s][k];
                        }
                    }
                }
                newData.Add(newKeyData);
            }
            return newData;
        }
        static void CreateSeekTimesForKeyData(SessionModel session, KeystrokeData keyData)
        {
            for (int j = 0; j < keyData.SeekTimes.Length; j++)
            {
                if (keyData.SeekTimes[j] != null && keyData.SeekTimes[j].Count != 0)
                {
                    SessionCombinationModel seekTimeData = new SessionCombinationModel();
                    seekTimeData.SeekTimesCount = keyData.SeekTimes[j].Count;
                    seekTimeData.SeekTimesAvg = keyData.SeekTimes[j].Sum(x => x) / keyData.SeekTimes[j].Count;
                    foreach (var item in keyData.SeekTimes[j])
                    {
                        var m = new SessionCombinationNumberModel();
                        m.SessionCombination = seekTimeData;
                        m.Value = item;
                        seekTimeData.SessionCombinationNumbers.Add(m);
                    }
                    seekTimeData.KeyCombination.FromKeyId = j;
                    seekTimeData.KeyCombination.ToKeyId = keyData.Key.KeyIndex;
                    seekTimeData.Session = session;
                    session.SessionCombinations.Add(seekTimeData);
                }
            }
        }
        static void CreateKeyDataForSession(SessionModel session, KeystrokeData keyData)
        {
            if (keyData != null && keyData.HoldTimes.Count != 0)
            {
                SessionKeyModel model = new SessionKeyModel();
                model.HoldTimesCount = keyData.HoldTimes.Count;
                model.HoldTimesAvg = keyData.HoldTimes.Sum(x => x) / keyData.HoldTimes.Count;
                foreach (var item in keyData.HoldTimes)
                {
                    var m = new HoldTimeNumberModel();
                    m.Value = item;
                    m.SessionKey = model;
                    model.HoldTimeNumbers.Add(m);
                }
                model.KeyId = keyData.Key.KeyIndex;
                model.Session = session;
                session.SessionKeys.Add(model);
                CreateSeekTimesForKeyData(session, keyData);
            }
        }
        static void CreateSessionRecordsFromDataList(List<KeystrokeData[]> newData, List<DateTime> dates, int UserId)
        {
            for (int i = 0; i < newData.Count; i++)
            {
                SessionModel session = new SessionModel();
                session.User = new UserModel { Id = UserId };
                session.StartTime = dates[i];
                session.EndTime = dates[i].AddHours(1);
                session.SessionCombinations = new List<SessionCombinationModel>();
                session.SessionKeys = new List<SessionKeyModel>();
                foreach (var keyData in newData[i])
                {
                    CreateKeyDataForSession(session, keyData);
                }
                Console.WriteLine($"{i} - session");
                Console.WriteLine("reading finishid");
                Console.WriteLine(DateTime.Now.ToString("h:mm:ss"));
                GlobalConfig.Connection.Sessions_Insert(session);
            }
        }
        public static void CreateDatabaseRecordsFromWrongFiles(string dirPath, int userId)
        {
            List<FileInfo> allFiles = GetOrderedFileList(dirPath);
            List<DateTime> dates = GetDatesOfFileList(allFiles);
            List<KeystrokeData[]> oldData = GetDataFromFileList(allFiles);
            List<KeystrokeData[]> newData = GetFixedDataFromWrongDataList(oldData);
            CreateSessionRecordsFromDataList(newData, dates, userId);
        }
        public static void CreateDatabaseRecordsFromCorrectFiles(string dirPath, int userId)
        {
            List<FileInfo> allFiles = GetOrderedFileList(dirPath);
            List<DateTime> dates = GetDatesOfFileList(allFiles);
            List<KeystrokeData[]> data = GetDataFromFileList(allFiles);
            CreateSessionRecordsFromDataList(data, dates, userId);
        }
    }
}
