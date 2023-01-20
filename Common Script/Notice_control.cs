using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notice_control : MonoBehaviour
{
    public UnityWeb unityWeb;
    public GameObject BoardPref;
    public GameObject PrefParent;
    public List<GameObject> BoardObjectList = new List<GameObject>();


    [Header("Board Code")]
    [Tooltip("")]
    private bool isFirst = true;
    private bool isEndPage = false;
    private bool isLoading = false;
    private int PageNum = 1;
    private int DelectPage = 0;

    [Header("Board List Control")]
    public ScrollRect boardList;
    public int BoardLimitCount;
    public int BoardreLoadLimtSize;
    public UIManager uiManager;
    private string CI_VAL;
    // Start is called before the first frame update


    private void Awake()
    {
        GameObject TempUiManager = GameObject.FindGameObjectWithTag("UIManager");
        uiManager = TempUiManager.GetComponent<UIManager>();
        unityWeb = TempUiManager.GetComponent<UnityWeb>();
        CI_VAL = uiManager.Get_member_info(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL);
    }


    void Start()
    {
        uiManager.HideMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFirst)
        {

            if (boardList.content.anchoredPosition.y > boardList.content.sizeDelta.y - BoardreLoadLimtSize)
            {
                if (!isLoading)
                {
                    // ScreenLoadObject.SetActive(true);
                    //  boardList.StopMovement();
                    isLoading = true;

                    PageNum += 1;
                    LoadBoard(CI_VAL, PageNum.ToString(), "", false);
                }
            }
            else if (boardList.content.anchoredPosition.y < BoardreLoadLimtSize && DelectPage > 0)
            {
                if (!isLoading)
                {
                    //   boardList.StopMovement();
                    //   ScreenLoadObject.SetActive(true);
                    isLoading = true;
                    LoadBoard(CI_VAL, DelectPage.ToString(), "", true);
                    DelectPage -= 1;
                }
            }


        }
        else
        {
            if (boardList.verticalNormalizedPosition != 0)
            {
                isFirst = false;
            }
        }
        //Debug.Log(boardList.verticalNormalizedPosition);
    }
    public void SetBoard(List<Dictionary<string, string>> contents, bool isAppend)
    {
        
        for (int i = 0; i < contents.Count; i++)
        {

            //키변동
            //       GameObject planeCopyObject = Instantiate(BoardPref, new Vector3(0, 0, 0), Quaternion.identity, PrefParent.transform);
            GameObject planeCopyObject = Instantiate(BoardPref, PrefParent.transform);
            planeCopyObject.name = contents[i]["ARA_LEME_NATV_MGNO"];
            planeCopyObject.GetComponent<NoticeContentControl>().notice_Control = GetComponent<Notice_control>();
            planeCopyObject.GetComponent<NoticeContentControl>().SetText(contents[i]);
            planeCopyObject.name = "[" + contents[i]["NUMROW"] + "]";



            if (isAppend) planeCopyObject.transform.SetSiblingIndex(i);
            if (isAppend) BoardObjectList.Insert(i, planeCopyObject);
            else BoardObjectList.Add(planeCopyObject);

        }
        if (BoardObjectList.Count > BoardLimitCount)
        {
            Vector2 moveValue = boardList.content.anchoredPosition;
            for (int i = 0; i < 20; i++)
            {
                if (!isAppend)
                {
                    moveValue.y -= BoardObjectList[0].GetComponent<RectTransform>().sizeDelta.y;
                    Destroy(BoardObjectList[0]);
                    BoardObjectList.Remove(BoardObjectList[0]);
                }
                else
                {
                    moveValue.y += BoardObjectList[BoardObjectList.Count - 1].GetComponent<RectTransform>().sizeDelta.y;
                    Destroy(BoardObjectList[BoardObjectList.Count - 1]);
                    BoardObjectList.Remove(BoardObjectList[BoardObjectList.Count - 1]);

                }

            }
            if (!isAppend) DelectPage += 1;
            boardList.content.anchoredPosition = moveValue;



        }
        uiManager.onLoadingPage(false);
        isLoading = false;

    }
    public void LoadBoard(string CI_VAL, string page, string limit, bool isAppend)
    {
        List<string> key = new List<string>();
        List<string> value = new List<string>();

        key.Add("ARA_MBRS_CI_VAL");
        if (!limit.Equals("")) key.Add("PAGE_COUNT");
        key.Add("PAGE_NUM");

        value.Add(CI_VAL);
        if (!limit.Equals("")) value.Add(limit);
        value.Add(page);

        if (isAppend) unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Notice_Select, URLs.Menu.notice, key, value, Append_BoardCallback);
        else unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Notice_Select, URLs.Menu.notice, key, value, BoardCallback);
    }

    private void BoardCallback(string req, string resCode, List<Dictionary<string, string>> data)
    {
        //  dataTemp = data;
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
                if (data[0].ContainsKey("ARA_LEME_RESULT_LIST"))
                {
                    isEndPage = true;
                }
                else
                {
                    SetBoard(data, false);
                }
            }
        }
        else
        {
            uiManager.onLoadingPage(false);
            gameObject.GetComponent<UI_Control>().CloseUI();
        }





    }
    private void Append_BoardCallback(string req, string resCode, List<Dictionary<string, string>> data)
    {
        //  dataTemp = data;
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
                if (data[0].ContainsKey("ARA_ITG_NB_RESULT_LIST"))
                {
                    isEndPage = true;
                }
                else
                {
                    SetBoard(data, true);
                }
            }
        }
        else
        {
            uiManager.onLoadingPage(false);

        }


    }



    string userCeckuid;
    //로그저장
    public void Send_UserCheck(string uid)
    {
        userCeckuid = uid;
        List<string> key = new List<string>();
        List<string> value = new List<string>();


        key.Add("ARA_MBRS_CI_VAL");
        key.Add("ARA_LEME_NATV_MGNO");

        value.Add(uiManager.Get_member_info(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL));
        value.Add(uid);


        unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Notice_Check, URLs.Menu.noticeCheck, key, value, NoticeUserCheckCallback);
    }

    private void NoticeUserCheckCallback(string req, string resCode, List<Dictionary<string, string>> data)
    {
        //  dataTemp = data;
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
                Debug.Log("로그 갱신 성공");
                PrefParent.transform.Find(userCeckuid).GetComponent<NoticeContentControl>().NewIconRemove();

            }
        }
        


    }















    private void OnEnable()
    {


        uiManager.onLoadingPage(true);
        PageNum = BoardLimitCount / 20;
        LoadBoard(CI_VAL, "1", BoardLimitCount.ToString(), false);

    }
    private void OnDisable()
    {
        DelectPage = 0;
        PageNum = 1;
        isEndPage = false;
        isFirst = true;
        isLoading = false;
        uiManager.onLoadingPage(false);
        for (int i = 0; i < BoardObjectList.Count; i++)
        {
            Destroy(BoardObjectList[i]);

        }
        BoardObjectList.Clear();
        Vector2 tempvec2 = PrefParent.GetComponent<RectTransform>().anchoredPosition;
        tempvec2.y = 0;
        PrefParent.GetComponent<RectTransform>().anchoredPosition = tempvec2;
    }

    public void gameObjectDestroy()
    {
        uiManager.ShowMenu();
        Destroy(gameObject);
    }



}
