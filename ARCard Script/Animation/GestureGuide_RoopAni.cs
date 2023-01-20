using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 버튼 주위의 원형과 화살표 애니메이션을 관리.
/// 치카포카의 악세사리 변경 조정.
/// </summary>
public class GestureGuide_RoopAni : MonoBehaviour
{
    public ColorType color;
    public float time;

    public Sprite[] changeImage = new Sprite[2];



    Image targetImage;
    bool isUpdateCheck;
    Color imageColor;

    public enum ColorType
    {
        Green,
        Yellow
    }


    void Start()
    {

        if (GetComponent<Image>() != null)
        {
            targetImage = GetComponent<Image>();

            if(transform.gameObject.name.Contains("Change"))
            {
                //아무것도 안함.
                targetImage.sprite = changeImage[0];
            }
            else //동그라미, 화살표임. 색상 넣어줌
            {
                switch (color)
                {
                    case ColorType.Green:
                        if (ColorUtility.TryParseHtmlString("#3eff8e", out imageColor)) targetImage.color = imageColor;

                        break;
                    case ColorType.Yellow:
                        if (ColorUtility.TryParseHtmlString("#f7ff5d", out imageColor)) targetImage.color = imageColor;

                        break;
                    default:

                        break;
                }
            }
               
        }
        else
        {
            Debug.Log("이미지 컴포넌트가 없습니다.");
        }


    }

    void Update()
    {
        if (!isUpdateCheck)
        {
            ImageAni_Start();  
        }
    }

    void ImageAni_Start()
    {
        isUpdateCheck = true;

        //이미지 바꾸기.치카포카에 사용.
        if (transform.gameObject.name.Contains("Change"))
        {
            iTween.ValueTo(transform.gameObject, iTween.Hash("from", 0f, "to", time, "onupdate", "imageChange_Update", "oncomplete", "imageChange_Completed", "time", time, "easetype", iTween.EaseType.linear));
        }
        else
        {
            iTween.ValueTo(transform.gameObject, iTween.Hash("from", 0f, "to", 1f, "onupdate", "ImageFillAmount_Update", "oncomplete", "ImageFillAmount_Completed", "time", time, "easetype", iTween.EaseType.linear));
        }
    }

    void imageChange_Update(float val)
    {

        if (val > time/2) //시간의 절반이 지나면
        {
            targetImage.sprite = changeImage[1];
        }
    }

    void imageChange_Completed()
    {
        isUpdateCheck = false;
        targetImage.sprite = changeImage[0];
    }


    void ImageFillAmount_Update(float val)
    {
        targetImage.fillAmount = val;
    }

    void ImageFillAmount_Completed()
    {
        isUpdateCheck = false;
        //Debug.Log(targetImage.name + "  End!!");
    }


    private void OnDisable()
    {
        //오브젝트가 꺼졌을때. 초기화 해준다.
        targetImage.fillAmount = 0;
        isUpdateCheck = false;
        iTween.Stop(this.gameObject);
        //Debug.Log(targetImage.name + "  Disable!!");
    }
}
