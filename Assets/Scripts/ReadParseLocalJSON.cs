using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class ReadParseLocalJSON : MonoBehaviour
{
    public string jsonFileName = "mathijs.json";

    // Start is called before the first frame update
    void Start()
    {
        string jsnStr = ReadLocalJSONFile(jsonFileName);
    }

    private string ReadLocalJSONFile(string jsonFile)
    {
        string filePath = "" + jsonFile.Replace(".json", "");
        TextAsset tA = Resources.Load<TextAsset>(filePath);
        return tA.text;
    }

    private void JSONParse(string jsonStr)
    {
        Debug.Log("_json = " + jsonStr);

        JSONNode jsonObj = JSON.Parse(jsonStr);

        Debug.Log("jsonObj [0] = " + jsonObj[0]);
        Debug.Log("jsonObj [0][1] = " + jsonObj[0][1]);
        Debug.Log("jsonObj [1] = " + jsonObj[1]);
        Debug.Log("jsonObj [\"Name\"][\"First Name\"] = " + jsonObj["Name"]["FirstName"]);
        Debug.Log("jsonObj ['Name'] is Object = " + jsonObj["Name"].IsObject);
        Debug.Log("jsonObj ['Name'] is Array = " + jsonObj["Name"].IsArray);
        Debug.Log("jsonObj ['Little Brothers'] is Array = " + jsonObj["Little Brothers"].IsArray);
    }
}
