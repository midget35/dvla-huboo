using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huboo {

    class Program {

        static ConsoleView consoleView;
        static DvlaProxy dvlaProxy;
        static SettingsProxy settingsProxy;

        static void Main(string[] args) {

            settingsProxy = new SettingsProxy();

            consoleView = new ConsoleView();
            consoleView.RegistrationEvt     += OnRegistrationEntered;
            consoleView.ExitAppEvt          += OnExitApp;
            consoleView.ApiKeyEnteredEvt    += OnApiKeyEntered;

            dvlaProxy = new DvlaProxy();
            dvlaProxy.DownloadOk    += OnDownloadOk;
            dvlaProxy.DownloadFail  += OnDownloadFail;

            if (!settingsProxy.GetApiKeyExists()) {
                consoleView.RequestApiKey(settingsProxy.SettingsFullpath);
            }
            else {
                consoleView.StartRegistrationSession();
            }

            System.Threading.Thread.Sleep(-1);
        }

        private static void OnApiKeyEntered(string apiKey) {
            settingsProxy.SetApiKey(apiKey);
            consoleView.StartRegistrationSession();
        }

        private static void OnExitApp(object sender, EventArgs e) {
            Environment.Exit(0);
        }

        private static void OnRegistrationEntered(string registration) {
            dvlaProxy.Download(registration, settingsProxy.GetApiKey());
        }

        private static void OnDownloadFail(string reason) {
            consoleView.ShowVehicleNotFound(reason);
        }

        private static void OnDownloadOk(VehicleVO vehicle) {
            consoleView.ShowVehicleFound(vehicle);
        }
    }
}
