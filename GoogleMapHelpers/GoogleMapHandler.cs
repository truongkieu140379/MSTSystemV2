using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TutorSearchSystem.GoogleMapHelpers
{
    public class GoogleMapHandler
    {
        public GoogleMapHandler()
        {
        }

        public float GetDistanceByAddresses(string origin, string destination)
        {
            try
            {
                GoogleMapHandler googleMapHandler = new GoogleMapHandler();
                string myJsonString = googleMapHandler.GetGoogleMapJsonByAddresses(origin, destination);

                var data = (JObject)JsonConvert.DeserializeObject(myJsonString);
                var rows = data["rows"].Children();
                foreach (var a in rows)
                {
                    foreach (var item in a["elements"])
                    {
                        return item["distance"]["value"].Value<float>();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return 0.0f;
        }

        private string GetGoogleMapJsonByAddresses(string origin, string destination)
        {
            String url = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + origin + "&destinations=" + destination + "&departure_time=now&key=AIzaSyD9-Hry2VU_1JRw3hO9nZaC42nxtg4vErk";

            try
            {

                WebRequest tRequest = WebRequest.Create(url);
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Close();

                WebResponse tresponse = tRequest.GetResponse();
                dataStream = tresponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);

                string sReponseFromServer = tReader.ReadToEnd();

                tReader.Close();
                dataStream.Close();
                return sReponseFromServer;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
