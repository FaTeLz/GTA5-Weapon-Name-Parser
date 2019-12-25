using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA5_Script_parser
{
    class Program
    {
        static string scriptAddress = "https://raw.githubusercontent.com/Reliency/1.50-Grand-Theft-Auto-V-Scripts/master/mp_weapons.ysc.c";
        static string searchKeyword = "weapon_";
        static string[] blacklistKeyword = { "vehicle_" };
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        static void title(string s)
        {
            ConsoleColor origColor = Console.ForegroundColor;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(s);
            Console.ForegroundColor = origColor;
        }
        static void Main(string[] args)
        {
            title("GTA5 Script Parser");

            System.Net.WebClient webClient = new System.Net.WebClient();
            string script = webClient.DownloadString(scriptAddress);
            string output = "";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[+] Successfuly downloaded script.");
     
            string[] lines = script.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int count = 0;
            foreach (string line in lines)
            {
                string gotBetween = getBetween(line, "joaat(", ")"); 
                if (line.Contains(searchKeyword) && !output.Contains(gotBetween))
                {
                    foreach(string s in blacklistKeyword)
                    {
                        if (!output.Contains(s))
                        {
                            output += gotBetween + "\n ,";
                            count++;
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[+] Successfuly parsed " + count + " lines containing '" + searchKeyword + "'");
            title("Output: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
