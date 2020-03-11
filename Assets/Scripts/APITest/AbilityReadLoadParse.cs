using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class AbilityReadLoadParse : MonoBehaviour
{
    public void StartAbilityRequest(string url)
    {
        //Debug.Log(url);
        StartCoroutine(GetRequestAbility(url));
    }

    private IEnumerator GetRequestAbility(string abilityUrl)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(abilityUrl))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = abilityUrl.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + "Error: " + webRequest.error);
            }
            else
            {
                string jsonTxt = webRequest.downloadHandler.text;
                //Debug.Log(pages[page] + "Recieved: " + jsonTxt);
                var json = JSON.Parse(jsonTxt);
                PokeReadLoadParse pRLP;
                pRLP = GetComponent<PokeReadLoadParse>();
                DisplayAbilityDesc(json["effect_entries"][0]);
            }
        }
    }

    public void DisplayAbilityDesc(JSONNode jsonAbilDesc)
    {
        Debug.Log("Desc: " + jsonAbilDesc["effect"]);
        Debug.Log("Short: " + jsonAbilDesc["short_effect"]);
    }
}
