using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace AzerBot
{
    static class Extensions
    {
        public static bool ValidateFileName(this string self)
        {
            if(self == "")
            {
                return false;
            }
            else { return true; }
        }
        public static void SaveObject(this object self, string destination)
        {
            string rawJson = JsonConvert.SerializeObject(self, Formatting.Indented);
            File.WriteAllText(destination, rawJson);
        }
    }
}
