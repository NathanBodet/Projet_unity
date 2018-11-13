using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject canvas, canvas2;
    public Text text1;

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
        }
    }

    public void loadB1()
    {
        string name = text1.text;
        if (name != "No datas")
        {
            canvas2.SetActive(false);
            Datas datas = (Datas)DataManager.Load(name + ".sav");
            if (datas != null)
            {
                datas.i = 1;
                datas.name = name;
                DataManager.Save(datas, name + ".sav");
                SceneManager.LoadScene(datas.nameScene);
            }
        }
    }


    public void Quit()
    {
        Application.Quit();
    }
}
