using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPool : Pool {

	void Start () {
        listePool = new List<GameObject>();

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    for (int l = 0; l < 2; l++)
                    {
                        if(i == 0 && j == 0 && k == 0 && l == 0)//room 0000 : n'existe pas
                        {
                            Debug.Log("Rooms/" + i.ToString() + j.ToString() + k.ToString() + l.ToString());
                        }
                        else
                        {
                            listePool.Add(Resources.Load("Rooms/" + i.ToString() + j.ToString() + k.ToString() + l.ToString()) as GameObject);

                        }


                    }
                }
            }
        }
        show();
    }

}
