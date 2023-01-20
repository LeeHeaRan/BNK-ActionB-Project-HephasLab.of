using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    public GameObject bar;
    public GameObject bar_icon;
    public Text text;
    float progressSize;
    // Start is called before the first frame update
    void Start()
    {
        progressSize = gameObject.GetComponent<RectTransform>().rect.width;
    
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void SetProgress(string s,float value) 
    {
        text.text = s;
        Vector2 temp = bar.GetComponent<RectTransform>().sizeDelta;
        Vector2 temp_icon = bar_icon.GetComponent<RectTransform>().anchoredPosition;
        bar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(progressSize * value, temp.y);
        bar_icon.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(progressSize * value, temp_icon.y);
    }
}
