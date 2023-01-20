using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectBoxSetPos : MonoBehaviour
{

    public GameObject target;
    public GameObject stepOutCheck;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                    gameObject.SetActive(false);
                }
            }
        }

       
    }

    public void OnEnable()
    {
        Vector2 stepOutCheckPos = stepOutCheck.GetComponent<RectTransform>().anchoredPosition;
        if (stepOutCheckPos.Equals(Vector2.zero))
        {
            Vector2 pos = target.GetComponent<RectTransform>().anchoredPosition;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, pos.y + 45);
        }
        else
        {
            Vector2 pos = target.GetComponent<RectTransform>().anchoredPosition;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, pos.y+ stepOutCheckPos.y + 508);//508
        }

    }
    
}
