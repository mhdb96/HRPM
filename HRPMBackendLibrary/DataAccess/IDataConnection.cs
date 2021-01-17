using HRPMBackendLibrary.Models;
using HRPMSharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPMBackendLibrary.DataAccess
{
    public interface IDataConnection
    {
        void User_Insert(UserModel model);
        bool User_Exists(UserModel model);
        void Log_Insert(LogModel model);
        void File_Insert(DataFileModel model);

        List<UserModel> User_SelectAll();
        List<DataFileModel> File_SelectAll();
        List<DataFileModel> GetFiles_BySearchValue(int id);
        List<DataFileModel> GetFiles_BySearchDate(string date);
        List<LogModel> Log_SelectAll();
        List<LogModel> GetLogs_ByLogType(string type);
        List<LogModel> GetLogs_BySearchDate(string date);
        List<UserModel> GetUsers_BySearchLogType(string type);
        List<LogModel> GetLogs_BySearchUser(int id);
        List<LogModel> GetLogs_AllLogs();
        List<DataFileModel> Get_AllFiles();
        DataFileModel GetFilesById(int id);
        List<LogModel> GetLogs_ByLogTypeAndUserId(int id, string type);

        List<DepartmentModel> Department_SelectAll();
        List<UserModel> User_GetByDepartment(int id);
        string User_GetPassword(int userId);
        void App_Insert(AppModel model);
        void Domain_Insert(DomainModel model);
        void SessionDomain_Insert(int sessionId, int domainId);
        void Session_Insert(SessionModel model);
        void Task_Insert(TaskModel model);
        void UsageTime_Insert(UsageTimeModel model);


    }
}