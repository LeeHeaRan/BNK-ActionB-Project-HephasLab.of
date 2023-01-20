using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// OCR인식이 되고 나오는 정보창을 관리.
/// 번들 작업.
/// </summary>
public class ocr_alert_uicontrol : MonoBehaviour
{
    public UIControll uicontrol;

    public void on_ocr_infomodal_()
    {
        uicontrol.on_ocr_info_modal();
    }

    public void on_ocr_infomodal_close_()
    {
        uicontrol.ocr_infoModal_close();
    }

}
