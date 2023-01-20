using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputField_Status : MonoBehaviour
{
    public Sprite FailChangeSprite;
    public Sprite PassChangeSprite;
    public Sprite NormalSprite;

    public GameObject info;
    public Color BaseColor;
    public Vector2 infoFpos;

    bool isChange = false;
    public bool isinfoActive = true;

    private void Start()
    {
        if (info != null)
        {
            if(isinfoActive) infoFpos = info.GetComponent<RectTransform>().anchoredPosition;
        }

    }

    public void SetFailChangeSprite()
    {
        gameObject.GetComponent<Image>().sprite = FailChangeSprite;
        isChange = true;
    }
    public void SetFailChangeSprite(string info_text)
    {
        gameObject.GetComponent<Image>().sprite = FailChangeSprite;
        if (isinfoActive) info.SetActive(true);
        if (info_text != "") { info.GetComponent<Text>().text = info_text; }
        info.GetComponent<Text>().color = new Color(1f, 0.2313726f, 0.1882353f);
        isChange = true;
    }





    public void SetPassChangeSprite()
    {
        gameObject.GetComponent<Image>().sprite = PassChangeSprite;
       
        if(info!=null && info.activeSelf)
        {
            if (isinfoActive) info.SetActive(false);
            else info.GetComponent<Text>().text = "";
        }
        isChange = true;
    }
    public void SetPassChangeSprite(string info_text)
    {
        info.GetComponent<Text>().color = BaseColor;
        gameObject.GetComponent<Image>().sprite = PassChangeSprite;
        if(isinfoActive) info.SetActive(true);
        info.GetComponent<Text>().text = info_text;
        isChange = true;
    }
    public void SetInfo(string info_text)
    {
        GetBackSprite();
        if (isinfoActive) info.SetActive(true);
        info.GetComponent<Text>().text = info_text;
        isChange = true;

      
    }

    public void GetBackSprite()
    {
        if (isChange)
        {
            if(BaseColor.a != 0)
            {
               if(info!=null) info.GetComponent<Text>().color = BaseColor;
            }
            
            gameObject.GetComponent<Image>().sprite = NormalSprite;
            if (info != null)
            {
                if (isinfoActive) info.SetActive(false);
                else info.GetComponent<Text>().text = "";
            }
            isChange = false;
        }
    }
    public void GetBackSprite(bool isTextVoid)
    {
        if (isChange)
        {
            if (BaseColor.a != 0)
            {
                if (info != null) info.GetComponent<Text>().color = BaseColor;
            }

            gameObject.GetComponent<Image>().sprite = NormalSprite;
            if (info != null)
            {
                if (isTextVoid) info.GetComponent<Text>().text = "";
            }
            isChange = false;
        }
    }
    public void infoMove(float y)
    {
        info.GetComponent<RectTransform>().anchoredPosition = new Vector2(infoFpos.x, -120);
    }
    public void infoNewMove(float y)
    {
        info.GetComponent<RectTransform>().anchoredPosition = new Vector2(infoFpos.x, y);
    }
    public void infoPosInit()
    {
        info.GetComponent<RectTransform>().anchoredPosition = infoFpos;
    }

    private void OnEnable()
    {
      
    }
    private void OnDisable()
    {
       
        isChange = false;
        if (info != null)
        {
            info.GetComponent<RectTransform>().anchoredPosition = infoFpos;
            if (isinfoActive)  info.SetActive(false);
            else info.GetComponent<Text>().text = "";
        }
        gameObject.GetComponent<Image>().sprite = NormalSprite;
    }
}
