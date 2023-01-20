using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotMemberAlertControl : MonoBehaviour
{
    UIManager ui_manager;
    private void Awake()
    {
        ui_manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }
    public void NotMemeberLoginMove()
    {
        ui_manager.NotMemeberLoginMove();
    }
}
