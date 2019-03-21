using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonInventaire : MonoBehaviour, IPointerClickHandler
{
    GameObject player;
    InventaireScript inv;
    private void Start()
    {
        player = GameObject.Find("Player");
        inv = player.GetComponent<InventaireScript>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Cette fonction va être appelée quand on va cliquer sur l'un des 10 boutons de l'inventaire
            //Debug.Log(EventSystem.current.currentSelectedGameObject.name[9]);
            var objet = inv.listeItems[(int)char.GetNumericValue(gameObject.name[9])];

            if (objet != null && objet != player.GetComponent<Player>().armeCorpsACorpsEquipee && objet != player.GetComponent<Player>().armeDistanceEquipee)
            {
                //Si on clique sur une arme à distance de tag ArmeD, on l'équipe

                if (objet.tag == "ArmeD")
                {
                    player.GetComponent<Player>().armeDistanceEquipee = objet;
                    objet.GetComponent<RangedWeapon>().equip(player);

                }
                //Pareil pour les armes au cac
                else if (objet.tag == "ArmeCAC")
                {
                    player.GetComponent<Player>().armeCorpsACorpsEquipee = objet;
                    objet.GetComponent<MeleeWeapon>().equip(player);
                }
                //pareil pour les consommables
                else if (objet.tag == "Consommable")
                {
                    objet.GetComponent<ItemConsommable>().use();
                    if (objet.GetComponent<ItemConsommable>().consommationsRestantes == 0)
                    {
                        inv.retirerItem(objet);
                    }
                }
                //Pareil pour les armures de tete
                else if (objet.tag == "ArmureTete")
                {
                    player.GetComponent<Player>().armureTeteEquipee = objet;
                    objet.GetComponent<HeadArmor>().equip(player);
                }
                //Pareil pour les armures de torse
                else if (objet.tag == "ArmureTorse")
                {
                    player.GetComponent<Player>().armureTorseEquipee = objet;
                    objet.GetComponent<BodyArmor>().equip(player);
                }
                //Pareil pour les armures de jambes
                else if (objet.tag == "ArmureJambe")
                {
                    player.GetComponent<Player>().armureJambesEquipee = objet;
                    objet.GetComponent<LegArmor>().equip(player);
                }


                inv.updateMenuInventaire();

            }
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            Debug.Log("Middle click");
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            var objet = inv.listeItems[(int)char.GetNumericValue(gameObject.name[9])];
            if(objet != null)
            {
                if(objet != player.GetComponent<Player>().armeCorpsACorpsEquipee && objet != player.GetComponent<Player>().armeDistanceEquipee)
                {
                    objet.GetComponent<Item>().drop();
                } 
            }
        }
    }
}
