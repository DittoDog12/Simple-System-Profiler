using System;

namespace Simple_System_Profiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Kernel main = new Kernel(args);
            main.MainLoop();
            main.Exit();
        }
    }
}
