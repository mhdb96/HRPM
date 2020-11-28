using KDABackendLibrary.Models;
using KDASharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDABackendLibrary.DataAccess
{
    public interface IDataConnection
    {
        void User_Insert(UserModel model);
        bool User_Exists(UserModel model);
        void Log_Insert(LogModel model);
        void File_Insert(FileModel model);
        void Key_Insert(KeysList key);
        void SessionKeys_Insert(SessionKeyModel model);
        void SessionCombinations_Insert(SessionCombinationModel model);
        List<SessionKeyModel> KeystrokeData_GetBySessionId(int id);
        void KeyCombinations_Insert(int fromId, int toId);
        void Sessions_Insert(SessionModel model);
        void GetHoldTimesStatistics(UserModel model);
        List<SessionModel> Sessions_GetByUserIdAndDate(int id, DateTime start, DateTime end);
        List<KeyCombinationModel> KeyCombinations_GetUsedCombinationsByAllUsers();
        List<SessionModel> Dataset_GetAll();
        List<UserModel> User_SelectAll();
        List<FileModel> File_SelectAll();
        List<FileModel> GetFiles_BySearchValue(int id);
        List<FileModel> GetFiles_BySearchDate(string date);
        List<LogModel> Log_SelectAll();
        List<LogModel> GetLogs_ByLogType(string type);
        List<LogModel> GetLogs_BySearchDate(string date);
        List<UserModel> GetUsers_BySearchLogType(string type);
        List<LogModel> GetLogs_BySearchUser(int id);
        List<LogModel> GetLogs_AllLogs();
        List<FileModel> Get_AllFiles();
        FileModel GetFilesById(int id);
        List<LogModel> GetLogs_ByLogTypeAndUserId(int id, string type);
        List<DailyLogCountModel> ChartDailyLogs(string startDate, string endDate);
        List<ChartLogTypeCountModel> ChartLogs_ByLogType(string startDate, string endDate);
        List<ChartLogMostPausedPersonModel> ChartLogsMostPausedPersone(string startDate, string endDate);
    }
}