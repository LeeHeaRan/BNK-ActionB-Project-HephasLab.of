using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{

 
    public Text AlertText;
    public float setTime;
    public float deltaTime;
    float initSetTime;
    bool isStart = false;
    bool isNoneClose = false;

    // Update is called once per frame
    private void Start()
    {
        initSetTime = setTime;
    }
    void Update()
    {
        if (isStart)
        {

            if (deltaTime >= setTime)
            {
                gameObject.GetComponent<UI_Control>().CloseUI();
                isStart = false;
                setTime = initSetTime;
            }
            else
            {
                deltaTime += Time.deltaTime;
            }
        }
    }

    public void ShowText(string text)
    {
        isStart = true;
        AlertText.text = text;
    }
    public void ShowText(float time,string text)
    {
        setTime = time;
        isStart = true;
        AlertText.text = text;
    }
    public void ShowText(string text,bool isNoneClose)
    {
        AlertText.text = text;
        this.isNoneClose = isNoneClose;
    }
    public void CloseText()
    {
        if (isNoneClose)
        {
            gameObject.GetComponent<UI_Control>().CloseUI();
            isNoneClose = false;
        }
        
    }

    private void OnDisable()
    {
        isNoneClose = false;
        isStart = false;
        deltaTime = 0;
    }
    
}
