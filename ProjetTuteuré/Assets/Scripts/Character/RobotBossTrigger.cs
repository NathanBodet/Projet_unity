using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBossTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hé");
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameObject.Find("RobotBoss").GetComponent<RobotBossIA>().state = 0;
            GameObject.Find("RobotBoss").GetComponent<RobotBossIA>().jumpTime = Time.time;
            Debug.Log("c bon");
            Destroy(this.gameObject);
        }
    }


}
