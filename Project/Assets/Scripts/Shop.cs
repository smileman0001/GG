using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int money = 100;
    public Text moneyText;
    public Text inventory;

    public void addItem(string item)
    {
        moneyText.text = money.ToString();
        inventory.text += "\n" + item;
    }
}
