using Dapper;
using HRPMBackendLibrary.Models;
using HRPMSharedLibrary.Enums;
using HRPMSharedLibrary.Helpers;
using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMBackendLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        public static string databaseName = "HRPM";

        public void File_Insert(DataFileModel model)
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
                p.Add("@UserName", model.Username);
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
                p.Add("@UserName", model.Username);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spUsers_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
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
        public List<DataFileModel> File_SelectAll()
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                List<DataFileModel> files = new List<DataFileModel>();
                files = connection.Query<DataFileModel, UserModel, DataFileModel>("dbo.spFiles_SelectAll",
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
        public List<DataFileModel> GetFiles_BySearchValue(int searchValue)
        {
            List<DataFileModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", searchValue);
                output = connection.Query<DataFileModel, UserModel, DataFileModel>("dbo.spFiles_GetByUserName",
                    (file, user) =>
                    {
                        file.User = user;
                        return file;
                    }, p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<DataFileModel> GetFiles_BySearchDate(string searchValue)
        {
            List<DataFileModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                DateTime dateTime = DateTime.Parse(searchValue);
                p.Add("@Date", searchValue);
                output = connection.Query<DataFileModel, UserModel, DataFileModel>("dbo.spFiles_GetByDate",
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

        public List<DataFileModel> Get_AllFiles()
        {
            List<DataFileModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                output = connection.Query<DataFileModel, UserModel, DataFileModel>("dbo.spFiles_SelectAll",
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

        public DataFileModel GetFilesById(int id)
        {
            DataFileModel output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@FileId", id);
                output = connection.Query<DataFileModel>("dbo.spFiles_GetById", p, commandType: CommandType.StoredProcedure).ToList().FirstOrDefault();
            }
            return output;
        }

        

        

        

        public List<DepartmentModel> Department_SelectAll()
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                List<DepartmentModel> deps = new List<DepartmentModel>();
                deps = connection.Query<DepartmentModel>("dbo.spDepartments_SelectAll").ToList();
                return deps;
            }
        }

        public List<UserModel> User_GetByDepartment(int id)
        {
            List<UserModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@DepartmentId", id);                
                output = connection.Query<UserModel>("dbo.spUsers_GetByDepartmentId",
                       p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public string User_GetPassword(int userId)
        {            
            UserModel output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                output = connection.Query<UserModel>("spUsers_GetPassword",
                       p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return output.Password;
        }

        public void App_Insert(AppModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@ProcessName", model.ProcessName);                                
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spApps_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
        }

        public void Domain_Insert(DomainModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@Domain", model.Domain);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spDomains_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
        }

        public void SessionDomain_Insert(int sessionId, int domainId)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@SessionId", sessionId);
                p.Add("@DomainId", domainId);                
                connection.Execute("spSessionDomain_Insert", p, commandType: CommandType.StoredProcedure);                
            }
        }

        public void Session_Insert(SessionModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(databaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@StartTime", model.StartTime);
                p.Add("@EndTime", model.EndTime);
                p.Add("@StrokesCount", model.StrokesCount);
                p.Add("@StrokeHoldTimes", model.StrokeHoldTimes);
                p.Add("@UniqueKeysCount", model.UniqueKeysCount);
                p.Add("@BackspaceStrokesCount", model.BackspaceStrokesCount);
                p.Add("@MouseClickCount", model.MouseClickCount);
                p.Add("@MouseClickTotalTime", model.MouseClickTotalTime);
                p.Add("@UserId", model.UserId);
                p.Add("@AppId", model.AppId);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spSessions_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
        }
    }
}
