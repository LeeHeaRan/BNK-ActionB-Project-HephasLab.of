using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// OCR�ν��� �ǰ� ������ ����â�� ����.
/// ���� �۾�.
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
