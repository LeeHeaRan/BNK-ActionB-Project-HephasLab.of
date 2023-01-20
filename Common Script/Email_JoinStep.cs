using Defective.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Email_JoinStep : MonoBehaviour
{




    public enum STEP_TYPE { 
    
        Name_Field,
        Reg_Num_Field,
        Mobile_Carrier_Field,
        Mobile_Number_Field,
        Mobile_Certify_Field,
        ID_Field,
        Password_Field,
        RePassword_Field,


    }
    public UIManager uiManager;
    public Alert_Manager alert_Manager;
    public UnityWeb unityWeb;
    public GameObject Alert;
    [Header("member_info_area")]

    public _Member_info member_info = new _Member_info();
    public _Member_info parents_info = new _Member_info();
    private int Step_index = 0;

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
   



    [Header("Children_Agree")]
    public GameObject Agree_area_1;
    public GameObject Agree_area_2;



    [Header("Target Control")]
    public GameObject StepObject;

   
    public GameObject reg_before_Focus;
    public GameObject reg_next_Focus;
    public Text reg_ShowText_Font;
    public Text reg_ShowText_Back;


    [Header("ID&PASW Field")]
    public GameObject Field_NextGroup_P;
    public GameObject Field_NextGroup;
    public JoinID joinIDManger;
    public GameObject Email_UserCustom;
    public GameObject Password_Fleid;
    public GameObject Password_input;
    public GameObject RePassword_Fleid;
    public GameObject RePassword_input;



    [Header("SubmitButton")]
    public GameObject JoinBtn;
    public GameObject KCBBtn;
    public GameObject Compelet_JoinPage;

    [Header("Parent Field")]
    public GameObject Parent_Field;

    [Header("Terms Prefab")]
    public Transform TermsParent;
    public GameObject[] TermsPrefabs;
    GameObject Terms;

    bool isChildren = false;
    bool isBackMove = false;
    bool isParentField = false;
    bool isParentComplete = false;
    bool isModifyROMO = false;
    public bool isIDCheckComplete = false;
    public bool isPassCheckComplete = false;
    bool isJoinEnd = false;
    string tras_seq_value="";


    public TestviewLog test;



    private void Awake()
    {
        GameObject TempUiManager = GameObject.FindGameObjectWithTag("UIManager");
        uiManager = TempUiManager.GetComponent<UIManager>();
        alert_Manager = TempUiManager.GetComponent<Alert_Manager>();
        unityWeb = TempUiManager.GetComponent<UnityWeb>();
        test = GameObject.Find("testViewLog").GetComponent<TestviewLog>();

    }

    // Start is called before the first frame update
    void Start()
    {
        step_type = STEP_TYPE.Name_Field;
        Step_index = 0;

       // SNSJoin("1","2","3");
     //   unityWeb.WebSend(TB_TYPE.MBRS_INFO,UnityWeb.SEND_URL.COCOA, "/main/test/sy.php", new string[] { "ARA_TBL_NATV_MGNO" }, new string[] { "1" }, Certify_Callback);


        /*        string JsonType = "Sucess : {\"_msg_\":{\"_common_\":{\"error.page\":\"CMNERR0010001\",\"error.biz.type\":\"11\",\"error.message\":\"[Session 값을 찾을수가 없습니다.]장시간 사용하지 않아 연결이 종료되었습니다.다시 시도하여 주십시오.\",\"error.customer.message\":\"\",\"error.page.parameters\":\"ib20_err_act = MWPCMN010000A10P\",\"resCode\":\"99\",\"error.instance.id\":\"WBM0101\",\"error.code\":\"BSIB110117\"}}}";
                JsonType = JsonType.Replace(".", "");
                JsonType = JsonType.Replace("Sucess :", "").Trim();
                var jsonObject = new JSONObject(JsonType);

                JsonField jsonField = new JsonField(TB_TYPE.KCB,JsonType);

                //  List<Dictionary<string, string>> ResultData = jsonField.GetDictionary();
                Certify_Callback("", jsonField.GetDictionary());*/


    }
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isJoinEnd)
            {
                //14세미만시 막기
                if (!isParentField) {
                    if (Terms != null)
                    {
                        if (!Terms.GetComponent<UI_Control>().isClose)
                        {
                            Terms.GetComponent<UI_Control>().CloseUI();
                        }
                    }else if (Agree_area_2.activeSelf)
                    {
                        if (!Agree_area_2.GetComponent<UI_Control>().isClose)
                        {
                            Agree_area_2.transform.GetChild(1).Find("close_btn").GetComponent<Button>().onClick.Invoke();
                        }
                    }else if (Agree_area_1.activeSelf)
                    {
                        if (!Agree_area_1.GetComponent<UI_Control>().isClose)
                        {
                            Agree_area_1.transform.GetChild(1).Find("close_btn").GetComponent<Button>().onClick.Invoke();
                        }
                    }
                    else
                    {
                        BackProcess();
                    }
                       
                }
            }
        }else if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isJoinEnd)
            {
               
                if (!isParentField)
                {
                    Debug.Log("isParentField false");
                    if (Terms != null)
                    {
                        if (!Terms.GetComponent<UI_Control>().isClose)
                        {
                            Terms.GetComponent<UI_Control>().CloseUI();
                        }
                    }
                    else if (Agree_area_2.activeSelf)
                    {
                        if (!Agree_area_2.GetComponent<UI_Control>().isClose)
                        {
                            Agree_area_2.transform.GetChild(1).Find("close_btn").GetComponent<Button>().onClick.Invoke();
                        }
                    }
                    else if (Agree_area_1.activeSelf)
                    {
                        if (!Agree_area_1.GetComponent<UI_Control>().isClose)
                        {
                            Agree_area_1.transform.GetChild(1).Find("close_btn").GetComponent<Button>().onClick.Invoke();
                        }
                    }
                    else
                    {
                        BackProcess();
                    }

                }
                else
                {
                    Debug.Log("isParentField true");
                }
            }
        }
    }
    public void SNSJoin(string SNSCODE, string SNSID,string SNSIDToken)
    {
        
        member_info.SetValue(_Member_info.KEY_NAME.ARA_LOGIN_DVCD, SNSCODE);
        member_info.SetValue(_Member_info.KEY_NAME.ARA_SNS_NATV_NO, SNSID);
        member_info.SetValue(_Member_info.KEY_NAME.ARA_SNS_AITM_TOKN_VAL, SNSIDToken);


        Debug.Log("SNSJoin진입" + member_info.GetValue(_Member_info.KEY_NAME.ARA_LOGIN_DVCD));
        Debug.Log("SNSJoin진입" + member_info.GetValue(_Member_info.KEY_NAME.ARA_SNS_NATV_NO));
        Debug.Log("SNSJoin진입" + member_info.GetValue(_Member_info.KEY_NAME.ARA_SNS_AITM_TOKN_VAL));
    }
    void SetPos(float value)
    {
        FieldGroup.GetComponent<RectTransform>().anchoredPosition = new Vector2(targetNowPos.x, value);
        targetNowPos = FieldGroup.GetComponent<RectTransform>().anchoredPosition;
    }
    void SetPosNext(float value)
    {
        Field_NextGroup.GetComponent<RectTransform>().anchoredPosition = new Vector2(NextTargetNowPos.x, value);
        NextTargetNowPos = Field_NextGroup.GetComponent<RectTransform>().anchoredPosition;
    }
    public void BackProcess()
    {
        Hashtable ht;
        GameObject BeforeTarget;
        GameObject NowTarget;
       // test.text += "backPress\n";
        switch (step_type)
        {
            case STEP_TYPE.Name_Field:

                if (!StepObject.GetComponent<UI_Control>().isClose)
                {
                    StepObject.GetComponent<UI_Control>().CloseUI();
                    FieldGroup.GetComponentInChildren<UI_Control>().CloseUI();

                    member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_NM);
                }
                break;

            case STEP_TYPE.Reg_Num_Field:

                reg_before_Focus.GetComponent<InputField>().text = "";
                reg_next_Focus.GetComponent<InputField>().text = "";

                ht = new Hashtable();
                ht.Add("from", targetNowPos.y);
                ht.Add("to", targetNowPos.y+153);
                ht.Add("easeType", easeType);
                ht.Add("loopType", "once");
                ht.Add("time", MoveTime);
                ht.Add("onupdatetarget", gameObject);
                ht.Add("onupdate", "SetPos");

                iTween.ValueTo(FieldGroup, ht);
                NowTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;
                NowTarget.SetActive(false);
                step_type = STEP_TYPE.Name_Field;
                BeforeTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;

                //  unityWeb.WebSend(TB_TYPE.MBRS_INFO,"/main/test/sy.php", new string[] { "ARA_TBL_NATV_MGNO" }, new string[] { "1" }, Certify_Callback);

                member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_RRNO);
                member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_SEX_DVCD);


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
                ht.Add("onupdate", "SetPos");

                iTween.ValueTo(FieldGroup, ht);

                NowTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;
                NowTarget.SetActive(false);

                step_type = STEP_TYPE.Reg_Num_Field;
           

                reg_next_Focus.GetComponentInChildren<InputField>().ActivateInputField();

                member_info.DelValue(_Member_info.KEY_NAME.MMT_TCCCO_DVCD);

                break;

            case STEP_TYPE.Mobile_Number_Field:

                if (!isBackMove)
                {
                    ht = new Hashtable();
                    ht.Add("from", targetNowPos.y);
                    ht.Add("to", targetNowPos.y + 153);
                    ht.Add("easeType", easeType);
                    ht.Add("loopType", "once");
                    ht.Add("time", MoveTime);
                    ht.Add("onupdatetarget", gameObject);
                    ht.Add("onupdate", "SetPos");

                    iTween.ValueTo(FieldGroup, ht);

                    NowTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;
                    NowTarget.GetComponentInChildren<InputField>().text = "";
                    NowTarget.SetActive(false);

                    step_type = STEP_TYPE.Mobile_Carrier_Field;
                    Mobile_SelectBox.SetActive(true);

                    member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_MPNO);

                }

                break;


                
            case STEP_TYPE.Mobile_Certify_Field:

               
                member_info.Del_ALLToggle();
                KCBBtn.GetComponent<ButtonTransition>().Disabled_btn();
                Mobile_Certify.GetComponent<UI_Control>().CloseUI();
                if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL))
                {
                    member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL);
                    member_info.DelValue(_Member_info.KEY_NAME.KCB_MPNO);
                    member_info.DelValue(_Member_info.KEY_NAME.Agree_1);
                    member_info.DelValue(_Member_info.KEY_NAME.Agree_2);
                    member_info.DelValue(_Member_info.KEY_NAME.Agree_3);
                }
                
                step_type = STEP_TYPE.Mobile_Number_Field;
                isBackMove = true;
                break;


            case STEP_TYPE.ID_Field:

             
                step_type = STEP_TYPE.Mobile_Number_Field;
                Field_NextGroup_P.GetComponent<join_NextField_init>().init();
                Field_NextGroup_P.GetComponent<UI_Control>().CloseUI();
                if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL))
                {
                    member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL);
                }
                break;

          
            case STEP_TYPE.Password_Field:

                ht = new Hashtable();
                ht.Add("from", NextTargetNowPos.y);
                ht.Add("to", NextTargetNowPos.y + 160);
                ht.Add("easeType", easeType);
                ht.Add("loopType", "once");
                ht.Add("time", MoveTime);
                ht.Add("onupdatetarget", gameObject);
                ht.Add("onupdate", "SetPosNext");

                iTween.ValueTo(Field_NextGroup, ht);

                step_type = STEP_TYPE.ID_Field;
                //   NextTarget = Field_NextGroup.transform.GetChild(1).gameObject;

                Password_Fleid.GetComponentInChildren<InputField>().text = "";
                Password_Fleid.SetActive(false);



                break;

            case STEP_TYPE.RePassword_Field:

                ht = new Hashtable();
                ht.Add("from", NextTargetNowPos.y);
                ht.Add("to", NextTargetNowPos.y + 160);
                ht.Add("easeType", easeType);
                ht.Add("loopType", "once");
                ht.Add("time", MoveTime);
                ht.Add("onupdatetarget", gameObject);
                ht.Add("onupdate", "SetPosNext");

                iTween.ValueTo(Field_NextGroup, ht);

                step_type = STEP_TYPE.Password_Field;
                //   NextTarget = Field_NextGroup.transform.GetChild(1).gameObject;

                RePassword_input.GetComponentInChildren<InputField>().text = "";
                RePassword_Fleid.SetActive(false);


                break;

        }
      
    }
    public void BackMovingComplete()
    {
        isBackMove = false;
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
                            ht.Add("onupdate", "SetPos");

                            iTween.ValueTo(FieldGroup, ht);

                            step_type = STEP_TYPE.Reg_Num_Field;
                            NextTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;
                            NextTarget.SetActive(true);
                            NextTarget.GetComponentInChildren<InputField>().ActivateInputField();

                            member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_NM, target.GetComponent<InputField>().text);

                            Debug.Log("End_Input" + member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_NM));
                          
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


                if (target.GetComponent<InputField>().text.Length == 7 && reg_before_Focus.GetComponent<InputField>().text.Length == 6)
                {
                    if (isModifyROMO&&parents_info.GetContainsKey(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL) && member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_MBRS_RRNO))
                    {
                        isModifyROMO = false;
                      if (!member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_RRNO).Equals(reg_before_Focus.GetComponent<InputField>().text
                                              + "-"
                                              + reg_next_Focus.GetComponent<InputField>().text))
                        {
                            isParentComplete = false;
                        }
                        else
                        {
                            isParentComplete = true;
                        }
                    }
                    if (isChildren && !isParentComplete)
                    {
                        Agree_area_1.SetActive(true);

                    }
                    else if ((isChildren && isParentComplete) || !isChildren)
                    {
                        ht = new Hashtable();
                        ht.Add("from", targetNowPos.y);
                        ht.Add("to", targetNowPos.y - 153);
                        ht.Add("easeType", easeType);
                        ht.Add("loopType", "once");
                        ht.Add("time", MoveTime);
                        ht.Add("onupdatetarget", gameObject);
                        ht.Add("onupdate", "SetPos");

                        iTween.ValueTo(FieldGroup, ht);

                        step_type = STEP_TYPE.Mobile_Carrier_Field;
                        NextTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;
                        NextTarget.SetActive(true);
                        Mobile_SelectBox.SetActive(true);
                        Mobile_SelectBox.GetComponentInChildren<ToogleAction>().ToggleInit();

                        member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_RRNO,
                                              reg_before_Focus.GetComponent<InputField>().text
                                              + "-"
                                              + reg_next_Focus.GetComponent<InputField>().text);

                        int SexTemp = int.Parse(reg_next_Focus.GetComponent<InputField>().text.Substring(0, 1)) % 2;
                        member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_SEX_DVCD, (SexTemp != 0) ? "M" : "F");



                       
                    }

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
                ht.Add("onupdate", "SetPos");

                iTween.ValueTo(FieldGroup, ht);

                step_type = STEP_TYPE.Mobile_Number_Field;
                NextTarget = FieldGroup.transform.GetChild((int)step_type).gameObject;

                NextTarget.SetActive(true);

                NextTarget.GetComponentInChildren<InputField>().ActivateInputField();


                break;

            //전화번호입력 및 동의페이지일때 다음과정
            case STEP_TYPE.Mobile_Number_Field:


                //  전부 동의 할때 넘어가게되어있음 
                // 전부 동의가 필요없을시 다시 작성하기

                if (isParentComplete && isChildren)
                {
                    step_type = STEP_TYPE.Mobile_Certify_Field;
                    member_info.SetValue(_Member_info.KEY_NAME.KCB_MPNO, member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_MPNO));
                    Certify();
                }
                else
                {
                    if (target.GetComponent<toggle_manager>().GetTogglesALLOn())
                    {
                        member_info.SetValue(_Member_info.KEY_NAME.KCB_MPNO, member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_MPNO));
                        member_info.SetValue(_Member_info.KEY_NAME.Agree_1, "Y");
                        member_info.SetValue(_Member_info.KEY_NAME.Agree_2, "Y");
                        member_info.SetValue(_Member_info.KEY_NAME.Agree_3, "Y");
                        member_info.SetValue(_Member_info.KEY_NAME.ARA_SV_U_LEME_YN, "Y");

                        if (target.GetComponent<toggle_manager>().GetoptionalToggleIsOn()[0])
                            member_info.SetValue(_Member_info.KEY_NAME.ARA_EVNT_AND_AD_LEME_YN, "Y");
                        else
                            member_info.SetValue(_Member_info.KEY_NAME.ARA_EVNT_AND_AD_LEME_YN, "N");


                        Debug.Log("End_Input" + member_info.GetValue(_Member_info.KEY_NAME.ARA_EVNT_AND_AD_LEME_YN));

                        step_type = STEP_TYPE.Mobile_Certify_Field;

                        target.GetComponent<toggle_manager>().Toggle_3_Close();
                        Invoke("Certify", 0.6f); 
                    }
                    else
                    {
                        alert_Manager.ShowAlert("필수항목에 동의하셔야 서비스이용이 가능합니다.");
                    }
                }


   

                break;

            //인증번호 입력 다음 과정
            case STEP_TYPE.Mobile_Certify_Field:


                if (Mobile_Certify.GetComponent<Certify_Timer>().isCertifyComplete())
                {
                    if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_LOGIN_DVCD))
                    {
                        SNS_Submit();
                    }
                    else
                    {
                        step_type = STEP_TYPE.ID_Field;
                        Field_NextGroup_P.SetActive(true);
                        Field_NextGroup_P.GetComponent<join_NextField_init>().SetFocus();
                    }




                }
                else
                {
                 
                        alert_Manager.ShowAlert("인증번호를 입력해주세요.");
          
                    
                }

                break;

            //아이디 다음
            case STEP_TYPE.ID_Field:

                ht = new Hashtable();
                ht.Add("from", NextTargetNowPos.y);
                ht.Add("to", NextTargetNowPos.y - 160);
                ht.Add("easeType", easeType);
                ht.Add("loopType", "once");
                ht.Add("time", MoveTime);
                ht.Add("onupdatetarget", gameObject);
                ht.Add("onupdate", "SetPosNext");

                iTween.ValueTo(Field_NextGroup, ht);

                step_type = STEP_TYPE.Password_Field;
                //   NextTarget = Field_NextGroup.transform.GetChild(1).gameObject;
                
                target.SetActive(true);

                target.GetComponentInChildren<InputField>().ActivateInputField();

                break;

            case STEP_TYPE.Password_Field:

                uiManager.Keyboard_out();
                Last_Password_validation(Password_input.GetComponent<InputField>().text);
                string tempstr = Password_input.GetComponent<InputField>().text;
                if (tempstr.Length>=8 && Validation.Password_Check(Password_input.GetComponent<InputField>().text))
                {

                    
                    ht = new Hashtable();
                    ht.Add("from", NextTargetNowPos.y);
                    ht.Add("to", NextTargetNowPos.y - 160);
                    ht.Add("easeType", easeType);
                    ht.Add("loopType", "once");
                    ht.Add("time", MoveTime);
                    ht.Add("onupdatetarget", gameObject);
                    ht.Add("onupdate", "SetPosNext");

                    iTween.ValueTo(Field_NextGroup, ht);

                    step_type = STEP_TYPE.RePassword_Field;

                    target.SetActive(true);

                    target.GetComponentInChildren<InputField>().ActivateInputField();

                }
               


                break;

            case STEP_TYPE.RePassword_Field:

                Debug.Log("끝");

                uiManager.Keyboard_out();
                Last_RePassword_Check(RePassword_input.GetComponent<InputField>().text);
                if (isIDCheckComplete && isPassCheckComplete)
                {
                    JoinBtn.GetComponent<ButtonTransition>().Enble_btn();
                }

                if (isPassCheckComplete)
                {
                    member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_PSWD, Password_input.GetComponent<InputField>().text);
                }
                else
                {
                    if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_MBRS_PSWD))
                    {
                        member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_PSWD);
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
                       if(!isOut) End_Input(target);
                    }
                    else
                    {
                        target.GetComponent<InputField_Status>().GetBackSprite();
                        member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_NM, target.GetComponent<InputField>().text);
                    }
                }
                else
                {
                    target.GetComponent<InputField_Status>().SetFailChangeSprite();
                    member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_NM);
                    alert_Manager.ShowAlert("이름 형식을 다시한번 확인해주세요.");

                }



            }
            else
            {
                target.GetComponent<InputField_Status>().SetFailChangeSprite();
                member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_NM);
                alert_Manager.ShowAlert("이름에 특수문자를 포함할 수 없습니다.");
            }

        }
    }



        public void CharChange(GameObject value)
    {
        if (isChildren&&isParentComplete&&!isModifyROMO)
        {
            alert_Manager.ShowAlert("주민등록번호 수정시 보호자동의를 다시진행하셔야 합니다.");
            isModifyROMO = true;
        }
        string reg_font_birthday = reg_before_Focus.GetComponent<InputField>().text;
        string temp_str = "";
        if (reg_font_birthday.Length == 6)
        {

            /*  string text = value.GetComponent<InputField>().text;

              if (int.Parse(DateTime.Now.ToString(("yyMMdd"))) - int.Parse(reg_font_birthday) < 14000)
              {
                  isChildren = false;
              }
              else
              {
                  isChildren = true;
              }*/
            string text = value.GetComponent<InputField>().text;
            int Today = int.Parse(DateTime.Now.ToString(("yyMMdd")));
            int birthday = int.Parse(reg_font_birthday);
            if (Today > birthday)
            {
                Today += 20000000;
                birthday += 20000000;
            }
            else
            {
                Today += 20000000;
                birthday += 19000000;
            }
          if (Today - birthday > 140000)
            {
                isChildren = false;
            }
            else
            {
                isChildren = true;
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
                ChildrenCheckPage(value);
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
    public void ChildrenCheckPage(GameObject gameObject)
    {
        if (isChildren)
        {
            Agree_area_1.SetActive(true);
        }
        else
        {
            if (step_type == STEP_TYPE.Reg_Num_Field)
            {
                End_Input(gameObject);
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
         
        }


    }
    public void Close_Complete()
    {
        StepObject.SetActive(false);
    }


    public void ChildrenCanelClear()
    {
        ///// 14세미만 취소시 주민등록번호 초기화
        reg_before_Focus.GetComponent<InputField>().text = "";
        reg_next_Focus.GetComponent<InputField>().text = "";
        reg_ShowText_Back.text = "";
        reg_ShowText_Font.text = "";
        // 필요없을시 지우기

        isChildren = false;
    }
    public void Exit_children_agree()
    {
        if (Agree_area_2.activeSelf)
        {
            Agree_area_1.SetActive(false);
            Agree_area_2.GetComponent<UI_Control>().CloseUI();
        }else if (Agree_area_1.activeSelf && !Agree_area_2.activeSelf)
        {
            Agree_area_1.GetComponent<UI_Control>().CloseUI();
        }

      


        ///// 14세미만 취소시 주민등록번호 초기화
        reg_before_Focus.GetComponent<InputField>().text = "";
        reg_next_Focus.GetComponent<InputField>().text = "";
        reg_ShowText_Back.text = "";
        reg_ShowText_Font.text = "";
        // 필요없을시 지우기

        isChildren = false;
    }



    /// <summary>
    /// Mobile 영역
    /// </summary>
    public void SetMobileCarrier(string carrier)
   {
        member_info.SetValue(_Member_info.KEY_NAME.MMT_TCCCO_DVCD, carrier);
        if (step_type == STEP_TYPE.Mobile_Carrier_Field)
        {
            End_Input(gameObject);
        }
   }
   public void SetMobileCertification(string value)
    {
        if (value.Length > 10)
        {
            if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_MBRS_NM))
            {
                member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_MPNO, value);
                if (isChildren && isParentComplete)
                {
                    End_Input(gameObject);
                }
                else
                {
                    Mobile_Certification.SetActive(true);
                }
            }
            else
            {
                alert_Manager.ShowAlert("이름 형식을 다시한번 확인해주세요.");
            }
          
        
        }
    }
    public void PhoneNumber_Validation(GameObject target)
    {
        string temp_phoneNum = target.GetComponent<InputField>().text;
       if (temp_phoneNum.Length != 11)
        {
            target.GetComponent<InputField_Status>().SetFailChangeSprite();
        }else if (!temp_phoneNum.Substring(0, 2).Equals("01"))
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
                // PhoneNumber_Validation(target);
            }
        }

    }
  
    public void Certify()
    {
      //  TB_TYPE.M

        Dictionary<string, string> temp_data = member_info.KCB_SendData();
        List<string> key = new List<string>();
        List<string> value = new List<string>();
        int count = 0;
        foreach (KeyValuePair<string,string> pair in temp_data)
        {
            key.Add(pair.Key);
            value.Add(pair.Value);
            count++;
        }



        //서버통신 원활해질경우 SetActive Callback으로 보내기
       
       //모바일 인증 요청 
        unityWeb.WebSend(TB_TYPE.KCB,UnityWeb.SEND_URL.Busan, URLs.Certify_Request,URLs.Menu.KCB, key, value, Certify_Callback);
        //Mobile_Certify.SetActive(true);

    }
    public void Certilfy_Check()
    {
        //MBPCMN1200ARA10
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
            alert_Manager.ShowAlert("인증번호 재발송이 요청되었습니다.");
            Certify();
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
                alert_Manager.ShowAlert("입력 제한시간이 초과되었습니다.");
            }
        }
       
    }

    public void Certify_SendNum(string num)
    {

        Dictionary<string, string> temp_data = member_info.KCB_Certify_SendNum(num, tras_seq_value);
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
           unityWeb.WebSend(TB_TYPE.KCB,UnityWeb.SEND_URL.Busan, URLs.Certify_Num,URLs.Menu.KCB, key, value, Certify_Num_Callback);

        // 테스트용
        //member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL, "testtesttesttesttesttesttesttesttesttesttest");
        //Mobile_Certify.GetComponent<Certify_Timer>().Timer_Stop();
       // KCBBtn.GetComponent<ButtonTransition>().Enble_btn();
        //   Mobile_Certify.GetComponent<Certify_Timer>().Timer_Stop();
        //콜백으로 CI값받고 중복확인 이후 과정
        // member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL, "sdfjlksdfjslkfdjslk");
        //   KCBBtn.GetComponent<ButtonTransition>().Enble_btn();
        //임시 CI_VAL 삽입dssd

    }

    

    public void Mobile_Certify_Close()
    {
        Mobile_Certify.SetActive(false);
    }

    public void Certify_Callback(string req,string resCode, List<Dictionary<string, string>> data)
    {
        test.SetLog(req + "\n");
         string contents = "";
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

                test.SetLog(pair.Key + "[" + i + "] : " + pair.Value + "[" + i + "]\n");
                contents += pair.Key + "[" + i + "] : " + pair.Value + "[" + i + "] ";
            }
        }
      

        if (req.Equals("fail") || resCode.Equals("99"))
        {
              if (!req.Equals("NetWork error"))
            {
                uiManager.ErrorReport("Mobile_Certify :" + contents);
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
        else
        {
            step_type = STEP_TYPE.Mobile_Number_Field;
            alert_Manager.ShowAlert("서버와 통신중에 오류가 발생하였습니다.");
        }


        

    }
    public void Certify_Num_Callback(string req, string resCode, List<Dictionary<string, string>> data)
    {
        Debug.Log(req);
        string contents = "";
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

                test.SetLog(pair.Key + " : " + pair.Value + "\n");
                contents += pair.Key + " : " + pair.Value + " ";
            }
        }



        if (req.Equals("Sucess"))
        {
            if (resCode.Equals("00"))
            {
                if (data[0].ContainsKey("CI_VAL"))
                {
                    member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL, data[0]["CI_VAL"]);
                    Mobile_Certify.GetComponent<Certify_Timer>().Timer_Stop();

                    List<string> key = new List<string>();
                    List<string> value = new List<string>();

                    key.Add("ARA_MBRS_CI_VAL");
                    key.Add("ARA_MBRS_NM");
                    value.Add(data[0]["CI_VAL"]);
                    value.Add(member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_NM));


                    member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL, data[0]["CI_VAL"]);
                    //URLs.CI_Val_MemberCheck 주소확인하기
                    //URLs.Menu.Join 메뉴확인하기
                    //전송된값 확인하기
                    unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.MemberCheckOrIDFind, URLs.Menu.Join, key, value, CI_OverLapCheckCallback);


                }
                else
                {
                    uiManager.ErrorReport("CI_VAL Unkown");
                


                }
            }
            else
            {
               Mobile_Certify.GetComponent<Certify_Timer>().Fail_Num();

           }
        }
        else
        {

            uiManager.ErrorReport(contents);
                Alert.SetActive(true);
                Alert.GetComponent<Alert>().ShowText("오류가 발생하였습니다. " +req);

            
        }
    }
    //CI_VAL 중복확인 콜백
    private void CI_OverLapCheckCallback(string req,string resCode, List<Dictionary<string, string>> data)
    {

        Debug.Log(req);
        test.SetLog("\n\n\n", false) ;
        test.SetLog(req +"\n");
        string contents = "";
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

                test.SetLog(pair.Key + " : " + pair.Value + "\n");
                contents += pair.Key + " : " + pair.Value + " ";
            }
        }

        if (req.Equals("Sucess"))
        {
            Mobile_Certify.GetComponent<Certify_Timer>().Timer_Stop();
            //if (resCode.Equals("00") || )
            // {
            try
            {
                if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_SNS_NATV_NO))
                {
                    if (data[0]["ARA_LOGIN_DVCD"].Equals("-1"))
                    {
                        //중복없음
                        KCBBtn.GetComponent<ButtonTransition>().Enble_btn();
                    }
                    else
                    {
                        //중복있음
                        BackProcess();
                        alert_Manager.ShowAlert("이미 SNS계정으로 가입된 내역이 존재합니다.\n가입여부를 확인해주세요");
                        StepObject.SetActive(false);
                    }
                }
                else
                {
                    if (data[0]["ARA_MBRS_EMLADR"].Equals("-1"))
                    {
                        //중복없음
                        KCBBtn.GetComponent<ButtonTransition>().Enble_btn();
                    }
                    else
                    {
                        //중복있음
                        BackProcess();
                        
                        alert_Manager.ShowAlert("이미 가입된 회원이 존재합니다.\n가입여부를 확인해주세요");
                        StepObject.SetActive(false);
                    }
                }
            }catch(Exception e)
            {
                test.SetLog(e.ToString());
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
                uiManager.ErrorReport(contents);
            }
            Alert.SetActive(true);
            Alert.GetComponent<Alert>().ShowText("오류가 발생하였습니다. " + req);


        }
    }
    /// Mobile End
    /// 





    ///<summary>
    /// 아이디 비번관련
    ///</summary>
    ///
    public void EmailDomain_SelectBox(GameObject selectBox)
    {
        if (selectBox.activeSelf)
        {
            
            selectBox.SetActive(false);
        
        }
        else
        {
            selectBox.SetActive(true);
         
         
        }
        
       
    }

    public void UserEmail_onCustom(GameObject selectBox)
    {
        joinIDManger.ID_InfoMove(true);
        selectBox.SetActive(false);
        Email_UserCustom.SetActive(true);
        joinIDManger.GetBackInfo();
        isIDCheckComplete = false;
    }
    public void UserEmail_onCustomCheck(bool toggle)
    {
        if (Email_UserCustom.activeSelf && toggle) {

           
            joinIDManger.EmailCustomClose();
            Email_UserCustom.SetActive(false);
            joinIDManger.ID_InfoMove(false);

        }
        
       
    }
    public void UserEmaildoMain_validation(GameObject target)
    {
       
        if (!Validation.EmailCheck(target.GetComponent<InputField>().text))
        {
            joinIDManger.Email_notCharFail();
        }
        else
        {
            target.GetComponent<InputField_Status>().SetPassChangeSprite();

        }
    }
    public void UserEmail_validation(GameObject target)
    {
        uiManager.Keyboard_out();
        if (!Validation.EmailCheck(target.GetComponent<InputField>().text))
        {
          //  joinIDManger.Email_notCharFail();
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
                List<string> key = new List<string>();
                List<string> value = new List<string>();

                key.Add("ARA_MBRS_EMLADR");
                value.Add(joinIDManger.GetFull_ID(dmain));
                Debug.Log(joinIDManger.GetFull_ID(dmain));
                  unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Join_IDCheck, URLs.Menu.Join, key, value, IDCheck_Callback);

                
            /*    if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
                {
                    //성공시
                    joinIDManger.FullID_OK();
                      isIDCheckComplete = true;
                     member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_EMLADR, joinIDManger.GetFull_ID());
                    if (step_type == STEP_TYPE.ID_Field)
                    {
                        End_Input(Password_Fleid);
                    }
                }
                else
                {
                    //실패시 
                    joinIDManger.FullID_Fail();
                     isIDCheckComplete = false;
                }*/



        }
        else
        {
            Debug.Log(joinIDManger.IDCheck()+"실패");
        }
        
    }
    public void ID_CharChange()
    {
        isIDCheckComplete = false;
    }

    public void ID_RunTime_validation(GameObject target)
    {
        string id = target.GetComponent<InputField>().text;
        if (!joinIDManger.IDCheck())
        {
            joinIDManger.ID_notCharFail();

        }
        else
        {
            target.GetComponent<InputField_Status>().SetPassChangeSprite();
        }
    }

    public void ID_validation(GameObject target)
    {
        uiManager.Keyboard_out();
        string id = target.GetComponent<InputField>().text;
        if (!joinIDManger.IDCheck())
        {
            if (id.Length != 0)
            {
               // joinIDManger.ID_notCharFail();
            }
            else
            {
                target.GetComponent<InputField_Status>().GetBackSprite();
            }
       
           
        }
        else
        {
            
            if (joinIDManger.EdomainCheck())
            {
                List<string> key = new List<string>();
                List<string> value = new List<string>();

                key.Add("ARA_MBRS_EMLADR");
                value.Add(joinIDManger.GetFull_ID());
                Debug.Log(joinIDManger.GetFull_ID());
                 unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Join_IDCheck, URLs.Menu.Join, key, value, IDCheck_Callback);

                ////테스트용
              /*  if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
                {
                    //성공시
                    joinIDManger.FullID_OK();
                    isIDCheckComplete = true;
                    member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_EMLADR, joinIDManger.GetFull_ID());
                    if (step_type == STEP_TYPE.ID_Field)
                    {
                        End_Input(Password_Fleid);
                    }
                    else
                    {
                        if (isPassCheckComplete)
                        {
                            JoinBtn.GetComponent<ButtonTransition>().Enble_btn();
                        }
                    }
                }
                else
                {
                    isIDCheckComplete = false;
                    //실패시 
                    joinIDManger.FullID_Fail();
                    if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_MBRS_EMLADR))
                    {
                        member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_EMLADR);
                    }
                }*/

                ///테스트용
            }
            else
            {
                if (Email_UserCustom.activeSelf)
                {
                    joinIDManger.Email_notCharFail();

                }

            }

        }
       
    }

    public void Password_validation(string value)
    {
        if (RePassword_input.GetComponent<InputField>().text.Length > 0) {

            RePassword_input.GetComponent<InputField>().text= "";
            RePassword_input.GetComponent<InputField_Status>().GetBackSprite();
            if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_MBRS_PSWD))
            {
                member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_PSWD);
            }
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
                }else if(value.Contains(" "))
                {
                    Password_input.GetComponent<InputField_Status>().SetFailChangeSprite("비밀번호에 띄어쓰기는 포함할 수 없습니다.");

                }else if (!Validation.Password_combineCheck(value))
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
    public void Last_Password_validation(string value)
    {
        if (RePassword_input.GetComponent<InputField>().text.Length > 0)
        {

            RePassword_input.GetComponent<InputField>().text = "";
            RePassword_input.GetComponent<InputField_Status>().GetBackSprite();
            if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_MBRS_PSWD))
            {
                member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_PSWD);
            }
        }
        if (value.Length >= 8)
        {
            if (Validation.Password_Check(value))
            {
                Password_input.GetComponent<InputField_Status>().GetBackSprite();


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
                RePassword_input.GetComponent<InputField_Status>().SetPassChangeSprite("비밀번호가 일치합니다.");
                isPassCheckComplete = true;
                
            }
            else
            {
                isPassCheckComplete = false;
                RePassword_input.GetComponent<InputField_Status>().SetFailChangeSprite("비밀번호가 일치하지 않습니다.");
                JoinBtn.GetComponent<ButtonTransition>().Disabled_btn();
            }
        }
        else
        {
            isPassCheckComplete = false;
            JoinBtn.GetComponent<ButtonTransition>().Disabled_btn();
        }
    }
    public void Last_RePassword_Check(string value)
    {
        if (value.Length > 0)
        {
            if (Password_input.GetComponent<InputField>().text.Equals(value))
            {
                RePassword_input.GetComponent<InputField_Status>().GetBackSprite();
                

            }
            else
            {
              
                RePassword_input.GetComponent<InputField_Status>().SetFailChangeSprite("비밀번호가 일치하지 않습니다.");
                JoinBtn.GetComponent<ButtonTransition>().Disabled_btn();
            }
        }
        else
        {
            isPassCheckComplete = false;
            JoinBtn.GetComponent<ButtonTransition>().Disabled_btn();
        }
    }

    private void IDCheck_Callback(string req,string resCode, List<Dictionary<string, string>> data)
    {
        Debug.Log(req);
        string contents = "";
   
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

                test.SetLog(pair.Key + " : " + pair.Value + "\n");
                contents += pair.Key + " : " + pair.Value + " ";
            }
        }

        if (req.Equals("Sucess"))
        {
            if(resCode.Equals("00"))
            {
                if (data[0]["ARA_MBRS_NATV_MGNO"].Equals("-1"))
                {
                    joinIDManger.FullID_OK();
                   
                    isIDCheckComplete = true;
                    member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_EMLADR, joinIDManger.GetFull_ID());
                    if (step_type == STEP_TYPE.ID_Field)
                    {
                        End_Input(Password_Fleid);
                        
                    }
                    else
                    {
                        if (isPassCheckComplete)
                        {
                            JoinBtn.GetComponent<ButtonTransition>().Enble_btn();
                        }
                    }

                 

                }
                else
                {
                    joinIDManger.FullID_Fail();
                    isIDCheckComplete = false;
                    if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_MBRS_EMLADR))
                    {
                        member_info.DelValue(_Member_info.KEY_NAME.ARA_MBRS_EMLADR);
                    }
                }
            }
            else
            {
              

            }
        }
        else
        {

            uiManager.ErrorReport(contents);
            Alert.SetActive(true);
            Alert.GetComponent<Alert>().ShowText("오류가 발생하였습니다. " + req);


        }
   

    }




    /// <summary>
    /// 부모동의
    /// </summary>
    /// 
    public void Children_AgreeCheck(GameObject target)
    {
        if (target.GetComponent<toggle_manager>().GetTogglesALLOn())
        {


           
            member_info.SetValue(_Member_info.KEY_NAME.Agree_1, "Y");
            member_info.SetValue(_Member_info.KEY_NAME.Agree_2, "Y");
            member_info.SetValue(_Member_info.KEY_NAME.Agree_3, "Y");
            member_info.SetValue(_Member_info.KEY_NAME.ARA_SV_U_LEME_YN, "Y");

            if (target.GetComponent<toggle_manager>().GetoptionalToggleIsOn()[0])
                member_info.SetValue(_Member_info.KEY_NAME.ARA_EVNT_AND_AD_LEME_YN, "Y");
            else
                member_info.SetValue(_Member_info.KEY_NAME.ARA_EVNT_AND_AD_LEME_YN, "N");
            Agree_area_1.GetComponent<UI_Control>().CloseUI();
            Invoke("Parent_OpenField", 0.6f);
        }
        else
        {
            alert_Manager.ShowAlert("필수항목에 동의하셔야 서비스이용이 가능합니다.");
  
        }
    }
    public void Parent_OpenField()
    {
        parents_info = new _Member_info();
        Parent_Field.SetActive(true);
        isParentField = true;
    }
    public void Parent_ClosenField()
    {
     
        isParentField = false;
    }
    public void ParentComplete(_Member_info pinfo)
    {
        parents_info = pinfo;
        Parent_ClosenField();
        isParentComplete = true;
        if (step_type == STEP_TYPE.Reg_Num_Field)
        {
            End_Input(reg_next_Focus);
        }
    }







    public void SNS_Submit()
    {

        member_info.CreateMemberHashValue(6);
        if (member_info.Submit_SNS_ValueCheck())
        {
            isJoinEnd = true;
            //테스트용
            Debug.Log("Join Submit [Submit_SNS_ValueCheck()" + member_info.Submit_SNS_ValueCheck() + "]");
            member_info.Test_Log();
            //테스트용 끝
           // Compelet_JoinPage.SetActive(true);
            //  unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Join_Submit,URLs.Menu.Join, member_info.GetALLKey(), member_info.GetALLValue(), JoinSubmitCallback);

            if (isChildren)
            {
                List<string> key = parents_info.GetParentALLKey();
                List<string> value = parents_info.GetParentALLValue();
                key.Add("ARA_MBRS_CI_VAL");
                value.Add(member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL));
                unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.JoinParentInfo_Submit, URLs.Menu.Member, key, value, JoinParentSubmitCallback);

            }
            else
            {
                Debug.Log("End_Input Count" + member_info.GetALLKey().Count);
                for (int i =0; i<member_info.GetALLKey().Count; i++)
                {
                    Debug.Log("End_Input Submit" + member_info.GetALLKey()[i]); 
                }
                unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Join_Submit, URLs.Menu.Join, member_info.GetALLKey(), member_info.GetALLValue(), JoinSubmitCallback);

            }
        }
    }




    public void Submit()
    {
        
        member_info.CreateMemberHashValue(6);
        if (member_info.SubmitValueCheck())
        {
            isJoinEnd = true;
            //테스트용
            Debug.Log("Join Submit [SubmitValueCheck()" + member_info.SubmitValueCheck() + "]");
           member_info.Test_Log();
            //테스트용 끝
            if (isChildren)
            {
                List<string> key = parents_info.GetParentALLKey();
                List<string> value = parents_info.GetParentALLValue();
                key.Add("ARA_MBRS_CI_VAL");
                value.Add(member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL));
                unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.JoinParentInfo_Submit, URLs.Menu.Member, key, value, JoinParentSubmitCallback);

            }
            else
            {
                unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Join_Submit, URLs.Menu.Join, member_info.GetALLKey(), member_info.GetALLValue(), JoinSubmitCallback);

            }

        }
    }
    private void JoinParentSubmitCallback(string req, string resCode, List<Dictionary<string, string>> data)
    { 
        Debug.Log(req);
        string contents = "";
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

                test.SetLog(pair.Key + " : " + pair.Value + "\n");
                contents += pair.Key + " : " + pair.Value + " ";
            }
        }

        if (req.Equals("Sucess"))
        {
            if (resCode.Equals("00") && data[0].ContainsKey("ARA_REQ_DVCD"))
            {
                if (data[0]["ARA_REQ_DVCD"].Equals("0"))
                {

                    unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Join_Submit, URLs.Menu.Join, member_info.GetALLKey(), member_info.GetALLValue(), JoinSubmitCallback);

                }
                else
                {
                    alert_Manager.ShowAlert("[오류:" + data[0]["ARA_REQ_DVCD "] + "] 회원가입도중 오류가 발생하였습니다.");
                }
            }
            else
            {
                alert_Manager.ShowAlert("회원가입도중 오류가 발생하였습니다.");
            }
        }
        else
        {
            alert_Manager.ShowAlert("[" + req + "] 회원가입도중 오류가 발생하였습니다.");
        }
        //  PlayerPrefs.SetInt("MemberUID",????);
        //

    }
    private void JoinSubmitCallback(string req,string resCode, List<Dictionary<string, string>> data)
    {
        Debug.Log(req);
        string contents = "";
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

                test.SetLog(pair.Key + " : " + pair.Value + "\n");
                contents += pair.Key + " : " + pair.Value + " ";
            }
        }
         
        if (req.Equals("Sucess"))
        {
            if (resCode.Equals("00") && data[0].ContainsKey("ARA_REQ_DVCD"))
            {
                if (data[0]["ARA_REQ_DVCD"].Equals("0")) {

                    // 가입성공시
                    SplayerPrefs.PlayerPrefsSave("ARA_MBRS_NATV_MGNO", data[0]["ARA_MBRS_NATV_MGNO"]);
                    SplayerPrefs.PlayerPrefsSave("CI_VAL", member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL));
                    SplayerPrefs.PlayerPrefsSave("MemberHash", member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_HASH_VAL));
                    SplayerPrefs.PlayerPrefsSave("AutoLogin", 1);


                    List<string> key = new List<string>();
                    List<string> value = new List<string>();

                    key.Add("ARA_MBRS_CI_VAL");
                    key.Add("ARA_LEME_CRCT_DVCD");
                    key.Add("ARA_LEME_LRG_TIT");
                    key.Add("ARA_LEME_DTL_CNTN");
                    key.Add("EVNT_TYPE_DVCD");
                    //  key.Add("SV_RQST_URL");
                    //  key.Add("ARA_LEME_RDNG_DTTI");

                    value.Add(member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL));
                    value.Add("2");
                    value.Add(AppInfo.AppTitle+"와 첫 만남");
                    value.Add("만나서 반가워요♡ 액션B와 함께 즐거운 시간 보내볼까요 ?");
                    value.Add("2");

                    unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Notice_create, URLs.Menu.noticeCreate, key, value, NoticeCreateCallBack);




                    //화면 꺼지는거 확인하기w
                    uiManager.SetMemberInfo(member_info);
                    Compelet_JoinPage.SetActive(true);



                }
                else
                {
                    if (member_info.GetContainsKey(_Member_info.KEY_NAME.ARA_SNS_NATV_NO))
                    {
             
                        unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.SNSID_Add, URLs.Menu.Join, member_info.GetSNS_Key(), member_info.GetSNS_value(), JoinAddSNS_SubmitCallback);

                    }
                }
            }
            else
            {
                alert_Manager.ShowAlert("회원가입도중 오류가 발생하였습니다.");
            }
        }
        else
        {
            alert_Manager.ShowAlert("["+req+"] 회원가입도중 오류가 발생하였습니다.");
        }
      //  PlayerPrefs.SetInt("MemberUID",????);
        //

    }
    private void JoinAddSNS_SubmitCallback(string req, string resCode, List<Dictionary<string, string>> data)
    {
        Debug.Log(req);
        test.SetLog("\nJoinAddSNS_SubmitCallback"+resCode + "\n");
        string contents = "";
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

                test.SetLog(pair.Key + " : " + pair.Value + "\n");
                contents += pair.Key + " : " + pair.Value + " ";
            }
        }

        if (req.Equals("Sucess"))
        {
            if (resCode.Equals("00"))
            {
                if (!data[0]["ARA_REQ_DVCD"].Equals("-1"))
                {
                    test.SetLog("\n가입성공??" +"\n");
                    // 가입성공시
                    SplayerPrefs.PlayerPrefsSave("ARA_MBRS_NATV_MGNO", data[0]["ARA_MBRS_NATV_MGNO"]);
                    SplayerPrefs.PlayerPrefsSave("CI_VAL", member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL));
                   // SplayerPrefs.PlayerPrefsSave("MemberHash", member_info.GetValue(_Member_info.KEY_NAME.ARA_MBRS_HASH_VAL));
                    SplayerPrefs.PlayerPrefsSave("AutoLogin", 1);

                    uiManager.SetMemberInfo(member_info);
                    Compelet_JoinPage.SetActive(true);
                }
               
            }
            else
            {
                alert_Manager.ShowAlert("SNS 회원가입도중 오류가 발생하였습니다.");
            }
        }
        else
        {
            alert_Manager.ShowAlert("[" + req + "] SNS 회원가입도중 오류가 발생하였습니다.");
        }
        //  PlayerPrefs.SetInt("MemberUID",????);
        //

    }
    public void NoticeCreateCallBack(string req, string resCode, List<Dictionary<string, string>> data)
    {
        Debug.Log(req);
        test.SetLog("\nJoinAddSNS_SubmitCallback" + resCode + "\n");
        string contents = "";
        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

                test.SetLog(pair.Key + " : " + pair.Value + "\n");
                contents += pair.Key + " : " + pair.Value + " ";
            }
        }
        if (req.Equals("Sucess"))
        {
            if (resCode.Equals("00"))
            {
                if (!data[0]["ARA_REQ_DVCD"].Equals("-1"))
                {
                    Debug.Log("알림등록 오류");
                }

            }
            else
            {
              
            }
        }
        else
        {
          
        }


    }

    public void TermsShow(int index)
    {
        if (Terms == null)
        {
            Terms = Instantiate(TermsPrefabs[index], new Vector3(0, 0, 0), Quaternion.identity, (Agree_area_2.activeSelf)?Agree_area_2.transform: TermsParent); //오브젝트 생성.

            Terms.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            Terms.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        }
       


    }
    public void TermsClose()
    {
        Debug.Log("TermsClose");
        if (Terms != null)
        {
            Destroy(Terms);
        }
    }


    public void Children_1_Click()
    {
        if (!Agree_area_2.activeSelf)
        {
            Agree_area_2.SetActive(true);
        }
    }



    public void Complete_Play()
    {
        uiManager.AutoLoginSceneStart();
    }

    public void Keyboard_out()
    {
        uiManager.Keyboard_out();
    }

    Vector2 Field_TempPos=Vector2.zero;
    bool isFieldMove = false;
    public void EmailField_Select()
    {
        if(step_type != STEP_TYPE.ID_Field && !isFieldMove)
        {
            isFieldMove = true;
            Field_TempPos = Field_NextGroup.GetComponent<RectTransform>().anchoredPosition;
            Field_NextGroup.GetComponent<RectTransform>().anchoredPosition = NextTargetStartPos;
        }
    }
    public void EmailField_Dselect()
    {
        if (step_type != STEP_TYPE.ID_Field && Field_TempPos !=Vector2.zero)
        {
             Field_NextGroup.GetComponent<RectTransform>().anchoredPosition= Field_TempPos;
             Field_TempPos = Vector2.zero;
             isFieldMove = false;

        }
    }
  
    private void OnEnable()
    {
        targetStartPos = FieldGroup.GetComponent<RectTransform>().anchoredPosition;
        NextTargetStartPos = Field_NextGroup.GetComponent<RectTransform>().anchoredPosition;
        targetNowPos = targetStartPos;
        NextTargetNowPos = NextTargetStartPos;
        FieldGroup.transform.GetChild(0).gameObject.SetActive(true);
        FieldGroup.transform.GetChild(0).GetComponentInChildren<InputField>().ActivateInputField();

      

    }
    private void OnDisable()
    {
        isFieldMove = false;
        Field_TempPos = Vector2.zero;
        step_type = STEP_TYPE.Name_Field;
        FieldGroup.GetComponent<RectTransform>().anchoredPosition = targetStartPos;
        isJoinEnd = false;
          isParentComplete = false;
        isChildren = false;
         isIDCheckComplete = false;
        isPassCheckComplete = false;
        Mobile_SelectBox.GetComponentInChildren<ToogleAction>().ToggleInit();

        InputField[] dd= gameObject.GetComponentsInChildren<InputField>();
        for(int i  =0; i<dd.Length; i++)
        {
            Debug.Log(dd[i].gameObject.name);
            dd[i].text = "";
          
        }

        for (int i = 0; i < FieldGroup.transform.childCount; i++) {
            FieldGroup.transform.GetChild(i).gameObject.SetActive(false);
       }
        //입력된정보 전부 초기화 작성하기

        Field_NextGroup_P.SetActive(false);
        Compelet_JoinPage.SetActive(false);

    }
}
