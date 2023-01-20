using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectBoxAreaCheck : MonoBehaviour, IPointerClickHandler
{
    public GameObject TargetObject;
    public Email_JoinStep eJoinStep;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.rawPointerPress.name);

        if (eventData.rawPointerPress != TargetObject)
        {
            if (TargetObject.transform.Find(eventData.rawPointerPress.name) == null)
            {
                TargetObject.SetActive(false);

                if (eJoinStep != null)
                {
                    eJoinStep.EmailField_Dselect();
                }
            }
        }
    }

}
