using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalleManager : MonoBehaviour {

    List<Enemy> listeEnnemis;
    public bool finie;
    GameObject salleTiles;
    List<Porte> listePortes;


	// Use this for initialization
	void Start () {
        listeEnnemis = new List<Enemy>();
        finie = false;
        listePortes = new List<Porte>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void debut()
    {
        foreach(Porte por in listePortes)
        {
            por.close();

        }
    }
}
