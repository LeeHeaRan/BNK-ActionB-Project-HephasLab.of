using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parent_JoinStep : MonoBehaviour
{



    public enum STEP_TYPE
    {

        Name_Field,
        Reg_Num_Field,
        Mobile_Carrier_Field,
        Mobile_Number_Field,
        Mobile_Certify_Field


    }
    public Email_JoinStep Orig_step;
    public Alert_Manager alert_Manager;
    public UnityWeb unityWeb;
    public GameObject Alert;
    public UIManager uiManager;
    [Header("member_info_area")]
    public _Member_info member_info = new _Member_info();

    [Header("------------------------------------------")]
    public STEP_TYPE step_type = STEP_TYPE.Name_Field;
    public GameObject FieldGroup;
    public iTween.EaseType easeType;
    public float MoveTime = 0;
    public Vector2 targetStartPos;
    public Vector2 targetNowPos;
    public Vector2 NextTargetStartPos;
    public Vector2 NextTargetNowPos;

    [Header("Mobile")]
    public GameObject Mobile_SelectBox;
    public GameObject Mobile_Carrier_Label;
    public GameObject Mobile_Certification;
    public GameObject Mobile_Certify;

    [Header("Target Control")]
    public GameObject StepObject;

    public GameObject reg_before_Focus;
    public GameObject reg_next_Focus;
    public Text reg_ShowText_Font;
    public Text reg_ShowText_Back;

    [Header("SubmitButton")]
    public GameObject KCBBtn;

    bool isAdult = false;
    string tras_seq_value = "";
    // Start is called before the first frame update
    void Start()
    {
        GameObject TempUiManager = GameObject.FindGameObjectWithTag("UIManager");
        uiManager = TempUiManager.GetComponent<UIManager>();
        alert_Manager = TempUiManager.GetComponent<Alert_Manager>();
        unityWeb = TempUiManager.GetComponent<UnityWeb>();
       
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackProcess();
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackProcess();
            }
        }
    }
    void SetPosField(float value)
    {
        FieldGroup.GetComponent<RectTransform>().anchoredPosition = new Vector2(targetNowPos.x, value);
        targetNowPos = FieldGroup.GetComponent<RectTransform>().anchoredPosition;
    }

    public void BackProcess()
    {
        Hashtable ht;
        GameObject BeforeTarget;
        GameObject NowTarget;

        switch (step_type)
        {
            case STEP_TYPE.Name_Field:

                if (!StepObject.GetComponent<UI_Control>().isClose)
                {
                    Orig_step.Parent_ClosenField();
                    StepObject.GetComponent<UI_Control>().CloseUI();
                    //  FieldGroup.GetComponentInChildren<UI_Control>().CloseUI();


                    member_info.DelValue(_Member_info.PARENT_KEY_NAME.LWCT_AGN_NM);
                }
                break;

            case STEP_TYPE.Reg_Num_Field:

                reg_before_Focus.GetComponent<InputField>().text = "";
                reg_next_Focus.GetComponent<InputField>().text = "";

                ht = new Hashtable();
                ht.Add("from", targetNowPos.y);
                ht.Add("to", targetNowPos.y + 153);
                ht.Add("easeType", easeType);
                ht.Add("loopType", "once");
                ht.Add("time", MoveTime);
                ht.Add("onupdatetarget", gameObject);
                ht.Add("onupdate", "SetPosField");

                iTween.ValueTo(FieldGroup, ht);
                NowTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;
                NowTarget.SetActive(false);
                step_type = STEP_TYPE.Name_Field;
                BeforeTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;
                BeforeTarget.GetComponentInChildren<InputField>().readOnly = false;

                //  unityWeb.WebSend(TB_TYPE.MBRS_INFO,"/main/test/sy.php", new string[] { "ARA_TBL_NATV_MGNO" }, new string[] { "1" }, Certify_Callback);

                //  member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_RRNO);
                //   member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_SEX_DVCD);
                   member_info.DelValue(_Member_info.PARENT_KEY_NAME.ARA_MBRS_RRNO);
                   member_info.DelValue(_Member_info.PARENT_KEY_NAME.ARA_MBRS_SEX_DVCD);
                break;

            case STEP_TYPE.Mobile_Carrier_Field:
                Mobile_SelectBox.GetComponentInChildren<ToogleAction>().ToggleInit();
                if (Mobile_SelectBox.activeSelf)
                {
                    Mobile_SelectBox.GetComponent<UI_Control>().CloseUI();
                }
                Mobile_Carrier_Label.GetComponent<Text>().text = "SKT";
             

                ht = new Hashtable();
                ht.Add("from", targetNowPos.y);
                ht.Add("to", targetNowPos.y + 153);
                ht.Add("easeType", easeType);
                ht.Add("loopType", "once");
                ht.Add("time", MoveTime);
                ht.Add("onupdatetarget", gameObject);
                ht.Add("onupdate", "SetPosField");

                iTween.ValueTo(FieldGroup, ht);

                NowTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;
                NowTarget.SetActive(false);

                step_type = STEP_TYPE.Reg_Num_Field;
                reg_before_Focus.GetComponent<InputField>().readOnly = false;
                reg_next_Focus.GetComponent<InputField>().readOnly = false;

                reg_next_Focus.GetComponentInChildren<InputField>().ActivateInputField();

                member_info.DelValue(_Member_info.PARENT_KEY_NAME.MMT_TCCCO_DVCD);

                break;

            case STEP_TYPE.Mobile_Number_Field:


              

                ht = new Hashtable();
                ht.Add("from", targetNowPos.y);
                ht.Add("to", targetNowPos.y + 153);
                ht.Add("easeType", easeType);
                ht.Add("loopType", "once");
                ht.Add("time", MoveTime);
                ht.Add("onupdatetarget", gameObject);
                ht.Add("onupdate", "SetPosField");

                iTween.ValueTo(FieldGroup, ht);

                NowTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;
                NowTarget.GetComponentInChildren<InputField>().text = "";
                NowTarget.SetActive(false);

                step_type = STEP_TYPE.Mobile_Carrier_Field;
                Mobile_SelectBox.SetActive(true);

                member_info.DelValue(_Member_info.PARENT_KEY_NAME.ARA_MBRS_MPNO);
                
                break;



            case STEP_TYPE.Mobile_Certify_Field:

                member_info.Del_ALLToggle();
                KCBBtn.GetComponent<ButtonTransition>().Disabled_btn();
                Mobile_Certify.GetComponent<UI_Control>().CloseUI();
                if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL))
                {
                    member_info.DelValue(_Member_info.PARENT_KEY_NAME.ARA_LWCT_AGN_CI_VAL);
                    member_info.DelValue(_Member_info.PARENT_KEY_NAME.ARA_MBRS_MPNO);
        
                }
                step_type = STEP_TYPE.Mobile_Number_Field;

                break;
        }
    }

    public void End_Input(GameObject target)
    {

        Hashtable ht;
        GameObject NextTarget;
        switch (step_type)
        {
            //이름 입력페이지일때 다음과정
            case STEP_TYPE.Name_Field:

                string name_temp = target.GetComponent<InputField>().text;
                if (name_temp != "")
                {
                    if (!Validation.CheckingSpecialText(name_temp))
                    {
                        if (!Validation.CheckingKoreanText(name_temp))
                        {
                            ht = new Hashtable();
                            ht.Add("from", targetNowPos.y);
                            ht.Add("to", targetNowPos.y - 153);
                            ht.Add("easeType", easeType);
                            ht.Add("loopType", "once");
                            ht.Add("time", MoveTime);
                            ht.Add("onupdatetarget", gameObject);
                            ht.Add("onupdate", "SetPosField");

                            iTween.ValueTo(FieldGroup, ht);

                            step_type = STEP_TYPE.Reg_Num_Field;
                            NextTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;
                            NextTarget.SetActive(true);
                            NextTarget.GetComponentInChildren<InputField>().ActivateInputField();

                            member_info.SetValue(_Member_info.PARENT_KEY_NAME.LWCT_AGN_NM, target.GetComponent<InputField>().text);
                        }
                        else
                        {
                            alert_Manager.ShowAlert("이름 형식을 다시한번 확인해주세요.");
                        }


                    }
                    else
                    {
                        alert_Manager.ShowAlert("이름에 특수문자를 포함할 수 없습니다.");
                    }

                }

                break;
            //주민등록번호 입력페이지일때 다음과정
            case STEP_TYPE.Reg_Num_Field:


                if (target.GetComponent<InputField>().text.Length == 7 && reg_before_Focus.GetComponent<InputField>().text.Length == 6 && isAdult)
                {

                    ht = new Hashtable();
                    ht.Add("from", targetNowPos.y);
                    ht.Add("to", targetNowPos.y - 153);
                    ht.Add("easeType", easeType);
                    ht.Add("loopType", "once");
                    ht.Add("time", MoveTime);
                    ht.Add("onupdatetarget", gameObject);
                    ht.Add("onupdate", "SetPosField");

                    iTween.ValueTo(FieldGroup, ht);

                    step_type = STEP_TYPE.Mobile_Carrier_Field;
                    NextTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;
                    NextTarget.SetActive(true);

                    Mobile_SelectBox.SetActive(true);
                    Mobile_SelectBox.GetComponentInChildren<ToogleAction>().ToggleInit();
                    member_info.SetValue(_Member_info.PARENT_KEY_NAME.ARA_MBRS_RRNO,
                                          reg_before_Focus.GetComponent<InputField>().text
                                          + "-"
                                          + reg_next_Focus.GetComponent<InputField>().text);

                    int SexTemp = int.Parse(reg_next_Focus.GetComponent<InputField>().text.Substring(0, 1)) % 2;
                    member_info.SetValue(_Member_info.PARENT_KEY_NAME.ARA_MBRS_SEX_DVCD, (SexTemp != 0) ? "M" : "F");



               

                }
                else
                {
                    alert_Manager.ShowAlert("보호자 주민등록번호를 입력해주세요");
                }
              


                break;

            //통신사 확인페이지일때 다음과정
            case STEP_TYPE.Mobile_Carrier_Field:


                ht = new Hashtable();
                ht.Add("from", targetNowPos.y);
                ht.Add("to", targetNowPos.y - 153);
                ht.Add("easeType", easeType);
                ht.Add("loopType", "once");
                ht.Add("time", MoveTime);
                ht.Add("onupdatetarget", gameObject);
                ht.Add("onupdate", "SetPosField");

                iTween.ValueTo(FieldGroup, ht);

                step_type = STEP_TYPE.Mobile_Number_Field;
                NextTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;

                NextTarget.SetActive(true);

                NextTarget.GetComponentInChildren<InputField>().ActivateInputField();


                break;

            //전화번호입력 및 동의페이지일때 다음과정
            case STEP_TYPE.Mobile_Number_Field:

                if (isAdult)
                {

                    Certify();
                    step_type = STEP_TYPE.Mobile_Certify_Field;
                }
                else
                {
                    alert_Manager.ShowAlert("보호자 주민등록번호를 입력해주세요");
                }





                break;

            //인증번호 입력 다음 과정
            case STEP_TYPE.Mobile_Certify_Field:


                if (Mobile_Certify.GetComponent<Certify_Timer>().isCertifyComplete())
                {
                    Orig_step.ParentComplete(member_info);
                    gameObject.SetActive(false);
                   

                    alert_Manager.ShowAlert("완료!","보호자 동의가 완료되었습니다\n가입을 계속 진행해주세요!");
                   

                }
                else
                {
                    if (!Alert.activeSelf)
                    {
                        alert_Manager.ShowAlert("인증번호를 입력해주세요.");
                    
                    }
                }

                break;

        }




    }

    public void NameCheck(GameObject target)
    {
        NameCheck(target, false);
    }
    public void NameCheck_OutInput(GameObject target)
    {
        NameCheck(target, true);
    }
    private void NameCheck(GameObject target, bool isOut)
    {
        string name_temp = target.GetComponent<InputField>().text;
        if (name_temp != "")
        {

            if (!Validation.CheckingSpecialText(name_temp))
            {
                if (!Validation.CheckingKoreanText(name_temp))
                {
                    if (step_type == STEP_TYPE.Name_Field)
                    {
                        if (!isOut) End_Input(target);
                    }
                    else
                    {
                        target.GetComponent<InputField_Status>().GetBackSprite();
                        member_info.SetValue(_Member_info.PARENT_KEY_NAME.LWCT_AGN_NM, target.GetComponent<InputField>().text);
                    }
                }
                else
                {
                    target.GetComponent<InputField_Status>().SetFailChangeSprite();
                    member_info.DelValue(_Member_info.PARENT_KEY_NAME.LWCT_AGN_NM);
                    alert_Manager.ShowAlert("이름 형식을 다시한번 확인해주세요.");

                }



            }
            else
            {
                target.GetComponent<InputField_Status>().SetFailChangeSprite();
                member_info.DelValue(_Member_info.PARENT_KEY_NAME.LWCT_AGN_NM);
                alert_Manager.ShowAlert("이름에 특수문자를 포함할 수 없습니다.");
            }

        }
    }
    public void CharChange(GameObject value)
    {
        string reg_font_birthday = reg_before_Focus.GetComponent<InputField>().text;
        string temp_str = "";
        if (reg_font_birthday.Length == 6)
        {

            string text = value.GetComponent<InputField>().text;
            int Today = int.Parse(DateTime.Now.ToString(("yyMMdd")));
            int birthday = int.Parse(reg_font_birthday);
            if(Today > birthday)
            {
                Today += 20000000;
                birthday += 20000000;
            }
            else
            {
                Today += 20000000;
                birthday += 19000000;
            }

        if (Today - birthday < 180000)
            {
                isAdult = false;
            }
            else
            {
                isAdult = true;
            }


            if (text.Length > 1)
            {

                for (int i = 0; i < text.Length - 1; i++)
                {
                    temp_str += "●";

                }
                reg_ShowText_Back.text = temp_str;
            }
            else if (text.Length == 1)
            {

                reg_ShowText_Back.text = "";
                reg_ShowText_Font.text = text;

            }
            else
            {
                reg_before_Focus.GetComponent<InputField>().ActivateInputField();
                reg_ShowText_Back.text = "";
                reg_ShowText_Font.text = text;
            }
            if (value.GetComponent<InputField>().text.Length == 7)
            {
                uiManager.Keyboard_out();
                AdultCheck(value);
                
            }

        }
        else
        {
            reg_before_Focus.GetComponent<InputField>().ActivateInputField();
            value.GetComponent<InputField>().text = "";
            reg_ShowText_Back.text = "";
            reg_ShowText_Font.text = "";

        }

    }
    public void AdultCheck(GameObject gameobject)
    {
        if (!isAdult)
        {
            alert_Manager.ShowAlert("보호자 주민등록번호를 입력해주세요");
        }
        else
        {
            if (step_type == STEP_TYPE.Reg_Num_Field)
            {
                End_Input(gameobject);
            }
        }
    }
    public void Reg_FontLengthCheck(string value)
    {

        if (value.Length == 6)
        {
            reg_next_Focus.GetComponent<InputField>().ActivateInputField();
        }
        else if (reg_next_Focus.GetComponent<InputField>().text.Length > 0)
        {
            reg_next_Focus.GetComponent<InputField>().text = "";
            reg_ShowText_Back.text = "";
            reg_ShowText_Font.text = "";
            reg_next_Focus.GetComponent<InputField>().readOnly = false;
        }


    }


    /// <summary>
    /// Mobile 영역
    /// </summary>
    public void SetMobileCarrier(string carrier)
    {
        member_info.SetValue(_Member_info.PARENT_KEY_NAME.MMT_TCCCO_DVCD, carrier);
        if (step_type == STEP_TYPE.Mobile_Carrier_Field)
        {
            End_Input(gameObject);
        }
    }
    public void SetMobileCertification(string value)
    {
        if (value.Length > 10)
        {
            if (member_info.GetContainsKey(_Member_info.PARENT_KEY_NAME.LWCT_AGN_NM))
            {
                member_info.SetValue(_Member_info.PARENT_KEY_NAME.ARA_MBRS_MPNO, value);
                End_Input(gameObject);
            }
            else
            {
                alert_Manager.ShowAlert("이름 형식을 다시한번 확인해주세요.");
            }

        }
    }
    public void Certify()
    {
        //  TB_TYPE.M
        Dictionary<string, string> temp_data = member_info.KCB_P_SendData();
        List<string> key = new List<string>();
        List<string> value = new List<string>();
        int count = 0;
        foreach (KeyValuePair<string, string> pair in temp_data)
        {
            key.Add(pair.Key);
            value.Add(pair.Value);
            count++;
        }




        //서버통신 원활해질경우 SetActive Callback으로 보내기
       

        unityWeb.WebSend(TB_TYPE.KCB, UnityWeb.SEND_URL.Busan, URLs.Certify_Request, URLs.Menu.KCB, key, value, Certify_Callback);
        // unityWeb.WebSend(TB_TYPE.KCB,UnityWeb.SEND_URL.Busan, URLs.Certify_Request, key, value, Certify_Callback);

    }
    public void Certify_Callback(string req,string resCode, List<Dictionary<string, string>> data)
    {
        Debug.Log(req);
        string contents = "";
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

             
                contents += pair.Key + "[" + i + "] : " + pair.Value + "[" + i + "] ";
            }
        }



        if (req.Equals("fail") || resCode.Equals("99"))
        {
            if (!req.Equals("NetWork error"))
            {
                GameObject.Find("Manager").GetComponent<UIManager>().ErrorReport("Mobile_Certify :" + contents);
            }
            step_type = STEP_TYPE.Mobile_Number_Field;
            // Alert.SetActive(true);
            //  Alert.GetComponent<Alert>().ShowText("오류가 발생하였습니다. " + req);
            alert_Manager.ShowAlert("통신사 및 이름,전화번호등 입력정보를 다시 한번 확인해주세요.");

        }
        else if (req.Equals("Sucess") && resCode.Equals("00"))
        {
            tras_seq_value = data[0]["TRNS_SEQ"];
            Mobile_Certify.SetActive(true);
        }

    }
    public void Certify_timer()
    {
        if (!Mobile_Certify.GetComponent<Certify_Timer>().Timer_Start())
        {
            if (Mobile_Certify.GetComponent<Certify_Timer>().isCertifyComplete())
            {
                alert_Manager.ShowAlert("인증이 완료되었습니다.\n다음 단계를 진행해주세요.");
            }
            else
            {
                alert_Manager.ShowAlert("재전송 휫수가 초과하였습니다.");
            }

        }
        else
        {
            //인증번호 재발송
        }
    }
    //input 6자리 입력 체크 6자리시 통신
    public void Certify_InputCheck(string value)
    {
        if (value.Length == 6)
        {
            if (Mobile_Certify.GetComponent<Certify_Timer>().isTimeOut())
            {

                Certify_SendNum(value);


            }
            else
            {
                alert_Manager.ShowAlert("재전송 휫수가 초과하였습니다.");
            }
        }

    }
    public void Certify_SendNum(string num)
    {

        Dictionary<string, string> temp_data = member_info.KCB_P_Certify_SendNum(num,tras_seq_value);
        List<string> key = new List<string>();
        List<string> value = new List<string>();
        int count = 0;
        foreach (KeyValuePair<string, string> pair in temp_data)
        {
            key.Add(pair.Key);
            value.Add(pair.Value);
            count++;
        }
        // 서버전송이 되면
        unityWeb.WebSend(TB_TYPE.KCB, UnityWeb.SEND_URL.Busan, URLs.Certify_Num, URLs.Menu.KCB, key, value, Certify_Num_Callback);


    }
    public void Certify_Num_Callback(string req,string resCode, List<Dictionary<string, string>> data)
    {
        Debug.Log(req);
        string contents = "";
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

          
                contents += pair.Key + " : " + pair.Value + " ";
            }
        }



        if (req.Equals("Sucess"))
        {
            if (resCode.Equals("00"))
            {
                if (data[0].ContainsKey("CI_VAL"))
                {
                    member_info.SetValue(_Member_info.PARENT_KEY_NAME.LWCT_AGN_NM, data[0]["CI_VAL"]);
                    Mobile_Certify.GetComponent<Certify_Timer>().Timer_Stop();

                    List<string> key = new List<string>();
                    List<string> value = new List<string>();

                    key.Add("ARA_MBRS_CI_VAL");
                    key.Add("ARA_MBRS_NM");
                    value.Add(data[0]["CI_VAL"]);
                    value.Add(member_info.GetValue(_Member_info.PARENT_KEY_NAME.LWCT_AGN_NM));


                    member_info.SetValue(_Member_info.PARENT_KEY_NAME.ARA_LWCT_AGN_CI_VAL, data[0]["CI_VAL"]);
                    Mobile_Certify.GetComponent<Certify_Timer>().Timer_Stop();
                    KCBBtn.GetComponent<ButtonTransition>().Enble_btn();
                    //URLs.CI_Val_MemberCheck 주소확인하기
                    //URLs.Menu.Join 메뉴확인하기
                    //전송된값 확인하기
                    //   unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.MemberCheckOrIDFind, URLs.Menu.Join, key, value, CI_OverLapCheckCallback);


                }
                else
                {
                    GameObject.Find("Manager").GetComponent<UIManager>().ErrorReport("CI_VAL Unkown");



                }
            }
            else
            {
                Mobile_Certify.GetComponent<Certify_Timer>().Fail_Num();

            }
        }
        else
        {

            GameObject.Find("Manager").GetComponent<UIManager>().ErrorReport(contents);
            Alert.SetActive(true);
            Alert.GetComponent<Alert>().ShowText("오류가 발생하였습니다. " + req);


        }
    }
    //CI_VAL 중복확인 콜백
    private void CI_OverLapCheckCallback(string req, string resCode, List<Dictionary<string, string>> data)
    {

        Debug.Log(req);
        
        string contents = "";
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

              
                contents += pair.Key + " : " + pair.Value + " ";
            }
        }

        if (req.Equals("Sucess"))
        {
            Mobile_Certify.GetComponent<Certify_Timer>().Timer_Stop();
            //if (resCode.Equals("00") || )
            // {
            if (!data[0].ContainsKey("ARA_LOGIN_DVCD"))
            {
                //중복없음


                KCBBtn.GetComponent<ButtonTransition>().Enble_btn();

            }
            else
            {
                //중복있음
                BackProcess();
                alert_Manager.ShowAlert("이미 가입된 회원이 존재합니다.\n가입여부를 확인해주세요");
            }
            // }
            // else
            //  {
            //      Mobile_Certify.GetComponent<Certify_Timer>().Fail_Num();

            //  }
        }
        else
        {
            if (!req.Equals("NetWork error"))
            {
                GameObject.Find("Manager").GetComponent<UIManager>().ErrorReport(contents);
            }
            Alert.SetActive(true);
            Alert.GetComponent<Alert>().ShowText("오류가 발생하였습니다. " + req);


        }
    }
    public void Keyboard_out()
    {
        uiManager.Keyboard_out();
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

            if (temp_phoneNum.Length == 11)
            {
                target.GetComponent<InputField_Status>().SetPassChangeSprite();
                uiManager.Keyboard_out();
                SetMobileCertification(temp_phoneNum);
            }
         
       
        }

    }

    private void OnEnable()
    {
        targetStartPos = FieldGroup.GetComponent<RectTransform>().anchoredPosition;
        targetNowPos = targetStartPos;
        FieldGroup.transform.GetChild(0).gameObject.SetActive(true);



    }
    private void OnDisable()
    {
        FieldGroup.GetComponent<RectTransform>().anchoredPosition = targetStartPos;
        member_info = new _Member_info();
        step_type = STEP_TYPE.Name_Field;
        isAdult = false;
        InputField[] dd = gameObject.GetComponentsInChildren<InputField>();
        for (int i = 0; i < dd.Length; i++)
        {
            Debug.Log(dd[i].gameObject.name);
            dd[i].text = "";
            dd[i].readOnly = false;
        }

        for (int i = 0; i < FieldGroup.transform.childCount; i++)
        {
            FieldGroup.transform.GetChild(i).gameObject.SetActive(false);
        }
        Mobile_Certify.SetActive(false);

    }

}
