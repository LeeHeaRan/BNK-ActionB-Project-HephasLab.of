using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 사용자가 ARObject를 화면상에 보이게 둔 상태인지 체크할 수 있는지 확인한다.
/// </summary>
public class ARCamera_Trigger : MonoBehaviour
{
    public bool isInObject = false; 

    private void OnTriggerExit(Collider other)
    {
        isInObject = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isInObject = true;
    }
}

