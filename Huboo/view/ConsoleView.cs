using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huboo {
    internal class ConsoleView {

        public Action<string> RegistrationEvt;
        public EventHandler ExitAppEvt;
        public Action<string> ApiKeyEnteredEvt;

        public string Registration { get ; private set; }

        private ConsoleColor defaultConsoleColour;

        internal void StartRegistrationSession() {
            defaultConsoleColour = Console.ForegroundColor;
            ClearView();
            RequestRegistration();
        }

        internal void ShowVehicleFound(VehicleVO vehicle) {
            string result = "";

            result += "Registration\t" + Registration;
            result += Environment.NewLine;
            result += "Make\t\t" + vehicle.Make;
            result += Environment.NewLine;
            result += "Model\t\t" + vehicle.Model;
            result += Environment.NewLine;
            result += "Colour\t\t" + vehicle.Colour;
            result += Environment.NewLine;
            result += "Expiry\t\t" + vehicle.ExpiryDate.ToString(HubooHelper.DATE_TIME_FORMAT);
            result += Environment.NewLine;
            result += "MOT Failures\t" + vehicle.FailedMotsTotal;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result);
            Console.ForegroundColor = defaultConsoleColour;

            ShowSessionEndMessage();
        }

        internal void RequestApiKey(string settingsFileFullpath) {
            Console.WriteLine(HubooHelper.APP_NAME);
            Console.WriteLine("");
            Console.WriteLine("The DVLA API Key could not be found. Entering it is a one-time process, providing it is correct.");
            Console.WriteLine("");
            Console.WriteLine("If you enter the API Key incorrectly, you will need to manually delete the the following file and run this program again: '" + settingsFileFullpath + "'.");
            Console.WriteLine("");

            Console.WriteLine("Please enter the API Key for accessing the DVLA API. Press 'Enter' when done:");

            string val = Console.ReadLine();

            if (ApiKeyEnteredEvt != null) {
                ApiKeyEnteredEvt.Invoke(val);
            }
        }

        internal void ShowVehicleNotFound(string reason) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Vehicle details were not found for '" + Registration + "'.");
            Console.WriteLine(reason);
            Console.ForegroundColor = defaultConsoleColour;

            ShowSessionEndMessage();
        }

        private void ShowSessionEndMessage() {

            ConsoleKey response;
            do {
                Console.WriteLine("");
                Console.WriteLine("Would you like to search for another vehicle (Y/N)?");
                response = Console.ReadKey(false).Key;
                if (response != ConsoleKey.Enter) {
                    Console.WriteLine();
                }
            } 
            while (response != ConsoleKey.Y && response != ConsoleKey.N);

            if (response == ConsoleKey.Y) {
                StartRegistrationSession();
            }
            else {
                if (ExitAppEvt != null) ExitAppEvt.Invoke(this, EventArgs.Empty);
            }
        }

        private void ClearView() {
            Console.Clear();
        }

        private void RequestRegistration() {
            Console.WriteLine(HubooHelper.APP_NAME);
            Console.WriteLine("");
            Console.WriteLine("Please enter the vehicle registration and press 'Enter':");
            Registration = HubooHelper.FormatRegistration(Console.ReadLine());

            Console.WriteLine("");
            Console.WriteLine("Retrieving information for the vehicle with registration: '" + Registration + "'. Please wait...");
            Console.WriteLine("");

            if (RegistrationEvt != null) {
                RegistrationEvt.Invoke(Registration);
            }
        }
    }
}
