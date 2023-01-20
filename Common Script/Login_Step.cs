using Defective.JSON;
using Firebase;
using Firebase.Auth;
using Google;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Login_Step : MonoBehaviour
{
    public UIManager uiManager;
    public Alert_Manager alert_Manager;
    public GameObject LoginEmail;
    public UnityWeb unityWeb;
   
    bool isJoin=false;
    Firebase.Auth.FirebaseAuth auth = null;
    Firebase.Auth.FirebaseUser user;
    private GoogleSignInConfiguration configuration;
    bool isLogin = false;


    [Header("Email Login")]
    public InputField EmailID;
    public InputField Password;
    public GameObject DoMainSelectBox;
    public GameObject DoMainCustom;
    string EmaildoMain = "naver.com";

    [Header("ID/PASSFind")]
    public GameObject ID_Find;
    public GameObject PassFind;



    private Dictionary<string, string> SNSUserData = new Dictionary<string, string>();



    [Header("Google")]
    public string webClientId; //= "177640853063-biksv8ne6jt4oqsq5p7oal9hqhhvgvra.apps.googleusercontent.com";
    [Space()]

    [Header("Naver")]
    public string NaverClientId; //= "ez48PBlDP2CqK5mh4y4i";
    public string NaverClientSecret; //= "eKkda08Iew";

    // Start is called before the first frame update



    private void Awake()
    {
        
        GameObject temp_manager = GameObject.FindGameObjectWithTag("UIManager");
        uiManager = temp_manager.GetComponent<UIManager>();
       
        alert_Manager = temp_manager.GetComponent<Alert_Manager>();
        unityWeb = temp_manager.GetComponent<UnityWeb>();

        //테스트용
        test = GameObject.Find("testViewLog").GetComponent<TestviewLog>();
    }

    void Start()
    {
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
                auth.StateChanged += Auth_StateChanged;
                Auth_StateChanged(this, null);
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                Debug.LogError(System.String.Format(
                   "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
        
    }
    private void OnDisable()
    {
        EmaildoMain = "naver.com";
        EmailID.text = "";
        Password.text = "";
        DoMainCustom.GetComponent<InputField>().text = "";
        DoMainCustom.SetActive(false);


    }

    //이메일 로그인관련start
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
    public void UserEmail_Select(string domain)
    {
        EmaildoMain = domain;
        if (DoMainCustom.activeSelf)
        {
            DoMainCustom.GetComponent<InputField>().text = "";
            DoMainCustom.SetActive(false);
        }
    }
    public void UserEmail_onCustom(GameObject selectBox)
    {
        DoMainCustom.SetActive(true);
        selectBox.SetActive(false);
        EmaildoMain = "";
        
       
    }
    public void Email_Login()
    {
        LoginEmail.SetActive(true);
    }
    public void EmailJoin()
    {
        uiManager.EmailJoin();
    }
    public void Email_LoginSubmit()
    {
        if (DoMainCustom.activeSelf)
        {
            EmaildoMain = DoMainCustom.GetComponent<InputField>().text;
        }
        if (Validation.ID_Check(EmailID.text))
        {
            if (Validation.EmailCheck(EmailID.text + EmaildoMain))
            {
                if (Password.text.Length == 0)
                {
                    alert_Manager.ShowAlert("비밀번호를 입력해주세요");
                }
                else
                {
                    List<string> key = new List<string>();
                    List<string> value = new List<string>();

                    key.Add("ARA_MBRS_EMLADR");
                    key.Add("ARA_MBRS_PSWD");

                    value.Add(EmailID.text + "@" + EmaildoMain);
                    value.Add(Password.text);
                    unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.MemberLogin, URLs.Menu.Join, key, value, LoginCallback);

                }
            }
            else
            {
                Debug.Log(EmailID.text + "@" + EmaildoMain);
                alert_Manager.ShowAlert("이메일형식을 확인해주세요");
            }
        }
        else
        {
            alert_Manager.ShowAlert("아이디를 확인해주세요");
        }
    }
    public void LoginCallback(string req, string resCode, List<Dictionary<string, string>> data)
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
                if (!data[0]["ARA_MBRS_EMLADR"].Equals("-1"))
                {
                    
                        uiManager.SetMemberInfo("ARA_MBRS_EMLADR", data[0]["ARA_MBRS_EMLADR"]);//회원 이메일
                        uiManager.SetMemberInfo("ARA_MBRS_NM", data[0]["ARA_MBRS_NM"]);//회원 이름
                        uiManager.SetMemberInfo("ARA_MBRS_CI_VAL", data[0]["ARA_MBRS_CI_VAL"]);//회원 CI_VAL
                        uiManager.SetMemberInfo("ARA_MBRS_HASH_VAL", data[0]["ARA_MBRS_HASH_VAL"]);//회원 자동로그인 해시
                        uiManager.SetMemberInfo("ARA_MBRS_RRNO", data[0]["ARA_MBRS_RRNO"]);//회원 주민번호 암호화
                        uiManager.SetMemberInfo("ARA_SNS_NATV_NO", data[0]["ARA_SNS_NATV_NO"]);//회원 SNS 고유번호
                        uiManager.SetMemberInfo("ARA_LOGIN_DVCD", data[0]["ARA_LOGIN_DVCD"]);//회원 구분코드 (SNS)
                        uiManager.SetMemberInfo("ARA_MBRS_SEX_DVCD", data[0]["ARA_MBRS_SEX_DVCD"]);//회원 성별

                        uiManager.SetMemberInfo("MMT_TCCCO_DVCD", data[0]["MMT_TCCCO_DVCD"]);//회원 통신사
                        uiManager.SetMemberInfo("ARA_MBRS_MPNO", data[0]["ARA_MBRS_MPNO"]);//회원 전화번호     
                        uiManager.SetMemberInfo("ARA_EVNT_AND_AD_LEME_YN", data[0]["ARA_EVNT_AND_AD_LEME_YN"]);//회원 이벤트알림여부
                                                                                                               //   uiManager.SetMemberInfo("ARA_MBNK_LNK_YN", data[0]["ARA_MBNK_LNK_YN"]);//회원 모바일 연동여부
                        uiManager.SetMemberInfo("ARA_SV_U_LEME_YN", data[0]["ARA_SV_U_LEME_YN"]);//회원서비스이용 알림여부
                                                                                                 // uiManager.SetMemberInfo("ARA_MBRS_STCD", data[0]["ARA_MBRS_STCD"]);//회원 상태코드

                        SplayerPrefs.PlayerPrefsSave("ARA_MBRS_NATV_MGNO", data[0]["ARA_MBRS_NATV_MGNO"]);
                        SplayerPrefs.PlayerPrefsSave("CI_VAL", data[0]["ARA_MBRS_CI_VAL"]);
                        SplayerPrefs.PlayerPrefsSave("MemberHash", data[0]["ARA_MBRS_HASH_VAL"]);
                        LoginEmail.GetComponent<UI_Control>().CloseUI();
                        uiManager.AutoLoginSceneStart();
                    
                  
                }
                else
                {
                    alert_Manager.ShowAlert("아이디와 비밀번호가 일치하지 않습니다");
                }

         

            }
            else
            {
                alert_Manager.ShowAlert("로그인중 오류가 발생하였습니다.");

            }
        }
    }
    //이메일로그인관련 End

   




    private void Auth_StateChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                test.SetLog("Signed out " + user.UserId+"\n");

            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                test.SetLog("Signed in " + user.UserId+"\n");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isJoin)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (ID_Find.activeSelf)
                    {
                        ID_Find.GetComponent<ID_Find>().BackProcess();
                    }
                    else if (PassFind.activeSelf)
                    {
                        PassFind.GetComponent<Pass_Find>().BackProcess();
                    }
                    else if (LoginEmail.activeSelf)
                    {
                        if (!LoginEmail.GetComponent<UI_Control>().isClose)
                        {
                            LoginEmail.transform.GetChild(1).Find("close_btn").GetComponent<Button>().onClick.Invoke();
                        }
                    }
                    else
                    {
                        uiManager.onAppExitAlert();
                    }
                }

            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (ID_Find.activeSelf)
                    {
                        ID_Find.GetComponent<ID_Find>().BackProcess();
                    }
                    else if (PassFind.activeSelf)
                    {
                        PassFind.GetComponent<Pass_Find>().BackProcess();
                    }
                    else if (LoginEmail.activeSelf)
                    {
                        if (!LoginEmail.GetComponent<UI_Control>().isClose)
                        {
                            LoginEmail.transform.GetChild(1).Find("close_btn").GetComponent<Button>().onClick.Invoke();
                        }
                    }
                    else
                    {
                        uiManager.onAppExitAlert();
                    }
                }
            }
        }
    }
    public void EmailLoginInit()
    {
        InputField[] infid = LoginEmail.GetComponentsInChildren<InputField>();
        for(int i =0; i<infid.Length; i++)
        {
            infid[i].text = "";
        }
    }
    public void JoinOpen()
    {
        isJoin = true;
    }
    public void JoinClose()
    {
        isJoin = false;
    }


    AndroidJavaObject pluginInstance;
    public TestviewLog test;

    public void NaverLogin()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                try
                {
                    
                    var pluginClass = new AndroidJavaClass("com.android.naverlogin.NaverLoginPlugin");
                    pluginInstance = pluginClass.CallStatic<AndroidJavaObject>("instance", "Manager", "NaverLoginCallBack");

                   //  NaverClientId = "ez48PBlDP2CqK5mh4y4i";
                   //  NaverClientSecret = "eKkda08Iew";

                    pluginInstance.Call("initData", NaverClientId, NaverClientSecret, "부산은행 테스트용");
                    pluginInstance.Call("SendNaverLogin", "GetLoginToken");

                   Debug.Log("352:"+pluginInstance.Call<string>("GetCallBackName"));

                 
                }
                catch (Exception e)
                {
                    uiManager.ErrorReport("[네이버로그인오류] "+e.ToString());
                    alert_Manager.ShowAlert("로그인도중 오류가 발생했습니다.");
                }
            }
            else
            {
                alert_Manager.ShowAlert("네트워크상태를 확인해주세요");
            }


        }
    }
    public void testLog(string msg)
    {
        Debug.Log("testLog:::::" + msg);
    }
    public void GetLoginToken(string msg)
    {
        Debug.Log(msg);
        Debug.Log("\n"+msg.Contains("errorCode")+"\n");

        if (!msg.Contains("errorCode"))
        {

            SNSUserData.Clear();
            SNSUserData.Add("idToken", msg);
            Debug.Log("\nLoginApi\n");
            //msg token
            try
            {
                pluginInstance.Call("LoginApi");

            }catch(Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }
    public void NaverTokenLogin()
    {
       
    }
    public void googleLogout(string msg)
    {
        test.SetLog("\ngoogle_logout_callback\n");
    }
    public void NaverLoginCallBack(string msg)
    {
        Debug.Log(msg);
        try
        {
            var jsonObject = new JSONObject(msg);
            Dictionary<string, string> data = jsonData(jsonObject);
            Dictionary<string, string> response = new Dictionary<string, string>();
            test.SetLog("\n\ndata_value\n");
           /* foreach (KeyValuePair<string,string> pair in data)
            {
                test.SetLog(pair.Key+" "+pair.Value);
            }*/

            if (data["resultcode"].Equals("00"))
            {
                
                response = jsonData(new JSONObject(data["response"]));
                test.SetLog("\n\n\nresponse:" + response["mobile"]);
                string token = pluginInstance.Call<string>("GetToken");
               
                SNSUserData.Add("UserId", response["id"]);
                SNSUserData.Add("SNSType", "02");
                test.SetLog("\n\nJoin Go:id[" + SNSUserData["UserId"] +"] Token["+ SNSUserData["idToken"] + "]");


                List<string> key = new List<string>();
                key.Add("ARA_SNS_NATV_NO");
                List<string> value = new List<string>();
                value.Add(SNSUserData["UserId"]);
                //아이디값 디비확인후 join or login
                //가입중단시 로그아웃넣기
                unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.MemberLogin, URLs.Menu.Join, key, value, SNSMemberCheckCallback);


            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private Dictionary<string,string> jsonData(JSONObject json)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();

        switch (json.type)
        {
            case JSONObject.Type.Object:
                    
                for(var i=0; i<json.list.Count; i++)
                {
                    data.Add(json.keys[i], TypeSearch(json.list[i]));
                }

                break;
        }

        return data; 
    }
    string TypeSearch(JSONObject value)
    {
        string tempValue = "";
        switch (value.type)
        {
            case JSONObject.Type.String:
                tempValue = value.stringValue.ToString();
                break;
            case JSONObject.Type.Number:
                tempValue = value.floatValue.ToString();
                break;
            case JSONObject.Type.Bool:
                tempValue = value.boolValue.ToString();
                break;
            case JSONObject.Type.Null:
                tempValue = "";
                break;
            case JSONObject.Type.Baked:
                tempValue = value.stringValue.ToString();
                break;
            case JSONObject.Type.Object:
                tempValue = value.ToString();
                break;
        }
        return tempValue;
    }






    public void GoogleLogin()
    {
        SignInWithGoogle();

    }
    private void SignInWithGoogle()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        try
        {
           
            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);

        }catch(Exception e)
        {
            Debug.Log("google_login_실패: " + e);
            alert_Manager.ShowAlert("구글 SNS 로그인에 실패하였습니다.\n다시 시도해주세요!");
            uiManager.SNS_LoadingPageDestroy();

        }
    }
    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.Log("Got Error: " + error.Status + " " + error.Message + "\n");
                }
                else
                {
                    Debug.Log("Got Unexpected Exception?!?" + task.Exception + "\n");
                    Debug.Log("Got Unexpected Exception?!?" + task.Exception + "\n");
                }
            }
            uiManager.SNS_LoadingPageDestroy();
        }
        else if (task.IsCanceled)
        {
            uiManager.SNS_LoadingPageDestroy();
        }
        else
        {

            Debug.Log("Welcome: " + task.Result.DisplayName+"\n");
            Debug.Log("Email = " + task.Result.Email + "\n");
            Debug.Log("task.Result.IdToken size:" +task.Result.IdToken.Length+"\n");
            Debug.Log("SignInWithGoogleOnFirebase\n");
            try
            {

                SignInWithGoogleOnFirebase(task.Result.IdToken);
            }catch(Exception e)
            {
                Debug.Log(e.ToString());
                uiManager.SNS_LoadingPageDestroy();
            }


        }
    }
   
    private void SignInWithGoogleOnFirebase(string idToken)
    {
        SNSUserData.Clear();
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                    test.SetLog("\nError code = " + inner.ErrorCode + " Message = " + inner.Message + "\n");
             
            }
            else
            {
                test.SetLog("\nSign In Successful." + "\n");
                test.SetLog("phone_number" +task.Result.PhoneNumber + "\n");
                test.SetLog("ProviderId" + task.Result.ProviderId + "\n");
               
                test.GetComponent<TestviewLog>().SetLog("user_id" + task.Result.UserId);

                test.SetLog("user_id_Length:" + task.Result.UserId.Length + "\n");

                SNSUserData.Add("UserId", task.Result.UserId);
                SNSUserData.Add("idToken", idToken);
                SNSUserData.Add("SNSType", "01");

                List<string> key = new List<string>();
                key.Add("ARA_SNS_NATV_NO");

                List<string> value = new List<string>();
                value.Add(task.Result.UserId);

                unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.MemberLogin, URLs.Menu.Join, key, value, SNSMemberCheckCallback);

            }
        });
    }
   
    public void SNSMemberCheckCallback(string req,string resCode, List<Dictionary<string, string>> data)
    {

        uiManager.SNS_LoadingPageDestroy();
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

        if (data[0].ContainsKey("resCode")) {
            alert_Manager.ShowAlert("문제가 발생했습니다.\n["+data[0]["error.code"]+"] "+data[0]["error.message"]);
        }
        else
        {
            if (!data[0]["ARA_MBRS_NATV_MGNO"].Equals("-1"))// 회원임 
            {
                Debug.Log("회원임");

                uiManager.SetMemberInfo("ARA_MBRS_EMLADR", data[0]["ARA_MBRS_EMLADR"]);//회원 이메일
                uiManager.SetMemberInfo("ARA_MBRS_NM", data[0]["ARA_MBRS_NM"]);//회원 이름
                uiManager.SetMemberInfo("ARA_MBRS_CI_VAL", data[0]["ARA_MBRS_CI_VAL"]);//회원 CI_VAL
                uiManager.SetMemberInfo("ARA_MBRS_HASH_VAL", data[0]["ARA_MBRS_HASH_VAL"]);//회원 자동로그인 해시
                uiManager.SetMemberInfo("ARA_MBRS_RRNO", data[0]["ARA_MBRS_RRNO"]);//회원 주민번호 암호화
                uiManager.SetMemberInfo("ARA_SNS_NATV_NO", data[0]["ARA_SNS_NATV_NO"]);//회원 SNS 고유번호
                uiManager.SetMemberInfo("ARA_LOGIN_DVCD", data[0]["ARA_LOGIN_DVCD"]);//회원 구분코드 (SNS)
                uiManager.SetMemberInfo("ARA_MBRS_SEX_DVCD", data[0]["ARA_MBRS_SEX_DVCD"]);//회원 성별

                uiManager.SetMemberInfo("MMT_TCCCO_DVCD", data[0]["MMT_TCCCO_DVCD"]);//회원 통신사
                uiManager.SetMemberInfo("ARA_MBRS_MPNO", data[0]["ARA_MBRS_MPNO"]);//회원 전화번호     
                uiManager.SetMemberInfo("ARA_EVNT_AND_AD_LEME_YN", data[0]["ARA_EVNT_AND_AD_LEME_YN"]);//회원 이벤트알림여부
                                                                                                       //   uiManager.SetMemberInfo("ARA_MBNK_LNK_YN", data[0]["ARA_MBNK_LNK_YN"]);//회원 모바일 연동여부
                uiManager.SetMemberInfo("ARA_SV_U_LEME_YN", data[0]["ARA_SV_U_LEME_YN"]);//회원서비스이용 알림여부
                                                                                         // uiManager.SetMemberInfo("ARA_MBRS_STCD", data[0]["ARA_MBRS_STCD"]);//회원 상태코드

                SplayerPrefs.PlayerPrefsSave("ARA_MBRS_NATV_MGNO", data[0]["ARA_MBRS_NATV_MGNO"]);
                SplayerPrefs.PlayerPrefsSave("CI_VAL", data[0]["ARA_MBRS_CI_VAL"]);
                SplayerPrefs.PlayerPrefsSave("MemberHash", data[0]["ARA_MBRS_HASH_VAL"]);
                if (pluginInstance != null) pluginInstance.Call("SendNaverLogout");
                uiManager.AutoLoginSceneStart();



            }
            else //회원이 아님
            {
                Debug.Log("회원아님");
                if (pluginInstance != null)
                {
                    pluginInstance.Call("SendNaverLogout");
                }
                Debug.Log("SNSType = " + SNSUserData["SNSType"] + " UserId" + SNSUserData["UserId"] + "   idToken" + SNSUserData["idToken"]);
                uiManager.SNSMemberJoin(SNSUserData["SNSType"], SNSUserData["UserId"], SNSUserData["idToken"]);

               
            }
        }


       /* for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

                test.SetLog(pair.Key + " : " + pair.Value + "\n");
                contents += pair.Key + " : " + pair.Value + " ";
            }
        }*/
    }
   

    void OnApplicationQuit()
    {

        
    }
  
}
