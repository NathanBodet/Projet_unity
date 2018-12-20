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

    }
	
	// Update is called once per frame
	void Update () {
        
        if (checkEnnemis())
        {
            gameManager.GetComponent<GameManager>().finirRoom(room);
            roomEntre = false;
            foreach (Porte por in listePortes)
            {
                if(por != null) { por.open(); }
                
            }
        }
	}

    public void debut()
    {
        if (!gameManager.GetComponent<GameManager>().isDebut(room))
        {
            int nbEnnemis = UnityEngine.Random.Range(0, 3);
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
