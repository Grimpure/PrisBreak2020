using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructTest : MonoBehaviour
{
    public string[] animals = new string[5] { "Deer", "Boar", "Duck", "Chicken", "Monkey" };

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Array 1 has " + animals.Length + " objects.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
