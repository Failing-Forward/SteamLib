using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamLib
{
    public class AppNotInstalledException : Exception
    {
        public AppNotInstalledException() : base("App not installed.")
        {
        }

        public AppNotInstalledException(string msg) : base(msg)
        {
        }

        public AppNotInstalledException(string msg, Exception inner) : base(msg, inner)
        {
        }
    }
}
