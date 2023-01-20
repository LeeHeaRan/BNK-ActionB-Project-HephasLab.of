using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 디바이스 해상도에 따라 UI의 위치를 조정한다.
/// </summary>
public class RePosiion : MonoBehaviour
{
    public float PosY; //이동할 만큼의 값
    public float PosX;

    float ratio;
    Vector3 rePositionVector;
    
    private void Awake()
    {
        if(PosY != 0)
        {
            ratio = Screen.height / 740f;
            float val = PosY * ratio;
            rePositionVector = new Vector3(gameObject.GetComponent<RectTransform>().anchoredPosition.x, val);
            gameObject.GetComponent<RectTransform>().anchoredPosition = rePositionVector;
        }

        if(PosX != 0)
        {
            ratio = Screen.height / 740f;
            float val = PosX * ratio;
            rePositionVector = new Vector3(val, gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            gameObject.GetComponent<RectTransform>().anchoredPosition = rePositionVector;
        }
       

    }

}
