using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XMLDb
{
    public static class Loger
    {
        static FileStream stream = File.Open("log.txt", FileMode.Create);
        
        public static void Write(string message)
        {
            byte[] s = System.Text.Encoding.UTF8.GetBytes(message+"\n");
            stream.Write(s, 0, s.Length);
        }
        public static void End()
        {
            stream.Close();
        }


    }
}
