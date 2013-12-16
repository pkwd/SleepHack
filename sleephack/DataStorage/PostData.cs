using System;
using System.Net;
using System.Text;
using quantifiedself.Helpers;

namespace SleepHack.DataStorage
{
    public class PostData : IDataService
    {
        private object Data { get; set; }
        public Type DataType { get; set; }

        public bool SaveData(object data)
        {
            Data = data;
            var url = new Uri("http://baseapps.pl/dev/sleephack/post/send", UriKind.Absolute);

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            request.BeginGetRequestStream(GetRequestStreamCallback, request);
            return true;
        }

        public void GetRequestStreamCallback(IAsyncResult asyncResult)
        {
            var request = (HttpWebRequest)asyncResult.AsyncState;
            var postStream = request.EndGetRequestStream(asyncResult);
            var postData = new StringBuilder();
            var json = DataSerialization.GetJson(DataType, Data);
            var data = string.Format("param1={0}", json);
            postData.Append(data);
            var byteArray = Encoding.UTF8.GetBytes(postData.ToString());
            postStream.Write(byteArray, 0, postData.Length);
            postStream.Close();

            request.BeginGetResponse(GetResponseCallback, request);
        }



        private void GetResponseCallback(IAsyncResult ar)
        {
            
        }

        public object LoadData()
        {
            var url = new Uri("http://baseapps.pl/dev/sleephack/post/send");
            var client = new WebClient();
            client.OpenReadAsync(url);
            client.OpenReadCompleted += ClientOnOpenReadCompleted;
            return 1;
            //throw new System.NotImplementedException();
        }

        private void ClientOnOpenReadCompleted(object sender, OpenReadCompletedEventArgs openReadCompletedEventArgs)
        {
            var result = openReadCompletedEventArgs.Result;
        }
    }
}