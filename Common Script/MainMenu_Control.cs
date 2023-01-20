using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Control : MonoBehaviour
{
    [SerializeField]
    List<GameObject> menu = new List<GameObject>();


 
    public PlayTopUI playTopUI;
    
    public float after_Opacity_value;
    public float base_Opacity_value;
    public float time;
    int OnIndex = 0;
    int Offindex = 1;

    bool isMove = false;
    float lerp_value=0;
    Hashtable OnHash,OffHash,UIHash;

    [Header("Chagne ALL Color")]
    public int ChangeColor_menu_index;
    public Color Base_Color;
    public Color Change_Color;
    bool isChangeColor = false;

    bool isMainMenuState = false;
    bool isShowbottomMenu = false;

    // Start is called before the first frame update
    void Awake()
    {
        lerp_value = menu[1].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y/ (after_Opacity_value -base_Opacity_value);
        OnHash = new Hashtable();
        OnHash.Add("name", "on");
        OnHash.Add("from", base_Opacity_value);
        OnHash.Add("to", after_Opacity_value);
        OnHash.Add("easeType", iTween.EaseType.linear);
        OnHash.Add("loopType", "once");
        OnHash.Add("time", time);
        OnHash.Add("onupdatetarget", gameObject);
        OnHash.Add("onupdate", "SetOnValue");
        OnHash.Add("oncompletetarget", gameObject);
        OnHash.Add("oncomplete", "OnComplete");
    

    OffHash = new Hashtable();
        OffHash.Add("name", "off");
        OffHash.Add("from", after_Opacity_value);
        OffHash.Add("to", base_Opacity_value);
        OffHash.Add("easeType", iTween.EaseType.linear);
        OffHash.Add("loopType", "once");
        OffHash.Add("time", time);
        OffHash.Add("onupdatetarget", gameObject);
        OffHash.Add("onupdate", "SetOffValue");


        UIHash = new Hashtable();
        UIHash.Add("from", gameObject.GetComponent<RectTransform>().anchoredPosition.y);
        UIHash.Add("to", 163);
        UIHash.Add("easeType", iTween.EaseType.linear);
        UIHash.Add("loopType", "once");
        UIHash.Add("time", 0.2f);
        UIHash.Add("onupdatetarget", gameObject);
        UIHash.Add("onupdate", "SetUIValue");

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void SetButtonEvent(int index)
    {
       
        if (OnIndex != index && !isMove)
        {
            isMove = true;
            Offindex = OnIndex;
            OnIndex = index;
            if (menu[OnIndex].GetComponent<iTween>() != null)
            {
                iTween.Stop(menu[OnIndex], "off");
                OnHash["from"] = menu[OnIndex].transform.GetChild(0).GetComponent<Image>().color.a;
              
            }
          

            if(menu[Offindex].GetComponent<iTween>() != null)
            {
                iTween.Stop(menu[Offindex], "on");
                OffHash["from"] = menu[Offindex].transform.GetChild(0).GetComponent<Image>().color.a;
              
            }

            
         





            menu[OnIndex].transform.GetChild(1).gameObject.SetActive(true);
            menu[Offindex].transform.GetChild(1).gameObject.SetActive(false);
            iTween.ValueTo(menu[Offindex], OffHash);
            iTween.ValueTo(menu[OnIndex], OnHash);

            if (OnIndex != 4)//더보기
            {
                playTopUI.StartSceneLoading(OnIndex);


                string send_value = "";
                switch (OnIndex)
                {
                    case 0:
                        send_value = "01";
                        break;
                    case 2:
                        send_value = "02";
                        break;
                    case 3:
                        send_value = "03";
                        break;

                }

                if (send_value != "") { playTopUI.PageViewLog(send_value); }





                
            }
      


        }
    }
    public void initUI()
    {

        Vector2 pos1 = menu[0].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;

        Vector2 pos2 = menu[OnIndex].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
        menu[OnIndex].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(pos1.x, -6.74001f);
        menu[OnIndex].transform.GetChild(1).gameObject.SetActive(false);


     
        menu[0].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(pos1.x, 0f);
        menu[0].transform.GetChild(1).gameObject.SetActive(true);

        isChangeColor = false;
        for (int i = 0; i < menu.Count; i++)
        {
            menu[i].transform.GetChild(0).GetComponent<Image>().color = new Color(Base_Color.r, Base_Color.g, Base_Color.b, 0.627451f);
            menu[i].transform.GetChild(1).GetComponent<Image>().color = new Color(Base_Color.r, Base_Color.g, Base_Color.b, 1f);
        }
        menu[0].transform.GetChild(0).GetComponent<Image>().color = new Color(Base_Color.r, Base_Color.g, Base_Color.b,1f);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x, 0);
        isMainMenuState = false;
        OnIndex = 0;
         Offindex = 1;
        gameObject.SetActive(false);

    }
    public void HideUI()
    {
        if (isMainMenuState)
        {
            if (gameObject.GetComponent<iTween>() != null)
            {
                iTween.Stop(gameObject);
            }
            isMainMenuState = false;
            UIHash["from"] = gameObject.GetComponent<RectTransform>().anchoredPosition.y;
            UIHash["to"] = 0;
            isShowbottomMenu = false;
            iTween.ValueTo(gameObject, UIHash);
        }
    }

    public void ShowUI()
    {
        Debug.Log("ShowUI실행");
        if (!isMainMenuState)
        {
            Debug.Log("ShowUI실행isMainMenuState");
            if (gameObject.GetComponent<iTween>() != null)
            {
                iTween.Stop(gameObject);
            }
            isMainMenuState = true;
            UIHash["from"] = gameObject.GetComponent<RectTransform>().anchoredPosition.y;
            UIHash["to"] = 94;
            isShowbottomMenu = true;
            iTween.ValueTo(gameObject, UIHash);
        }
    }
    public void StartShowUI()
    {

        if (!isMainMenuState)
        {
            Debug.Log("ShowUI실행isMainMenuState");
            isMainMenuState = true;
            UIHash["from"] = gameObject.GetComponent<RectTransform>().anchoredPosition.y;
            UIHash["to"] = 94;
            isShowbottomMenu = true;
            iTween.ValueTo(gameObject, UIHash);
        }
        
    }
   
    public void SetOnValue(float value)
    {
      
        Vector2 pos = menu[OnIndex].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
        pos.y = lerp_value * (after_Opacity_value - value);
        menu[OnIndex].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = pos;

        Color color = menu[OnIndex].transform.GetChild(0).GetComponent<Image>().color;
        color.a = value;
        menu[OnIndex].transform.GetChild(0).GetComponent<Image>().color = color;

    }
    public void SetOffValue(float value)
    {
        
        Vector2 pos = menu[Offindex].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
        pos.y = lerp_value * (after_Opacity_value - value);
        Debug.Log(pos);
        menu[Offindex].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = pos;

        Color color = menu[Offindex].transform.GetChild(0).GetComponent<Image>().color;
        color.a = value;
        menu[Offindex].transform.GetChild(0).GetComponent<Image>().color = color;

    }
    
    public void SetUIValue(float value)
    {


       gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x, value);


    }

    public void OnComplete()
    {

        if (OnIndex == ChangeColor_menu_index)
        {
            if (!isChangeColor)
            {
                isChangeColor = true;
                for (int i = 0; i < menu.Count; i++)
                {
                    float tempA = menu[i].transform.GetChild(0).GetComponent<Image>().color.a;
                    menu[i].transform.GetChild(0).GetComponent<Image>().color = new Color(Change_Color.r, Change_Color.g, Change_Color.b, tempA);
                    menu[i].transform.GetChild(1).GetComponent<Image>().color = new Color(Change_Color.r, Change_Color.g, Change_Color.b, tempA);
                }
            }
        }
        else
        {
            if (isChangeColor)
            {
                isChangeColor = false;
                for (int i = 0; i < menu.Count; i++)
                {
                    float tempA = menu[i].transform.GetChild(0).GetComponent<Image>().color.a;
                    menu[i].transform.GetChild(0).GetComponent<Image>().color = new Color(Base_Color.r, Base_Color.g, Base_Color.b, tempA);
                    menu[i].transform.GetChild(1).GetComponent<Image>().color = new Color(Base_Color.r, Base_Color.g, Base_Color.b, tempA);
                }

            }

        }
      
            if (OtherSetting.isLoadType)
            {
          
                playTopUI.MenuUnActive();
                playTopUI.MenuActive(OnIndex);
            }
            else
            {
                if (OnIndex == 0 && Offindex == 2)
                {
                     playTopUI.chikapokaUnload();
                }
                else
                {
                    playTopUI.MenuUnLoad();
                    playTopUI.MenuLoadScene(OnIndex);
                }
            }
            Application.targetFrameRate = 60;
            isMove = false;
        


      //  uiManager.StartSceneLoadingVideo(OnIndex);


       
    }

  


}
