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
        Vector3 pos = new Vector3(transform.parent.position.x - 0.2f, transform.parent.position.y + 1.2f, transform.parent.position.z);
        dialogBox.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        dialogBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Vu !");
            ShowDialog(linesEnter);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Mais où est-il ?!");
            ShowDialog(linesQuit);
        }
    }

    private IEnumerator ShowDialog(string lines)
    {
        Debug.Log("Coucou le dialogue de merde");
        dialogBox.SetActive(true);
        dialogBox.GetComponentInChildren<Text>().text = lines;
        yield return new WaitForSeconds(10);
        dialogBox.SetActive(false);
    }


}
