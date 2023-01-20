using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTopUI : MonoBehaviour
{
    public UIManager ui_manager;
    public GameObject OtherPageScroll;
 
    public GameObject MemberOut_page;
    public GameObject OtherPage;
    [SerializeField]
    public GameObject Reward_page;
    [SerializeField]
    private GameObject bottom_menu;
    [SerializeField]
    private GameObject MemberOutScreen;
    [SerializeField]
    private GameObject PointGetPage;
    [SerializeField]
    private GameObject Banner;


    // Start is called before the first frame update

    private void Awake()
    {
        GameObject temp_manager = GameObject.FindGameObjectWithTag("UIManager");
        ui_manager = temp_manager.GetComponent<UIManager>();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameObject.FindGameObjectWithTag("NotMember") != null)
                {
                    GameObject.FindGameObjectWithTag("NotMember").GetComponent<UI_Control>().CloseUI();
                }
                else if (PointGetPage.activeSelf)
                {
                    PointGetPage.GetComponent<UI_Control>().CloseUI();
                }
                else if (OtherPage.GetComponent<MainOtherPageControl>().isActivePage())
                {
                    OtherPage.GetComponent<MainOtherPageControl>().CloseActivePage();
                }else if (OtherPage.activeSelf && ui_manager.GetContainsScene("other_page"))
                {
                    ui_manager.onAppExitAlert();
                }
            }
        }
    }

    public void onReward_Click()
    {
        if (ui_manager.isLoginState())
        {
            OtherPage.GetComponent<MainOtherPageControl>().OnSetActivePage(Reward_page);
            
           
        }
        else
        {
            ui_manager.NotMemberAlertOpen("PlayTopUI");
        }
    }
    public void RewardGetPage()
    {
        PointGetPage.SetActive(true);
    }
    public void Reward_Tester_Alert()
    {
        ui_manager.GetComponent<Alert_Manager>().ShowAlert("알림", "서버작업중 입니다.");
    }
    /// <summary>
    /// MainMenu_Control
    /// </summary>

    public void StartSceneLoading(int index)
    {
        ui_manager.StartSceneLoading(index);
    }
    public void MenuUnActive()
    {
        ui_manager.MenuUnActive();
    }
    public void MenuActive(int index)
    {
        ui_manager.MenuActive(index);
    }

    public void chikapokaUnload()
    {
        ui_manager.chikapokaUnload();
    }
    public void MenuUnLoad()
    {
        ui_manager.MenuUnLoad();
    } 
   public void MenuLoadScene(int index)
    {
        ui_manager.MenuLoadScene(index);
    }
    public void ShowAlert(string text)
    {
        ui_manager.gameObject.GetComponent<Alert_Manager>().ShowAlert(text);
    }


    public bool isLoginState()
    {
        return ui_manager.isLoginState();
    }




    public void MemberOut()
    {
        ui_manager.MemberOut();
    }

    public void SetAutoLoginState(bool isOn)
    {
        ui_manager.SetAutoLoginState(isOn);
    }




    //UI_Manager Receive

    public void Bottom_menuActive(bool isOn)
    {
        bottom_menu.SetActive(isOn);
    }
    public void Bottom_menuSendMessage(string msg)
    {
        bottom_menu.SendMessage(msg);
    }
    public MainMenu_Control Bottom_MainMenu_Control()
    {
        return bottom_menu.GetComponent<MainMenu_Control>();
    }


    public void MemberOutScreen_Active(bool isOn)
    {
        MemberOutScreen.SetActive(isOn);
    }


    public void ContentsMove(int index)
    {
        bottom_menu.GetComponent<MainMenu_Control>().SetButtonEvent(index);
    }

    public void BannerSelect()
    {
        List<string> key = new List<string>();
        List<string> value = new List<string>();

        key.Add("ARA_PRCDG_BANNER_LOCAT_DVCD");
        key.Add("ARA_PRCDG_BANNER_PROCE_DVCD");

        value.Add("");
        value.Add("");

        ui_manager.gameObject.GetComponent<UnityWeb>().WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.OhterPageBanner, URLs.Menu.otherPagebanner, key, value, OhterPageBannerCallback);
    }


    public void PageViewLog(string index_value)
    {
        List<string> key = new List<string>();
        List<string> value = new List<string>();

        key.Add("ARA_CTS_DVCD");
        value.Add(index_value);

        ui_manager.gameObject.GetComponent<UnityWeb>().WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.PageViewLog, URLs.Menu.pageViewLog, key, value, PageViewLogCallback);
    }
    private void OhterPageBannerCallback(string req, string resCode, List<Dictionary<string, string>> data)
    {
        Debug.Log(req);

        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);


            }
        }
        if (req.Equals("Sucess"))
        {
            if (resCode.Equals("00"))
            {
                if (!data[0]["ARA_PRCDG_BANNER_RESULT_LIST"].Equals("RESULT IS NULL")) {
                    List<string> ImageUrl = new List<string>();
                    for (int i = 0; i < data.Count; i++)
                    {
                        ImageUrl.Add(data[i]["IMG_URL"]);
                    }
                    ui_manager.gameObject.GetComponent<UnityWeb>().WebGetTexture(UnityWeb.SEND_URL.Busan, ImageUrl, GetTextureCallback);
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
    private void GetTextureCallback(List<Texture> texture)
    {
        Banner.GetComponent<banner>().SetTexture(texture);
    }

    private void PageViewLogCallback(string req, string resCode, List<Dictionary<string, string>> data)
    {
        Debug.Log(req);

        for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);


            }
        }
        if (req.Equals("Sucess"))
        {
            if (resCode.Equals("00"))
            {
               
            }
            else
            {
               
            }
        }
        else
        {
   
        }
    }

    private void OnDisable()
    {
       
    }

}
