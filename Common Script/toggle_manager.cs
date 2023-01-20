using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle_manager : MonoBehaviour
{
    public Toggle toggleHead;
    public Toggle[] toggles;
    public Toggle[] optional_toggles;
    public SubToggle subtoggle;
    bool isALLbtn = false;
    bool isSubCon = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool[] GetTogglesIsOn()
    {
        bool[] isOn = new bool[toggles.Length + optional_toggles.Length];
        for (int i = 0; i < toggles.Length; i++)
        {
            isOn[i] = toggles[i].isOn;
        }
        for (int i = 0; i < optional_toggles.Length; i++)
        {
            isOn[i] = optional_toggles[i].isOn;
        }
        return isOn;
    }
    public bool GetTogglesALLOn()
    {
        bool isOn = true;
        for (int i = 0; i < toggles.Length; i++)
        {
            if (!toggles[i].isOn) isOn = false;
        }
        return isOn;
    }
    public bool[] GetoptionalToggleIsOn()
    {
        bool[] isOn = new bool[optional_toggles.Length];

        for (int i = 0; i < optional_toggles.Length; i++)
        {
            isOn[i] = optional_toggles[i].isOn;
        }
        return isOn;
    }

    public void ALL_Toggle_Active(bool isOn)
    {
        if (!isSubCon)
        {
            isALLbtn = isOn;
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn != isOn) toggles[i].isOn = isOn;
            }
            for (int i = 0; i < optional_toggles.Length; i++)
            {
                if (optional_toggles[i].isOn != isOn) optional_toggles[i].isOn = isOn;
            }
        }
        else
        {
            isSubCon = false;
        }


    }
    public void CheckToggle()
    {
        bool tempCheck = true;
        if (!isALLbtn)
        {

            for (int i = 0; i < toggles.Length; i++)
            {
                if (!toggles[i].isOn) tempCheck = false;

            }
            for (int i = 0; i < optional_toggles.Length; i++)
            {
                if (!optional_toggles[i].isOn) tempCheck = false;

            }

            if (toggleHead.isOn != tempCheck) toggleHead.isOn = tempCheck;
        }
        else
        {
            tempCheck = true;
            for (int i = 0; i < toggles.Length; i++)
            {
                if (!toggles[i].isOn) tempCheck = false;

            }
            for (int i = 0; i < optional_toggles.Length; i++)
            {
                if (!optional_toggles[i].isOn) tempCheck = false;

            }
            if (!tempCheck)
            {
                isSubCon = true;
                isALLbtn = false;
                toggleHead.isOn = false;

            }

        }

    }
    public void Toggle_3_Close()
    {
        subtoggle.GroupClose();
    }
    public void Cancel()
    {
        ALL_Toggle_Active(false);
    }
    private void OnDisable()
    {
        Cancel();
    }

}
