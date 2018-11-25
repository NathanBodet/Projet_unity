using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SalleManagerScript : MonoBehaviour {

    List<Enemy> listeEnnemis;
    public bool finie;
    GameObject salleTiles;
    public Porte porteOuest,porteNord,porteEst,porteSud;
    public Enemy ennemi1;
    List<Porte> listePortes;




    // Use this for initialization
    void Start () {
        if (!finie)
        {
            listeEnnemis = new List<Enemy>();
            listePortes = new List<Porte>();
            listePortes.Add(porteOuest);
            listePortes.Add(porteNord);
            listePortes.Add(porteEst);
            listePortes.Add(porteSud);
            listeEnnemis.Add(ennemi1);
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (checkEnnemis())
        {
            foreach(Porte por in listePortes)
            {
                if(por != null) { por.open(); }

            }
        }
	}

    public void debut()
    {
        foreach(Porte por in listePortes)
        {
            if (por != null) { por.close(); } 
        }
    }

    public bool checkEnnemis()
    {
        foreach(Enemy ennemi in listeEnnemis)
        {
            if (ennemi.isAlive)
            {
                return false;
            }
        }
        return true;
    }
}
