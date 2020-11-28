﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDASharedLibrary.Enums
{
    public enum LogType
    {
        ProgramOpened,
        ProgramClosed,
        LoggerStarted,
        LoggerPaused,
        LoggerStopped,
        FileDeleted,
        FileSent,
        FileCreated,
        UpdateFailed,
        UpdateSuccessed
    }
}
