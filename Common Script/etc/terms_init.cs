using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class terms_init : MonoBehaviour
{
    public Button BackBtn;
    public Email_JoinStep Manager;
   
    public void CloseClick()
    {
        if (Manager != null)
        {
            Manager.TermsClose();
        }
        else
        {
            gameObject.GetComponentInParent<terms_control>().TermsClose();
        }
    }
    private void OnEnable()
    {
        
        Manager = gameObject.GetComponentInParent<Email_JoinStep>();
        BackBtn.onClick.AddListener(gameObject.GetComponent<UI_Control>().CloseUI);

    }
    
}
