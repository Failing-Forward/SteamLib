using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamLib
{
    public class SteamNotInstalledException : Exception
    {
        public SteamNotInstalledException() : base("Steam not installed.")
        {
        }
    }
}
