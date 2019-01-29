using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SalleManager : MonoBehaviour {

    public bool roomEntre;
    public GameObject salleTiles, gameManager,room;
    public Porte porteOuest,porteNord,porteEst,porteSud;
    List<Porte> listePortes;
    public GameObject ennemiPrefab;
    public Vector3 posCentre;
    public int nbEnnemisInSalle;
    public bool itemsDetruits;
    public List<GameObject> listeItem;
    public float timeAfterRoomEnd;//utilisé pour faire déspawn les items après un certain temps

    private void Awake()
    {
        nbEnnemisInSalle = 0;
        listePortes = new List<Porte>();
        listePortes.Add(porteOuest);
        listePortes.Add(porteNord);
        listePortes.Add(porteEst);
        listePortes.Add(porteSud);

    }

    // Use this for initialization
    void Start () {
        roomEntre = false;
        listeItem = new List<GameObject>();
        itemsDetruits = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time-timeAfterRoomEnd> 30f && !itemsDetruits)
        {
            itemsDetruits = true;
            foreach(GameObject obj in listeItem)
            {
                if (obj != null && (!GameObject.Find("Player").GetComponent<InventaireScript>().isInInventaire(obj)))
                {
                    Destroy(obj);
                }
            }
        }
        if (checkEnnemis())
        {
            gameManager.GetComponent<GameManager>().finirRoom(room);
            timeAfterRoomEnd = Time.time;
            roomEntre = false;
            foreach (Porte por in listePortes)
            {
                if(por != null) { por.open(); }
                
            }
        }
	}

    public void debut(GameObject pl)
    {
        if(GetComponentInChildren<ExitLevel>()!= null)
        {
            Debug.Log("trouvé");
            GetComponentInChildren<ExitLevel>().player = pl;
        }
        if (!gameManager.GetComponent<GameManager>().isDebut(room))
        {
            int nbEnnemis = Random.Range(0, 3);
            for (int k = 0; k < nbEnnemis; k++)
            {
                GameObject enemyInstancie = Instantiate(ennemiPrefab, posCentre, Quaternion.identity);
                enemyInstancie.GetComponent<Enemy>().manager = this;
                AddEnemy();
            }
            roomEntre = true;
            foreach (Porte por in listePortes)
            {
                if (por != null) { por.close(); }
            }
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
