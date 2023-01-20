using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubToggle : MonoBehaviour
{
    public UI_Control P_Area;
    public Toggle toggleHead;
    public GameObject under_toggle;
    public GameObject group;
    public GameObject OnOffImage;
    public GameObject MovingObject;
    public Toggle[] toggles;
    
    bool isALLbtn = false;
    bool isSubCon = false;


    Vector2 under_pos;
    float OpenValue;
    float CloseValue;
    bool isOnOff;
    bool isOut = false;
    private void Start()
    {
        isOnOff = true;
        CloseValue = MovingObject.GetComponent<RectTransform>().anchoredPosition.y;
        OpenValue = CloseValue + 285;
        under_pos = under_toggle.GetComponent<RectTransform>().anchoredPosition;
    }
    public void ALL_AciveToggles(bool isOn)
    {
        if (!isSubCon)
        {
            isALLbtn = isOn;
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn != isOn) toggles[i].isOn = isOn;
            }
        }
        else
        {
            isSubCon = false;
        }
    }
    public void ToggleChange(bool isOn)
    {
        if (!isALLbtn)
        {
            bool tempCheck = true;
            for (int i = 0; i < toggles.Length; i++)
            {
                if (!toggles[i].isOn) tempCheck = false;
            }

            if (tempCheck)
            {
                toggleHead.isOn = true;
               
            }
        }
        else
        {
            if (!isOn)
            {
                isSubCon = true;
                isALLbtn = false;
                toggleHead.isOn = false;
            }
        }
       
        
    }
    public void GroupOnOff()
    {
        if (!isOnOff)
        {

            isOnOff = true;
            if (gameObject.GetComponent<iTween>() != null)
            {
                iTween.Stop(gameObject);
            }
            group.SetActive(false);
            OnOffImage.GetComponent<Transform>().localScale = new Vector3(1, -1, 1);
            under_toggle.GetComponent<RectTransform>().anchoredPosition = under_pos;

            Hashtable ht = new Hashtable();
            ht.Add("name", "on");
            ht.Add("from", MovingObject.GetComponent<RectTransform>().anchoredPosition.y);
            ht.Add("to", CloseValue);
            ht.Add("easeType", iTween.EaseType.easeInOutCubic);
            ht.Add("loopType", "once");
            ht.Add("time", 0.2f);
            ht.Add("onupdatetarget", gameObject);
            ht.Add("onupdate", "SetPos");
            ht.Add("oncompletetarget", gameObject);
            ht.Add("oncomplete", "OnOffComplete");
            iTween.ValueTo(gameObject, ht);

        }
        else
        {

            isOnOff = false;
            if (gameObject.GetComponent<iTween>() != null)
            {
                iTween.Stop(gameObject);
            }
            
            Hashtable ht = new Hashtable();
            ht.Add("name", "on");
            ht.Add("from", MovingObject.GetComponent<RectTransform>().anchoredPosition.y);
            ht.Add("to", OpenValue);
            ht.Add("easeType", iTween.EaseType.easeInOutCubic);
            ht.Add("loopType", "once");
            ht.Add("time", 0.2f);
            ht.Add("onupdatetarget", gameObject);
            ht.Add("onupdate", "SetPos");
            ht.Add("oncompletetarget", gameObject);
            ht.Add("oncomplete", "OnOffComplete");
            iTween.ValueTo(gameObject, ht);

        }
    }

    public void SetPos(float value)
    {
        MovingObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, value);
    }
    public void OnOffComplete()
    {
        if (!isOnOff)
        {

            group.SetActive(true);
            OnOffImage.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            under_toggle.GetComponent<RectTransform>().anchoredPosition = new Vector2(under_pos.x, -336);
        }

        if (isOut)
        {
            P_Area.CloseUI();
            isOut = false;

        }
    }
    public void GroupClose()
    {
        if (!isOnOff)
        {
            GroupOnOff();
            isOut = true;
        }
        else
        {
            P_Area.CloseUI();
        }
       
    }
    public void SubmitClose()
    {
        if (MovingObject.GetComponent<toggle_manager>().GetTogglesALLOn())
        {
            if (!isOnOff)
            {
                GroupOnOff();
                isOut = true;
            }
            else
            {
                P_Area.CloseUI();
            }
        }
       
    }



}
