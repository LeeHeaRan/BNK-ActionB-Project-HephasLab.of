using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 자동차 오브젝트가 트리거할 박스에 저장될 정보값.
/// 회전속도, 다음타겟(트리거), 외부로 빠지는 자동차인지 확인하는 체크사항. 사용자 조정이 가능하다.
/// </summary>
public class AnimationBox : MonoBehaviour
{
    public float rotSpeed = 0.5f;
    public GameObject nextTarget;
    public GameObject sideTarget;
}
