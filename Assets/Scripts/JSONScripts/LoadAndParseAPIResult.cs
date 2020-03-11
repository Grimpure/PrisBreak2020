using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class LoadAndParseAPIResult : MonoBehaviour
{
    public string apiCallUrl = "https://api.nationalize.io?name=Mathijs";

    public List<string> names = new List<string>() {"Mathijs", "Baris", "Ad", "Kevin", "Bram", "Paul", "Gordon"};

    private Dictionary<string, string> countryDict = new Dictionary<string, string>()
    {
        ["NL"] = "Netherlands",
        ["BE"] = "Belguim",
        ["FR"] = "France",
        ["ZA"] = "South Africa",
        ["UK"] = "United Kingdom",
        ["BY"] = "Belarus",
        ["UA"] = "Ukraine",
        ["AF"] = "Afghanistan",
        ["AU"] = "Australia",
        ["BG"] = "Bulgaria",
        ["CA"] = "Canada",
        ["ID"] = "Indonesia",
        ["IE"] = "Ireland",
        ["IT"] = "Italy",
        ["IL"] = "Israel"
    };

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest(apiCallUrl));
    }

    void RandomizeName()
    {

    }

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
                Debug.Log("Name: " + json[0]);
                //Debug.Log("Country: " + json[1][0][0]);
                //Debug.Log("Probabitlity: " + json[1][0][1]);

                List<float> probabilities = new List<float>();
                for (int i = 0; i < json["country"].Count; i++)
                {
                    float myFloatValue = (float)json["country"][i]["probability"];
                    //float myFloatValue2 = float.Parse(json["country"][i]["probability"]);
                    probabilities.Add(myFloatValue);
                    JSONNode countryID = json["country"][i]["country_id"];
                    //Debug.Log("CountryIDstr = " + countryID);
                    if (countryDict.ContainsKey(countryID))
                    {
                        Debug.Log("Country: " + countryDict[countryID]);
                    }
                    //Debug.Log("Country: " + json["country"][i]["country_id"]);
                    Debug.Log("Probability: " + probabilities[i]);
                    //Debug.Log("myFloatValue = " + myFloatValue);
                    //Debug.Log("myFloatValue2 = " + myFloatValue2);
                    //Debug.Log(json["country"][i]["probability"].IsNumber);

                }
            }
        }
    }
}
