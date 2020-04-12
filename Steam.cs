using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamLib
{
    public class Steam
    {
        private readonly string InstalledPath;
        private readonly string AppsPath;

        public Steam()
        {
            object steamPath = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam", "InstallPath", null);

            if (steamPath == null)
            {
                throw new SteamNotInstalledException();
            }
            else
            {
                InstalledPath = steamPath.ToString();
                AppsPath = InstalledPath + "\\steamapps";
            }
        }

        public string GetInstalledPath()
        {
            return InstalledPath;
        }

        public string GetAppsPath()
        {
            return AppsPath;
        }

        public string GetAppPathById(string appId)
        {
            try
            {
                string data = File.ReadAllText(AppsPath + $"\\appmanifest_{appId}.acf");
                Regex regex = new Regex("\"installdir\".+\"(.*?)\"");
                Match match = regex.Match(data);

                return $"{AppsPath}\\common\\{match.Groups[1].Value}";
            } catch (FileNotFoundException)
            {
                throw new AppNotInstalledException();
            }
        }
    }
}
