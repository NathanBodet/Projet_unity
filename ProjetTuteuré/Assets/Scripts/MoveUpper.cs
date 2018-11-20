using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpper : MonoBehaviour {

    public Vector3 moveDirection = Vector3.up;
    public float speed = 10.0f;

	void Update () {
        transform.localPosition = transform.localPosition + moveDirection * speed * Time.deltaTime;
	}
}
