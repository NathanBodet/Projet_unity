using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    GameObject[][] rooms;
    public MapPool pool;
    bool[][] roomsFinies;

	// Use this for initialization
	void Start () {
        rooms = new GameObject[5][];
        roomsFinies = new bool[5][];
        for(int i = 0; i<5; i++)
        {
            rooms[i] = new GameObject[5];
            roomsFinies[i] = new bool[5];
        }

        initieNiveau();

    }


    public void initieNiveau()
    {
        int[] req = new int[4];
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                roomsFinies[i][j] = false;
                //Détermination des consitions de génréation req
                req[0] = 1;
                req[1] = 1;
                req[2] = 1;
                req[3] = 1;
                if (i == 0 ) {
                    req[3] = 0;
                } else
                {
                    req[3] = (int)(Int32.Parse(rooms[i - 1][j].name) / Math.Pow(10, 0) % 10);
                }
                if (i == 4)
                {
                    req[2] = 0; 
                } else
                {
                    req[2] = 2;
                }

                if (j == 0)
                {
                    req[1] = 0;
                } else
                {
                    req[1] = (int)(Int32.Parse(rooms[i][j-1].name) / Math.Pow(10, 2) % 10);
                }
                if (j == 4)
                {
                    req[0] = 0;
                } else
                {
                    req[0] = 2;
                }
                
                rooms[i][j] = pool.tire(req);//tire une room au hasard dans le pool
                creeRoom(i, j, rooms[i][j]);
            }
        }

    }

    public void creeRoom(int i, int j, GameObject room)//instaciation d'une room aux coordonnées i,j
    {
        Vector3 pos = new Vector3(i * 38, j * 26,0);
        GameObject objinst = Instantiate(room, pos, Quaternion.identity);
        objinst.transform.localScale = new Vector3(0.05f, 0.05f, 1);

    }
}
