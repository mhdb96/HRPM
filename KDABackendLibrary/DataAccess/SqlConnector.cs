using Dapper;
using KDABackendLibrary.Models;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        public static string databaseName = "KDA";

        public void File_Insert(FileModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@Path", model.Path);
                p.Add("@UserId", model.User.Id);
                p.Add("@Date", model.Date);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spFiles_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
        }

        public void Key_Insert(KeysList key)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@Id", (int)key);
                p.Add("@Code", key.ToString());
                p.Add("@Name", key.GetDescription());
                connection.Execute("dbo.spKeys_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }

        public void Log_Insert(LogModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@Text", model.Text);
                p.Add("@UserId", model.User.Id);
                p.Add("@Type", model.Type.GetDescription());
                p.Add("@Date", model.Date);
                p.Add("@Severity", model.Severity.GetDescription());
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spLogs_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
        }

        public bool User_Exists(UserModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                List<UserModel> users = new List<UserModel>();
                var p = new DynamicParameters();
                p.Add("@UserName", model.UserName);
                users = connection.Query<UserModel>("dbo.spUsers_GetUserByUserName", p, commandType: CommandType.StoredProcedure).ToList();
                if (users.Any())
                {
                    model.Id = users.FirstOrDefault().Id;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void User_Insert(UserModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@UserName", model.UserName);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spUsers_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
        }
        public void SessionKeys_Insert(SessionKeyModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@HoldTimesCount", model.HoldTimesCount);
                p.Add("@HoldTimesAvg", model.HoldTimesAvg);
                p.Add("@KeyId", model.KeyId);
                p.Add("@SessionId", model.Session.Id);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spSessionKeys_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
            foreach (var ht in model.HoldTimeNumbers)
            {
                HoldTimeNumbers_Insert(ht);
            }
        }
        public void SessionCombinations_Insert(SessionCombinationModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@SeekTimesCount", model.SeekTimesCount);
                p.Add("@SeekTimesAvg", model.SeekTimesAvg);
                p.Add("@SessionId", model.Session.Id);
                p.Add("@FromKeyId", model.KeyCombination.FromKeyId);
                p.Add("@ToKeyId", model.KeyCombination.ToKeyId);
                //p.Add("@KeyCombinationId", model.KeyCombination.Id);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spSessionCombinations_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
            foreach (var st in model.SessionCombinationNumbers)
            {
                SeekTimeNumbers_Insert(st);
            }
        }
        public void SeekTimeNumbers_Insert(SessionCombinationNumberModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@SessionCombinationId", model.SessionCombination.Id);
                p.Add("@Value", model.Value);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spSessionCombinationNumbers_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
        }

        public void KeyCombinations_Insert(int fromId, int toId)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@FromKeyId", fromId);
                p.Add("@ToKeyId", toId);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spKeyCombinations_Insert", p, commandType: CommandType.StoredProcedure);
                //model.Id = p.Get<int>("@id");
            }
        }

        public void HoldTimeNumbers_Insert(HoldTimeNumberModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@SessionKeyId", model.SessionKey.Id);
                p.Add("@Value", model.Value);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spHoldTimeNumbers_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
        }

        public List<SessionKeyModel> KeystrokeData_GetBySessionId(int id)
        {
            throw new NotImplementedException();
            //List<SessionKeyModel> model;
            //using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            //{
            //    var p = new DynamicParameters();
            //    p.Add("@SessionId", id);
            //    using (var multi = connection.QueryMultiple("dbo.spKeystrokeData_GetBySessionId", p, commandType: CommandType.StoredProcedure))
            //    {
            //        model = multi.Read<SessionKeyModel>().ToList();
            //        var seektimes = multi.Read<SessionCombinationModel>().ToList();
            //        foreach (var kd in model)
            //        {
            //            kd.SeekTimes = seektimes.Where(x => x.KeystrokeDataId == kd.Id).ToList();
            //        }
            //    }
            //}
            //return model;
        }

        public void Sessions_Insert(SessionModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@StartTime", model.StartTime);
                p.Add("@EndTime", model.EndTime);
                p.Add("@UserId", model.User.Id);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spSession_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }            
            foreach (var sessionKey in model.SessionKeys)
            {
                SessionKeys_Insert(sessionKey);
            }
            Console.WriteLine("keys finishid");
            Console.WriteLine(DateTime.Now.ToString("h:mm:ss"));            
            foreach (var sessionCombination in model.SessionCombinations)
            {
                SessionCombinations_Insert(sessionCombination);
            }
            Console.WriteLine("seek finishid");
            Console.WriteLine(DateTime.Now.ToString("h:mm:ss"));
        }

        public void GetHoldTimesStatistics(UserModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", model.Id);
                using (var multi = connection.QueryMultiple("dbo.spSessionKeys_GetByUserId", p, commandType: CommandType.StoredProcedure))
                {
                    model.Sessions = multi.Read<SessionModel>().ToList();
                    var sessionKeys = multi.Read<SessionKeyModel>().ToList();
                    foreach (var session in model.Sessions)
                    {
                        session.SessionKeys = sessionKeys.Where(x => x.SessionId == session.Id).ToList();
                    }
                }
            }
        }

        public List<SessionModel> Sessions_GetByUserIdAndDate(int userId, DateTime start, DateTime end)
        {
            List<SessionModel> sessions;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@StartDate", start);
                p.Add("@EndDate", end);
                using (var multi = connection.QueryMultiple("dbo.spSessions_GetUserByIdAndDate", p, commandType: CommandType.StoredProcedure))
                {
                    sessions = multi.Read<SessionModel>().ToList();
                    var sessionKeys = multi.Read<SessionKeyModel>().ToList();
                    foreach (var session in sessions)
                    {
                        session.SessionKeys = sessionKeys.Where(x => x.SessionId == session.Id).ToList();
                    }
                }
            }
            return sessions;
        }

        public List<KeyCombinationModel> KeyCombinations_GetUsedCombinationsByAllUsers()
        {
            List<KeyCombinationModel> model = new List<KeyCombinationModel>();
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                
                model = connection.Query<KeyCombinationModel>("dbo.spKeyCombinations_GetUsedCombinationsByAllUsers", commandType: CommandType.StoredProcedure).ToList();
            }
            return model;
        }

        public List<SessionModel> Dataset_GetAll()
        {
            List<SessionModel> sessions;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                using (var multi = connection.QueryMultiple("dbo.spDataset_GetAll", commandType: CommandType.StoredProcedure))
                {
                    sessions = multi.Read<SessionModel>().ToList();
                    var sessionKeys = multi.Read<SessionKeyModel>().ToList();
                    var holdTimes = multi.Read<HoldTimeNumberModel>().ToList();
                    var sessionCombos = multi.Read<SessionCombinationModel>().ToList();                    
                    var seekTimes = multi.Read<SessionCombinationNumberModel>().ToList();
                    int count = sessions.Count;
                    int i = 1;
                    foreach (var session in sessions)
                    {
                        session.SessionKeys = sessionKeys.Where(x => x.SessionId == session.Id).ToList();
                        session.SessionCombinations = sessionCombos.Where(x => x.SessionId == session.Id).ToList();
                        foreach (var key in session.SessionKeys)
                        {
                            key.HoldTimeNumbers = holdTimes.Where(x => x.SessionKeyId == key.Id).ToList();
                        }
                        foreach (var combo in session.SessionCombinations)
                        {
                            combo.SessionCombinationNumbers = seekTimes.Where(x => x.SessionCombinationId == combo.Id).ToList();
                        }
                        Console.Clear();
                        Console.Write(i * 100.0 / count);
                        i++;
                    }
                }
            }
            return sessions;
        }
        public List<UserModel> User_SelectAll()
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                List<UserModel> users = new List<UserModel>();
                users = connection.Query<UserModel>("dbo.spUsers_SelectAll").ToList();
                return users;
            }
        }
        public List<FileModel> File_SelectAll()
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                List<FileModel> files = new List<FileModel>();
                files = connection.Query<FileModel, UserModel, FileModel>("dbo.spFiles_SelectAll",
                   (file, user)
                   =>
                   {
                       file.User = user;
                       return file;
                   }).ToList();
                return files;
            }
        }
        //GetUserUploadFiles
        public List<FileModel> GetFiles_BySearchValue(int searchValue)
        {
            List<FileModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", searchValue);
                output = connection.Query<FileModel, UserModel, FileModel>("dbo.spFiles_GetByUserName",
                    (file, user) =>
                    {
                        file.User = user;
                        return file;
                    }, p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<FileModel> GetFiles_BySearchDate(string searchValue)
        {
            List<FileModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                DateTime dateTime = DateTime.Parse(searchValue);
                p.Add("@Date", searchValue);
                output = connection.Query<FileModel, UserModel, FileModel>("dbo.spFiles_GetByDate",
                     (file, user) =>
                     {
                         file.User = user;
                         return file;
                     }, p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<LogModel> Log_SelectAll()
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                List<LogModel> logs = new List<LogModel>();
                logs = connection.Query<LogModel, UserModel, LogModel>("dbo.spLogs_SelectAll",
                   (log, user)
                   =>
                   {
                       log.User = user;
                       return log;
                   }).ToList();
                return logs;
            }
        }

        public List<LogModel> GetLogs_BySearchValue(int searchValue)
        {
            List<LogModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", searchValue);
                output = connection.Query<LogModel, UserModel, LogModel>("dbo.spLogs_GetByUserName",
                    (log, user) =>
                    {
                        log.User = user;
                        return log;
                    }, p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<LogModel> GetLogs_BySearchDate(string searchValue)
        {
            List<LogModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                DateTime dateTime = DateTime.Parse(searchValue);
                p.Add("@Date", searchValue);
                output = connection.Query<LogModel, UserModel, LogModel>("dbo.spLogs_GetByDate",
                     (log, user) =>
                     {
                         log.User = user;
                         return log;
                     }, p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<UserModel> GetUsers_BySearchLogType(string searchValue)
        {
            List<UserModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@Type", searchValue);
                output = connection.Query<UserModel>("dbo.spUsers_GetByLogType", p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<LogModel> GetLogs_BySearchUser(int searchValue)
        {
            List<LogModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", searchValue);
                output = connection.Query<LogModel, UserModel, LogModel>("dbo.spLogs_GetByUser",
                     (log, user) =>
                     {
                         log.User = user;
                         return log;
                     }, p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<LogModel> GetLogs_AllLogs()
        {
            List<LogModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                output = connection.Query<LogModel, UserModel, LogModel>("dbo.spLogs_SelectAll",
                     (log, user) =>
                     {
                         log.User = user;
                         return log;
                     }, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<FileModel> Get_AllFiles()
        {
            List<FileModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                output = connection.Query<FileModel, UserModel, FileModel>("dbo.spFiles_SelectAll",
                     (file, user) =>
                     {
                         file.User = user;
                         return file;
                     }, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<LogModel> GetLogs_ByLogTypeAndUserId(int searchValue, string type)
        {
            List<LogModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", searchValue);
                p.Add("@Type", type);
                output = connection.Query<LogModel, UserModel, LogModel>("dbo.spLogs_GetByLogTypeAndUser",
                    (log, user) =>
                    {
                        log.User = user;
                        return log;
                    }, p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<LogModel> GetLogs_ByLogType(string type)
        {
            List<LogModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@Type", type);
                output = connection.Query<LogModel, UserModel, LogModel>("dbo.spLogs_GetByLogType",
                    (log, user) =>
                    {
                        log.User = user;
                        return log;
                    }, p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public FileModel GetFilesById(int id)
        {
            FileModel output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@FileId", id);
                output = connection.Query<FileModel>("dbo.spFiles_GetById", p, commandType: CommandType.StoredProcedure).ToList().FirstOrDefault();
            }
            return output;
        }

        public List<DailyLogCountModel> ChartDailyLogs(string startDate, string endDate)
        {

            List<DailyLogCountModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@startDate", startDate);
                p.Add("@endDate", endDate);
                output = connection.Query<DailyLogCountModel>("dbo.spChartLogs_GetByDay",
                       p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<ChartLogTypeCountModel> ChartLogs_ByLogType(string startDate, string endDate)
        {

            List<ChartLogTypeCountModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@startDate", startDate);
                p.Add("@endDate", endDate);
                output = connection.Query<ChartLogTypeCountModel>("dbo.spChartLogs_ByLogType",
                       p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<ChartLogMostPausedPersonModel> ChartLogsMostPausedPersone(string startDate, string endDate)
        {
            List<ChartLogMostPausedPersonModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@startDate", startDate);
                p.Add("@endDate", endDate);
                output = connection.Query<ChartLogMostPausedPersonModel>("dbo.spChartLogs_MostPauseByLogType",
                    p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }
    }
}
