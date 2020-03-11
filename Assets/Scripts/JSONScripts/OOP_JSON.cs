using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public abstract class OOP_JSON : MonoBehaviour
{
    protected JSONNode currentJSONObj;

    protected virtual void ParseJSON(string jsonString) { }
    protected virtual void ReceivedTextureHandler(Texture2D texture2D) { }

    private IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + "Error: " + webRequest.error);
            }
            else
            {
                string jsonTxt = webRequest.downloadHandler.text;
                Debug.Log(pages[page] + "Recieved: " + jsonTxt);
                var json = JSON.Parse(jsonTxt);
            }
        }
    }

    private IEnumerator GetTexture(string imgUrl)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imgUrl))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                var tex = DownloadHandlerTexture.GetContent(uwr);
            }
        }
    }
}
    
