using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppExitAlert : MonoBehaviour
{
    UIManager ui_manager;
    private void Awake()
    {
        ui_manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }
    public void AppExit()
    {
        ui_manager.AppQuit();
    }
}
