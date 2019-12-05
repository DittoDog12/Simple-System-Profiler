using System;
using System.IO;
using Simple_System_Profiler.Interfaces;

namespace Simple_System_Profiler
{
    class Logger : ILogger
    {
        #region Variables
        private string _LogPath; // Logfile path
        private StreamWriter _LogFile; // Log File writer
        private bool _LogOK = false; // Check for logfile path OK
        private int _LogCount = 0; // Total log count to offset cursor
        private string _TimeNow;
        #endregion

        /// <summary>
        /// Log Counter
        /// </summary>
        public int LogCount
        {
            get { return _LogCount; }
        }

        public Logger(string pDir)
        {
            _LogPath = pDir + "\\simpleprofilerlog.txt";
            while (!_LogOK)
            {
                try
                {
                    _LogFile = new StreamWriter(_LogPath, false);
                    _LogOK = true;
                }
                catch
                {
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(@"Issue with Log file directory, defaulting to %temp%");
                    _LogPath = Path.GetTempPath() + "simpleprofilerlog.txt";
                    _LogCount++;
                }
            }
            WriteProgress("Log created, begun application");
            WriteProgress("OutputDir: " + pDir);
        }

        /// <summary>
        /// Write a progress report to the log
        /// </summary>
        /// <param name="pEntry"> Report to write </param>
        public void WriteProgress(string pEntry)
        {
            _TimeNow = DateTime.Now.ToString("ddMMyyyy hh:mm:ss");
            _LogFile.WriteLine(_TimeNow + " ---- " + pEntry);
            _LogFile.Flush();
        }
        /// <summary>
        /// Write an error report to the log
        /// Also print to screen
        /// </summary>
        /// <param name="pEntry"> Report to write </param>
        /// <param name="pDetails"> Exception details to record </param>
        public void WriteComponentFault(string pEntry, string pDetails)
        {
            _TimeNow = DateTime.Now.ToString(@"ddMMyyyy hh:mm:ss");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Issue with Component: " + pEntry + ", See log for more details");
            _LogFile.WriteLine("ERROR ---- " + _TimeNow + " ---- Component: " + pEntry + " ---- " + pDetails);
            _LogFile.Flush();
            Console.ForegroundColor = ConsoleColor.Green;
            _LogCount++;
        }
        /// <summary>
        /// For the Kernel to save the arguments to the logfile
        /// </summary>
        /// <param name="pModes"> list of arguments </param>
        public void SetModes(string[] pModes)
        {
            foreach (string s in pModes)
            {
                switch (s)
                {
                    case "/t":
                        WriteProgress("MODE: Text Output");
                        break;
                    case "/x":
                        WriteProgress("MODE: XML Output");
                        break;
                    case "/q":
                        WriteProgress("MODE: Quiet");
                        break;
                }
            }
        }
    }
}
