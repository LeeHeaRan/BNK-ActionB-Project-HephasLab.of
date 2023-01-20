using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class _MoveControl_target
{

    public iTween.EaseType EaseType;
    public GameObject Tobject;
    public Vector3 AddPos;
    [Header("Start Value")]
    public float time;
    private Vector3 m_StartPos;
    public Vector3 startPos
    {
        get => m_StartPos;
        set => m_StartPos = value;
    }

    public GameObject callbackTarget;
    public string callbackFun;

    [Header("Return Value")]
    public float Returntime;
    public GameObject returnCallbackTarget;
    public string returnCallbackFun;
  
    public void StartMovePos()
    {
        startPos = Tobject.GetComponent<ValueToUpdate>().StartPos;

        Hashtable ht = new Hashtable();
        ht.Add("name", "in");
        ht.Add("from", m_StartPos);
        ht.Add("to", AddPos);
        ht.Add("easeType", EaseType);
    
        ht.Add("time", time);
        ht.Add("onupdatetarget", Tobject);
        ht.Add("onupdate", "SetPos");

        if (callbackTarget != null)
        {
            ht.Add("oncompletetarget", callbackTarget);
            ht.Add("oncomplete", callbackFun);
        }
        iTween.ValueTo(Tobject, ht);
    }
    public void ReturnMovePos()
    {
        if (Tobject.GetComponent<iTween>() != null)
        {
            iTween.Stop(Tobject,"in");
        }
        Hashtable ht = new Hashtable();
        ht.Add("from", Tobject.GetComponent<ValueToUpdate>().NowPos);
        ht.Add("to", m_StartPos);
        ht.Add("easeType", EaseType);
     
        ht.Add("time", Returntime);
        ht.Add("onupdatetarget", Tobject);
        ht.Add("onupdate", "SetPos");


        if (returnCallbackTarget != null)
        {
            ht.Add("oncompletetarget", returnCallbackTarget);
            ht.Add("oncomplete", returnCallbackFun);
        }
        iTween.ValueTo(Tobject, ht);
    }
}
