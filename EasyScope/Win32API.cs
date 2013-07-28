#region

using System;
using System.Runtime.InteropServices;

#endregion

namespace EasyScope
{
    internal class Win32API
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}