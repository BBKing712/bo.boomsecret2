using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Flurl;
using Flurl.Http;

namespace bo.boomsecret.Common
{
    public static class BoomLoginHelper
    {

        private const string boomServer = "https://boom.dev.enapt.de";

        public static async Task<bool> CheckExistenceAsync(string encryptedString)
        {
            bool exists = false;
            string boomserverExists = (boomServer + "/users/exists?s=");
            Url existsUrl = new Url(boomserverExists + encryptedString);
            //-------------------------------------------------------
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(existsUrl.ToString());
            //request.Method = "GET";
            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //{

            //}
            try
            {
                HttpResponseMessage response = await existsUrl.AllowHttpStatus("404").SendAsync(HttpMethod.Get, null);
                exists = (response.StatusCode == System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception exception)
            {
                try
                {
                    TraceHelper.TraceException("D7B1FC61_D70A_4A08_9235_0236C3904065", exception);
                }
                catch (Exception)
                {
                }
            }


            return exists;
        }
        public static bool CheckExistence(string encryptedString)
        {
            bool exists = false;
            string boomserverExists = (boomServer + "/users/exists?s=");
            Url existsUrl = new Url(boomserverExists + encryptedString);
            //-------------------------------------------------------
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(existsUrl.ToString());
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    exists = (response.StatusCode == System.Net.HttpStatusCode.NoContent);
                }
            }
            catch(System.Net.WebException webException)
            {
                HttpWebResponse response = (HttpWebResponse)webException.Response;
                if ((response != null) && (response.StatusCode == HttpStatusCode.NotFound))
                {
                    exists = false;

                }
                else
                {
                    TraceHelper.TraceException("6E36DD20_1E82_4671_8EB7_D0FF5CA42886", (Exception)webException);
                }
            }
            catch (Exception exception)
            {
                try
                {
                    TraceHelper.TraceException("D7B1FC61_D70A_4A08_9235_0236C3904065", exception);
                }
                catch (Exception)
                {
                }
            }


            return exists;
        }
    }
}