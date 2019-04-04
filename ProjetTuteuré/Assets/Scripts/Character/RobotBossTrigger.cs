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
                boss.GetComponent<RobotBossIA>().state = 1;
                boss.GetComponent<RobotBossIA>().jumpTime = Time.time;
                boss.GetComponent<EdgeCollider2D>().isTrigger = false;
            } else if(boss.GetComponent<DarkBossIA>() != null)
            {
                boss.GetComponent<DarkBossIA>().state = 1;
                boss.GetComponent<BoxCollider2D>().isTrigger = false;
            }
            else
            {
                boss.GetComponent<AliseAI>().state = 1;
                boss.tag = "Enemy";
            }
            
            
            Destroy(this.gameObject);
        }
    }


}
