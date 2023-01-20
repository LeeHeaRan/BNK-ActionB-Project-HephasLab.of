using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARToggle : MonoBehaviour
{
    float toggleTimer = 0f;
    bool isOn;
    bool isMoving = false;
    public RectTransform Moveimg;
    public GameObject toggleBG;

    private void Start()
    {
        isOn = gameObject.GetComponent<Toggle>().isOn;
        Moveimg.anchoredPosition = (isOn) ? new Vector3(gameObject.GetComponent<RectTransform>().sizeDelta.x - Moveimg.sizeDelta.x, 0, 0) : Vector3.zero; //초기화 
    }
    private void Update()
    {
        if (isMoving)
        {
            ToggleAnim(isOn);
        }
    }
    public void ToggleClick(bool isOn)
    {
        toggleTimer = 0;
        this.isOn = isOn;
        isMoving = true;

        if (!this.isOn) //꺼질때
        {
            if (toggleBG.GetComponent<iTween>() != null) //기존에 실행되고 있는 iTween이 있다면 중지시키고 
            {
                iTween.Stop(toggleBG);
            }
            iTween.ValueTo(toggleBG, iTween.Hash("from", toggleBG.GetComponent<RectTransform>().anchoredPosition.x, "to", -toggleBG.GetComponent<RectTransform>().rect.width + this.gameObject.GetComponent<RectTransform>().rect.width, "onupdate", "updateToggleBG", "onupdatetarget", this.gameObject, "time", 1.2f, "easetype", iTween.EaseType.easeOutQuart));
        }
        else //켜질때
        {
            if (toggleBG.GetComponent<iTween>() != null)
            {
                iTween.Stop(toggleBG);
            }
            iTween.ValueTo(toggleBG, iTween.Hash("from", toggleBG.GetComponent<RectTransform>().anchoredPosition.x, "to", 0f, "onupdate", "updateToggleBG", "onupdatetarget", this.gameObject, "time", 1.2f, "easetype", iTween.EaseType.easeOutQuart));
        }
    }

    private void ToggleAnim(bool isOn)
    {
        var toggleTime = .1f;
        if (isOn) //꺼질때 //시간동안 여러번 실행됨.
        {
            toggleTimer += Time.deltaTime;
            Moveimg.anchoredPosition =
            Vector3.Lerp(Vector3.zero, new Vector3(gameObject.GetComponent<RectTransform>().sizeDelta.x - Moveimg.sizeDelta.x, 0, 0), toggleTimer / toggleTime);
        }
        else if (!isOn) //켜질때
        {
            toggleTimer += Time.deltaTime;
            Moveimg.anchoredPosition =
            Vector3.Lerp(new Vector3(gameObject.GetComponent<RectTransform>().sizeDelta.x - Moveimg.sizeDelta.x, 0, 0), Vector3.zero, toggleTimer / toggleTime);
        }

        if (toggleTime < toggleTimer)
        {
            isMoving = false;
        }
    }

    void updateToggleBG(float val)
    {
        toggleBG.GetComponent<RectTransform>().anchoredPosition = new Vector3(val, 0f, 0f);
    }


    private void OnDisable() //초기화
    {
        toggleBG.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f, 0f); //BG 원래위치로
        Moveimg.anchoredPosition = Vector3.zero;
        gameObject.GetComponent<Toggle>().isOn = true;
    }
}
