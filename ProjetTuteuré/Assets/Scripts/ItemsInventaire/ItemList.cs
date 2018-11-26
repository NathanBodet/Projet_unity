using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemList : MonoBehaviour {
    public List<Item> itemList;

    public void Start()
    {
        itemList = new List<Item>();
    }
}
