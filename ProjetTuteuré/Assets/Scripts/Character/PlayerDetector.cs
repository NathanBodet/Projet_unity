using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDetector : MonoBehaviour {

    public bool playerIsNearby;

	

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            playerIsNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            playerIsNearby = false;
        }
    }
}
