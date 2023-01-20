using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeeplinkCheck : MonoBehaviour
{

    public DeeplinkCheck Instance { get; private set; }
    public string deeplinkURL;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Application.deepLinkActivated += Application_deepLinkActivated;
            if (!String.IsNullOrEmpty(Application.absoluteURL))
            {
                Debug.Log("딥링크확인");
                Application_deepLinkActivated(Application.absoluteURL);
            }
            // Initialize DeepLink Manager global variable.
            else deeplinkURL = "[none]";

        }
        else
        {
            this.enabled = false;
            Debug.Log("딥링크확인 없음");
        }
    }
    //busanbank://actionb?scene1
    private void Application_deepLinkActivated(string url)
    {
        Debug.Log("딥링크확인 url"+ url);
        deeplinkURL = url;
        string BRNO = "null";
        string ENOB = "null";
        if (deeplinkURL.Contains("?"))
        {
           string[] reciveValue = url.Split("?"[0]);

            for (int i = 0; i < reciveValue.Length; i++)
            {
                if (reciveValue[i].Contains("BRNO"))
                {
                    BRNO = reciveValue[i].Split("="[0])[1];
                }
                else if (reciveValue[i].Contains("ENOB"))
                {
                    ENOB = reciveValue[i].Split("="[0])[1];
                }
            }
         
        }
        UIManager uiManager = gameObject.GetComponent<UIManager>();

        Debug.Log("매개변수저장   BRNO=" + BRNO + "ENOB" + ENOB);
        uiManager.SetDeeplinkinfo(BRNO,ENOB);

        if (uiManager.isApp_running)
        {
            uiManager.DeepLinkMemuMove();
        }
        
    }


}
