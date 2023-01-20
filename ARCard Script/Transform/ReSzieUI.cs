using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 디바이스 해상도에 따라 UI의 사이즈를 조정한다.
/// </summary>
public class ReSzieUI : MonoBehaviour
{

    public Vector2 Orig_imaeSize;
    Vector2 reSizePos;
    float ratio;
    public RectTransform Center;
    public RectTransform ChildrenTransform;
    public GameObject guideRect;
    //1440 2960///
    //360 740  ///
    // Start is called before the first frame update
    void Start()
    {
        ratio = Screen.width / 360f; //비율을 구한다.
        
        if(Screen.width > 1500f)
        {
            ratio *= 0.6f;
        }

        reSizePos = new Vector2((Orig_imaeSize.x * ratio), Orig_imaeSize.y * ratio); //새로운 사이즈 벡터
        gameObject.GetComponent<RectTransform>().sizeDelta = reSizePos; //적용.

        if (Center != null)
        {
            //BG의 블러된 웹캠 이미지와 마스크된 원본이미지의 위치를 같게 한다. 
            this.gameObject.transform.position = Center.transform.position;
            //Debug.Log("CenterPosition!!!!");

        }

        if (ChildrenTransform != null)
        {
            transform.GetChild(0).transform.position = ChildrenTransform.transform.position;
        }
    }

    public Vector2 GetMasKSize()
    {
        return reSizePos;
    }
}
