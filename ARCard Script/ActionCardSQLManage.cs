using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// ARCard콘텐츠 안에 모든 웹통신을 관리한다.
/// </summary>
public class ActionCardSQLManage : MonoBehaviour
{
    public SetDBDataManager setdbdatamanager;
    public OCRControll_02 ocrControll02_s;
    public GameObject uiManager;
 
    public void Awake()
    {
        uiManager = GameObject.Find("Manager");
    }
    
    /// <summary>
    ///url로 들어왔을떼 직원테이블에 값 저장. 
    /// </summary>
    /// <param name="empInfo"></param>
    /// <param name="callback"></param>
    public void ActionCard_EmpInfo_Insert_Sql(string[] empInfo, Action<string, string, List<Dictionary<string, string>>> callback)
    {
        List<string> key = new List<string>();
        List<string> value = new List<string>();

        key.Add("ARA_MBRS_CI_VAL");
        key.Add("ARA_STF_NM");
        key.Add("ARA_STF_JGDNM");
        key.Add("ARA_STF_DPNM");

        string ci_val = "0";
        if (uiManager.GetComponent<UIManager>().isLoginState())
        {
            ci_val = uiManager.GetComponent<UIManager>().Get_member_info(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL);
        }

        value.Add(ci_val);
        value.Add(ocrControll02_s.infoName);
        value.Add(ocrControll02_s.infoRank);
        value.Add(ocrControll02_s.infoDept);

        key.Add("ARA_STF_ENOB");
        key.Add("ARA_STF_BRNO");
        value.Add(empInfo[1]);
        value.Add(empInfo[0]);

        uiManager.GetComponent<UnityWeb>().WebSend(TB_TYPE.BNCD_INFO, UnityWeb.SEND_URL.Busan, URLs.Card_Save, URLs.Menu.Card, key, value, callback); //직원테이블에 값을 넣음.
    }

    //
    /// <summary>
    /// ocr로 들어왔을때 직원테이블에 값 저장
    /// </summary>
    /// <param name="callback"></param>
    public void ActionCard_EmpInfo_Insert_Sql(Action<string, string, List<Dictionary<string, string>>> callback)
    {

        List<string> key = new List<string>();
        List<string> value = new List<string>();

        key.Add("ARA_MBRS_CI_VAL");
        key.Add("ARA_STF_NM");
        key.Add("ARA_STF_JGDNM");
        key.Add("ARA_STF_DPNM");

        string ci_val = "0";

        if (uiManager.GetComponent<UIManager>().isLoginState())
        {
            ci_val = uiManager.GetComponent<UIManager>().Get_member_info(_Member_info.KEY_NAME.ARA_MBRS_CI_VAL);
        }

        value.Add(ci_val);
        value.Add(ocrControll02_s.infoName);
        value.Add(ocrControll02_s.infoRank);
        value.Add(ocrControll02_s.infoDept);
        
        uiManager.GetComponent<UnityWeb>().WebSend(TB_TYPE.BNCD_INFO, UnityWeb.SEND_URL.Busan, URLs.Card_Save, URLs.Menu.Card, key, value, callback); //직원테이블에 값을 넣음.
    
    }
   

    /// <summary>
    /// 메인배너의 URL 및 기능을 가져옴.
    /// </summary>
    private void setMainBannerCallBack()
    {
        //전체메인배너를 업데이트 해줘야한다.
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GetComponent<UIControll>().UI_AR_subscribe_all.transform.GetChild(0).GetChild(0);
            }
        }
    }


    /// <summary>
    /// 서브배너의 이미지 URL을 가져옴
    /// </summary>
    public void ActionCard_SubBanner_Select_Sql()
    {
        List<string> key = new List<string>();
        List<string> value = new List<string>();
        key.Add("ARA_BNCD_BANNER_KND_DVCD");
        key.Add("ARA_BNCD_BANNER_LOCAT_DVCD");

        value.Add("02"); //서브배너
        value.Add("10"); //서브배너 콘텐츠

        //URLs.Menu.Card NBPCMN400ARA10임. 서브배너 이미지조회 ap의 메뉴 아이디는 URLs.Menu.Menber와 동일. 
        uiManager.GetComponent<UnityWeb>().WebSend(TB_TYPE.BNCD_BANNER_INFO, UnityWeb.SEND_URL.Busan, URLs.ActionCard_SubImageSelect, URLs.Menu.Member, key, value, setSubBannerCallBack); //서브배너 이미지 url을 받아옴.
    }


    /// <summary>
    /// <para>ActionCard_SubBanner_Select_Sql Callback</para>
    /// AR오브젝트에 전광판 이미지를 바꿔줄때 사용한다. 실행할때마다 업데이트한다. arobject에서 map이 바뀌면 BoardImage_Plane을 다시 넣어주고 스크립트에 넣어줘야한다.
    /// </summary>
    /// <param name="req"></param>
    /// <param name="resCode"></param>
    /// <param name="data"></param>
    public void setSubBannerCallBack(string req, string resCode, List<Dictionary<string, string>> data) //url은 AR앱배너정보기본에서 배너 이미지 주소를 받아와야한다.
    {
        string contents = ""; //초기화
        Debug.Log("server call: " + req); //성공유무

        try
        {
            //값 찍기 편하게 만들어 둠. 테스트용
            /*for (int i = 0; i < data.Count; i++)
                 {
                     foreach (KeyValuePair<string, string> pair in data[i]) //반환된 값의 키-벨류 데이터를 돈다.
                     {
                         //Debug.Log(pair.Key + " : " + pair.Value);
                         //if (pair.Key.Equals("ARA_BNCD_BANNER_RESULT_LIST")) 
                         if (pair.Key.Equals("IMG_URL")) 
                         {
                             //Debug.Log(pair.Key + " : " + pair.Value);
                             //if (pair.Value.Equals(null) || pair.Value == "") //널일 경우 디폴드 값을 유지한다.
                             if (pair.Value.Equals(null) || pair.Value == "" || pair.Value == "RESULT IS NULL") //널일 경우 디폴드 값을 유지한다.
                             {
                                 setdbdatacontrol.boardImage_m.mainTexture = setdbdatacontrol.default_subbannerImage;
                             }
                             else //널이 아닌경우 이미지를 바꿔준다.
                             {
                                 setdbdatacontrol.subbanner_imageData = pair.Value;
                                 setdbdatacontrol.getWebImage(gameObject.GetComponent<UnityWeb>().baseUrl[0] + pair.Value);
                             }
                         }
                         else
                         {
                         }
                }
            }*/

            if (req.Equals("fail") || resCode.Equals("99"))
            {
                Debug.Log("서브배너 이미지 조회중 오류가 발생하였습니다.");

                if (!req.Equals("Error retrieving sub banner"))
                {
                    GameObject.Find("ARCardAll_get").GetComponent<UIManager>().ErrorReport("Mobile_Certify :" + contents);
                }
            }
            else if (req.Equals("Sucess") && resCode.Equals("00"))
            {
                //관리자페이지에 값이 들어가서 키가 제대로 들어오면 먼저 key가 있는지 확인하고 키에 있는 값을 확인한다.
                //정상적으로 들어왔을 때도 들어와 있을 수 있음.
                //data[0].ContainsKey("ARA_BNCD_BANNER_RESULT_LIST")  //있어도 들어옴. 사용못함

                if (data[0]["ARA_BNCD_BANNER_RESULT_LIST"].Equals("RESULT IS NULL")) //값이 없으면
                {
                    Debug.Log("서브배너 정보가 없습니다. RESULT IS NULL");
                    //이미지 디폴트
                    setdbdatamanager.boardImage_m.mainTexture = setdbdatamanager.default_subbannerImage;
                }
                else //값이 있으면
                {
                    Debug.Log("서브배너 이미지 조회를 성공하였습니다.");

                    for (int i = 0; i < data.Count; i++)
                    {
                        setdbdatamanager.subbanner_imageData = data[i]["IMG_URL"];
                        setdbdatamanager.getWebImage(gameObject.GetComponent<UnityWeb>().baseUrl[0] + data[i]["IMG_URL"]);
                    }
                }
            }

        }
        catch (System.Exception e)
        {
        }
    }

    
    /// <summary>
    /// 메인배너의 텍스트와 링크를 불러오는 함수. all.cs 스크립트에서 실행
    /// </summary>
    public void ActionCard_MainBanner_Select_Sql()
    {
        List<string> key = new List<string>();
        List<string> value = new List<string>();
        key.Add("ARA_BNCD_BANNER_KND_DVCD");
        key.Add("ARA_BNCD_BANNER_LOCAT_DVCD");

        value.Add("01"); //메인배너
        value.Add("10"); //메인배너 콘텐츠

        uiManager.GetComponent<UnityWeb>().WebSend(TB_TYPE.BNCD_BANNER_INFO, UnityWeb.SEND_URL.Busan, URLs.ActionCard_MainImageSelect, URLs.Menu.Member, key, value, setMainBannerCallBack); //메인배너 정보를 받아옴.
    }

    public Dictionary<string, List<string>> getMainbannerData = new Dictionary<string, List<string>>();

    /// <summary>
    /// ActionCard_MainBanner_Select_Sql() 콜백
    /// </summary>
    /// <param name="req"></param>
    /// <param name="resCode"></param>
    /// <param name="data"></param>
    public void setMainBannerCallBack(string req, string resCode, List<Dictionary<string, string>> data)
    {
        string contents = ""; //초기화
        Debug.Log("server call: " + req); //성공유무
        try
        {
           //테스트용
           /* List<string> temp = new List<string>(); //[0]:타이틀 [1]:이벤트타입 [2]:URL
            for (int i = 0; i < data.Count; i++) //저장되어있는 아이템들을 모두 가져온다.
            {
                temp.Add(data[i]["ARA_BNCD_BANNER_TIT"]); //0
                temp.Add(data[i]["EVNT_TYPE_DVCD"]); //1
                temp.Add(data[i]["SV_RQST_URL"]); //2

                getMainbannerData[data[i]["ARA_BNCD_BANNER_LOCAT_DVCD"]] = temp;
                //List<string> getTemp = getMainbannerData["02"];

                for (int j = 1; i < 10; i++) //01~09 처음부터 끝까지 돈다. 
                {
                    if (!getMainbannerData["0" + j].Equals(null) || getMainbannerData["0" + j] != null)  //getMainbannerData["02"] 가 널이 아니라면
                    {
                        List<string> getTemp = getMainbannerData["0" + j];
                        if (getTemp[0] != "" && getTemp[2] != "null" && getTemp[3] == "05") //타이틀이나 url값이 빈값이 아닐때.
                        {
                            setdbdatacontrol.Title[j - 1] = getTemp[0]; //타이틀을 넣어준다.  title[0~8]값이 있음. 0 = "01"
                            setdbdatacontrol.Type[j - 1] = getTemp[1]; //항목을 넣어준다.
                            setdbdatacontrol.textArr[j - 1].text = setdbdatacontrol.Title[j - 1];
                            setdbdatacontrol.Title[j - 1] = getTemp[0]; //url을 넣어준다.
                        }
                        else
                        {
                            setdbdatacontrol.Title[j - 1] = "";
                        }
                    }
                }
            }*/

            if (req.Equals("fail") || resCode.Equals("99"))
            {
                if (!req.Equals("Error retrieving main banner"))
                {
                    GameObject.Find("ARCardAll_get").GetComponent<UIManager>().ErrorReport("Mobile_Certify :" + contents);
                }

                Debug.Log("메인배너 정보 조회중 오류가 발생하였습니다.");
            }
            else if (req.Equals("Sucess") && resCode.Equals("00"))
            {
                if (data[0]["ARA_BNCD_BANNER_RESULT_LIST"].Equals("RESULT IS NULL")) //값이 없으면
                {
                    Debug.Log("메인배너 정보가 없습니다. RESULT IS NULL");
                }
                else 
                {
                    List<string> temp = new List<string>(); //[0]:타이틀 [1]:이벤트타입 [2]:URL
                    for (int i = 0; i < data.Count; i++) //저장되어있는 아이템들을 모두 가져온다.
                    {
                        temp.Add(data[i]["ARA_BNCD_BANNER_TIT"]); //0
                        temp.Add(data[i]["EVNT_TYPE_DVCD"]); //1
                        temp.Add(data[i]["SV_RQST_URL"]); //2

                        getMainbannerData[data[i]["ARA_BNCD_BANNER_LOCAT_DVCD"]] = temp;
                        //List<string> getTemp = getMainbannerData["02"];

                        for (int j = 1; i < 10; i++) //01~09 처음부터 끝까지 돈다. 
                        {
                            if (!getMainbannerData["0" + j].Equals(null) || getMainbannerData["0" + j] != null)  //getMainbannerData["02"] 가 널이 아니라면
                            {
                                List<string> getTemp = getMainbannerData["0" + j];
                                if (getTemp[0] != "" && getTemp[2] != "null" && getTemp[3] == "05") //타이틀이나 url값이 빈값이 아닐때.
                                {
                                    setdbdatamanager.Title[j - 1] = getTemp[0]; //타이틀을 넣어준다.  title[0~8]값이 있음. 0 = "01"
                                    setdbdatamanager.Type[j - 1] = getTemp[1]; //항목을 넣어준다.
                                    setdbdatamanager.textArr[j - 1].text = setdbdatamanager.Title[j - 1];
                                    setdbdatamanager.Title[j - 1] = getTemp[0]; //url을 넣어준다.
                                }
                                else
                                {
                                    setdbdatamanager.Title[j - 1] = "";
                                }
                            }
                        }
                    }
                    Debug.Log("메인배너 정보 조회를 성공 하였습니다.");
                }
            }
        }
        catch (System.Exception e)
        {

        }
    }


    /// <summary>
    /// <para>URL을 통해 ActionCard의 AR에 들어갈때 실행하는 함수 all.cs 스크립트에서 실행</para>
    /// <para>URl을 통해 얻은 행번으로 직원 테이블에서 부서, 직급, 이름을 조회한다.</para>
    /// </summary>
    public void ActionCard_FindEmpInfo_Select_Sql()
    {
        List<string> key = new List<string>();
        List<string> value = new List<string>();
        key.Add("ARA_BNCD_BANNER_KND_DVCD");
        key.Add("ARA_BNCD_BANNER_LOCAT_DVCD");

        value.Add("02");
        value.Add("10");

        uiManager.GetComponent<UnityWeb>().WebSend(TB_TYPE.BNCD_BANNER_INFO, UnityWeb.SEND_URL.Busan, URLs.ActionCard_SubImageSelect, URLs.Menu.Member, key, value, setSubBannerCallBack); //서브배너 이미지 url을 받아옴.
    }
  
    /// <summary>
    /// 직원정보를 조회하는 콜백
    /// </summary>
    /// <param name="req"></param>
    /// <param name="resCode"></param>
    /// <param name="data"></param>
    public void ActionCard_FindEmpInfo_CallBack(string req, string resCode, List<Dictionary<string, string>> data)
    {
        if (req.Equals("fail") || resCode.Equals("99"))
        {
            if (!req.Equals("Error finding employee information"))  
            {
            }

        }
        else if (req.Equals("Sucess") && resCode.Equals("00"))
        {
            //행번으로 직원정보를 조회하는 작업 아직 안됨.
        }
    }
   

    /// <summary>
    /// 모바일영업점 링크를 가져오는 함수. 행번을 통해 조회한다. 직원정보 조회후 실행.
    /// </summary>
    /// <param name="enob"></param>
    public void ActionCard_mobileBranch_Select_Sql(string enob)
    {

        List<string> key = new List<string>();
        List<string> value = new List<string>();
        key.Add("MBL_SLBR_ENOB");
        value.Add(enob); 

        uiManager.GetComponent<UnityWeb>().WebSend(TB_TYPE.BNCD_MOBILEBRANCH, UnityWeb.SEND_URL.Busan, URLs.ActionCard_MobileBranch_Select, URLs.Menu.Member, key, value, ActionCard_mobileBranch_CallBack); //서브배너 이미지 url을 받아옴.
    }


    /// <summary>
    /// 직원행번을 사용하여 모바일영업점을 조회. all.cs 스크립트에서 실행
    /// </summary>
    /// <param name="req"></param>
    /// <param name="resCode"></param>
    /// <param name="data"></param>
    public void ActionCard_mobileBranch_CallBack(string req, string resCode, List<Dictionary<string, string>> data)
    {

        try
        {
            if (req.Equals("fail") || resCode.Equals("99"))
            {
                Debug.Log("모바일영업점 조회중 오류가 발생하였습니다.");
                setdbdatamanager.mobilebranchURL = "";
                //모바일영업점 버튼을 비활성화한다.
                setdbdatamanager.mobileBranch_OnOffButton(false);

            }
            else if (req.Equals("Sucess") && resCode.Equals("00"))
            {
                    Debug.Log("모바일영업점 정보를 조회하는데 성공하였습니다.");

                    for (int i = 0; i < data.Count; i++)
                    {
                        setdbdatamanager.mobilebranchURL = data[i]["MBL_SLBR_URL_GN_VAL"];
                    }

                //모바일영업점 버튼을 활성화한다.
                setdbdatamanager.mobileBranch_OnOffButton(true);

            }
        }
        catch (System.Exception e)
        {
            Debug.Log("모바일영업점 조회: " + e);
        }
    }

   //직원행번을 넣어서 모바일 영업점을 조회
    public void DebugTest()
    {
        ActionCard_mobileBranch_Select_Sql("OU13736");
    }

}

