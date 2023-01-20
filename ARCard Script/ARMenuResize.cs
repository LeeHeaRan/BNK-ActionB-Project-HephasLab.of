using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMenuResize : MonoBehaviour
{
    //AR화면에 가로 기준으로 값을 조정.
    //scale값을 조정한다. RectTransform으로 조정하면 안됨.
    float reSize;
    float ratio;

    void Start()
    {
        ratio = Screen.width / 1080f;

        reSize = ratio * 0.6f;

        if (Screen.width >= 1500f)
        {
            this.transform.localScale = new Vector3(reSize, reSize, reSize);
        }
    }

  
}
