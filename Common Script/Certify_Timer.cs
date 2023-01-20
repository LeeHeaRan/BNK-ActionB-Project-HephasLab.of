using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Certify_Timer : MonoBehaviour
{
    public GameObject Timer;
    public GameObject info;
    public GameObject input;
    public Sprite fail_sprite;
    public Sprite normal_sprite;
    float MaxTime = 180f;
    float deltaTime = 0;
    bool isStart = false;
    bool isFail = false;
    bool isComplete = false;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            if(deltaTime >= MaxTime)
            {
                isStart = false;
                deltaTime = 0;
            }
            else
            {
                deltaTime+=Time.deltaTime;
                Timer.GetComponent<Text>().text = ((int)((MaxTime - deltaTime) / 60)).ToString("D2") + ":" + ((int)((MaxTime - deltaTime) % 60)).ToString("D2");

            }
        }
    }
    public bool isTimeOut()
    {
        return (deltaTime < MaxTime) ? true : false;
    }
    public bool Timer_Start()
    {
        if (!isComplete)
        {
            if (count < 4)
            {
                input.GetComponent<InputField>().text = "";
                input.GetComponent<Image>().sprite = normal_sprite;
                info.SetActive(false);
                isStart = true;
                deltaTime = 0;
                count += 1;

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
      
    }
 
    public void Timer_Stop()
    {
        isStart = false;
        info.SetActive(true);
        info.GetComponent<Text>().text = "인증번호가 일치합니다.";
        info.GetComponent<Text>().color = new Color(0.6235294f, 0.2627451f, 1);
        input.GetComponent<InputField>().readOnly = true;
        isComplete = true; 
    }

    public void Fail_Num()
    {
        
        info.SetActive(true);
        info.GetComponent<Text>().text = "인증번호가 일치하지 않습니다.";
      
        input.GetComponent<Image>().sprite = fail_sprite;
        
        info.GetComponent<Text>().color = new Color(1f, 0.2313726f, 0.1882353f);
        input.GetComponent<InputField>().Select();
    //    input.GetComponent<InputField>().caretColor = new Color(1f, 0f, 0f);
        input.GetComponent<InputField>().readOnly = false;
        isFail = true;
        isComplete = false;


    }
    public void input_NormalChange()
    {
        if (isFail)
        {
            input.GetComponent<Image>().sprite = normal_sprite;
            info.SetActive(false);
            isFail = false;
        }
    }
    public bool isCertifyComplete()
    {
        return isComplete;
    }
    private void OnEnable()
    {
       
        deltaTime = 0;
        count = 0;
        Timer_Start();

    }
    private void OnDisable()
    {
        input_NormalChange();
        input.GetComponent<InputField>().readOnly = false;
        isComplete = false;
        isStart = false;
        deltaTime = 0;
        count = 0;
    }
}
