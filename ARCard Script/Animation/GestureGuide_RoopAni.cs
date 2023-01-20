using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

/// <summary>
/// ��ư ������ ������ ȭ��ǥ �ִϸ��̼��� ����.
/// ġī��ī�� �Ǽ��縮 ���� ����.
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
                //�ƹ��͵� ����.
                targetImage.sprite = changeImage[0];
            }
            else //���׶��, ȭ��ǥ��. ���� �־���
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
            Debug.Log("�̹��� ������Ʈ�� �����ϴ�.");
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

        //�̹��� �ٲٱ�.ġī��ī�� ���.
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

        if (val > time/2) //�ð��� ������ ������
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
        //������Ʈ�� ��������. �ʱ�ȭ ���ش�.
        targetImage.fillAmount = 0;
        isUpdateCheck = false;
        iTween.Stop(this.gameObject);
        //Debug.Log(targetImage.name + "  Disable!!");
    }
}
