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
    public GameObject canvas, canvas2, canvas3, canvas4, canvas5, canvas6, canvas7;
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

            if (Input.GetKeyDown(KeyCode.I) && showGUI == false && showGUI3==false)
            {
                showGUI2 = !showGUI2;
            }

            if (showGUI && !showGUI2 && !showGUI3)
            {
                canvas.SetActive(true);
                Time.timeScale = 0;
            }
            else if (!showGUI && showGUI2 && !showGUI3)
            {
                canvas2.SetActive(true);
            Time.timeScale = 0;
            }
        else if (!showGUI && !showGUI2 && showGUI3)
        {
            Time.timeScale = 0;
        }
        else
            {
                canvas.SetActive(false);
                canvas2.SetActive(false);
                Time.timeScale = 1;
            }
    }

        void OnLevelWasLoaded()
        {
            //canvas = GameObject.Find("Canvas");
            //canvas2 = GameObject.Find("Canvas2");
        }

        public void Continue()
        {
            showGUI = false;
            canvas.SetActive(false);
        canvas4.SetActive(false);
            canvas5.SetActive(false);
        canvas6.SetActive(false);
        canvas7.SetActive(false);
        Time.timeScale = 1;
        }

    public void save()
    {
        canvas.SetActive(false);
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
        canvas.SetActive(false);
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

    public void changeWeapon()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name[9]);
        var objet = GetComponent<Player>().GetComponent<InventaireScript>().listeItems[(int)char.GetNumericValue(EventSystem.current.currentSelectedGameObject.name[9])];

        if (objet != null)
        {
            if (objet.tag == "ArmeD")
            {
                GetComponent<Player>().armeDistanceEquipee = objet;
                objet.GetComponent<RangedWeapon>().equip(gameObject);
                
            } else if (objet.tag == "ArmeCAC")
            {
                GetComponent<Player>().armeCorpsACorpsEquipee = objet;
                objet.GetComponent<MeleeWeapon>().equip(gameObject);
            }

        }
    }

}
