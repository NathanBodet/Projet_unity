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
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                roomsFinies[i][j] = false;
                rooms[i][j] = pool.tire();
                creeRoom(i, j, rooms[i][j]);
            }
        }

    }

    public void creeRoom(int i, int j, GameObject room)
    {
        Debug.Log("je crée");
        Vector3 pos = new Vector3(i * 38, j * 26,0);
        GameObject objinst = Instantiate(room, pos, Quaternion.identity);
        objinst.transform.localScale = new Vector3(0.05f, 0.05f, 1);

    }
}
