using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boardContentControl : MonoBehaviour
{
    public GameObject fullcontent;
    public Text title;
    public GameObject SmallCotents;
    public GameObject DateText;
    public GameObject arrwor;
    public Font bold;
    public Font Default;
    Vector2 origSize;
    Vector2 DateOrigPos;
    bool isDetail = false;
    
    private void Start()
    {
        origSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        DateOrigPos = DateText.GetComponent<RectTransform>().anchoredPosition;
    }

    public void SetText(string title_str,string contents_str,string date)
    {
        title.text = title_str;
        SmallCotents.GetComponent<Text>().text = (contents_str.Length>20)?contents_str.Substring(0, 20): contents_str;
        fullcontent.GetComponent<TextChangeMatch>().SetText(contents_str);
        DateText.GetComponent<Text>().text = date.Substring(0, 4)+"-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);

    }
    public void BoardContentsClick()
    {
        if(!isDetail) // 닫힘
        {
            ShowDetailContents();
            isDetail = true;
        }
        else
        {
            HideDetailContents();
            isDetail = false;
        }
    }
    private void ShowDetailContents()
    {
        fullcontent.SetActive(true);
        title.font = bold;
        DateText.GetComponent<RectTransform>().anchoredPosition = new Vector2(DateOrigPos.x, SmallCotents.GetComponent<RectTransform>().anchoredPosition.y);
        SmallCotents.SetActive(false);
      
        arrwor.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 180), Space.World);

        if (gameObject.GetComponent<iTween>() != null)
        {
            iTween.Stop(gameObject);
        }

        Hashtable ht = new Hashtable();
        ht.Add("name", "open");
        ht.Add("from", origSize);
        ht.Add("to", new Vector2(origSize.x, origSize.y + fullcontent.GetComponent<TextChangeMatch>().Getheight()));
        ht.Add("easeType", iTween.EaseType.easeOutCubic);
        ht.Add("loopType", "once");
        ht.Add("time", 0.3f);
        ht.Add("onupdatetarget", gameObject);
        ht.Add("onupdate", "SetScale");

        iTween.ValueTo(gameObject, ht);
    }
    
    private void HideDetailContents()
    {
        fullcontent.SetActive(false);
        title.font = Default;
        SmallCotents.SetActive(true);
        
        DateText.GetComponent<RectTransform>().anchoredPosition = DateOrigPos;
        arrwor.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 180), Space.World);

        if (gameObject.GetComponent<iTween>() != null)
        {
            iTween.Stop(gameObject);
        }

        Hashtable ht = new Hashtable();
        ht.Add("name", "close");
        ht.Add("from", gameObject.GetComponent<RectTransform>().sizeDelta);
        ht.Add("to", origSize);
        ht.Add("easeType", iTween.EaseType.easeOutCubic);
        ht.Add("loopType", "once");
        ht.Add("time", 0.3f);
        ht.Add("onupdatetarget", gameObject);
        ht.Add("onupdate", "SetScale");

        iTween.ValueTo(gameObject, ht);
    }

    public void SetScale(Vector2 vec2)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = vec2;
    }


}
