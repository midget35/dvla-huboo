using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Huboo {
    internal class DvlaProxy {

        public Action<VehicleVO> DownloadOk;
        public Action<string> DownloadFail;

        private WebClient webClient = null;
        private string apiKey;
        public void Download(string registration, string apiKey) {

            this.apiKey = apiKey;

            if (string.IsNullOrWhiteSpace(registration)) {
                ThrowError("The registration is invalid.");
            }
            else {
                ResetWebClient(registration);
                webClient.DownloadDataAsync(new Uri(HubooHelper.API_ENDPOINT));
            }
        }


        private void ResetWebClient(string registration) {

            if (webClient != null) {
                webClient.DownloadDataCompleted -= OnDownloadDataComplete;
            }
            webClient = new WebClient();

            // Convince endpoint that client is a browser:
            webClient.Headers.Add(
                "user-agent", HubooHelper.HTTP_USER_AGENT
            );

            webClient.Headers.Add(
                "x-api-key", apiKey
            );

            webClient.QueryString.Add("registration", registration);

            webClient.DownloadDataCompleted += OnDownloadDataComplete;
        } // end ResetWebClient()

        private void OnDownloadDataComplete(object sender, DownloadDataCompletedEventArgs e) {
            webClient.DownloadDataCompleted -= OnDownloadDataComplete;

            if (e.Error != null || !GetWebResultValid(e.Result)) {
                ThrowError("The server could not complete the request.");
                return;
            }

            else if (GetWebResultValid(e.Result)) {
                string str = Encoding.Default.GetString(e.Result);
                VehicleVO vo = null;
                JObject json = null;

                if (string.IsNullOrWhiteSpace(str) || str.Length < 3) {
                    ThrowError("The server did not return data in the expected format.");
                    return;
                }

                str = JsonHelper.DeBracket(str);

                try {
                    json = JObject.Parse(str);
                }
                catch(Exception jsonError) {
                    ThrowError("The server response could not be parsed.");
                    return;
                }
                //Debug.WriteLine(json);

                vo = CreateVehicle(json);

                if (vo == null) {
                    ThrowError("The server response was not in the expected format.");
                    return;
                }

                if (DownloadOk != null) {
                    DownloadOk.Invoke(vo);
                }
            }
        } // end OnDownloadDataComplete()

        private VehicleVO CreateVehicle(JObject json) {
            JArray motArr;
            string make;
            string model;
            string colour;

            try {
                make    = (string)json["make"];
                model   = (string)json["model"];
                colour  = (string)json["primaryColour"];

                motArr  = (JArray)json["motTests"];
            }
            catch (Exception) {
                return null;
            }
            // TODO: Found an instance where this arr can be null, and need to review other vars. Needs discussion - ask Sean:
            if (motArr == null) return null;

            uint motFailCount = GetMotFailCount(motArr);
            DateTime expiry = GetMotExpiry(motArr);

            VehicleVO vo = new VehicleVO(
                make, model, colour, expiry, motFailCount
            );
            return vo;
        }

        private DateTime GetMotExpiry(JArray motArr) {
            DateTime dt = DateTime.MinValue;
            string dateStr;

            if (motArr.Count > 0) {
                dateStr = (string)motArr[0]["expiryDate"];
                //Debug.WriteLine("DATE STR: " + dateStr);

                try {
                    dt = DateTime.ParseExact(dateStr, HubooHelper.DATE_TIME_FORMAT, CultureInfo.InvariantCulture);
                }
                catch (FormatException formatException) {
                    Debug.WriteLine(formatException);

                    return dt; 
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex);
                    return dt; 
                }
                // Debug.WriteLine("DATE >>>: " + dt.ToString(HubooHelper.DATE_TIME_FORMAT));
            }
            return dt;
        }

        private uint GetMotFailCount(JArray motArr) {

            uint failed = 0;
            string result;

            for (int i = 0; i < motArr.Count; i++) {
                result = (string)motArr[i]["testResult"];
                if (result.ToUpper() == "FAILED") failed++;
            }
            return failed;
        }

        private bool GetWebResultValid(byte[] result) {
            return result != null && result.Length > 0;
        }

        private void ThrowError(string reason) {
            if (DownloadFail == null) return;
            else DownloadFail.Invoke(reason);
        }
    }
}
