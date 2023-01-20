using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// OCR인식 화면에서 카메라, 도움말, 알림버튼을 눌렀을때 행동을 관리.
/// 번들작업.
/// </summary>
public class ocr_mainbtn_uicontol : MonoBehaviour
{
    public All all;
    public OCRControll_02 ocrcontroll_02; 
    public UIControll uicontroll;
    
    //OCR화면에서 알림버튼을 눌렀을때.
    public void click_ocr_alarmbtn()
    {

        if (!all.arCardAll_Get_s.uIManager.GetComponent<UIManager>().isLoginState()) //로그인 상태를 확인한다. 로그인이 안되어 있을경우.
        {
            //로그인이 안되어 있는 경우.
            all.actioncard_loginAlert();
            uicontroll.backControll.changeStep(this.gameObject, BackControll.ARCARD_STEP.OCRAlarm);
        }
        else
        {
            //알림내역 뷰를 띄워준다.

            all.Notice_Page();
            uicontroll.backControll.changeStep(this.gameObject, BackControll.ARCARD_STEP.OCRAlarm);
            Debug.Log("알림뷰 오픈");
            Debug.Log(uicontroll.backControll.arCard_Step+" 5번인지 체크");
        }
    }

    public void click_ocr_snapbtn()
    {
        ocrcontroll_02.SnapshotOCR();
    }

    public void click_ocr_helpbtn()
    {
        uicontroll.click_ocr_help();
    }
}
