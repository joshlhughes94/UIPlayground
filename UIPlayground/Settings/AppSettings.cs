using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIPlayground.Settings
{
    public class AppSettings
    {
        public string SauceDemoURL { get; set; } = null;
        public string ValidUsername { get; set; } = null;
        public string ValidPassword { get; set; } = null;
        public string InvalidUsername { get; set; } = null;
        public string InvalidPassword { get; set; } = null;
    }
}