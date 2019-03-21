using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenusJeu : MonoBehaviour
    {

    public bool showGUI = false;
    public bool showGUI2 = false;
    public bool showGUI3 = false;
    public GameObject canvas1, canvas2, canvas3, canvas4, canvas5, canvas6, canvas7, uiarmes, lifebar;
    public Text text1, text2, text3, text4, text5, text6;

    // Use this for initialization
    void Start()
        {
        canvas3.SetActive(false);
        canvas4.SetActive(false);
        canvas5.SetActive(false);
        canvas6.SetActive(false);
        canvas7.SetActive(false);
    }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && showGUI2 == false && showGUI3==false)
            {
                showGUI = !showGUI;
            }

            if (Input.GetKeyDown(KeyCode.A) && showGUI == false && showGUI3==false)
            {
                showGUI2 = !showGUI2;
            }

            if (showGUI && !showGUI2 && !showGUI3)
            {
                canvas1.SetActive(true);
                uiarmes.SetActive(false);
                lifebar.SetActive(false);
                Time.timeScale = 0;
            }
            else if (!showGUI && showGUI2 && !showGUI3)
            {
                uiarmes.SetActive(false);
                canvas2.SetActive(true);
            }
            else if (!showGUI && !showGUI2 && showGUI3)
            {
            }
            else
            {
                uiarmes.SetActive(true);
                //On remet à jour les images après les changements dans l'inventaire, c'est un peu du cheat mais c'est normal
                GameObject UIEquip = GameObject.FindGameObjectWithTag("ArmeUI");
                UIEquip.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().armeCorpsACorpsEquipee.GetComponent<SpriteRenderer>().sprite;
                UIEquip.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().GetChild(1).gameObject.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().armeDistanceEquipee.GetComponent<SpriteRenderer>().sprite;
                UIEquip.GetComponent<RectTransform>().GetChild(2).GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Text>().text =
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().armeDistanceEquipee.GetComponent<RangedWeapon>().ammunition + "/" +
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().armeDistanceEquipee.GetComponent<RangedWeapon>().totalammunition;

                lifebar.SetActive(true);
                canvas1.SetActive(false);
                canvas2.SetActive(false);
                Time.timeScale = 1;
            }
    }

    public void Continue()
    {
        showGUI = false;
        showGUI3 = false;
        canvas1.SetActive(false);
        canvas4.SetActive(false);
        canvas5.SetActive(false);
        canvas6.SetActive(false);
        canvas7.SetActive(false);
        Time.timeScale = 1;
    }

    public void save()
    {
        canvas1.SetActive(false);
        showGUI = false;
        showGUI3 = true;
        canvas3.SetActive(true);
        DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
        if (datasnames != null)
        {
            if (datasnames.name != null)
            {
                text1.text = datasnames.name;
            }
            if (datasnames.name2 != null)
            {
                text2.text = datasnames.name2;
            }
            if (datasnames.name3 != null)
            {
                text3.text = datasnames.name3;
            }
        }
    }

    public void load()
    {
        canvas1.SetActive(false);
        showGUI = false;
        showGUI3 = true;
        canvas5.SetActive(true);
        DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
        if (datasnames != null)
        {
            if (datasnames.name != null)
            {
                text4.text = datasnames.name;
            }
            if (datasnames.name2 != null)
            {
                text5.text = datasnames.name2;
            }
            if (datasnames.name3 != null)
            {
                text6.text = datasnames.name3;
            }
        }
    }

    public void butSaves()
    {
        canvas3.SetActive(false);
        canvas4.SetActive(true);
    }

    public void butSaves2()
    {
        canvas3.SetActive(false);
        canvas6.SetActive(true);
    }

    public void butSaves3()
    {
        canvas3.SetActive(false);
        canvas7.SetActive(true);
    }


    public void validate()
    {
        showGUI3 = false;
        canvas4.SetActive(false);
        canvas6.SetActive(false);
        canvas7.SetActive(false);
    }

    public void Quit()
        {
            SceneManager.LoadScene("MainMenu");
        }

  
    public bool getShowGUI2()
    {
        return this.showGUI2;
    }
}
