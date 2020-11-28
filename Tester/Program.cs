using KDABackendLibrary;
using KDABackendLibrary.Helpers;
using KDABackendLibrary.Models;
using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using KDASharedLibrary.Helpers;
using System.Threading;

namespace Tester
{
    class Program
    {

        static void Main(string[] args)
        {
            //var hash = File.ReadAllText(@"C:\Users\mhdb9\Desktop\hash.txt");
            //byte[] data = System.Text.Encoding.ASCII.GetBytes(Console.ReadLine());
            //data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            //String hash = System.Text.Encoding.ASCII.GetString(data);
            //File.WriteAllText(@"C:\Users\mhdb9\Desktop\hash.txt", hash, Encoding.ASCII);
            GlobalConfig.InitializeConnections();
            //var t = GlobalConfig.Connection.Sessions_GetByUserIdAndDate(12, new DateTime(2020, 9, 1), new DateTime(2020, 10, 1));
            //var t = BinaryConnector.StaticLoad<KeystrokeData[]>(@"C:\Users\mhdb9\Desktop\02-11-2020\data-file_16.kdf");
            WrongDataFixer.CreateDatabaseRecordsFromCorrectFiles(@"C:\Users\mhdb9\Desktop\nurgul", 17);
            //CreateDatabaseRecordsFromFiles();
            //var user = new UserModel {Id = 12 };
            //GlobalConfig.Connection.GetHoldTimesStatistics(user);
            //Dictionary<int,int> counts = new Dictionary<int, int>();
            //foreach (var key in (KeysList[])Enum.GetValues(typeof(KeysList)))
            //{
            //    counts.Add((int)key, 0);
            //}
            //foreach (var session in user.Sessions)
            //{
            //    foreach (var key in session.SessionKeys)
            //    {
            //        counts[key.KeyId]++;
            //    }
            //}
            //var mylist = counts.ToList();
            //var t = mylist.OrderByDescending(x => x.Value);

            //CreateDataset();
            Console.WriteLine("finished");
            Console.ReadLine();
        }
        static void CreateDataset()
        {
            var keyCombinations = GlobalConfig.Connection.KeyCombinations_GetUsedCombinationsByAllUsers(); 
            List<int[]> dataset = new List<int[]>();
            Dictionary<int, int> comboIndexes = new Dictionary<int, int>();
            int comboCount = keyCombinations.Count;
            int index = 2 + FileHelper.GetEnumCount<KeysList>() - 1;
            foreach (var combo in keyCombinations)
            {
                comboIndexes.Add(combo.Id, index);
                index++;
            }
            var sessions = GlobalConfig.Connection.Dataset_GetAll();
            for (int i = 1; i < sessions.Count; i++)
            {
                int[] features = new int[2 + FileHelper.GetEnumCount<KeysList>() + comboCount];
                features[0] = sessions[i].Id;
                features[1] = sessions[i].UserId;
                foreach (var key in sessions[i].SessionKeys)
                {
                    features[key.KeyId + 2] = key.HoldTimesAvg;
                }
                foreach (var combo in sessions[i].SessionCombinations)
                {
                    if (comboIndexes.ContainsKey(combo.KeyCombinationId))
                    {
                        features[comboIndexes[combo.KeyCombinationId]] = combo.SeekTimesAvg;
                    }
                }
                dataset.Add(features);
            }
            //WriteDatasetToCsv(dataset);
        }

        static void WriteDatasetToCsv(List<int[]> dataset)
        {
            StringBuilder allData = new StringBuilder();
            foreach (var session in dataset)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in session)
                {
                    sb.Append(item + ",");
                }
                allData.AppendLine(sb.ToString());
            }
            File.WriteAllText(@"C:\Users\mhdb9\Desktop\newDataset2.csv", allData.ToString());
        }
        static void AddKeysToDB()
        {
            var values = Enum.GetValues(typeof(KeysList));
            foreach (KeysList key in values)
            {
                GlobalConfig.Connection.Key_Insert(key);
            }
        }

        static void AddKeyCombinationsToDb()
        {
            foreach (var from in Enum.GetValues(typeof(KeysList)))
            {
                foreach (var to in Enum.GetValues(typeof(KeysList)))
                {
                    GlobalConfig.Connection.KeyCombinations_Insert((int)from, (int)to);
                }
            }
        }
        
    }
}
