using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ThunderPacker
{
    class Program
    {
        static string filePath;
        static string folderPath;
        static string fileName;

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Choose which folder you would like to pack your mod in!");
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    folderPath = fbd.SelectedPath;
                }
            }
            Console.WriteLine("Your file will be created at " + folderPath);

            Console.WriteLine("Choose your mod!");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Choose Mod";
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Mods (*.dll)|*.dll";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    fileName = openFileDialog.SafeFileName;
                }
            }

            /*

            Console.WriteLine("Choose your Thumbnail!!");
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Choose Thumbnail";
                ofd.InitialDirectory = "c:\\";
                ofd.Filter = "PNG Images (*.png)|*.png";
                ofd.FilterIndex = 2;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    imgPath = ofd.FileName;
                    imgName = ofd.SafeFileName;
                }
            }
            */

            Console.WriteLine("Your mod name is: " + fileName);
            Console.WriteLine("Your mod is located at: " + filePath);

            Console.WriteLine("What is the name of your mod?");
            string modNameForFile;
            modNameForFile = "\\" + Console.ReadLine() + "\\";

            Directory.CreateDirectory(folderPath + modNameForFile);
            Directory.CreateDirectory(folderPath + modNameForFile + "Mods");
            Console.WriteLine("Created Mod Folder");
            using (FileStream fs = File.Create(folderPath + modNameForFile + "\\manifest.json"))
            {
            }

            string path = folderPath + modNameForFile + "\\manifest.json";
            string tempPath = folderPath + modNameForFile + "Mods\\";

            File.WriteAllText(path, "\"name\": \"nameText\",\n\"version_number\": \"verText\",\n\"website_url\": \"\",\n\"description\": \"descText\",\n\"dependencies\": [\"depText\"]");
            Console.WriteLine("Created Manifest");

            string tempStuff = tempPath + fileName;

            Console.WriteLine(tempStuff + " " + filePath + " " + tempPath);
            try
            {
                File.Copy(filePath, tempStuff);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }

            Console.WriteLine("Enter the name of your mod:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the version of your mod:");
            string ver = Console.ReadLine();
            Console.WriteLine("Write the description of your mod:");
            string descText = Console.ReadLine();
            Console.WriteLine("Write the dependencies of your mod:");
            string depText = Console.ReadLine();

            string strName = File.ReadAllText(path);
            string strVer = File.ReadAllText(path);
            string strDesc = File.ReadAllText(path);
            string strDep = File.ReadAllText(path);

            strName = strName.Replace("nameText", name);
            strName = strName.Replace("verText", ver);
            strName = strName.Replace("descText", descText);
            strName = strName.Replace("depText", depText);
            File.WriteAllText(path, strName);
            //File.WriteAllText("test.json", strVer);
            //File.WriteAllText("test.json", strDesc);
            //File.WriteAllText("test.json", strDep);

            using (FileStream fs = File.Create("D:\\ThunderMods\\" + modNameForFile + "\\README.md"))
            {
            }
            Console.WriteLine("Created your manifest!");
            Console.WriteLine("Created README, do you want to edit it now? (y/n)");
            bool edit = false;
            string conf;
            path = "D:\\ThunderMods\\" + modNameForFile + "\\README.md";
            conf = Console.ReadLine();
            if (conf == "y")
            {
                edit = true;
            }
            else if (conf == "n")
            {
                edit = false;
            }

            if (edit == true)
            {
                Console.WriteLine("Write your README here:");
                string rmText = Console.ReadLine();
                File.WriteAllText(path, rmText);
                Console.WriteLine("Made the readme!");
                Console.WriteLine("Everything is set up correctly, press any key to exit the program");
                Console.ReadKey();
            }
            else if (edit == false)
            {
                Console.WriteLine("Everything is set up correctly, press any key to exit the program");
                Console.ReadKey();
            }
        }
    }
}
