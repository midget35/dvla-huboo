using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huboo {
    internal class SettingsProxy {

        public string SettingsFullpath { get; private set; }
        public SettingsProxy() {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            SettingsFullpath = Path.Combine(path, "settings.txt");
        }

        internal void SetApiKey(string apiKey) {
            File.WriteAllText(SettingsFullpath, apiKey.Trim());
        }

        internal string GetApiKey() {
            return File.ReadAllText(SettingsFullpath).Trim();
        }

        internal bool GetApiKeyExists() {
            if (!File.Exists(SettingsFullpath)) return false;

            string val = File.ReadAllText(SettingsFullpath);

            if (string.IsNullOrWhiteSpace(val)) return false;

            return true;
        }

    }
}
