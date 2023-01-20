using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����İ��̵��� �ؽ�Ʈ �̹����� ũ�⸦ ����. 
/// �����۾�
/// </summary>
public class GuideText_ReSize : MonoBehaviour
{
    public Vector2 originSize = Vector2.zero; //���� �Է�
    Vector2 reSize = Vector2.zero;
    float ratio = 0f;

    void Start()
    {

        if(Screen.width <= 900 || (Screen.width <= 1080 && Screen.height > 2500))
        {
            ratio = (originSize.x / Screen.width) * 1.4f;
            reSize = new Vector2(originSize.x * ratio, originSize.y * ratio);
            this.GetComponent<RectTransform>().sizeDelta = reSize;
        }
       
    }

}
