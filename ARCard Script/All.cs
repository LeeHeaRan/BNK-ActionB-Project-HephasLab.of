using OpenCVForUnity.CoreModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ARCard씬 내에서 최상위 스크립트. 
/// 초기화를 관리한다.
/// </summary>
public class All : MonoBehaviour
{
    public SetAR setAR_s;
    public UIControll uiControll_s;
    public OCRControll_01 ocrControll_01_s;
    public OCRControll_02 ocrCountroll_02_s;
    public ARCardAll_Get arCardAll_Get_s;
    public Transform Notice_page;
    GameObject notice;

    public BackControll backControll;

    public ActionCardSQLManage actioncard_sqlmanager;

    public bool isSearchEMPInfo = false; //직원정보를 찾았는지 확인

    private void Start() //씬이 로드되면 실행.
    {
        ActionCard_Load();
    }

    /// <summary>
    ///  ProcessRequest()코루틴을 강제중지시킨다.
    /// </summary>
    public void stopCoroutineOcrControll_02() //ActionCard에 모든 코루틴을 중지시킨다.
    {
        StopCoroutine(ocrCountroll_02_s.coroutine);
    }

    private void OnDisable()
    {
        try
        {
            if (ocrCountroll_02_s.coroutine != null)
            {
                StopCoroutine(ocrCountroll_02_s.coroutine); 
            }

            uiControll_s.InitUI(); 
            ocrControll_01_s.InitOCR();
            setAR_s.InitAR(); 
            ocrControll_01_s.isLodingCheck = false; //카메라가 다 켜졌는지 확인하는 변수. ARCardAll_Get스크립트의 Load_completed()함수.
        }
        catch (Exception e)
        {
            //text.SetLog(e.ToString(),false);
        }
    }


    private void OnDestroy()
    {
        setAR_s.ARControll_s.nullSave(); //라이팅 맵을 지우고 장면을 전환한다.
    }
    private void OnEnable()
    {
        //서버의 데이터를 조회.
        actioncard_sqlmanager.ActionCard_MainBanner_Select_Sql(); //메인배너의 타이틀, 링크 등을 셋팅.
        actioncard_sqlmanager.ActionCard_SubBanner_Select_Sql(); //ar오브젝트의 전광판 서브배너를 셋팅.
        //actioncard_sqlmanager.ActionCard_mobileBranch_Select_Sql();
    }

    /// <summary>
    /// 액션명함 전체를 초기화
    /// </summary>
    public void ActionCard_Load() 
    {
        
        if (ocrCountroll_02_s.coroutine != null)
        {
            stopCoroutineOcrControll_02();
        }

        uiControll_s.InitUI(); 
        ocrControll_01_s.InitOCR();
        setAR_s.InitAR();
    }

    /// <summary>
    /// 하단의 카테고리바 비활성화.
    /// </summary>
    public void hide_BottonMeun()
    {
        arCardAll_Get_s.hideBottonMenu(); //하단의 메뉴바를 사라지게 한다.
    }
    /// <summary>
    /// 하단의 카테고리바 활성화.
    /// </summary>
    public void show_BottonMenu()
    {
        arCardAll_Get_s.showBottonMenu(); //show를 해주고 가야함.
    }

    /// <summary>
    /// 로그인 요청 모달창 활성화.
    /// </summary>
    public void actioncard_loginAlert()
    {
        arCardAll_Get_s.actioncard_NotmemberAlerOpen();
    }

    /// <summary>
    /// 알림창 활성화
    /// </summary>
    public void Notice_Page()
    {
        if (notice == null)
        {
            notice = Instantiate(arCardAll_Get_s.uIManager.GetComponent<UIManager>().GetNoticePage(), Notice_page);
            notice.SetActive(true);
        }
    }

    /// <summary>
    /// AR화면에서 AR부분만 초기화
    /// </summary>
    public void resetActionCard_AR()
    {
        setAR_s.InitAR();
        uiControll_s.ar_UIReDetection();
    }

    /// <summary>
    /// 씬이 로드된때마다 실행
    /// </summary>
    /// <param name="m_getEmpInfo"></param>
    public void ActionCard_AR_Set(string[] m_getEmpInfo) //url을 통해 들어왔을때 실행. 씬이 로드된 후 실행.
    {
        Debug.Log("-------BRNO: " + m_getEmpInfo[0] + "------ENOB: " + m_getEmpInfo[1]);

        //매개변수의 값이 null이 아닐때. 행번정보가 있을때. [0] [1]
        if (m_getEmpInfo[0] != null && m_getEmpInfo[1] != null && !m_getEmpInfo[0].Equals("") && !m_getEmpInfo[1].Equals("") && !m_getEmpInfo[0].Equals("null") && !m_getEmpInfo[1].Equals("null"))
        {
            Debug.Log("URL로 들어온 매개변수 정보가 존재합니다.");
            uiControll_s.click_ocr_info_successButton(m_getEmpInfo); //매개변수값을 통해 DB 직원테이블에서 직원정보를 조회한다.
            backControll.changeStep(this.gameObject, BackControll.ARCARD_STEP.ARMain);
        }
        else //매개변수가 없음.
        {
            Debug.Log("URL로 들어온 매개변수 정보가 없습니다.");
            //아무 행동없는건지 알럿 띄워줘야하는지 물어보기.
        }
    }


    public void onLoading()
    {
        Debug.Log("--------onloading()");
        arCardAll_Get_s.Load_start();
        ocrControll_01_s.isLodingCheck = false;
    }
}
