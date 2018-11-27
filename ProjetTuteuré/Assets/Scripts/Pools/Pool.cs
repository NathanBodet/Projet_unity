using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

    protected List<GameObject> listePool;

    // Use this for initialization
    void Start () {

    }

    public GameObject tire()
    {
        int rnd = (int)(Random.Range(0, 15));
        int i = 0;
        foreach(GameObject element in this.listePool)
        {
            if(i == rnd)
            {
                return element;
            }
            i++;
        }
        return null;
    }

    public void show()
    {
        foreach(Object element in listePool)
        {
            element.ToString();
        }
    }

}
