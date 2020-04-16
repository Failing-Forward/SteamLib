using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using SteamLib.Exceptions;

namespace SteamLib
{
    public class Steam
    {
        public readonly string InstalledPath;
        public readonly string AppsPath;
        public readonly string ConfigPath;

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

        public string GetAppPathById(string appId)
        {
            try
            {   
                string data = File.ReadAllText(AppsPath + $"\\appmanifest_{appId}.acf");
                Regex regex = new Regex("\"installdir\".+\"(.*?)\"");
                Match match = regex.Match(data);

                if (match.Success)
                {
                    return $"{AppsPath}\\common\\{match.Groups[1].Value}";
                } else
                {
                    throw new ConfigNotFoundException();
                }
            } catch (FileNotFoundException)
            {
                throw new AppNotInstalledException();
            }
        }

        public string GetSteamIdByUserName(string username)
        {
            try
            {
                string data = File.ReadAllText(ConfigPath + $"\\config.vdf");
                Regex regex = new Regex($"{ username }[\\W]+\"SteamID\".+\"(.*?)\"");
                Match match = regex.Match(data);

                if (match.Success)
                {
                    return match.Groups[1].Value;
                } else
                {
                    throw new UserNotFoundException();
                }             
            }
            catch (FileNotFoundException)
            {
                throw new ConfigNotFoundException();
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
                throw new ConfigNotFoundException();
            }
        }
    }
}
