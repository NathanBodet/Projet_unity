using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    GameObject[][] rooms;
    public GameObject player;
    public MapPool poolMap;
    public Pool poolItem;
    bool[][] roomsFinies;
    public int[][] mapInit, roomsSpeciales;//0 si salle normale, 1 si salle speciale
    List<int[]> listeFinale;
    public GameObject ennemiPrefab,roomFin;
    public int maxEnnemis,numeroNiveau;
    public GameObject[][] roomsInst;
    public AudioClip newLevelClip;

    public bool shopActive; //prevent the player to move


    // Use this for initialization
    void Start() {

        instance = this;

        //Instanciations
        GameObject.Find("GameAudioManager").GetComponent<AudioSource>().volume = 0.2f;
        numeroNiveau = 1;
        roomsSpeciales = new int[5][];
        maxEnnemis = 3;
        mapInit = new int[5][];
        for (int i = 0; i < 5; i++)
        {
            mapInit[i] = new int[5];

        }
        listeFinale = new List<int[]>();
        rooms = new GameObject[5][];
        roomsInst = new GameObject[6][];
        roomsFinies = new bool[5][];
        for (int i = 0; i < 5; i++)
        {
            rooms[i] = new GameObject[5];
            roomsFinies[i] = new bool[5];
            roomsSpeciales[i] = new int[5];
            for(int j =0; j<5; j++)
            {
                roomsFinies[i][j] = false;
            }
        }
        for (int i = 0; i < 6; i++)
        {
            roomsInst[i] = new GameObject[6];
        }
        player.GetComponent<Player>().timerStart = (float)((int)(Time.time * 1000)) / 1000;
        genereMap();
        initieNiveau(false);

        DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
        if (datasnames != null)
        {
            if (datasnames.m == 1)
            {
                Datas datas = (Datas)DataManager.Load("Slot1.sav");
                if (datas != null)
                {
                    if (datas.j == 1)
                    {
                        loadDatasf1();
                        datas.j = 0;
                        DataManager.Save(datas, "Slot1.sav");
                        datasnames.m = 0;
                        DataManager.Save(datasnames, "names.sav");
                    }
                }
            }
            if (datasnames.m == 2)
            {
                Datas datas = (Datas)DataManager.Load("Slot2.sav");
                if (datas != null)
                {
                    if (datas.j == 1)
                    {
                        loadDatasf2();
                        datas.j = 0;
                        DataManager.Save(datas, "Slot2.sav");
                        datasnames.m = 0;
                        DataManager.Save(datasnames, "names.sav");
                    }
                }
            }
            if (datasnames.m == 3)
            {
                Datas datas = (Datas)DataManager.Load("Slot3.sav");
                if (datas != null)
                {
                    if (datas.j == 1)
                    {
                        loadDatasf3();
                        datas.j = 0;
                        DataManager.Save(datas, "Slot3.sav");
                        datasnames.m = 0;
                        DataManager.Save(datasnames, "names.sav");
                    }
                }
            }
        }

        //TODO: if shopActive : prevents the player to move
    }

    public void rechargerNiveau(int[][] map, bool[][] roomsFinies,int[][] roomsSpec)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Destroy(roomsInst[i][j]);
            }
        }
        this.mapInit = map;
        this.roomsFinies = roomsFinies;
        this.roomsSpeciales = roomsSpec;
        initieNiveau(false);
        
    }


    public void initieNiveau(bool detruireRooms)// Initie la map, génère les rooms
    {
        
        if (detruireRooms)
        {
            numeroNiveau++;
            Destroy(roomFin);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Destroy(roomsInst[i][j]);
                    
                    roomsFinies[i][j] = false;
                }
            }
        }
        Debug.Log("Bienvenue au niveau " + numeroNiveau);
        int[] req = new int[4];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {

                req = determineContraintes(i, j);//Détermination des conditions de génération req

                if (req != null)
                {
                    rooms[i][j] = poolMap.tire(req,roomsSpeciales[i][j]);//tire une room au hasard dans le pool, suivant les conditions req
                    creeRoom(i, j, rooms[i][j]);
                }
            }
        }
        roomFin = creeRoom(5,4, poolMap.tire("Salle_Fin"));
        GameObject.Find("GameAudioManager").GetComponent<AudioSource>().Play();
    }

    public GameObject creeRoom(int i, int j, GameObject room)//instanciation d'une room aux coordonnées i,j
    {
        GameObject objinst;
        //génération de la salle
        Vector3 pos = new Vector3(i * 38.3f, j * 28.8f, 0);
        Vector3 posCentre = new Vector3(i * 38.3f +82, j * 28.8f -58, 0);
        objinst = Instantiate(room, pos, Quaternion.identity) as GameObject;
        objinst.transform.localScale = new Vector3(0.05f, 0.05f, 1);
        //objinst.GetComponentInChildren<SalleManager>().gameManager = gameObject;
        roomsInst[i][j] = objinst;
        objinst.GetComponentInChildren<SalleManager>().posCentre = posCentre;
        objinst.GetComponentInChildren<SalleManager>().gameManager = gameObject;
        objinst.GetComponentInChildren<SalleManager>().room = objinst;

        //Spawn des ennemis
        if (!(i == 0 && j == 0))
        {
            
            int chanceSpawnObjet = UnityEngine.Random.Range(0, 100);

            if(chanceSpawnObjet <= 15)
            {
                try
                {
                    Instantiate(poolItem.TireAndRemove(), posCentre, Quaternion.identity);
                } catch(ArgumentException e)
                {

                }
                
            }
        }
        return objinst;
    }



    public int[] determineContraintes(int i, int j)
    {
        int[] req = new int[4];
        
        if(mapInit[i][j] == 0)
        {
            return null;
        }

        if (i == 0)
        {//si on est sur un bord
            req[3] = 0; }
        else
        { //cherche si la room située à l'ouest existe
            req[3] = mapInit[i-1][j]; }

        if (i == 4)//si on est sur un bord
        { req[2] = 0; }
        else
        {//cherche si la room située à l'est existe
            req[2] = mapInit[i + 1][j]; }

        if (j == 0)//si on est sur un bord
        { req[1] = 0; }
        else
        { //cherche si la room située au sud existe
            req[1] = mapInit[i][j-1]; }

        if (j == 4)//si on est sur un bord
        { req[0] = 0; }
        else
        {//cherche si la room située au nord existe
            req[0] = mapInit[i][j+1]; }

        if (i == 4 && j == 4)//si on est en haut à droite : une sortie est artificiellement demandée au cas ou la seule room possible soit 0000
        { req[2] = 1; }

        return req;
    }

    public void genereMap()
    {
        List<int[]> listMaze = new List<int[]>();
        int[] sortie = { 4, 4 };
        int[] tni = new int[2];

        while (!contient(listMaze,sortie))
        {
            listMaze = new List<int[]>();
            listeFinale = new List<int[]>();
            tni[0] = 0;
            tni[1] = 0;
            listMaze.Add(tni);
            regenereMapInit();
            listMaze = getMaze(0, 0);
        }
        purgeMapInit();
      
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
            int[] tabAjout = { tab[0], tab[1] };
            liste.Add(tabAjout);
            this.listeFinale.Add(tabAjout);

            listeRetour = getMaze(i - 1, j);
            foreach (int[] tabb in listeRetour)
            {
               liste.Add(tabb);
            }
        }
        tab[0] = i + 1;
        if (i <4 && mapInit[i + 1][j] == 1 && !contient(listeFinale, tab))
        {
            int[] tabAjout = { tab[0], tab[1] };
            liste.Add(tabAjout);
            this.listeFinale.Add(tabAjout);

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
            int[] tabAjout = { tab[0], tab[1] };
            liste.Add(tabAjout);
            this.listeFinale.Add(tabAjout);
            listeRetour = getMaze(i , j-1);
            foreach (int[] tabb in listeRetour)
            {
               liste.Add(tabb);

            }
        }

        tab[1] = j + 1;
        if (j < 4 && mapInit[i][j+1] == 1 && !contient(listeFinale, tab))
        {
            int[] tabAjout = { tab[0], tab[1] };
            liste.Add(tabAjout);
            this.listeFinale.Add(tabAjout);
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

    public int genereZeroOuUn(int chance)
    {
        float rand = UnityEngine.Random.Range(0,100);
        if(rand > chance)
        {
            return 0;
        } else
        {
            return 1;
        }
    }

    public void regenereMapInit()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                mapInit[i][j] = genereZeroOuUn(40);
                roomsSpeciales[i][j] = genereZeroOuUn(30);
            }
        }
        mapInit[0][0] = 1;
        mapInit[4][4] = 1;
    }

    public void purgeMapInit()
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if(mapInit[i][j] == 1)
                {
                    int[] test = { i, j };
                    if (!contient(listeFinale, test))
                    {
                        mapInit[i][j] = 0;
                    }
                }
                
            }
        }
    }

    public void saveDatasf1()
    {
        saveDatas("Slot1.sav");
    }

    public void saveDatasf2()
    {
        saveDatas("Slot2.sav");
    }

    public void saveDatasf3()
    {
        saveDatas("Slot3.sav");
    }

    public void loadDatasf1()
    {
        loadDatas("Slot1.sav");
    }

    public void loadDatasf2()
    {
        loadDatas("Slot2.sav");
    }

    public void loadDatasf3()
    {
        loadDatas("Slot3.sav");
    }

    public void saveDatas(string filename)
    {
        Datas datas = (Datas)DataManager.Load(filename);
        datas.map = mapInit;
        datas.roomsFinies = roomsFinies;
        datas.roomsSpec = roomsSpeciales;
        DataManager.Save(datas, filename);
    }

    public void loadDatas(string filename)
    {
        Datas datas = (Datas)DataManager.Load(filename);
        if (datas != null)
        {
            mapInit = datas.map;
            roomsFinies = datas.roomsFinies;
            roomsSpeciales = datas.roomsSpec;
            rechargerNiveau(mapInit, roomsFinies,roomsSpeciales);
        }
    }

    public void finirRoom(GameObject room)
    {
        if (room.Equals(roomFin))
        {
            return;
        }
        for(int i = 0; i<5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (rooms[i][j] != null && roomsInst[i][j].Equals(room))
                {
                    roomsFinies[i][j] = true;
                }
            }
        }
    }

    public bool isDebut(GameObject room)
    {
        if (room.Equals(roomsInst[0][0]))
        {
            return true;
        } else
        {
            for(int i = 0; i<5; i++)
            {
                for(int j = 0; j<5; j++)
                {
                    if(rooms[i][j] != null && room.Equals(roomsInst[i][j]) && !roomsFinies[i][j])
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}
