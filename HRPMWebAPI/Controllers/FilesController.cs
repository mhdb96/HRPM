using HRPMBackendLibrary;
using HRPMBackendLibrary.DataAccess;
using HRPMBackendLibrary.Models;
using HRPMSharedLibrary.DataAccess;
using HRPMSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Routing;

namespace HRPMWebAPI.Controllers
{
    public class FilesController : ApiController
    {
        [Route("api/files/UploadFile")]
        [HttpPost]
        public void UploadFile(BinaryFile file)
        {
            DataFileModel model = new DataFileModel();
            model.Date = file.Date;
            model.User.Id = file.User.Id;
            model.Path = FileManager.GetFileManager().SaveSessionsFile(file);
            GlobalConfig.Connection.File_Insert(model);
            var sessions = BinaryConnector.StaticLoad<List<AppSession>>(model.Path);
            foreach (var session in sessions)
            {
                if (session.MouseData != null && session.EndTime != DateTime.MinValue)
                {
                    AppModel appModel = new AppModel();
                    appModel.ProcessName = session.App.ProcessName;
                    GlobalConfig.Connection.App_Insert(appModel);                                        
                    SessionModel sessionModel = new SessionModel();
                    sessionModel.AppId = appModel.Id;
                    sessionModel.BackspaceStrokesCount = session.KeyboardData.BackspaceStrokesCount;
                    sessionModel.StrokesCount = session.KeyboardData.StrokesCount;
                    sessionModel.StrokeHoldTimes = session.KeyboardData.StrokeHoldTimes;
                    sessionModel.UniqueKeysCount = session.KeyboardData.UniqueKeysCount;
                    sessionModel.MouseClickCount = (int)session.MouseData.MouseClickCount;
                    sessionModel.MouseClickTotalTime = (int)session.MouseData.MouseClickTotalTime;
                    sessionModel.StartTime = session.StartTime;
                    sessionModel.EndTime = session.EndTime;
                    sessionModel.UserId = model.User.Id;
                    sessionModel.AppId = appModel.Id;
                    GlobalConfig.Connection.Session_Insert(sessionModel);
                    if (session.App.Type == HRPMSharedLibrary.Enums.AppType.Browser)
                    {
                        DomainModel domainModel = new DomainModel();
                        domainModel.Domain = session.App.Content;
                        GlobalConfig.Connection.Domain_Insert(domainModel);
                        GlobalConfig.Connection.SessionDomain_Insert(sessionModel.Id, domainModel.Id);
                    }                    
                }                
            }
        }
    }
}