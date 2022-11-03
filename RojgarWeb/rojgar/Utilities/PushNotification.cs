using RestSharp;

namespace rojgar.Utilities
{
    public class PushNotification
    {
        private readonly string token;
        private readonly string title;
        private readonly string message;
        private readonly string imageUrl;
        private readonly string FirebaseServerKey;
        public PushNotification(string firebaseServerKey, string token, string title, string message, string imageUrl)
        {
            this.token = token;
            this.title = title;
            this.message = message;
            this.imageUrl = imageUrl;
            this.FirebaseServerKey = firebaseServerKey;
        }
        public object Send()
        {
            string firebaseServerKey = FirebaseServerKey;
            var client = new RestClient("https://fcm.googleapis.com/fcm/send");
            var send = new RestRequest(Method.POST);
            send.AddHeader("content-type", "application/json");
            send.AddHeader("Authorization", "Bearer " + firebaseServerKey);
            string body = message;
            send.AddJsonBody(new { to = token, notification = new { title = title, body = body, image = imageUrl } });
            IRestResponse response = client.Execute(send);
            return response;
        }
    }
}
