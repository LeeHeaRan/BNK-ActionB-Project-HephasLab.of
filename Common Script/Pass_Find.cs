using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pass_Find : MonoBehaviour
{
    public Login_Step login_Step;
    public GameObject CustomEmail;
    public JoinID joinIDManger;
    public GameObject EmailStep_Btn;
    public GameObject id_input;
    public Text SelectDominText;
    public GameObject id_info;
    
    string EmaildoMain = "naver.com";
    public GameObject NextKCBInput;
    public GameObject SelectBox;

    [Header("KCB")]
    public GameObject carrier_area;
    public GameObject kcb_control;
    public Text Certifocation;
    public InputField PhoneNum;
    public GameObject Request_Page;
    
    public string email;
    string CI_VAL;


    [Header("Password_input")]
    public GameObject PassInputGroup;
    bool isRePassword_Step = false;
    public GameObject Password_input;
    public GameObject rePassword_input;
    public GameObject Password_Sumit;


   

    void Update()
    {

       
    }
    public void BackProcess()
    {
        Debug.Log("PassFind BackPress");
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
        else if (NextKCBInput.activeSelf)
        {
            PhoneNum.GetComponent<InputField>().text = "";
            NextKCBInput.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);

        }
    }


    public void NextStep()
    {
        NextKCBInput.SetActive(true);
        carrier_area.SetActive(true);
        if (CustomEmail.activeSelf)
        {
            email = id_input.GetComponent<InputField>().text + "@" + CustomEmail.GetComponent<InputField>().text;
        }
        else
        {
            email = id_input.GetComponent<InputField>().text + SelectDominText.text;
        }
    }

    public void ID_validation(GameObject target)
    {
        string id = target.GetComponent<InputField>().text;
        if (!joinIDManger.IDCheck())
        {
            if (id.Length != 0)
            {
                joinIDManger.ID_notCharFail();
                email = "";
                EmailStep_Btn.GetComponent<ButtonTransition>().Disabled_btn();
            }
            else
            {
                email = "";
                target.GetComponent<InputField_Status>().GetBackSprite();
                EmailStep_Btn.GetComponent<ButtonTransition>().Disabled_btn();
            }

        }
        else
        {
            if (CustomEmail.activeSelf)
            {
                UserEmail_Custom_Validation(CustomEmail.GetComponent<InputField>().text);
            }
            else
            {
                target.GetComponent<InputField_Status>().SetPassChangeSprite();
                EmailStep_Btn.GetComponent<ButtonTransition>().Enble_btn();
            }
            
        }
    }

    public void UserEmail_Select(string domain)
    {
        EmaildoMain = domain;
        EmailStep_Btn.GetComponent<ButtonTransition>().Disabled_btn();
        id_input.GetComponent<InputField_Status>().GetBackSprite();
        if (CustomEmail.activeSelf)
        {
            CustomEmail.GetComponent<InputField_Status>().GetBackSprite(true);

            CustomEmail.GetComponent<InputField>().text = "";
            CustomEmail.SetActive(false);
        }
        else
        {
            id_info.GetComponent<RectTransform>().anchoredPosition = new Vector2(id_info.GetComponent<RectTransform>().anchoredPosition.x, -50f);
            ID_validation(id_input);
        }
 


    }

    public void UserEmail_onCustom(GameObject selectBox)
    {
        id_input.GetComponent<InputField_Status>().GetBackSprite();
        EmailStep_Btn.GetComponent<ButtonTransition>().Disabled_btn();
        CustomEmail.SetActive(true);
        selectBox.SetActive(false);
        EmaildoMain = "";
         CustomEmail.GetComponent<InputField_Status>().infoNewMove(-144f);
       // id_info.GetComponent<RectTransform>().anchoredPosition = new Vector2(id_info.GetComponent<RectTransform>().anchoredPosition.x, -144f);
            

    }
    public void UserEmail_validation(GameObject target)
    {
        if (!Validation.EmailCheck(target.GetComponent<InputField>().text))
        {
            joinIDManger.Email_notCharFail();
        }
        else
        {
            UserEmail_validation("@" + joinIDManger.custom_edomain.GetComponent<InputField>().text);
        }
    }
    public void UserEmail_validation(string dmain)
    {


        if (joinIDManger.IDCheck())
        {
            Debug.Log(joinIDManger.IDCheck() + "dd");

        }
        else
        {
            Debug.Log(joinIDManger.IDCheck() + "실패");
        }

    }
    public void UserEmail_Custom_Validation(string dmain)
    {
        CustomEmail.GetComponent<InputField_Status>().infoNewMove(-144f);
        if (!Validation.EmailCheck(dmain))
        {
            CustomEmail.GetComponent<InputField_Status>().SetFailChangeSprite("이메일 형식을 확인해주세요.");
            EmailStep_Btn.GetComponent<ButtonTransition>().Disabled_btn();
        }
        else{
            if (Validation.ID_Check(id_input.GetComponent<InputField>().text))
            {
                CustomEmail.GetComponent<InputField_Status>().SetPassChangeSprite();
                id_input.GetComponent<InputField_Status>().SetPassChangeSprite();
                EmailStep_Btn.GetComponent<ButtonTransition>().Enble_btn();
            }
            else
            {
              
                CustomEmail.GetComponent<InputField_Status>().SetPassChangeSprite();
                id_input.GetComponent<InputField_Status>().SetFailChangeSprite("아이디 형식을 확인해주세요.");
                EmailStep_Btn.GetComponent<ButtonTransition>().Disabled_btn();
            }
            
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
            kcb_control.GetComponent<KCB_Control>().SetSendTarget(gameObject,email, KCB_Control.USE_TYPE.PASSWORD_FIND);
            kcb_control.GetComponent<KCB_Control>().ID_Password_Find(PhoneNum.text, Certifocation.text);

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



    }
    public void Send_CI_VAL(string ci_val)
    {
        CI_VAL = ci_val;
    }





    public void Password_InputMove(GameObject target)
    {
        

        string tempstr = Password_input.GetComponent<InputField>().text;
        if (tempstr.Length >= 8 && Validation.Password_Check(Password_input.GetComponent<InputField>().text))
        {
            if (PassInputGroup.GetComponent<iTween>() != null) iTween.Stop(PassInputGroup);

            Hashtable ht = new Hashtable();
            ht.Add("from", PassInputGroup.GetComponent<RectTransform>().anchoredPosition.y);
            ht.Add("to", 434);
            ht.Add("easeType", iTween.EaseType.easeInOutCirc);
            ht.Add("loopType", "once");
            ht.Add("time", 0.2f);
            ht.Add("onupdatetarget", gameObject);
            ht.Add("onupdate", "SetPosNext");

            iTween.ValueTo(PassInputGroup, ht);

            isRePassword_Step = true;

            target.SetActive(true);

            target.GetComponentInChildren<InputField>().ActivateInputField();

        }
    }
    public void rePassword_InputMove(GameObject target)
    {

        if (isRePassword_Step)
        {
       

            if (PassInputGroup.GetComponent<iTween>() != null) iTween.Stop(PassInputGroup);

            Hashtable ht = new Hashtable();
            ht.Add("from", PassInputGroup.GetComponent<RectTransform>().anchoredPosition.y);
            ht.Add("to", 634);
            ht.Add("easeType", iTween.EaseType.easeInOutCirc);
            ht.Add("loopType", "once");
            ht.Add("time", 0.2f);
            ht.Add("onupdatetarget", gameObject);
            ht.Add("onupdate", "SetPosNext");

            iTween.ValueTo(PassInputGroup, ht);

            isRePassword_Step = false;
            Password_input.GetComponentInChildren<InputField>().ActivateInputField();
            rePassword_input.GetComponent<InputField>().text = "";
            rePassword_input.SetActive(false);
        }

        
    }
    void SetPosNext(float value)
    {
        PassInputGroup.GetComponent<RectTransform>().anchoredPosition = new Vector2(PassInputGroup.GetComponent<RectTransform>().anchoredPosition.x, value);
        
    }

    public void Password_validation(string value)
    {
        if (rePassword_input.GetComponent<InputField>().text.Length > 0)
        {

            rePassword_input.GetComponent<InputField>().text = "";
            rePassword_input.GetComponent<InputField_Status>().GetBackSprite();
          
        }
        if (value.Length >= 8)
        {
            if (Validation.Password_Check(value))
            {
                Password_input.GetComponent<InputField_Status>().SetPassChangeSprite();


            }
            else
            {

                if (Validation.isKorean(value))
                {
                    Password_input.GetComponent<InputField_Status>().SetFailChangeSprite("비밀번호에 한글을 포함할 수 없습니다.");
                }
                else if (Validation.SequenceCharCheck(value))
                {
                    Password_input.GetComponent<InputField_Status>().SetFailChangeSprite("동일한 문자를 3글자이상 연속해서 사용할 수 없습니다.");
                }
                else if (value.Contains(" "))
                {
                    Password_input.GetComponent<InputField_Status>().SetFailChangeSprite("비밀번호에 띄어쓰기는 포함할 수 없습니다.");

                }
                else if (!Validation.Password_combineCheck(value))
                {
                    Password_input.GetComponent<InputField_Status>().SetFailChangeSprite("영문,숫자 포함하여 입력해주세요");
                }
            }
        }
        else
        {
            Password_input.GetComponent<InputField_Status>().SetFailChangeSprite("영문,숫자 포함 8자 이상으로 입력해주세요");
        }
    }

    public void RePassword_Check(string value)
    {
        if (value.Length > 0)
        {
            if (Password_input.GetComponent<InputField>().text.Equals(value))
            {
                rePassword_input.GetComponent<InputField_Status>().SetPassChangeSprite("비밀번호가 일치합니다.");
                Password_Sumit.GetComponent<ButtonTransition>().Enble_btn();

            }
            else
            {
          
                rePassword_input.GetComponent<InputField_Status>().SetFailChangeSprite("비밀번호가 일치하지 않습니다.");
                Password_Sumit.GetComponent<ButtonTransition>().Disabled_btn();
            }
        }
        else
        {
           
            Password_Sumit.GetComponent<ButtonTransition>().Disabled_btn();
        }
    }






    public void PasswordChangeSubmit()
    {
        Debug.Log("CI_VAL:"+ CI_VAL+"   ARA_MBRS_EMLADRL:"+ email + "   ARA_MBRS_PSWD:"+ Password_input.GetComponent<InputField>().text + "   (re)ARA_MBRS_PSWD:" + rePassword_input.GetComponent<InputField>().text);

        List<string> key = new List<string>();
        List<string> value = new List<string>();

        key.Add("ARA_MBRS_CI_VAL");
        key.Add("ARA_MBRS_EMLADR");
        key.Add("ARA_MBRS_PSWD");

        value.Add(CI_VAL);
        value.Add(email);
        value.Add(Password_input.GetComponent<InputField>().text);

        login_Step.unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.MemberPasswordChange, URLs.Menu.Member, key, value, PasswordChangeCallback);


    }

    public void PasswordChangeCallback(string req, string resCode, List<Dictionary<string, string>> data)
    {

        string contents = "";
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

                //   test.SetLog(pair.Key + "[" + i + "] : " + pair.Value + "[" + i + "]\n");
                contents += pair.Key + "[" + i + "] : " + pair.Value + "[" + i + "] ";
            }
        }


        if (req.Equals("fail") || resCode.Equals("99"))
        {
            if (req.Equals("NetWork error"))
            {
                //실패
            }
            else
            {

                //실패
                login_Step.alert_Manager.ShowAlert("비밀번호 변경에 실패하였습니다.");
                Request_Page.GetComponent<Active_control>().UnActive(gameObject);
                PassFind_KCBInput_Init();
                email = "";
                CI_VAL = "";
                UserEmail_Select("@naver.com");
            }

        }
        else if (req.Equals("Sucess") && resCode.Equals("00"))
        {
            if (data[0].ContainsKey("ARA_REQ_DVCD"))
            {
                if (data[0]["ARA_REQ_DVCD"].Equals("0"))
                {
                    //성공
                    Request_Page.GetComponent<Active_control>().UnActive(gameObject);
                    PassFind_KCBInput_Init();
                     email="";
                     CI_VAL="";
                     UserEmail_Select("@naver.com");
                   


                }
                else
                {
                    //실패
                    login_Step.alert_Manager.ShowAlert("비밀번호 변경에 실패하였습니다.");
                }
            }
            else
            {
                //실패
                login_Step.alert_Manager.ShowAlert("[서버오류] 비밀번호 변경에 실패하였습니다.");
            }
        }
    }
    public void PassFind_KCBInput_Init()
    {
        id_input.GetComponent<InputField>().text = "";
        id_input.GetComponent<InputField_Status>().GetBackSprite();
        if (CustomEmail.activeSelf)
        {
            CustomEmail.GetComponent<InputField>().text = "";
            CustomEmail.GetComponent<InputField_Status>().GetBackSprite();
            CustomEmail.SetActive(false);
        }
        SelectDominText.text = "@naver.com";
        SelectBox.SetActive(true);
        SelectBox.GetComponent<ToogleAction>().init();
        SelectBox.SetActive(false);
        Certifocation.text = "SKT";
        PhoneNum.GetComponent<InputField>().text = "";
        NextKCBInput.SetActive(false);
        email  = "";
        CI_VAL = "";
    
    }


    private void OnEnable()
    {
        PassFind_KCBInput_Init();
        UserEmail_Select("naver.com");
        EmailStep_Btn.GetComponent<ButtonTransition>().Disabled_btn();

    }
}

