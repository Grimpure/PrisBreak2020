using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class PokeReadLoadParse : MonoBehaviour
{
    public string pokeName;
    public string nameURL = "https://pokeapi.co/api/v2/pokemon/";
    public string abilURL;
    
    enum APICallState
    {
        NAME,
        ABILITY
    }

    // Start is called before the first frame update
    void Start()
    {        
        pokeName.ToLower();
        nameURL = nameURL + pokeName;
        //Debug.Log(url);
        StartCoroutine(GetRequestPokemon(nameURL));
    }

    private IEnumerator GetRequestPokemon(string url)
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
                //Debug.Log(pages[page] + "Recieved: " + jsonTxt);
                var json = JSON.Parse(jsonTxt);

                Debug.Log("Name: " + json["forms"][0]["name"]);
                CheckAbility(json["abilities"]);
            }
        }
    }

    private void CheckAbility(JSONNode jsonAbility)
    {
        AbilityReadLoadParse aRLP;
        aRLP = GetComponent<AbilityReadLoadParse>();

        for (int i = 0; i < jsonAbility.Count; i++)
        {
            JSONNode abilityJson = jsonAbility[i]["ability"];            
            Debug.Log("Ability " + (i+1) + ": " + abilityJson["name"]);
            aRLP.StartAbilityRequest(abilityJson["url"]);            
        }
    }
}
