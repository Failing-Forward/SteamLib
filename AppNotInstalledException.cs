﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamLib
{
    public class AppNotInstalledException : Exception
    {
        public AppNotInstalledException(string msg) : base(msg + " not installed.")
        {
        }
    }
}
