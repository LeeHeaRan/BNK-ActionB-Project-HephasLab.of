using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 배너의 UI애니메이션
/// </summary>
public class OtherUIAnimation : MonoBehaviour
{
    public GameObject target;
    [Tooltip("scale이 작아질수록 오브젝트가 작아진다.")]
    public float scale;

    /// <summary>
    /// 매인배너 상단의 캐릭터. 크기 줄이고 위치 위로 이동.
    /// </summary>
    public void StartAnimation()
    {
        if (this.GetComponent<iTween>() == null)
        {
            iTween.ScaleTo(target.gameObject, new Vector3(scale, scale, scale), 0.5f); //Scale이 조작된다.
        }
    }

    /// <summary>
    /// 매인배너 상단의 캐릭터. 원래 크기 및 위치로 이동.
    /// </summary>
    public void EndAnimation()
    {
        if(this.GetComponent<iTween>() == null)
        {
            iTween.ScaleTo(target.gameObject, new Vector3(1,1,1), 0.3f);
        }
    }

    private void OnDisable()
    {
        iTween.ScaleTo(target.gameObject, new Vector3(1, 1, 1), 0.5f);
    }
    
}


