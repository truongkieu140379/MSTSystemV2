using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TutorSearchSystem.FCMHelpers
{
    public class FCMHandler
    {

        public void SendNotification(object data)
        {
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            SendByteArrayNotification(byteArray);
        }

        private void SendByteArrayNotification(Byte[] byteArray)
        {

            try
            {
                string server_api_key = "AAAABkf4iJ8:APA91bGsWZiIYlK4GtmzVIcH8_sRBT0f-KKNOSdswWX624vewzTo2RHyqQB9lTy1JjqzFZmi2gFj7DRHmpf8amzEHVzFPF4b8Rnh94zVcaAoOZ_M4252FrNeItoBIZEvWP3rs8EI0pIu";
                string sender_id = "";

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add($"Authorization: key={server_api_key}");
                tRequest.Headers.Add($"Sender: id={sender_id}");

                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tresponse = tRequest.GetResponse();
                dataStream = tresponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);

                string sReponseFromServer = tReader.ReadToEnd();

                tReader.Close();
                dataStream.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
