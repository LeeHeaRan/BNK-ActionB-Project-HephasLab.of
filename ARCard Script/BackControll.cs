 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 디바이스의 뒤로가기 버튼을 눌렀을때 실행되는 함수.
/// 스텝을 나누어 진행된다.
/// </summary>
public class BackControll : MonoBehaviour
{
    public enum ARCARD_STEP
    {
        OCRMain,
        OCRGuide,
        OCRModal,
        OCRDBError,
        ARMain,
        OCRAlarm
    }

    public ARCARD_STEP arCard_Step = ARCARD_STEP.OCRMain;
    public GameObject clickObj;
    public ARCardAll_Get arCardAll_Get; //init을 사용하기 위해 선언.
    UIControll uiControll_s;
    public All all;

    public GameObject UI_AR_Back;

    void Start()
    {
        uiControll_s = this.GetComponent<UIControll>();

        if (SplayerPrefs.isPlayerPrefs("isARCArdGuideKey"))
        {
            arCard_Step = ARCARD_STEP.OCRMain;
        }
        else
        {
            arCard_Step = ARCARD_STEP.OCRGuide;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor) //android
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //pc
            {
                BackButton();
            }
        }
    }

    public void BackButton()
    {
        Debug.Log("arCard_Step: " + arCard_Step);
        switch (arCard_Step)
        {
            case ARCARD_STEP.OCRMain:
                //아무것도 없음.
                arCardAll_Get.uIManager.GetComponent<UIManager>().onAppExitAlert();

                break;
            case ARCARD_STEP.OCRGuide:

                uiControll_s.gestureGuide_AllClose();

                arCard_Step = ARCARD_STEP.OCRMain; 

                break;
            case ARCARD_STEP.OCRModal:
                //OCR메인화면으로 돌아감.
                //현재 모달창을 닫아준다.

                clickObj.GetComponent<UI_Control>().CloseUI();
                arCard_Step = ARCARD_STEP.OCRMain;


                break;
            case ARCARD_STEP.OCRDBError:
                //OCR정보 입력하는 화면으로 돌아감.
                //현재 팓업 창을 닫아준다..
                clickObj.GetComponent<UI_Control>().CloseUI();
                arCard_Step = ARCARD_STEP.OCRDBError;

                break;
            case ARCARD_STEP.ARMain: //모달을 띄워줌
                //OCR화면으로 돌아간다.
                //Debug.Log("ARMain Back2");
                UI_AR_Back.SetActive(true); //"메인으로 돌아가기"알럿을 띄워준다.
                break;
            case ARCARD_STEP.OCRAlarm: //모달을 띄워줌
                //아무것도 없음.
                arCard_Step = ARCARD_STEP.OCRMain;
                break;
            default:
                break;
        }
    }

    public void changeStep(GameObject obj, ARCARD_STEP str)
    {
        arCard_Step = str;
        clickObj = obj;
    }

    public void ToOCRMain() //메인으로 돌아간다. 메인으로 돌아가기에 이동하기를 클릭했을경우 실행. 
    {
        Debug.Log("--------ToOCRMain()");

        if (arCard_Step == ARCARD_STEP.OCRMain)
        {
            Debug.Log("--------ToOCRMain()>>if");

            arCardAll_Get.init(); //껐다 키면서 초기화
            all.show_BottonMenu();

            //로딩창을 띄워준다.
            all.onLoading();
        }
        else if (arCard_Step == ARCARD_STEP.ARMain)
        {
            all.hide_BottonMeun();
        }

    }

    public void stepOCRMain()
    {
        Debug.Log("--------stepOCRMain()");
        arCard_Step = ARCARD_STEP.OCRMain;
    }
}

