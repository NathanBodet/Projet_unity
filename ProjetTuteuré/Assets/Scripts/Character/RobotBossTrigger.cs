using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBossTrigger : MonoBehaviour
{
    public GameObject boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if(boss.GetComponent<RobotBossIA>() != null)
            {
                boss.GetComponent<RobotBossIA>().state = 0;
                boss.GetComponent<RobotBossIA>().jumpTime = Time.time;
            } else
            {
                boss.GetComponent<DarkBossIA>().state = 0;
            }
            
            Destroy(this.gameObject);
        }
    }


}
