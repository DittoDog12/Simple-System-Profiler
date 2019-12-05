using System;

namespace Simple_System_Profiler
{
    /// <summary>
    /// James' main exception handler
    /// Here be dragons
    /// </summary>
    class Hex : Exception
    {
        public Hex(string e)
        {
            Console.Clear();
            Console.WriteLine("+++ MELON MELON MELON +++");
            Console.WriteLine("---> " + e + " <---");
            Console.WriteLine("+++ REINSTALL UNIVERSE AND REBOOT +++");
            Console.WriteLine("Press Any Key to Exit");
            Console.ReadKey();
            Environment.Exit(0);
        }

        public Hex(string e, Exception e2)
        {
            Console.Clear();
            Console.WriteLine("+++ MELON MELON MELON +++");
            Console.WriteLine("---> " + e + " <---");
            Console.WriteLine("---> " + e2.Message + " <---");
            Console.WriteLine("+++ REINSTALL UNIVERSE AND REBOOT +++");
            Console.WriteLine("Press Any Key to Exit");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
