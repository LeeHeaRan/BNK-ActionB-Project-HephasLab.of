using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ڰ� ARObject�� ȭ��� ���̰� �� �������� üũ�� �� �ִ��� Ȯ���Ѵ�.
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

