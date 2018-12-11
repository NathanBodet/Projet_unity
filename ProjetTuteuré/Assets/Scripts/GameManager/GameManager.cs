using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    GameObject[][] rooms;
    public MapPool pool;
    bool[][] roomsFinies;
    int[][] mapInit;
    List<int[]> listeFinale;

    // Use this for initialization
    void Start() {
        //Instanciations
        mapInit = new int[5][];
        for (int i = 0; i < 5; i++)
        {
            mapInit[i] = new int[5];

        }
        listeFinale = new List<int[]>();
        rooms = new GameObject[5][];
        roomsFinies = new bool[5][];
        for (int i = 0; i < 5; i++)
        {
            rooms[i] = new GameObject[5];
            roomsFinies[i] = new bool[5];
        }

        initieNiveau();

    }


    public void initieNiveau()// Initie la map, génère les rooms
    {
        genereMap();
        int[] req = new int[4];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                roomsFinies[i][j] = false;

                req = determineContraintes(i, j);//Détermination des conditions de génération req

                rooms[i][j] = pool.tire(req);//tire une room au hasard dans le pool, suivant les conditions req
                if (rooms[i][j] != null)
                {
                    creeRoom(i, j, rooms[i][j]);
                }
                
            }
        }

    }

    public void creeRoom(int i, int j, GameObject room)//instaciation d'une room aux coordonnées i,j
    {
        Vector3 pos = new Vector3(i * 38, j * 26, 0);
        GameObject objinst = Instantiate(room, pos, Quaternion.identity);
        objinst.transform.localScale = new Vector3(0.05f, 0.05f, 1);

    }

    public int[] determineContraintes(int i, int j)
    {
        int[] req = new int[4];
        
       /* if(mapInit[i][j] == 0)
        {
            for(int k =0; k<4; k++)
            {
                req[k] = 3;
            }
            return req;
        }*/

        if (i == 0)
        {//si on est sur un bord
            req[3] = 0; }
        else
        { //renvoie l'élément 3 du nom de la room précédente en j : cette valeur donne la contrainte du mur Ouest
            req[3] = (int)(Int32.Parse(rooms[i - 1][j].name) / 10) % 10; }

        if (i == 4)//si on est sur un bord
        { req[2] = 0; }
        else
        { req[2] = 2; }

        if (j == 0)//si on est sur un bord
        { req[1] = 0; }
        else
        { //renvoie l'élément 1 du nom de la room précédente en j : cette valeur donne la contrainte du mur Sud
            req[1] = ((int)(Int32.Parse(rooms[i][j - 1].name)) / 1000) % 10; }

        if (j == 4)//si on est sur un bord
        { req[0] = 0; }
        else
        { req[0] = 2; }

        if (i == 4 && j == 4)//si on est en haut à droite : une sortie est artificiellement demandée au cas ou la seule room possible soit 0000
        { req[2] = 1; }

        return req;
    }

    public void genereMap()
    {
        int x, y = 0;


        

        for(int i = 0; i < 5; i++)
        {
            for(int j =0; j < 5; j++)
            {
                mapInit[i][j] = (int)(UnityEngine.Random.Range(0, 2));
            }
        }

        List<int[]> listMaze = new List<int[]>();
        int[] tni = new int[2];
        tni[0] = 0;
        tni[1] = 0;
        listMaze.Add(tni);
        //while (!listMaze.Contains(tni)){
            mapInit[0][0] = 1;
            
        listMaze = getMaze(0,0);
            foreach (int[] i in listMaze)
        {
            Debug.Log("i :"+i[0]);
            Debug.Log("j :"+i[1]);
        }
        //}

      
    }

    public List<int[]> getMaze(int i,int j)
    {
        List<int[]> liste = new List<int[]>();
        List<int[]> listeRetour;
        int[] tab = new int[2];
        tab[0] = i-1;
        tab[1] = j;
        
        if (i > 0 && mapInit[i - 1][j] == 1 && !contient(listeFinale,tab))
        {
            liste.Add(tab);
            listeFinale.Add(tab);

           listeRetour = getMaze(i - 1, j);
            foreach (int[] tabb in listeRetour)
            {
               liste.Add(tabb);
            }
        }
        tab[0] = i + 1;
        if (i <4 && mapInit[i + 1][j] == 1 && !contient(listeFinale, tab))
        {
            liste.Add(tab);
            listeFinale.Add(tab);

            listeRetour = getMaze(i + 1, j);
            foreach (int[] tabb in listeRetour)
            {
                liste.Add(tabb);

            }
        }
        tab[0] = i;

        tab[1] = j - 1;
        if (j > 0 && mapInit[i][j-1] == 1 && !contient(listeFinale, tab))
        {
            liste.Add(tab);
            listeFinale.Add(tab);
            listeRetour = getMaze(i , j-1);
            foreach (int[] tabb in listeRetour)
            {
               liste.Add(tabb);

            }
        }

        tab[1] = j + 1;
        if (j < 4 && mapInit[i][j+1] == 1 && !contient(listeFinale, tab))
        {
            liste.Add(tab);
            listeFinale.Add(tab);
            listeRetour = getMaze(i, j+1);
            foreach (int[] tabb in listeRetour)
            {
               liste.Add(tabb);

            }
        }
        return liste;
    }

    public bool contient(List<int[]> list, int[] tab)
    {
        foreach(int[] i in list)
        {
            if(i[0] == tab[0] && i[1] == tab[1])
            {
                return true;
            }
        }
        return false;
    }
}
