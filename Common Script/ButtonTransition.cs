using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTransition : MonoBehaviour
{

    [Header("Common")]
    public Text ButtonText;
    public bool isInitDisabled = false;
    public bool RunTimeOnlyControl = true;

    [Header("Enable")]
    public Sprite Enable_Image;
    public Color Enable_imageColor;
    public Color Enable_TextColor;


    [Header("Disabled")]
    public Sprite Disabled_Image;
    public Color Disabled_imageColor;
    public Color Disabled_TextColor;

    

    public void Enble_btn()
    {
        gameObject.GetComponent<Image>().sprite = Enable_Image;
        gameObject.GetComponent<Image>().color = Enable_imageColor;
        ButtonText.color = Enable_TextColor;
        if (RunTimeOnlyControl) gameObject.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
        else gameObject.GetComponent<Button>().interactable = true;
    }
    public void Disabled_btn()
    {
        gameObject.GetComponent<Image>().sprite = Disabled_Image;
        gameObject.GetComponent<Image>().color = Disabled_imageColor;
        ButtonText.color = Disabled_TextColor;
        if (RunTimeOnlyControl) gameObject.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        else gameObject.GetComponent<Button>().interactable = false;

    }
 

    private void OnDisable()
    {
        if (isInitDisabled)
        {
            gameObject.GetComponent<Image>().sprite = Disabled_Image;
            gameObject.GetComponent<Image>().color = Disabled_imageColor;
            ButtonText.color = Disabled_TextColor;
            if (RunTimeOnlyControl) gameObject.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            else gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Enable_Image;
            gameObject.GetComponent<Image>().color = Enable_imageColor;
            ButtonText.color = Enable_TextColor;
            if (RunTimeOnlyControl) gameObject.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
            else gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
