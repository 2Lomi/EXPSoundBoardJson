using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp2
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Place every mp3 files you need in the same folder");
            Console.WriteLine("Choose the directory where your MP3's are located");
            string path = "E:\\Bureau\\direcotr";
            
            FolderBrowserDialog FDB = new FolderBrowserDialog();
            
            DialogResult result = FDB.ShowDialog();

            if (result == DialogResult.OK)
            {
                 path = FDB.SelectedPath;
            }

            Console.WriteLine(path);

            string[] Files = Directory.GetFiles(path);

            saveJsonFile(generateJsonFileTxt(Files), path);
          
            Console.WriteLine("\n Your file has been saved, you can now open it with EXP Soundboard");
            Console.ReadKey();
            
        }

        private static void saveJsonFile(string p, string path)
        {
            Console.WriteLine("Name your json file");
            Console.Write("Name :"); string fullPath = path + @"\" + Console.ReadLine() + ".json";

            System.IO.File.WriteAllText(@fullPath, p);

        }

        public static string generateJsonFileTxt(string[] Files)
        {
            string beginTxt = "{\"soundboardEntries\":[";
            string endtxt = "]}";
            string finalTxt = "";
            string middleText = "";
            finalTxt += beginTxt;
            
            foreach (string k in Files)
            {
                if (Regex.Replace(k, @"\\", @"\\").IndexOf(".mp3") != -1)
                {
                    middleText += "{\"file\":\"";
                    middleText += Regex.Replace(k, @"\\", @"\\");
                    middleText += "\",\"activationKeysNumbers\":[]},";
                    finalTxt += middleText;
                    middleText = "";
                }
            }
            
            finalTxt += endtxt;

            StringBuilder t = new StringBuilder(finalTxt);

            t.Remove(finalTxt.Length - 3, 1);
            
            Console.WriteLine(t.ToString());
            return t.ToString(); 
        }
    }
}