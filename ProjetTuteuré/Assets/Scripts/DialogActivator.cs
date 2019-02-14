using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class DialogActivator : MonoBehaviour
{
    public string linesEnter;
    public string linesQuit;
    public GameObject dialogBox;

    private void Start()
    {
        dialogBox.SetActive(false);
    }

    private void Update()
    {
        Vector3 pos = new Vector3(transform.parent.position.x - 0.2f, transform.parent.position.y + 1.2f, transform.parent.position.z);
        dialogBox.transform.position = Camera.main.WorldToScreenPoint(pos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(ShowDialogue(linesEnter));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(ShowDialogue(linesQuit));
        }
    }

    private IEnumerator ShowDialogue(string lines)
    {
        dialogBox.SetActive(true);
        dialogBox.GetComponentInChildren<Text>().text = lines.Replace("\\n", "\n");
        yield return new WaitForSeconds(3f);
        dialogBox.SetActive(false);
    }


}
