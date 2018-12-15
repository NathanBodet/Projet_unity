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
                        if(!(i == 0 && j == 0 && k == 0 && l == 0))//room 0000 : n'existe pas
                        {
                            listePool.Add(Resources.Load("Rooms/" + i.ToString() + j.ToString() + k.ToString() + l.ToString(), typeof(GameObject)) as GameObject);
                        }
                        
                    }
                }
            }
        }
    }

    public GameObject tire(int[] req)//tire une salle dans al pool, en suivant les contraintes contenues dans req
        //req : tableau de 4 entiers valant 0, 1 ou 2 : 1 = sortie demandée, 0 = mur demandé, 2 = n'importe
    {
        int[] reqTrois = new int[4];
        for(int i = 0; i < 4; i++)
        {
            reqTrois[i] = 3;
        }
        if (req.Equals(reqTrois))
        {
            return null;
        }
        bool trouve = false;
        bool diffzero;
        int[] currComb = getRandComb();


        while (!trouve)
        {
            currComb = getRandComb();
            trouve = true;
            for (int i = 0; i<4; i++)
            {
                if(req[i] != 2)//si la room concernée n'existe pas encore (on s'en fiche du coup)
                {
                    if (currComb[i] != req[i])// compare si sa sortie/mur coincide avec la room d'a côté
                    {
                        trouve = false;
                    }
                }
                
            }   

        }
        //retourne la bonne salle trouvée
        return tire(currComb[0].ToString() + currComb[1].ToString() + currComb[2].ToString() + currComb[3].ToString());
    }

    public GameObject tire(String nom) // va chercher la salle 'nom' dans la pool de salles
    {
        foreach(GameObject obj in listePool)
        {
            if (obj.name.Equals(nom))
            {
                return obj;
            }
        }
        return null;
    }

    public int[] getRandComb()//retourne un tableau de 4 entiers valant chacun 1 ou 0
    {
        int[] retour = new int[4];

        for (int i = 0; i < 4; i++)
        {
            retour[i] = UnityEngine.Random.Range(0, 2);
        }
        //test si le tableau généré vaut 0000
        bool diffzero = false;
        foreach(int ini in retour)
        {
            if(ini == 1)
            {
                diffzero = true;
            }
        }
        if (!diffzero)//dans ce cas, on relance la fonction pour obtenir quelque chose de différent de 0000
        {
            return getRandComb();
        }
        return retour;
    }
    

}
