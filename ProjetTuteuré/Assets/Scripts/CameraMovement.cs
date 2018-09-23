using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject target;
    private Vector3 targetPos;
    public float trackingSpeed;


    void Update()
    {
        if(target != null)
        {
            targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, trackingSpeed * Time.deltaTime);
        }
    }
}
