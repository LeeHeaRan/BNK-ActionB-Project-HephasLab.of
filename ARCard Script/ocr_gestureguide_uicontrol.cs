using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ocr_gestureguide_uicontrol : MonoBehaviour
{
    public UIControll uicontrol;
    public GameObject UI_blackbg; //가이드 버튼 클릭시 BG가 나오게 해주어야한다.


    private void Start()
    {
        //set_rectimage_center();
    }

    /// <summary>
    /// 명함인식 가이드의 다음버튼 클릭.
    /// </summary>
    public void click_ocr_gesturegudie_nextbtn()
    {
        this.transform.GetChild(0).gameObject.SetActive(true); //인포창
        this.transform.GetChild(3).gameObject.SetActive(true); //명함정보확인을 활성화
    }


    /// <summary>
    /// 명함정보확인 가이드의 다음버튼 클릭.
    /// </summary>
    public void clilck_ocr_gestureguide_exitbtn()
    {
        uicontrol.backControll.changeStep(this.gameObject, BackControll.ARCARD_STEP.OCRMain);
    }

    public void closeAllGuide()
    {
        this.transform.GetChild(0).gameObject.GetComponent<UI_Control>().CloseUI();
        this.transform.GetChild(1).gameObject.GetComponent<UI_Control>().CloseUI();
        this.transform.GetChild(2).gameObject.GetComponent<UI_Control>().CloseUI();
        this.transform.GetChild(3).gameObject.GetComponent<UI_Control>().CloseUI();
    }



}
