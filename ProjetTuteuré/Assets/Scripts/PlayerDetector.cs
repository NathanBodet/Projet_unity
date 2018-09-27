using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDetector : MonoBehaviour {

    public bool playerIsNearby;

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            playerIsNearby = true;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player")
        {
            playerIsNearby = false;
        }
    }
}
