using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ExitLevel : MonoBehaviour
{
    public GameObject winIndicator;
    private bool aFini = false;

    //Win
    public AudioSource audioSource;
    public AudioClip winClip;

    private void Update()
    {
        if (aFini)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.Find("GameAudioManager").GetComponent<AudioSource>().Stop();
            audioSource.PlayOneShot(winClip);
            StartCoroutine("ShowWin");
          
        }
    }

    private IEnumerator ShowWin()
    {
        for(int i = 0; i < 4; i++)
        {
            winIndicator.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            winIndicator.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
        aFini = true;
    }
}
