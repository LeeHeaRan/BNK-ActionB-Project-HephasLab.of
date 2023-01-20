using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class join_NextField_init : MonoBehaviour
{

    public JoinID joinID;
    public void init()
    {

        joinID.init();



    }
    public void SetFocus()
    {
        joinID.SetIDField_Focus();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}
