using System;
using System.Runtime.InteropServices;
using System.Text;


namespace KDACore.Logic
{
    public static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        // overload for use with LowLevelKeyboardProc
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WM wParam, [In] KBDLLHOOKSTRUCT lParam);

        // overload for use with LowLevelMouseProc
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WM wParam, [In] MSLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(IntPtr lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        //[DllImport("coredll.dll", EntryPoint = "GetModuleHandleW", SetLastError = true)]
        //public static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);
        [DllImport("user32.dll")] 
        public static extern IntPtr GetKeyboardLayout(uint thread);

        public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);
    }
}
