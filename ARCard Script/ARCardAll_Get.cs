using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 다른 씬에서 접근할 수 있는 최상위 스크립트. 다른씬에서 접근할 수 있도록 하는 인터페이스 역할을 한다.
/// common씬에 있는 uiManager를 사용하기 위해 필요.
/// </summary>
public class ARCardAll_Get : MonoBehaviour
{
    public GameObject ARCardAll;
    public GameObject uIManager;
    bool isInit = false;
    bool isFirst = true;

    //스키마로 들어온 경우 호출함 함수.
    public All all;
    public GameObject arCamTest;

    string urlLog = "";

    void Start()
    {
        isInit = false;
    }

    void Update()
    {
        if (isInit && !ARCardAll.activeSelf)
        {
            ARCardAll.SetActive(true);
            isInit = false;
        }
        //uIManager.GetComponent<UIManager>().ContentsMove(1); //컨텐츠 이동하는 함수. AR배너 메뉴에 연결해주기.
        if (isFirst)
        {
            if (ARCardAll.activeSelf)
            {
                if (uIManager == null)
                {
                    try
                    {
                        if (GameObject.FindGameObjectWithTag("UIManager") != null)
                        {
                            uIManager = GameObject.FindGameObjectWithTag("UIManager");
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
                else
                {
                    isFirst = false;
                }
            }
        }

    }
    public void init()   //on버튼을 눌렀을때,
    {
        ARCardAll.SetActive(false);
        isInit = true;
    }

    public void ActiveOut() //off버튼을 눌었을때.
    {
        if (!arCamTest.activeSelf) //꺼져있다면
        {
            arCamTest.SetActive(true);
        }
        else
        {
            arCamTest.SetActive(false);
        }
    }

    public void hideBottonMenu()
    {
        if (uIManager != null)
        {
            if (uIManager != null) uIManager.GetComponent<UIManager>().HideMenu();
        }
    }

    public void showBottonMenu()
    {
        if (uIManager != null) uIManager.GetComponent<UIManager>().ShowMenu();
    }

    //카메라 셋팅이 끝났을때 호출.
    //OCRControll_01에서 실행해준다.
    public void Load_completed()
    {
        URL_ActionCard_ARLoad();
        //로딩화면을 꺼준다.
        if (uIManager != null) uIManager.GetComponent<UIManager>().EndSceneLoading("ARCardAll_Get");
    }
    public void Load_start()
    {
        Debug.Log("--------Load_start()");

        //로딩화면을 켜준다.
        if (uIManager != null) uIManager.GetComponent<UIManager>().StartSceneLoading(4);
    }

    public void actioncard_NotmemberAlerOpen()
    {
        if (uIManager != null) uIManager.GetComponent<UIManager>().NotMemberAlertOpen("ARCardAll_Get");
    }

    //uimaanger에 있는 함수로 직원정보를 불러오고. 값을 전달한다.
    //ar명함 카테고리를 클릭하면 SetDeeplinkinfo()를 실행해줘야한다.
    //값이 있는 링크 필요할때 실장님한테 말하기. 10/11

    //Schema
    //앱이 꺼져있는 상태에서 URL을 눌렀을때 실행.
    public void URL_ActionCard_ARLoad()
    {
        string[] getEmpInfo = uIManager.GetComponent<UIManager>().GetDeeplinkinfo(); //DeepLink_BRNO, DeepLink_ENOB는 "". 값을 참조할 수 없음. 

        all.ActionCard_AR_Set(getEmpInfo); //함수 안에서 AR로 가는거 분기 나눠서 실행.
    }

    //앱이 켜져있는 상태에서 URL을 눌렀을때 실행. url을 실행했을때 넘어오는 값을 받을 매개변수가 필요함.
    public void URL_ActionCard_ARReLoad(string[] deepData)
    {
        //카테고리가 명함인지도 확인하고 명함이 아닐경우 명함으로 이동하고 함수를 실행해줘야함. 10/12
        if (SceneManager.GetActiveScene().name != "BusanAR_Card")//다른 씬이 켜져있는 상태일때
        {
            uIManager.GetComponent<UIManager>().ContentsMove(3); //명함으로 이동
            //씬이 켜지면서 Load_completed()가 실행된다.
        }
        else //명함 카테고리일 경우. cam이 준비되어 있음. 바로 all.ActionCard_AR_Set을 실행해주면 됨.
        {
            string[] getEmpInfo = uIManager.GetComponent<UIManager>().GetDeeplinkinfo();
            all.ActionCard_AR_Set(getEmpInfo);
        }

        if (deepData != null)
        {
            foreach (string str in deepData)
            {
                Debug.Log("DATA: " + str);
            }
        }
    }
}
