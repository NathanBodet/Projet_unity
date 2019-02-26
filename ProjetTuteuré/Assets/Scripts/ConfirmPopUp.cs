using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConfirmPopUp : MonoBehaviour
{
    public static ConfirmPopUp instance;

    public Text message;
    public Button yesButton, noButton;

    public int result;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        result = -1;
        gameObject.SetActive(false);
    }

    public void OpenPopupConfirm(string texte)
    {
        gameObject.SetActive(true);
        message.text = texte;
        yesButton.GetComponentInChildren<Text>().text = "Yes";
        noButton.gameObject.SetActive(true);
        result = -1;
    }

    public void OpenPopupSimple(string texte)
    {
        gameObject.SetActive(true);
        message.text = texte;
        yesButton.GetComponentInChildren<Text>().text = "Understood";
        noButton.gameObject.SetActive(false);
        result = -1;
    }

    private void ClosePopup()
    {
        gameObject.SetActive(false);
    }

    public void YesPressed()
    {
        result = 1;
        ClosePopup();
    }

    public void NoPressed()
    {
        result = 0;
        ClosePopup();
    }
}
