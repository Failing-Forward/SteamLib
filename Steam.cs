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
        private readonly string ConfigPath;

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
                ConfigPath = InstalledPath + "\\config";
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

        public string GetConfigPath()
        {
            return ConfigPath;
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
                throw new AppNotInstalledException($"App with ID {appId}");
            }
        }

        public string GetSteamIdByUserName(string username)
        {
            try
            {
                string data = File.ReadAllText(ConfigPath + $"\\config.vdf");
                Regex regex = new Regex($"{username}[\\W]+\"SteamID\".+\"(.*?)\"");
                Match match = regex.Match(data);

                return match.Groups[1].Value;
            }
            catch (FileNotFoundException)
            {
                throw new UserNotFoundException();
            }
        }

        public string[] GetUsersName()
        {
            try
            {
                string data = File.ReadAllText(ConfigPath + $"\\loginusers.vdf");
                Regex regex = new Regex("\"AccountName\".+\"(.*?)\"");
                MatchCollection matches = regex.Matches(data);
                List<string> names = new List<string>();
                foreach (Match match in matches)
                    names.Add(match.Groups[1].Value);
                return names.ToArray();
            }
            catch (Exception)
            {
                throw new UserNotFoundException();
            }
        }
    }
}
