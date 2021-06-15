using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int cost;
    public string itemName;

    public void buy()
    {
        if(GetComponentInParent<GameScript>().AllGold >= cost)
        {
            GetComponentInParent<GameScript> ().AllGold -= cost;
        }
    }
}
