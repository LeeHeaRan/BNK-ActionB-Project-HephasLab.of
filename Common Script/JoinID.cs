using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinID : MonoBehaviour
{

    public GameObject id_input;
    public GameObject edomin_input;
    public GameObject custom_edomain;
    public GameObject edomain_selectBox;

    public void init()
    {
        id_input.GetComponent<InputField>().text = "";
        edomain_selectBox.GetComponent<ToogleAction>().ToggleInit();
        edomin_input.GetComponentInChildren<Text>().text = "@naver.com";
        edomain_selectBox.SetActive(false);
    }
    public void SetIDField_Focus()
    {
        id_input.GetComponent<InputField>().ActivateInputField();
    }
    public string Get_ID()
    {
        return id_input.GetComponent<InputField>().text;
    }
    public string GetFull_ID()
    {
        return id_input.GetComponent<InputField>().text+GetEdomain();
    }
    public string GetFull_ID(string dmain)
    {
        return id_input.GetComponent<InputField>().text+ dmain;
    }
    public string GetEdomain()
    {

        if (custom_edomain.activeSelf)
        {

            return "@"+custom_edomain.GetComponent<InputField>().text;
        }
        else
        {
            return edomin_input.GetComponentInChildren<Text>().text;
        }

    }
    public bool IDCheck()
    {
        return Validation.ID_Check(Get_ID());
    }
    public bool EdomainCheck()
    {
        return Validation.EmailCheck(GetEdomain());
    }

    public void FullID_OK()
    {
        id_input.GetComponent<InputField_Status>().SetPassChangeSprite("사용가능한 이메일입니다.");
        id_input.GetComponent<InputField_Status>().GetBackSprite();
    }
    public void FullID_Fail()
    {
        id_input.GetComponent<InputField_Status>().SetFailChangeSprite("이미 사용중인 아이디가 있습니다.");
    }
    public void ID_notCharFail()
    {
        id_input.GetComponent<InputField_Status>().SetFailChangeSprite("아이디형식이 올바르지 않습니다.");
    }
    public void Email_notCharFail()
    {
        custom_edomain.GetComponent<InputField_Status>().SetFailChangeSprite("이메일 형식을 확인해주세요!");
    }
    public void GetBackInfo()
    {
        id_input.GetComponent<InputField_Status>().GetBackSprite();
    }
    public void ID_InfoMove(bool isMove)
    {
        if (isMove) id_input.GetComponent<InputField_Status>().infoMove(-120f);
        else id_input.GetComponent<InputField_Status>().infoPosInit();

    }
    public void EmailCustomClose()
    {
        custom_edomain.GetComponent<InputField>().text = "";
    }

 



    private void OnDisable()
    {
      
    }


}
