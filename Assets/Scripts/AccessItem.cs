using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessItem : Item
{

    public int doorID;

    public AccessItem(string name, float weight, int doorID)
    {
        this.doorID = doorID;
    }

}
