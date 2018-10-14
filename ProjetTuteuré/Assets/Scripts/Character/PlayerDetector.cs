using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerDetector : MonoBehaviour {

    //Enemy parent;
    public bool playerIsNearby;

	/*void Start () {
        parent = transform.parent.GetComponent<Enemy>();
	}*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //parent.JoueurDetecte(collision.gameObject);
            playerIsNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //parent.JoueurDetecte(null);
            playerIsNearby = false;
        }
    }
}
