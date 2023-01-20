using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Alert_Manager : MonoBehaviour
{
    public GameObject Alert_Canvas;
    public Text Title;
    public Text contents;
    private GameObject dd;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ShowAlert(string text)
    {
        Alert_Canvas.SetActive(true);
        contents.text = text;
    }
    public void ShowAlert(string title,string text)
    {
        Alert_Canvas.SetActive(true);
        Title.text = title;
        contents.text = text;
    }

    private void OnDisable()
    {
        try
        {
            Title.text = "잠깐!";
            contents.text = "";
        }catch
        {

        }
    }

  
}


