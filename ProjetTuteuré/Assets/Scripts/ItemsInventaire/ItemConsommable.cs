using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConsommable : Item
{
    private Player playerScript;
    public int consommationsRestantes;
    public int typeConsommable;// 1 pour heal, 2 pour levelUp agility, 3 pour levelUp strength, 4 pour levelUp endurance
    public int healPoints,levelPoints;//quantité de heal et quantité de points à ajouter à la caractéristique

    public void use()
    {
        consommationsRestantes--;
        playerScript = player.GetComponent<Player>();
        switch (typeConsommable)
        {
            case 1:
                if(playerScript.currentHealth+healPoints >= playerScript.maxHealth)
                {
                    playerScript.currentHealth = playerScript.maxHealth;
                } else
                {
                    playerScript.currentHealth += healPoints;
                }
                playerScript.lifeBar.EnableLifeBar(true);
                playerScript.lifeBar.SetProgress(playerScript.currentHealth / playerScript.maxHealth);
                break;
            case 2:
                playerScript.baseagility+= levelPoints;
                break;
            case 3:
                playerScript.basestrength+= levelPoints;
                break;
            case 4:
                playerScript.baseendurance += levelPoints;
                break;
        }
    }


}
