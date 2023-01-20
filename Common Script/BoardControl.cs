using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoardControl : MonoBehaviour
{
    public UnityWeb unityWeb;
    public GameObject BoardPref;
    public GameObject PrefParent;
    public List<GameObject> BoardObjectList = new List<GameObject>();
    public ScrollRect  scrollRect;

    [Header("Board Code")]
    [Tooltip("")]
    public string BoardCode1;
    public string BoardCode2;
    private bool isFirst = true;
    private bool isEndPage=false;
    private bool isLoading = false;
    private int PageNum = 1;
    private int DelectPage = 0;
    
    [Header("Board List Control")]
    public ScrollRect boardList;
    public int BoardLimitCount;
    public int BoardreLoadLimtSize;
    public UIManager uiManager;
    // Start is called before the first frame update


    private void Awake()
    {
        GameObject TempUiManager = GameObject.FindGameObjectWithTag("UIManager");
        uiManager = TempUiManager.GetComponent<UIManager>();
        unityWeb = TempUiManager.GetComponent<UnityWeb>();
    
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFirst)
        {
            
            if (boardList.content.anchoredPosition.y > boardList.content.sizeDelta.y- BoardreLoadLimtSize)
            {
                if (!isLoading)
                {
                   // ScreenLoadObject.SetActive(true);
                    //  boardList.StopMovement();
                    isLoading = true;
              
                    PageNum += 1;
                    LoadBoard(BoardCode1, BoardCode2, PageNum.ToString(),"", false);
                }
            }
            else if (boardList.content.anchoredPosition.y < BoardreLoadLimtSize && DelectPage > 0)
            {
                if (!isLoading)
                {
                 //   boardList.StopMovement();
                 //   ScreenLoadObject.SetActive(true);
                    isLoading = true;
                    LoadBoard(BoardCode1, BoardCode2, DelectPage.ToString(), "", true);
                    DelectPage -= 1;
                }
            }

           
        }
        else
        {
            if (!isLoading) {
                //  if (boardList.verticalNormalizedPosition != 0)
                //  {
                boardList.verticalNormalizedPosition = 1;
                      isFirst = false;
                    scrollRect.enabled = true;
             //   }
            }
        }
        //Debug.Log(boardList.verticalNormalizedPosition);
    }
    public void SetBoard(List<Dictionary<string, string>> contents,bool isAppend)
    {
      /*  "FST_REG_DTTI":"20220706131027",
        "LT_CHPR_ID":"OU13582",
        "LT_CH_DTTI":"20220706131027806",
        "ARA_ITG_NB_MGNO":"358",
        "ARA_NB_TIT":"네",
        "ARA_NB_CNTN":"네",
        "NUMROW":"1",
        "FST_RGPR_ID":"OU13582",
        "ARA_ITG_NB_DV_VAL2":"null",
        "ARA_ITG_NB_DV_VAL1":"null",
        "ARA_MBRS_CI_VAL":"null"*/
        for (int i = 0; i < contents.Count; i++) {

            //키변동
            //       GameObject planeCopyObject = Instantiate(BoardPref, new Vector3(0, 0, 0), Quaternion.identity, PrefParent.transform);
            GameObject planeCopyObject = Instantiate(BoardPref, PrefParent.transform);
            
            planeCopyObject.GetComponent<boardContentControl>().SetText("[" + contents[i]["NUMROW"] + "]" + contents[i]["ARA_NB_TIT"], contents[i]["ARA_NB_CNTN"], contents[i]["LT_CH_DTTI"]);
            planeCopyObject.name = "[" + contents[i]["NUMROW"] + "]";



            if (isAppend) planeCopyObject.transform.SetSiblingIndex(i);
            if (isAppend) BoardObjectList.Insert(i,planeCopyObject);
            else BoardObjectList.Add( planeCopyObject);

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
                    Destroy(BoardObjectList[BoardObjectList.Count-1]);
                    BoardObjectList.Remove(BoardObjectList[BoardObjectList.Count - 1]);

                }

            }
            if (!isAppend) DelectPage +=  1;
            boardList.content.anchoredPosition = moveValue;
           


        }
        uiManager.onLoadingPage(false);
        isLoading = false;
        
       
    }
    public void LoadBoard(string code1,string code2,string page,string limit,bool isAppend)
    {
        List<string> key = new List<string>();
        List<string> value = new List<string>();

        key.Add("ARA_ITG_NB_DVCD");
       if(!code2.Equals("")) key.Add("ARA_ITG_NB_DVCD_2");
        if (!limit.Equals("")) key.Add("PAGE_COUNT");
        key.Add("PAGE_NUM");

        value.Add(code1);
        if (!code2.Equals("")) value.Add(code2);
        if (!limit.Equals("")) value.Add(limit);
        value.Add(page);

       if(isAppend) unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Board, URLs.Menu.board, key, value, Append_BoardCallback);
        else unityWeb.WebSend(TB_TYPE.MBRS_INFO, UnityWeb.SEND_URL.Busan, URLs.Board, URLs.Menu.board, key, value, BoardCallback);
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
                if (data[0].ContainsKey("ARA_ITG_NB_RESULT_LIST"))
                {
                    isEndPage = true;
                }
                else
                {
                    SetBoard(data,false);
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
                    SetBoard(data,true);
                }
            }
        }
        else
        {
            uiManager.onLoadingPage(false);

        }





    }
    private void OnEnable()
        {
        scrollRect.enabled = false;
        isLoading = true;
        uiManager.onLoadingPage(true);
        PageNum = BoardLimitCount/20;
            LoadBoard(BoardCode1, BoardCode2, "1", BoardLimitCount.ToString(), false);
           
        
          /*  List<Dictionary<string, string>> listtemp = new List<Dictionary<string, string>>();
       

            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                Dictionary<string, string> temp = new Dictionary<string, string>();
                temp.Add("title", "[공지] 안녕하세요.테스트 게시판입니다. ");
                string temp_cotents = "";
                for (int j = 0; j < Random.Range(20, 100); j++)
                {
                    temp_cotents += "TEST ";
                }
                temp.Add("contents", temp_cotents);

                listtemp.Add(temp);
          
            }

            SetBoard(listtemp);*/

         

        }
    private void OnDisable()
    {
        
        DelectPage = 0;
        PageNum = 1;
        isEndPage = false;
        isFirst = true;
        
        uiManager.onLoadingPage(false);
        for (int i =0; i<BoardObjectList.Count; i++)
        {
            Destroy(BoardObjectList[i]);
           
        }
        BoardObjectList.Clear();
        Vector2 tempvec2 = PrefParent.GetComponent<RectTransform>().anchoredPosition;
        tempvec2.y = 0;
        PrefParent.GetComponent<RectTransform>().anchoredPosition = tempvec2;
        boardList.content.sizeDelta = new Vector2(boardList.content.sizeDelta.x, 0);
       
    }

}
