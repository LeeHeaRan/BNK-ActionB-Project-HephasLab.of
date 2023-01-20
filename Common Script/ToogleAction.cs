using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToogleAction : MonoBehaviour
{
    public Email_JoinStep email_JoinStep;
    public Parent_JoinStep parent_JoinStep;
    public GameObject connect_InputFleid_Label;
    public string append_str = "";
    public bool isSelectBoxEnable=false;
    public GameObject CallBackObejct;
    public string ChangeCallBack;
    public GameObject Arror;
  

    Toggle ActiveToggle;
    Toggle NowToggle; 
    
    public void Start()
    {
        ActiveToggle = gameObject.GetComponentsInChildren<Toggle>()[0];
        NowToggle = ActiveToggle;
    }

    public void ToggleValueChanged(Toggle change)
    {
        if (change.isOn)
        {
            gameObject.GetComponent<ToggleGroup>().allowSwitchOff = false;
            Text[] texts = change.gameObject.GetComponentsInChildren<Text>();
            string text = "";
            for (int i = 0; i < texts.Length; i++)
            {

                texts[i].color = (change.isOn) ? new Color(0.6235294f, 0.2627451f, 1f) : new Color(0, 0, 0);
                text += texts[i].text + " ";
                NowToggle = change;
            }

            if (change.isOn)
            {
                connect_InputFleid_Label.GetComponent<Text>().text = (append_str + text).Trim();
            }

            if (isSelectBoxEnable)
            {
                gameObject.SetActive(false);

            }
            if (ChangeCallBack != "")
            {
                CallBackObejct.SendMessage(ChangeCallBack, (append_str + text).Trim());
            }
        }
        else
        {
            Text[] texts = change.gameObject.GetComponentsInChildren<Text>();
           
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = (change.isOn) ? new Color(0.6235294f, 0.2627451f, 1f) : new Color(0, 0, 0);
                
            
            }

        }
    }
    public void OtherBtn(string setText)
    {
        gameObject.GetComponent<ToggleGroup>().allowSwitchOff = true;
        NowToggle.isOn = false;

        connect_InputFleid_Label.GetComponent<Text>().text = setText;


    }
    public void Close()
    {
        ActiveToggle.isOn = true;

    }
    public void ToggleInit()
    {
        ActiveToggle = gameObject.GetComponentsInChildren<Toggle>()[0];
        ActiveToggle.isOn = true;
    }
    
    public void SelectBtn()
    {
        ActiveToggle = NowToggle;
        if ( email_JoinStep != null) {
            email_JoinStep.SetMobileCarrier(connect_InputFleid_Label.GetComponent<Text>().text);
        }
        else if(parent_JoinStep !=null)
        {
            parent_JoinStep.SetMobileCarrier(connect_InputFleid_Label.GetComponent<Text>().text);

        }

    }
    public void init()
    {
        gameObject.GetComponentsInChildren<Toggle>()[0].isOn = true ;
    }
    












    private void OnEnable()
    {
        if (Arror != null)
        {
            Arror.GetComponent<Transform>().localScale = new Vector3(1, -1, 1);
        }

    }
    private void OnDisable()
    {
        if (Arror != null)
        {
            Arror.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
    }

}
