using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Huboo {
    internal static class HubooHelper {

        public const string APP_NAME            = "Huboo - DVLA MOT Sample App (v2022.04.14)";

        public const string HTTP_USER_AGENT     = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";

        public const string API_ENDPOINT        = @"https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests";

        public const string DATE_TIME_FORMAT    = "yyyy.MM.dd";

        public static string FormatRegistration(string registration) {
            string str = Regex.Replace(registration, @"\s+", "");
            str = str.ToUpper();
            return str;
        }
    }
}
