using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

//namespace KDAUILibrary.UserData
//{
//    //public class UserProperties
//    //{
//    //    private static readonly int _charCount = FileHelper.GetEnumCount<KeysList>();
//    //    private static string rootFolderPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\KDAnlyzer";
//    //    public static string logName = FileHelper.MakePath(RootFolderPath, "logs", "log");
//    //    public static string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

//    //    public static KeystrokeData[] Keystrokes { private get; set; }
//    //    public static string RootFolderPath
//    //    {
//    //        get 
//    //        {
//    //            Directory.CreateDirectory(rootFolderPath);
//    //            return rootFolderPath; 
//    //        }
//    //    }

//    //    //public static KeystrokeData[] GetKeyStrokesData()
//    //    //{
//    //    //    if (Keystrokes == null)
//    //    //    {
//    //    //        try
//    //    //        {
//    //    //            return BinaryConnector.DynamicLoad<KeystrokeData[]>("KeystrokesData");
//    //    //        }
//    //    //        catch (Exception)
//    //    //        {
//    //    //            return new KeystrokeData[_charCount];
//    //    //        }
//    //    //    }
//    //    //    else
//    //    //    {
//    //    //        return Keystrokes;
//    //    //    }
//    //    //}
//    //}
//}
