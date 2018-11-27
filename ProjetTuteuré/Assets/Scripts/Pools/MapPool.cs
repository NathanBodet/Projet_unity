using System;
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
    }

    public GameObject tire(int[] req)
    {
        char[] reqstr = new char[4];
        for(int i = 0; i < 4; i++)
        {
            reqstr[i] = req[i].ToString()[0];
        }
        GameObject test;
        bool trouve = false;
        test = base.tire();

        
        /*while (!trouve)
        {
            test = base.tire();
            trouve = true;
            for (int i = 0; i<4; i++)
            {
                if(reqstr[i] == '2')//si la room concernée n'existe pas encore (on s'en fiche du coup)
                {
                    if (test.name[i] != req[i])// compare si sa sortie/mur coincide avec la room d'a côté
                    {
                        trouve = false;
                    }
                }
            }

        }*/
        return test;
    }

}
