using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ExitLevel : MonoBehaviour
{
    public GameObject winIndicator;
    private bool aFini = false;
    public GameObject player;
    bool fini = false;

    //Win
    public AudioClip winClip;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (aFini)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().initieNiveau(true);
            player.transform.position = new Vector3(85.5f,-59f,0.0f);
            GameObject.Find("Main Camera").transform.position = new Vector3(85.5f, -59, -7);
            aFini = false;
            fini = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !fini)
        {
            GameObject.Find("GameAudioManager").GetComponent<AudioSource>().Stop();
            StartCoroutine("ShowWin");
            StartCoroutine("PlayFinalAudio");
            fini = true;

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
        
    }
    private IEnumerator PlayFinalAudio()
    {
        GameObject.Find("GameAudioManager").GetComponent<AudioSource>().PlayOneShot(winClip);
        yield return new WaitWhile(() => GameObject.Find("GameAudioManager").GetComponent<AudioSource>().isPlaying);
        aFini = true;
    }
}
