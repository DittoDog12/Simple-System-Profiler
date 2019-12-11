using System;
using System.Collections.Generic;
using System.Text;
using Simple_System_Profiler.Hardware;
using Simple_System_Profiler.Software;
using Simple_System_Profiler.Interfaces;
using Simple_System_Profiler.Output;
using System.IO;
using System.Text.RegularExpressions;

namespace Simple_System_Profiler
{
    class Kernel
    {
        #region Varaibles
        private IDictionary<string, string> _AllSpecs; // Dictionary of Specifications

        private List<IComponentProfiler> _Profilers; // List of COmponent Profilers

        private List<IWriter> _Outputters; // List of all Output devices

        private string _CurrentDir = Environment.CurrentDirectory; // Save the current Directory

        private string _Path; // Variable to hold the file save path

        private ILogger _Logger; // Logger subsystem

        // Command Line Argument controls and defaults
        private bool _OutputText = false;
        private bool _OutputXML = false;
        private bool _OutputScreen = true;
        #endregion

        public Kernel(string[] pArgs)
        {
            //_Path = _CurrentDir; // Set the current running directory as the save path

            _Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Set the default save path to the Desktop folder

            // Check for Server Save File in running directory
            //string[] mOutputPath = Directory.GetFiles(_CurrentDir, "*.txt"); // Get filenames of all text files in the root of Current Dir
            //foreach (string s in mOutputPath) // Look for the ServerDir file
            //    if (s == _CurrentDir + @"\OutputDir.txt") // if found read it and overwrite _Path  
            //    {
            //        try
            //        {
            //            // See what is contained in the OutputDir file, if HERE then save to running Dir otherwise save in specified
            //            if (File.ReadAllText(_CurrentDir + @"\OutputDir.txt") != "HERE")
            //                _Path = File.ReadAllText(_CurrentDir + @"\OutputDir.txt");
            //        }
            //        catch
            //        {
            //            throw new Hex("OutputDir.txt exists but unable to read it");
            //        }
            //    }


            // Check Command Line Arguments
            foreach (string s in pArgs)
            {
                if (s == "/t") _OutputText = true;
                if (s == "/x") _OutputXML = true;
                if (s == "/q") _OutputScreen = false;
            }
            // If output mode enabled then take the last argument and test it is a directory that can be written to
            if (_OutputText || _OutputXML)
                if (ParseOutput(pArgs[pArgs.Length - 1]))
                    _Path = pArgs[pArgs.Length - 1];



            // Initialize Logger
            _Logger = new Logger(_Path);

            // Write the selected modes to the logfile
            _Logger.SetModes(pArgs);

            // Initialise Specification Dictionary
            _AllSpecs = new Dictionary<string, string>();

            // Intialise Component Profilers list
            // Create each Component Profiler at same time
            _Profilers = new List<IComponentProfiler>
            {
                new CPU(),
                new Memory(),
                new HDD(),
                new LAN(),
                new GPU(),
                new BIOS(),
                new OS(),
                new User(),
                new Office()
            };

            // Intialise Outputters
            // Create Outputter specifed by arguments
            _Outputters = new List<IWriter>();
            if (_OutputXML) _Outputters.Add(new XMLWriter());
            if (_OutputText) _Outputters.Add(new TXTWriter());
        }

        #region Methods
        public void MainLoop()
        {
            //Console.Clear();
            // Run main Profiler
            _Logger.WriteProgress("Begin Profile");
            ProfileSystem();
            _Logger.WriteProgress("Profile Complete");

            // Run Print to screen
            if (_OutputScreen)
            {
                _Logger.WriteProgress("Writing to screen");
                PrinttoScreen();
            }
            // Run Requested Outputters
            if (_Outputters.Count > 0)
            {
                _Logger.WriteProgress("Writing to Files");
                WriteFiles();
            }
        }

        public bool ParseOutput(string pArg)
        {
            // Test can write to the specified output path
            bool mPathOK = false;
            string mPath = pArg + @"\Outputtest.txt";
            while (!mPathOK)
            {
                try
                {
                    StreamWriter mOutpath = new StreamWriter(mPath, false);
                    mPathOK = true;
                    mOutpath.Close();
                    File.Delete(mPath);
                }
                catch
                {
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(@"Issue with output file directory, defaulting to %Desktop%");
                }
            }

            return mPathOK;
        }
        public void ProfileSystem()
        {
            Spinner mSpin = new Spinner(0, _Logger.LogCount, 50);
            // Change text to blue and display please wait
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(3, _Logger.LogCount);
            Console.WriteLine("Please wait, Profiling System");
            // Start Console Spinner
            mSpin.Start();

            // For each profiler created, pass the current spec list into GetDetails() and read the returned updated list
            foreach (IComponentProfiler p in _Profilers)
            {
                try
                {
                    _AllSpecs = p.GetDetails(_AllSpecs);
                }
                catch (Exception e)
                {
                    _Logger.WriteComponentFault(p.Name, e.ToString());
                }
            }


            // Stop the spinner
            mSpin.Stop();
        }
        public void PrinttoScreen()
        {
            // Clear screen and set text colour to green
            //Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            // Foreach spec in the lst, print it to the screen
            foreach (KeyValuePair<string, string> keyPair in _AllSpecs)
                Console.WriteLine(keyPair.Key + ": " + keyPair.Value);

            if (_Outputters.Count > 0)
            {
                Console.WriteLine("Press Any Key to Continue");
                Console.ReadKey();
            }
        }

        public void WriteFiles()
        {
            Spinner mSpin = new Spinner(0, _Logger.LogCount, 50);

            //Console.Clear();
            // Change text to blue and display please wait
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(3, _Logger.LogCount);
            Console.WriteLine("Please wait, Saving Files");
            mSpin.Start();
            // for each selected outputter
            // Send it the completed spec list and have it save the files
            foreach (IWriter w in _Outputters)
                w.Write(_AllSpecs, _Path);

            mSpin.Stop();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Files Saved");
        }

        public void Exit()
        {
            if (_OutputScreen)
            {
                Console.WriteLine("Press Any Key to Exit");
                Console.ReadKey();
                _Logger.WriteProgress("Application complete, exiting");
            }
        }
        #endregion
    }
}
