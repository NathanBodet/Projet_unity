using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBossTrigger : MonoBehaviour
{
    public GameObject boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hé");
        if (collision.gameObject.tag.Equals("Player"))
        {
            boss.GetComponent<RobotBossIA>().state = 0;
            boss.GetComponent<RobotBossIA>().jumpTime = Time.time;
            Destroy(this.gameObject);
        }
    }


}
