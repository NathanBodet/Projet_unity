using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : Pool
{
    public int nbItems;

    // Start is called before the first frame update
    void Start()
    {
        listePool = new List<GameObject>(nbItems);
        for(int i = 1; i <= nbItems; i++)
        {
            listePool.Add(Resources.Load("Items/0" + i.ToString(), typeof(GameObject)) as GameObject);
        }
    }
}
