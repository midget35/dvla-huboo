using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huboo {
    internal static class JsonHelper {
        
        internal static string UnArray(string str) {
            return str.Substring(1, str.Length - 2);
        }
    }
}
