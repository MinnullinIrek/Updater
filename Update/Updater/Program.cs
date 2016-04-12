using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string processOriginalName = args[1].Replace(".exe", "");
                string newFileName = args[0];
                Process[] processes = Process.GetProcessesByName(processOriginalName);
                while (processes.Length > 0)
                {
                    foreach (Process proces in processes)
                    {
                        proces.Kill();
                    }
                    Thread.Sleep(300);
                    processes = Process.GetProcessesByName(processOriginalName);
                }
                if (File.Exists(args[1]))
                {
                    File.Delete(args[1]);
                }

                File.Move(args[0], args[1]);
                Process.Start(args[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
            
        }
    }
}
