using System;
using System.IO;
using System.Threading;
using quantifiedself.Helpers;

namespace SleepHack.DataStorage
{
    class SkydriveData : IDataService
    {
        public string OAuthAuthorizeUri { get; set; }

        public void Authorize()
        {
            //var client = new LiveConnectClient(e.Session);
            //:         App.Current.LiveSession = e.Session;
            //         this.txtLoginResult.Text = "Signed in.";
        }

        public Type DataType { get; set; }

        public bool SaveData(object data)
        {
            var fileName = "test";
            var byteArray = DataSerialization.SerializeObject(data);
            var fileStream = new MemoryStream(byteArray);
            var handle = new AutoResetEvent(false);
            var status = false;
            //var uploadClient = new LiveConnectClient(App.Current.LiveSession);
            //uploadClient.UploadCompleted += (s, e) =>
            //    {
            //        status = true;
            //        handle.Set();
            //    };
            //handle.WaitOne();
            //uploadClient.UploadAsync("me/skydrive", fileName, fileStream, OverwriteOption.Overwrite);
            return status;
        }

        public object LoadData()
        {
            return 1;
            //var client = new LiveConnectClient(App.Current.LiveSession);
            //client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(clientDataFetch_GetCompleted);
            //client.GetAsync("/me/skydrive/files");
            //var oAuthUri = BuildOAuthUri();
            //var client = new WebClient();
            //var handle = new AutoResetEvent(false);
            //object data = null;
            //client.OpenReadCompleted += (s, e) =>
            //    {
            //        data = e;
            //        handle.Set();
            //    };
            //client.OpenReadAsync(oAuthUri);
            //handle.WaitOne();
            //return data;
        }


    }
}
