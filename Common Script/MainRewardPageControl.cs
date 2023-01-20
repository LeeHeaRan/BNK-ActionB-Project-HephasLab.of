using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class MainRewardPageControl : MonoBehaviour
{

    public UIManager ui_manager;
    public PlayTopUI playTopUI;
    public GameObject reward_list;
    public Text Reward_ALLCount;
    public Text Reward_leftCount;
    public GameObject[] icon_pref;
    public GameObject date_control_area;
    public GameObject Reward_list_background;
    public Image[] DataSideBtnImage;
    
    Vector2 Reward_list_backround_andhored;
    Vector2 Reward_list_TempPos; 
    Vector2 BeganPos = new Vector2();
    Vector2 UpdatePos = new Vector2();

    List<GameObject> iconListObjects = new List<GameObject>();
    string[] ToDayDate = new string[2];
    string[] ChangedDate = new string[2];
    int DateChangeIndex = 0;
    int NowDateIndex = 0;
    bool isRewardListViewControl = false;
    bool isRewardListState = false;
    string iimitMonth = "202201";
    int ALLCount = 0;
    int delCount = 1;
    int MonthTempCount = 0;
    bool isChanging = false;
    int BeforeCount = 0;
    List<int> SucessData = new List<int>();
    public GameObject loadingPage;
    // Start is called before the first frame update
    
    void Awake()
    {

        ui_manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();


        DateTime now = DateTime.Now;
        ToDayDate[0] = now.ToString("yyyy");
        ToDayDate[1] = now.ToString("MM");
        ChangedDate = (string[])ToDayDate.Clone();
        date_control_area.transform.GetChild(2).GetComponent<Text>().text = ToDayDate[0] + "." + ToDayDate[1];

        Reward_list_backround_andhored = Reward_list_background.GetComponent<RectTransform>().sizeDelta;
        Reward_list_TempPos = Reward_list_backround_andhored;

        
        Debug.Log(Reward_list_background.GetComponent<RectTransform>().sizeDelta);
    }



    private void Update()
    {
        if (Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                BeganPos = touch.position;
                Reward_list_TempPos = Reward_list_background.GetComponent<RectTransform>().sizeDelta;

            } else if (touch.phase == TouchPhase.Moved)
            {
                if (isRewardListViewControl)
                {
                    Reward_list_background.GetComponent<RectTransform>().sizeDelta = new Vector2(Reward_list_backround_andhored.x, Reward_list_TempPos.y + ((touch.position.y - BeganPos.y)*(1080f/(float)Screen.height)));
                }
            } else if (touch.phase == TouchPhase.Ended)
            {


                if (isRewardListViewControl)
                {
                    isRewardListViewControl = false;
                    float temp_value = touch.position.y - BeganPos.y;
                    if (Math.Abs((temp_value) * (1080f / (float)Screen.height)) > 50 && ((isRewardListState) ? -temp_value :temp_value) >50)
                    {
                        if (Reward_list_background.GetComponent<iTween>() != null)
                        {
                            iTween.Stop(Reward_list_background);
                        }
                        
                        Vector2 tempFromPos = Reward_list_background.GetComponent<RectTransform>().sizeDelta;
                        Vector2 tempToPos;
                        Hashtable ht = new Hashtable();
                        if (isRewardListState)
                        {
                            tempToPos = Reward_list_backround_andhored;
                            isRewardListState = false;
                            ht.Add("name", "close");
                        }
                        else
                        {
                            tempToPos = new Vector2(Reward_list_backround_andhored.x, 940);
                            isRewardListState = true;
                            ht.Add("name", "open");
                        }

                        
                        moveUI(ht,tempFromPos, tempToPos, 0.2f);

                    }
                    else
                    {
                        Vector2 tempFromPos = Reward_list_background.GetComponent<RectTransform>().sizeDelta;
                        Vector2 tempToPos;
                        if (isRewardListState)//열려있음
                        {
                            tempToPos =new Vector2(Reward_list_backround_andhored.x, 940);
                        }
                        else // 닫혀있음
                        {
                            tempToPos = Reward_list_backround_andhored;
                        }
                        Hashtable ht = new Hashtable();
                        moveUI(ht,tempFromPos, tempToPos,0.2f);
                    }
                }
            }

        }
    }
    public void moveUI(Hashtable ht, Vector2 tempFromPos, Vector2 tempToPos, float time)
    {
       
        ht.Add("from", tempFromPos);
        ht.Add("to", tempToPos);
        ht.Add("easeType", iTween.EaseType.easeOutCubic);
        ht.Add("loopType", "once");
        ht.Add("time", time);
        ht.Add("onupdatetarget", gameObject);
        ht.Add("onupdate", "SetRewardListPos");

        iTween.ValueTo(Reward_list_background, ht);
    }
    public void SetRewardListPos(Vector2 vec2)
    {
       
        Reward_list_background.GetComponent<RectTransform>().sizeDelta = vec2;

       
    }
    public void ListCloseomplete()
    {
        gameObject.GetComponent<UI_Control>().CloseUI();
    }

    public void DateChagne(bool isDir)
    {
        if (!isChanging)
        {
           // loadingPage.SetActive(true);
            // true 왼쪽
            // false 오른쪽
            if (isDir)
            {
                if (!iimitMonth.Equals(ChangedDate[0] + ChangedDate[1]))
                {
                    isChanging = true;
                    //나중에 시작날짜 받아와야함
                    //시작달 도달시 버튼 반투명처리
                    DateChangeIndex -= 1;

                    DateTime now = DateTime.Now.AddMonths(DateChangeIndex);
                    ChangedDate[0] = now.ToString("yyyy");
                    ChangedDate[1] = now.ToString("MM");
                    DataSideBtnImage[1].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                    date_control_area.transform.GetChild(2).GetComponent<Text>().text = ChangedDate[0] + "." + ChangedDate[1];
                    GetMonthData(now.ToString("yyyyMM") + "01", now.AddMonths(1).ToString("yyyyMM") + "01");
                    if (iimitMonth.Equals(ChangedDate[0] + ChangedDate[1]))
                    {
                        DataSideBtnImage[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                    }
                }

            }
            else
            {


                DateTime AddmonthsTime = DateTime.Now.AddMonths(DateChangeIndex + 1);

                if (DateChangeIndex + 1 <= 0)
                {
                    isChanging = true;
                    DateChangeIndex += 1;
                    ChangedDate[0] = AddmonthsTime.ToString("yyyy");
                    ChangedDate[1] = AddmonthsTime.ToString("MM");

                    date_control_area.transform.GetChild(2).GetComponent<Text>().text = ChangedDate[0] + "." + ChangedDate[1];
                    GetMonthData(AddmonthsTime.ToString("yyyyMM") + "01", AddmonthsTime.AddMonths(1).ToString("yyyyMM") + "01");

                    if (DateChangeIndex == 0)
                    {
                        DataSideBtnImage[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                    }
                }

            }
        }


       

    }

    public void GameDataValue()
    {

        int TempCount= SucessData.Count - 1;
        int limitCount = 0;
       
        int year = int.Parse(ChangedDate[0]);
        int day =  DateTime.DaysInMonth(year, int.Parse(ChangedDate[1]));
        int Today =(ChangedDate[1].Equals(ToDayDate[1]))?DateTime.Now.Day:99;
        Debug.Log(ChangedDate[1]+" "+ToDayDate[1]);
        Debug.Log(ChangedDate[1].Equals(ToDayDate[1]));
        Debug.Log(Today);
        for (int i = 0; i < iconListObjects.Count; i++)
        {
            Destroy(iconListObjects[i]);
        }
        if(NowDateIndex > DateChangeIndex)
        {
            MonthTempCount -= SucessData.Count;
        }
        else
        {
            NowDateIndex += DateChangeIndex;
        }
            
        
        iconListObjects.Clear();


        SucessData.Sort();

        for (int i = 1; i <= day; i++)
        {
            GameObject temp=null;

            
           //성공
           //실패
           //리워드
           //수행전
            if (Today >= i && year ==  DateTime.Now.Year) {

                if (TempCount >= limitCount)
                {
                    if (SucessData[limitCount] == i)
                    {
                        temp = Instantiate(icon_pref[0], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), reward_list.transform);
                        temp.GetComponent<Reward_Select>().RewardSetting(GetComponent<MainRewardPageControl>(),(BeforeCount + limitCount).ToString());
                        //     temp.GetComponentInChildren<Text>().text = ((BeforeCount + limitCount)).ToString();
                        temp.name = "Reward_" + (BeforeCount + limitCount).ToString();
                         limitCount += 1;
                    }
                    else
                    {
                        temp = Instantiate(icon_pref[1], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), reward_list.transform);
                    }
                }
                else
                {
                    temp = Instantiate(icon_pref[1], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), reward_list.transform);
                }
                
            } else
            {
                temp = Instantiate(icon_pref[3], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), reward_list.transform);
                temp.GetComponentInChildren<Text>().text = ChangedDate[1] + "." + i.ToString("00");
            }
        
            iconListObjects.Add(temp);

        }
        //loadingPage.SetActive(false);

    }
    public void Close()
    {
        if (isRewardListState)
        {
            Vector2 tempFromPos = Reward_list_background.GetComponent<RectTransform>().sizeDelta;
            Vector2 tempToPos;
            Hashtable ht = new Hashtable();
            tempToPos = Reward_list_backround_andhored;
            isRewardListState = false;
            ht.Add("name", "close");
            ht.Add("oncompletetarget", gameObject);
            ht.Add("oncomplete", "ListCloseomplete");

            moveUI(ht, tempFromPos, tempToPos, 0.1f);
        }
        else
        {
            ListCloseomplete();
        }
    }
    public void ListClose()
    {
        if (isRewardListState)
        {
            Vector2 tempFromPos = Reward_list_background.GetComponent<RectTransform>().sizeDelta;
            Vector2 tempToPos;
            Hashtable ht = new Hashtable();
            tempToPos = Reward_list_backround_andhored;
            isRewardListState = false;
            ht.Add("name", "close");
            ht.Add("oncompletetarget", gameObject);
            ht.Add("oncomplete", "ListCloseomplete");

            moveUI(ht, tempFromPos, tempToPos, 0.1f);
        }
    }
    public void RewardListOpen()
    {
        isRewardListViewControl = true;
        Debug.Log("RewardListOpen");
    }



    public void RewardClick(string Count)
    {

      /*  List<string> key = new List<string>();
        List<string> value = new List<string>();
        key.Add("COUNT_DVCD");
        key.Add("ARA_MBRS_CI_VAL");
        key.Add("SELECT_MONTH");
        value.Add("1");
        value.Add(ui_manager.Get_member_info(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL));
        value.Add(select_time);

        ui_manager.GetComponent<UnityWeb>().WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs., URLs.Menu.Reward, key, value, Reward_countCallback);*/






        playTopUI.RewardGetPage();
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
                    ALLCount = int.Parse(data[0]["COUNTRESULT"]);
                    //    rewardCount = data[0]["COUNTRESULT"];
                    //     Reward_count.text = rewardCount;
                    Reward_ALLCount.text = data[0]["COUNTRESULT"];
                    Reward_leftCount.text = ALLCount.ToString("D3");
                }
                else
                {
                    //ui_manager.GetComponent<Alert_Manager>().ShowAlert("");
                 Reward_ALLCount.text = "0";
                }



            }
            else
            {
                Reward_ALLCount.text = "0";
                // ui_manager.GetComponent<Alert_Manager>().ShowAlert("");


            }
        }
    }
    public void Reward_MonthDataCallback(string req, string resCode, List<Dictionary<string, string>> data)
    {
        isChanging = false;
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
                SucessData.Clear();
                if (data[0].ContainsKey("ARA_MBRS_CI_VAL"))
                {
                   
                    // LT_DATE ARA_BHVR_COMPT_YN
                    for (int i = data.Count - 2; i>=0 ; i--)
                    {
                        SucessData.Add(int.Parse(data[i]["LT_DATE"].Substring(6,2)));
                    }
                    BeforeCount = int.Parse(data[data.Count-1]["COUNTRESULT"])+1;

                    GameDataValue();
                }
                else
                {
                    GameDataValue();
                    //ui_manager.GetComponent<Alert_Manager>().ShowAlert("");
                    //Reward_ALLCount.text = "0";
                }



            }
            else
            {
               
               // Reward_ALLCount.text = "0";
                // ui_manager.GetComponent<Alert_Manager>().ShowAlert("");


            }
        }
    }
    private void GetALLCount()
    {
        DateTime now = DateTime.Now;
        string select_time  = now.ToString("yyyy") + now.ToString("MM") + now.ToString("dd");

        List<string> key = new List<string>();
        List<string> value = new List<string>();
        key.Add("COUNT_DVCD");
        key.Add("ARA_MBRS_CI_VAL");
        key.Add("SELECT_MONTH");
        value.Add("1");
        value.Add(ui_manager.Get_member_info(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL));
        value.Add(select_time);

        ui_manager.GetComponent<UnityWeb>().WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.RewardALLCount, URLs.Menu.Reward, key, value, Reward_countCallback);


    }

    private void GetMonthData(string start,string end)
    {
        ChangedDate[0]= start.Substring(0,4);
        ChangedDate[1]= start.Substring(4,2);
        List<string> key = new List<string>();
        List<string> value = new List<string>();
        key.Add("SELECT_DATE");
        key.Add("SELECT_DATE2");
        key.Add("ARA_MBRS_CI_VAL");
       
        value.Add(start);
        value.Add(end);
        value.Add(ui_manager.Get_member_info(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL));
        ui_manager.GetComponent<UnityWeb>().WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Reward_MonthSelect, URLs.Menu.Reward, key, value, Reward_MonthDataCallback);
    }







    private void OnEnable()
    {
        GetALLCount();
        DateTime now = DateTime.Now;


        GetMonthData(now.ToString("yyyyMM")+"01", now.AddMonths(1).ToString("yyyyMM")+"01");
      


    }
    private void OnDisable()
    {
        for(int i=0; i<iconListObjects.Count; i++)
        {
            Destroy(iconListObjects[i]);
        }
        isChanging = false;
        delCount = 1;
        ALLCount = 0;
        DateChangeIndex = 0;
        iconListObjects.Clear();
        SucessData.Clear();
        isRewardListViewControl = false;
        Reward_list_background.GetComponent<RectTransform>().sizeDelta = Reward_list_backround_andhored;
        isRewardListState = false;
    }

}
