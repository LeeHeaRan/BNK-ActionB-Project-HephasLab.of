using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChangeMatch : MonoBehaviour
{

    float height;
    bool isSize = false;
    private void Start()
    {
        height = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Debug.Log(height);
    }
    private void Update()
    {
        if (isSize)
        {
           height = transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.y;
            if (height != 0)
            {
                isSize = false;
                gameObject.SetActive(false);
            }
        }
    }
    public void SetText(string str)
    {
        Text ContentsText = transform.GetChild(0).GetComponent<Text>();
        ContentsText.text = str;
        ContentsText.text = ContentsText.text.Replace(' ', '\u00A0');
        height = ContentsText.gameObject.GetComponent<RectTransform>().sizeDelta.y;
        isSize = true;



    }

    public float Getheight()
    {
        height = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Debug.Log(height);
        return height + 46f;
    }
   
}
