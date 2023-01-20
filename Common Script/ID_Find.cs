using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ID_Find : MonoBehaviour
{

    public GameObject kcb_control;
    public GameObject carrier_area;
    public Text Certifocation;
    public InputField PhoneNum;
    public GameObject Request_Page;
    public Text id_text;
    public bool isBack = false;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       
    }
    public void Send_CI_VAL(string s) { }
    public void BackProcess()
    {
        Debug.Log("IDFind BackPress");
        if (Request_Page.activeSelf)
        {
            Request_Page.GetComponent<Active_control>().UnActive();
        }
        else if (carrier_area.activeSelf)
        {
            carrier_area.GetComponent<UI_Control>().CloseUI();
        }
        else if (kcb_control.activeSelf)
        {
            kcb_control.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<UI_Control>().CloseUI();

        }
    }
 
    public void PhoneNumber_Validation(GameObject target)
    {
        string temp_phoneNum = target.GetComponent<InputField>().text;
        if (temp_phoneNum.Length != 11)
        {
            target.GetComponent<InputField_Status>().SetFailChangeSprite();
        }
        else if (!temp_phoneNum.Substring(0, 2).Equals("01"))
        {
            target.GetComponent<InputField_Status>().SetFailChangeSprite();
        }
        else
        {
            kcb_control.SetActive(true);
            kcb_control.GetComponent<KCB_Control>().SetSendTarget(gameObject,KCB_Control.USE_TYPE.ID_FIND);
            kcb_control.GetComponent<KCB_Control>().ID_Password_Find(PhoneNum.text, Certifocation.text);
          
        }
    }
    public void PhoneNumberLengthCheck(GameObject target)
    {
        string temp_phoneNum = target.GetComponent<InputField>().text;
        if (temp_phoneNum.Length == 11)
        {
            UIManager uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
            uiManager.Keyboard_out();
            PhoneNumber_Validation(target);
        }
    }


    public void KCB_SendData(string req)
    {
       
        Request_Page.SetActive(true);
        Request_Page.GetComponent<Active_control>().SetActive(1);
        //없음
    }
    public void KCB_SendData(List<Dictionary<string, string>> data)
    {
       
        Request_Page.SetActive(true);
        Request_Page.GetComponent<Active_control>().SetActive(0);
        string[] emailSqlit = data[0]["ARA_MBRS_EMLADR"].Split('@');
        int index=0;
        if (emailSqlit[0].Length > 4) index = emailSqlit[0].Length - 3;
        else if (emailSqlit[0].Length == 4) index = 2;
        else index = 1;

        string id = "";
        for (int i= 0; i< emailSqlit[0].Length; i++)
        {
            if (i >= index)
            {
                id += "*";
            }
            else
            {
                id += emailSqlit[0][i];
            }
        }

        string[] domainSqlit = emailSqlit[1].Split('.');
        string domain = "";
        for (int i = 0; i < domainSqlit[0].Length; i++)
        {
            if (i >= 2)
            {
                domain += "*";
            }
            else
            {
                domain += domainSqlit[0][i];
            }
        }

        id_text.text = id + "@" + domain+"."+ domainSqlit[1];
    }
    private void OnEnable()
    {
        carrier_area.SetActive(true);
    }
    private void OnDisable()
    {
        PhoneNum.text = "";
        Request_Page.SetActive(false);
    }
}
