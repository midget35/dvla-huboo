using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huboo {
    internal static class JsonHelper {
        
        /**
         * Remove square brackets at start and end of string, as returned by WebClient.
         */
        internal static string DeBracket(string str) {
            return str.Substring(1, str.Length - 2);
        }
    }
}
