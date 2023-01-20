using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeContentControl : MonoBehaviour
{
    public Notice_control notice_Control;
    public string uid;
    public GameObject fullcontent;
    public Text title;
    public GameObject DateText;

    public GameObject move_text;

    private string url = "";
    public GameObject NewIcon;


    private void Start()
    {
    
    }

    public void SetText(Dictionary<string, string> data)
    {
        uid = data["ARA_LEME_NATV_MGNO"];
        title.text = "[" + data["NUMROW"] + "]" + data["ARA_LEME_LRG_TIT"];
        fullcontent.GetComponent<Text>().text =data["ARA_LEME_DTL_CNTN"];
        DateText.GetComponent<Text>().text = data["ARA_LEME_STRT_DTTI"].Substring(0, 4) + "-" + data["ARA_LEME_STRT_DTTI"].Substring(4, 2) + "-" + data["ARA_LEME_STRT_DTTI"].Substring(6, 2);

        if (!data["SV_RQST_URL"].Equals("null"))
        {
            move_text.SetActive(true);
            move_text.GetComponent<Text>().text = "상세페이지로 이동하기";
            url = data["SV_RQST_URL"];
        }
        if (data["ARA_MBRS_CI_VAL"].Equals("null"))
        {
            NewIcon.SetActive(true);
        }
    }
    public void BoardContentsClick()
    {
        notice_Control.Send_UserCheck(uid);
    }
    public void NewIconRemove()
    {
        NewIcon.SetActive(false);
    }

    public void SetScale(Vector2 vec2)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = vec2;
    }
    public void MoveURL()
    {
        if (url != "")
        {
            Application.OpenURL(url);
        }
    }
}
