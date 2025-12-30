using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace _Scripts
{
    public static class HttpClient
    {
        public static async Task<T> Get<T>(string endpoint)
        {
            var getRequest = CreateRequest(endpoint);
            await getRequest.SendWebRequest();

            while (!getRequest.isDone) await Task.Delay(10);
            return JsonConvert.DeserializeObject<T>(getRequest.downloadHandler.text);
        }

        public static async Task<T> Post<T>(string endpoint, object payload)
        {
            var postRequest = CreateRequest(endpoint, RequestType.POST, payload);
            await postRequest.SendWebRequest();

            while (!postRequest.isDone) await Task.Delay(10);

            string responseText = postRequest.downloadHandler.text;

            Debug.Log($"[Server Response]: {responseText}");

            if (postRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error POST: {postRequest.error}");
                return default;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(responseText);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Nu am putut citi JSON-ul! Serverul a trimis: '{responseText}'. Eroare: {ex.Message}");
                return default;
            }
        }

        public static async Task<T> Delete<T>(string endpoint)
        {
            var deleteRequest = CreateRequest(endpoint, RequestType.DELETE);
            await deleteRequest.SendWebRequest();

            while (!deleteRequest.isDone) await Task.Delay(10);
            return JsonConvert.DeserializeObject<T>(deleteRequest.downloadHandler.text);
        }

        private static UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
        {
            var request = new UnityWebRequest(path, type.ToString());

            if (data != null)
            {
                var bodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            return request;
        }

        private static void AttachHeader(UnityWebRequest request, string key, string value)
        {
            request.SetRequestHeader(key, value);
        }
    }

    public enum RequestType
    {
        GET = 0,
        POST = 1,
        PUT = 2,
        DELETE = 3,
    }
}