using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class KCB_Control : MonoBehaviour
{
    public enum USE_TYPE
    { 
    
       ID_FIND,
       PASSWORD_FIND
    
    }
    [Header("Send Target/ Submit Func")]
    public GameObject SendTarget;
    public GameObject Submit;
    [Header("Common")]
    public GameObject uiManager;
  //  public UnityWeb unityWeb;
  //  public Alert_Manager alert_manager;
    public InputField Num;
    [Header("KCB PAGE")]
    public GameObject certification_Page ;
    public GameObject Certify_Page;
    public GameObject KCBBtn;
    private bool isOn = false;


    private Dictionary<string, string> KCB_Send_UserData = new Dictionary<string, string>();
    // Start is called before the first frame update
    private string certifocation = "";
    private string phoneNum="";
    private string tras_seq_value = "";
    private string CI_VAL = "";
    private string UserName = "";
    private string UserEmail = "";
    private string UserRegNum = "";
    public USE_TYPE useType;

    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager");
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isOn)
            {
                BackProcess();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isOn)
            {
                Debug.Log("KCB_Control BackPress");
                BackProcess();
            }
        }
    }
    public void BackProcess()
    {
        if (Certify_Page.activeSelf)
        {
            gameObject.GetComponent<Active_control>().UnActive();
            isOn = false;

        }
        else if (certification_Page.activeSelf)
        {
            certification_Page.GetComponent<UI_Control>().CloseUI();
            isOn = false;
        }
    }
    

    public void SetSendTarget(GameObject target,USE_TYPE _use_Type)
    {
        SendTarget = target;

        Submit.GetComponent<Button>().onClick.RemoveAllListeners();
        if (_use_Type == USE_TYPE.ID_FIND)
        {
            Submit.GetComponent<Button>().onClick.AddListener(GetFindID);
        }
        else if (_use_Type == USE_TYPE.PASSWORD_FIND)
        {
            Submit.GetComponent<Button>().onClick.AddListener(GetFindPass);

        }
    }
    public void SetSendTarget(GameObject target,string email ,USE_TYPE _use_Type)
    {
        SendTarget = target;
        UserEmail =email;
        Submit.GetComponent<Button>().onClick.RemoveAllListeners();
        if (_use_Type == USE_TYPE.ID_FIND)
        {
            Submit.GetComponent<Button>().onClick.AddListener(GetFindID);
        }
        else if (_use_Type == USE_TYPE.PASSWORD_FIND)
        {
            Submit.GetComponent<Button>().onClick.AddListener(GetFindPass);

        }
    }

    public void SetUserData(string name,string regNum)
    {
        KCB_Send_UserData = KCB_SendData(name, regNum);
        Certify();
    }

    public void ID_Password_Find(string _phoneNum, string _certifocation)
    {
        
        isOn = true;
        phoneNum = _phoneNum;
        certifocation = _certifocation;
        List<string> key = new List<string>();
        List<string> value = new List<string>();

        key.Add("ARA_MBRS_MPNO");
        key.Add("MMT_TCCCO_DVCD");

        value.Add(_phoneNum);
        value.Add(_certifocation);

        uiManager.GetComponent<UnityWeb>().WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.ID_Find_DataRequest, URLs.Menu.Member, key, value, BeforDataCallback);

        
    }
    public void BeforDataCallback(string req, string resCode, List<Dictionary<string, string>> data)
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
                gameObject.SetActive(false);
            }
            else
            {
                SendTarget.SendMessage("KCB_SendData", "NotFound");
                gameObject.GetComponent<Active_control>().UnActive();
                // alert_manager.ShowAlert("????????? ?????????????????????.");
                gameObject.SetActive(false);
            }

        }
        else if (req.Equals("Sucess") && resCode.Equals("00"))
        {
            if (data[0].ContainsKey("ARA_MBRS_NM"))
            {
                transform.GetChild(1).gameObject.SetActive(true);
                UserName = data[0]["ARA_MBRS_NM"];
                UserRegNum = data[0]["ARA_MBRS_RRNO"];
                SetUserData(UserName, data[0]["ARA_MBRS_RRNO"]);
            }
            else
            {
                uiManager.GetComponent<Alert_Manager>().ShowAlert("?????????/???????????? ?????? ????????? ?????????????????????.");
                gameObject.SetActive(false);
            }
        }
    }

  












    public void Certify()
    {
        //  TB_TYPE.M

     
        List<string> key = new List<string>();
        List<string> value = new List<string>();
 
        foreach(KeyValuePair<string,string> pair in KCB_Send_UserData)
        {
            key.Add(pair.Key);
            value.Add(pair.Value);
        }
        // key.Add(pair.Key);
        //  value.Add(pair.Value);




        //???????????? ?????????????????? SetActive Callback?????? ?????????
        // Certify_Page.SetActive(true);
        //????????? ?????? ?????? 
        uiManager.GetComponent<UnityWeb>().WebSend(TB_TYPE.KCB, UnityWeb.SEND_URL.Busan, URLs.Certify_Request, URLs.Menu.KCB, key, value, Certify_Callback);

    }

    public void Certify_Callback(string req, string resCode, List<Dictionary<string, string>> data)
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
                BackProcess();
            }
            else
            {
                uiManager.GetComponent<Alert_Manager>().ShowAlert("????????? ?????????????????????.");
                BackProcess();
            }

        }
        else if (req.Equals("Sucess") && resCode.Equals("00"))
        {
            tras_seq_value = data[0]["TRNS_SEQ"];
            gameObject.GetComponent<Active_control>().SetActive(0);
        
        }
    }
    public Dictionary<string, string> KCB_SendData(string name, string regNum)
    {

        Dictionary<string, string> temp_data = new Dictionary<string, string>();


        temp_data.Add("CMD_SVC_CD", "01");
        temp_data.Add("KCB_CUST_NM", name);
        temp_data.Add("KCB_RRNO1", regNum.Substring(0,6));
        temp_data.Add("KCB_RRNO3", regNum.Substring(6,1));

        string carrier = certifocation;
        if(carrier.Contains("?????????"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "55");
        }
        else if (carrier.Contains("SKT") || carrier.Equals("KT") || carrier.Equals("LG U+"))
        {
            if (carrier.Contains("SKT")) temp_data.Add("KCB_TCCCO_DVCD", "01");
            else if (carrier.Equals("KT")) temp_data.Add("KCB_TCCCO_DVCD", "02");
            else if (carrier.Equals("LG U+")) temp_data.Add("KCB_TCCCO_DVCD", "03");
        }
        else
        {
            temp_data.Add("KCB_TCCCO_DVCD", "55");
        }
        temp_data.Add("KCB_BRCH_NO", "836");
        temp_data.Add("KCB_MPNO", phoneNum);
        temp_data.Add("AGREE_1", "Y");
        temp_data.Add("AGREE_2", "Y");
        temp_data.Add("AGREE_3", "Y");
        temp_data.Add("KCB_PURP_SV_CD", "01");
        temp_data.Add("HSLF_CTF_RQST_RNCD", "00");

        return temp_data;
    }
    public Dictionary<string, string> KCB_Certify_SendNum(string num, string trans_seq)
    {

        Dictionary<string, string> temp_data = new Dictionary<string, string>();


        temp_data.Add("CMD_SVC_CD", "01");
        temp_data.Add("KCB_CUST_NM", name);


        string carrier = certifocation;
        if (carrier.Equals("SKT"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "01");
        }
        else if (carrier.Equals("KT"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "02");
        }
        else if (carrier.Equals("LG U+"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "03");
        }
        else
        {
            temp_data.Add("KCB_TCCCO_DVCD", "55");
        }
        temp_data.Add("KCB_RQST_NO", num);
        temp_data.Add("TRNS_SEQ", trans_seq);

        temp_data.Add("KCB_MPNO", phoneNum);
        temp_data.Add("KCB_BRCH_NO", "836");
        temp_data.Add("KCB_PURP_SV_CD", "01");
        temp_data.Add("ARA_FLAG", "Y");
        temp_data.Add("HSLF_CTF_RQST_RNCD", "00");

        return temp_data;

    }
    public void Certify_SendNum(string num)
    {

        Dictionary<string, string> temp_data = KCB_Certify_SendNum(num, tras_seq_value);
        List<string> key = new List<string>();
        List<string> value = new List<string>();
        int count = 0;
        foreach (KeyValuePair<string, string> pair in temp_data)
        {
            key.Add(pair.Key);
            value.Add(pair.Value);
            count++;
        }
        // ??????????????? ??????
        uiManager.GetComponent<UnityWeb>().WebSend(TB_TYPE.KCB, UnityWeb.SEND_URL.Busan, URLs.Certify_Num, URLs.Menu.KCB, key, value, Certify_Num_Callback);

        // ????????????



        //   Mobile_Certify.GetComponent<Certify_Timer>().Timer_Stop();
        //???????????? CI????????? ???????????? ?????? ??????
        // member_info.SetValue(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL, "sdfjlksdfjslkfdjslk");
        //   KCBBtn.GetComponent<ButtonTransition>().Enble_btn();
        //?????? CI_VAL ??????dssd

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

               
                contents += pair.Key + " : " + pair.Value + " ";
            }
        }



        if (req.Equals("Sucess"))
        {
            if (resCode.Equals("00"))
            {
                if (data[0].ContainsKey("CI_VAL"))
                {
                   // data[0]["CI_VAL"]
                    Certify_Page.GetComponent<Certify_Timer>().Timer_Stop();

                    /*List<string> key = new List<string>();
                    List<string> value = new List<string>();

                    key.Add("ARA_MBRS_CI_VAL");
                    key.Add("ARA_MBRS_NM");*/
                    CI_VAL = data[0]["CI_VAL"];
                    // value.Add(name);
                    SendTarget.SendMessage("Send_CI_VAL", data[0]["CI_VAL"]);
                    KCBBtn.GetComponent<ButtonTransition>().Enble_btn();
                    //URLs.CI_Val_MemberCheck ??????????????????
                    //URLs.Menu.Join ??????????????????
                    //???????????? ????????????


                }
                else
                {
                    // uiManager.ErrorReport("CI_VAL Unkown");
                    SendTarget.SendMessage("KCB_SendData", "NotFound");
                    gameObject.GetComponent<Active_control>().UnActive();


                }
            }
            else
            {
                Certify_Page.GetComponent<Certify_Timer>().Fail_Num();

            }
        }
        else
        {

            uiManager.GetComponent<Alert_Manager>().ShowAlert("");


        }
    }

    public void GetFindID()
    {
        if (CI_VAL.Equals("") || CI_VAL.Length == 0)
        {
            Certify_InputCheck(Num.text);
        }
        else
        {
            List<string> key = new List<string>();
            List<string> value = new List<string>();
            key.Add("ARA_MBRS_CI_VAL");
            key.Add("ARA_MBRS_NM");
            value.Add(CI_VAL);
            value.Add(UserName);
            uiManager.GetComponent<UnityWeb>().WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.MemberCheckOrIDFind, URLs.Menu.Join, key, value, CI_OverLapCheckCallback);
        }

    }
    public void GetFindPass()
    {
        if (CI_VAL.Equals("") || CI_VAL.Length == 0)
        {
            Certify_InputCheck(Num.text);
        }
        else
        {
            List<string> key = new List<string>();
            List<string> value = new List<string>();
            key.Add("ARA_MBRS_NM");
            key.Add("ARA_MBRS_CI_VAL");
            value.Add(UserName);
            value.Add(CI_VAL);
            uiManager.GetComponent<UnityWeb>().WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.MemberCheckOrIDFind, URLs.Menu.Join, key, value, CI_PasswordMemberCallback);
        }

    }

    //CI_VAL ???????????? ??????
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
            Certify_Page.GetComponent<Certify_Timer>().Timer_Stop();
            //if (resCode.Equals("00") || )
            // {
            if (!data[0].ContainsKey("ARA_LOGIN_DVCD") || data[0]["ARA_MBRS_EMLADR"].Equals("-1"))
            {
                //????????????
                SendTarget.SendMessage("KCB_SendData", "NotFound");
                gameObject.GetComponent<Active_control>().UnActive();
               
                //????????? ??????

            }
            else
            {
                SendTarget.SendMessage("KCB_SendData", data);
                gameObject.GetComponent<Active_control>().UnActive();
               
                //????????????

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
               //
               //uiManager.ErrorReport(contents);
            }
            uiManager.GetComponent<Alert_Manager>().ShowAlert("????????? ?????????????????????. ");
          


        }
    }
    //CI_VAL ???????????? ??????
    private void CI_PasswordMemberCallback(string req, string resCode, List<Dictionary<string, string>> data)
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
            Certify_Page.GetComponent<Certify_Timer>().Timer_Stop();
            //if (resCode.Equals("00") || )
            // {
            if (!data[0].ContainsKey("ARA_LOGIN_DVCD"))
            {
                //????????????
                SendTarget.SendMessage("KCB_SendData", "NotFound");
                gameObject.GetComponent<Active_control>().UnActive();

                //????????? ??????

            }
            else
            {
                if (data[0]["ARA_MBRS_EMLADR"].Equals(UserEmail))
                {
                    SendTarget.SendMessage("KCB_SendData", data);
                   
                }
                else
                {
                    SendTarget.SendMessage("KCB_SendData", "NotFound");
                }
                gameObject.GetComponent<Active_control>().UnActive();
                //????????????

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
                //
                //uiManager.ErrorReport(contents);
            }
            uiManager.GetComponent<Alert_Manager>().ShowAlert("????????? ?????????????????????. ");



        }
    }


    public void Certify_timer()
    {
        if (!Certify_Page.GetComponent<Certify_Timer>().Timer_Start())
        {
            if (Certify_Page.GetComponent<Certify_Timer>().isCertifyComplete())
            {
                uiManager.GetComponent<Alert_Manager>().ShowAlert("????????? ?????????????????????.\n?????? ????????? ??????????????????.");
            }
            else
            {
                uiManager.GetComponent<Alert_Manager>().ShowAlert("????????? ????????? ?????????????????????.");
            }

        }
        else
        {
            uiManager.GetComponent<Alert_Manager>().ShowAlert("???????????? ???????????? ?????????????????????.");
            Certify();
        }
    }
    //input 6?????? ?????? ?????? 6????????? ??????
    public void Certify_InputCheck(string value)
    {
        if (value.Length == 6)
        {
            if (Certify_Page.GetComponent<Certify_Timer>().isTimeOut())
            {

                Certify_SendNum(value);


            }
            else
            {
                uiManager.GetComponent<Alert_Manager>().ShowAlert("?????? ??????????????? ?????????????????????.");
            }
        }

    }
    private void OnDisable()
    {
     KCB_Send_UserData.Clear();
    // Start is called before the first frame update
    certifocation="";
    phoneNum="";
    tras_seq_value="";
    CI_VAL="";
    UserName="";
    UserRegNum="";
        isOn = false;

        if (transform.GetChild(0).gameObject.activeSelf) transform.GetChild(0).gameObject.SetActive(false);
        if (transform.GetChild(1).gameObject.activeSelf) transform.GetChild(0).gameObject.SetActive(false);

    }

}
