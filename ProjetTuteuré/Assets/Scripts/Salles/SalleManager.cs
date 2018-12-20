using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SalleManager : MonoBehaviour {

    public bool finie, roomEntre;
    GameObject salleTiles;
    public Porte porteOuest,porteNord,porteEst,porteSud;
    List<Porte> listePortes;

    public int nbEnnemisInSalle;

    private void Awake()
    {
        nbEnnemisInSalle = 0;
    }

    // Use this for initialization
    void Start () {
        roomEntre = false;
        listePortes = new List<Porte>();

        if (!finie)
        {
            listePortes.Add(porteOuest);
            listePortes.Add(porteNord);
            listePortes.Add(porteEst);
            listePortes.Add(porteSud);
        }

    }
	
	// Update is called once per frame
	void Update () {
        
        if (checkEnnemis())
        {
            Debug.Log("nb " + nbEnnemisInSalle);
            foreach (Porte por in listePortes)
            {
                if(por != null) { por.open(); }
                roomEntre = false;
            }
        }
	}

    public void debut()
    {
        roomEntre = true;
        foreach (Porte por in listePortes)
        {
            if (por != null) { por.close(); } 
        }
    }

    public bool checkEnnemis()
    {
        if (!roomEntre)
        {
            return false;
        }

        return nbEnnemisInSalle == 0;
    }

    public void AddEnemy()
    {
        nbEnnemisInSalle += 1;
        
    }

    public void RemoveEnemy()
    {
        nbEnnemisInSalle--;
    }
}
