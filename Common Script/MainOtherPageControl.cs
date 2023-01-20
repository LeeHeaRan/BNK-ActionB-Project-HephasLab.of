using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainOtherPageControl : MonoBehaviour
{
    public GameObject ScrollContent;
    public PlayTopUI playTopUI;
    [Header("Mapping Member Info")]
    public Text UserName;
    public Text Reward_count;
    public Toggle AutoLogin;
    public string rewardCount;
    [SerializeField]
    private GameObject ActivePage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        Vector2 tempvec2 = ScrollContent.GetComponent<RectTransform>().anchoredPosition;
        tempvec2.y = 0;
        ScrollContent.GetComponent<RectTransform>().anchoredPosition = tempvec2;
    }


    public void OnSetActivePage(GameObject page)
    {
        ActivePage = page;
        ActivePage.SetActive(true);
    }

    public bool isActivePage()
    {
        return (ActivePage==null)?false:true;
    }

    public void CloseActivePage()
    {
        ActivePage.SendMessage("CloseUI");
        ActivePage = null;
    }

    public void Reward_countCallback(string req, string resCode, List<Dictionary<string, string>> data)
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
                if (data[0].ContainsKey("COUNTRESULT"))
                {
                    rewardCount = data[0]["COUNTRESULT"];
                    Reward_count.text = rewardCount;


                }
                else
                {
                    //ui_manager.GetComponent<Alert_Manager>().ShowAlert("");
                    Reward_count.text = "0";
                }



            }
            else
            {
                // ui_manager.GetComponent<Alert_Manager>().ShowAlert("");
                Reward_count.text = "0";

            }
        }
    }




    private void OnEnable()
    {
        if (playTopUI.isLoginState())
        {
            AutoLogin.isOn = (SplayerPrefs.GetPlayerPrefs_int("AutoLogin")==1)?true:false;
            UserName.text = playTopUI.ui_manager.Get_member_info(_Member_info.KEY_NAME.ARA_MBRS_NM) + "님,";
            DateTime now = DateTime.Now;
            string select_time = now.ToString("yyyy") + now.ToString("MM") + now.ToString("dd");


            List<string> key = new List<string>();
            List<string> value = new List<string>();
            key.Add("COUNT_DVCD");
            key.Add("ARA_MBRS_CI_VAL");
            key.Add("SELECT_MONTH");
            value.Add("1");
            value.Add(playTopUI.ui_manager.Get_member_info(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL));
            value.Add(select_time);
            playTopUI.ui_manager.GetComponent<UnityWeb>().WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.RewardALLCount, URLs.Menu.Reward, key, value, Reward_countCallback);



        }
        else
        {
            AutoLogin.isOn = false;
            AutoLogin.interactable = false;
        }
    }
}
