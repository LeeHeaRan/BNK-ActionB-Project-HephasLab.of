using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle_anim : MonoBehaviour
{
    
    float toggleTimer = 0f;
    bool isOn;
    bool isMoving = false;
    public RectTransform Moveimg;
    private void Start()
    {
        isOn =  gameObject.GetComponent<Toggle>().isOn;
        Moveimg.anchoredPosition = (isOn)?new Vector3(gameObject.GetComponent<RectTransform>().sizeDelta.x - Moveimg.sizeDelta.x, 0, 0) : Vector3.zero;
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
    }

    private void ToggleAnim(bool isOn)
    {
     
        //Debug.Log(currentToggle.transform.parent.name);
        var toggleTime = .1f;
        if (isOn)
        {
            toggleTimer += Time.deltaTime;
            Moveimg.anchoredPosition =
            Vector3.Lerp(Vector3.zero, new Vector3(gameObject.GetComponent<RectTransform>().sizeDelta.x - Moveimg.sizeDelta.x, 0, 0), toggleTimer / toggleTime);
        }
        else if (!isOn)
        {
            toggleTimer += Time.deltaTime;
            Moveimg.anchoredPosition =
                Vector3.Lerp(new Vector3(gameObject.GetComponent<RectTransform>().sizeDelta.x - Moveimg.sizeDelta.x, 0, 0), Vector3.zero, toggleTimer / toggleTime);
        }

        if(toggleTime < toggleTimer)
        {
            isMoving = false;
        }

    }
}
