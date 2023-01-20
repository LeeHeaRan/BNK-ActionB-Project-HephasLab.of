using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class _ColotControl_target
{


    
    public iTween.EaseType EaseType;
    
    public GameObject Tobject;
    public Color color;

    [Header("Start Value")]
    public float time;
    private Color m_StartColor;
    public Color startColor
    {
        get => m_StartColor;
        set => m_StartColor = value;
    }

    public GameObject callbackTarget;
    public string callbackFun;

    [Header("Return Value")]
    public float Returntime;
    public GameObject returnCallbackTarget;
    public string returnCallbackFun;
    


    public void StartColor()
    {
       
        startColor = Tobject.GetComponent<ValueToUpdate>().StartColor;
        Hashtable ht = new Hashtable();
        ht.Add("name", "in");
        ht.Add("from", m_StartColor);
        ht.Add("to", color);
        ht.Add("easeType", EaseType);
      
        ht.Add("time", time);
        ht.Add("onupdatetarget", Tobject);
        ht.Add("onupdate", "SetColor");


        if (callbackTarget != null)
        {
            ht.Add("oncompletetarget", callbackTarget);
            ht.Add("oncomplete", callbackFun);

        }
        iTween.ValueTo(Tobject, ht);
    }
    public void ReturnColor()
    {
        if (Tobject.GetComponent<iTween>() != null)
        {
            iTween.Stop(Tobject);
        }
        Hashtable ht = new Hashtable();
        ht.Add("from", Tobject.GetComponent<ValueToUpdate>().NowColor);
        ht.Add("to", m_StartColor);
        ht.Add("easeType", EaseType);
     
        ht.Add("time", Returntime);
        ht.Add("onupdatetarget", Tobject);
        ht.Add("onupdate", "SetColor");

        if (returnCallbackTarget != null)
        {
            ht.Add("oncompletetarget", returnCallbackTarget);
            ht.Add("oncomplete", returnCallbackFun);
        }
        iTween.ValueTo(Tobject, ht);
    }
    
}
