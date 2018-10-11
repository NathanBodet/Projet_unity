using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusJeu : MonoBehaviour
    {

        public bool showGUI = false;
        public bool showGUI2 = false;
        public GameObject canvas, canvas2;
      
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && showGUI2 == false)
            {
                showGUI = !showGUI;
            }

            if (Input.GetKeyDown(KeyCode.I) && showGUI == false)
            {
                showGUI2 = !showGUI2;
            }

            if (showGUI && !showGUI2)
            {
                canvas.SetActive(true);
                Time.timeScale = 0;
            }
            else if (!showGUI && showGUI2)
            {
                canvas2.SetActive(true);
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
            Time.timeScale = 1;
        }


        public void Quit()
        {
            SceneManager.LoadScene("MainMenu");
        }
}
