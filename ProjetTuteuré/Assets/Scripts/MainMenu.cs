using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject canvas, canvas2;
    public Text text1, text2, text3;

    // Use this for initialization
    void Start()
    {
        canvas.SetActive(true);
        canvas2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void start()
    {
        SceneManager.LoadScene("Salle1");
    }

    public void load()
    {
        canvas.SetActive(false);
        canvas2.SetActive(true);
        DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
        if (datasnames != null)
        {
            text1.text = datasnames.name;
            text2.text = datasnames.name2;
            text3.text = datasnames.name3;
        }
    }

    public void loadB1()
    {
        string name = text1.text;
        if (name != "No datas")
        {
            canvas2.SetActive(false);
            Datas datas = (Datas)DataManager.Load("Slot1.sav");
            DatasNames dtn = (DatasNames)DataManager.LoadNames("names.sav");
            if (datas != null)
            {
                datas.i = 1;
                dtn.n = 1;
                datas.j = 1;
                dtn.m = 1;
                datas.name = name;
                DataManager.Save(dtn, "names.sav");
                DataManager.Save(datas, "Slot1.sav");
                SceneManager.LoadScene(datas.nameScene);
            }
        }
    }

    public void loadB2()
    {
        string name = text2.text;
        if (name != "No datas")
        {
            canvas2.SetActive(false);
            Datas datas = (Datas)DataManager.Load("Slot2.sav");
            DatasNames dtn = (DatasNames)DataManager.LoadNames("names.sav");
            if (datas != null)
            {
                datas.i = 1;
                dtn.n = 2;
                datas.j = 1;
                dtn.m = 2;
                datas.name = name;
                DataManager.Save(dtn, "names.sav");
                DataManager.Save(datas, "Slot2.sav");
                SceneManager.LoadScene(datas.nameScene);
            }
        }
    }

    public void loadB3()
    {
        string name = text3.text;
        if (name != "No datas")
        {
            canvas2.SetActive(false);
            Datas datas = (Datas)DataManager.Load("Slot3.sav");
            DatasNames dtn = (DatasNames)DataManager.LoadNames("names.sav");
            if (datas != null)
            {
                datas.i = 1;
                dtn.n = 3;
                datas.j = 1;
                dtn.m = 3;
                datas.name = name;
                DataManager.Save(dtn, "names.sav");
                DataManager.Save(datas, "Slot3.sav");
                SceneManager.LoadScene(datas.nameScene);
            }
        }
    }


    public void Quit()
    {
        Application.Quit();
    }
}
