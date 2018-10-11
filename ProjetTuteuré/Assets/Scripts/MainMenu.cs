using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void start()
    {
        SceneManager.LoadScene("Salle1");

    }

    public void load()
    {
        if (File.Exists(Application.persistentDataPath + "/Save1.sav"))
        {
            Datas datas = (Datas)DataManager.Load("Save1.sav");
            datas.i = 1;
            DataManager.Save(datas, "Save1.sav");
            SceneManager.LoadScene(datas.nameScene);
        }
    }


    public void Quit()
    {
        Application.Quit();
    }
}
