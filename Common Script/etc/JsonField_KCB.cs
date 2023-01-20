using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class JsonField_KCB
{

    public object TBClass;

    JsonField_KCB()
    {
        TBClass = this;
    }
    /// <summary>
    /// 회원관련 변수
    /// </summary>
    
    [SerializeField] msg _msg_;        //교유관리번호
    [SerializeField] string _common_;          //EMAIL 주소(아이디로 사용)
    [SerializeField] string errorpage;            //회원 비밀번호
    [SerializeField] string errorbiztype;              //회원명
    [SerializeField] string errormessage;            //주민등록번호
    [SerializeField] string errorcustomermessage;            //휴대폰 번호
    [SerializeField] string errorpageparameters;          //회원 CI값
    [SerializeField] string ib20_err_act;             //회원 별명
    [SerializeField] string resCode;        //셩별 구분코드
    [SerializeField] string errorinstanceid;          //SNS 고유번호
    [SerializeField] string errorcode;           //로그인 구분코드

    public string GetValue()
    {
        return _msg_.GetValue();
    }

    public class msg
    {
        [SerializeField] common _common_;

        public string GetValue()
        {
            return _common_.GetValue();
        }

        public class common
        {
            [SerializeField] string errorpage;            //회원 비밀번호
            [SerializeField] string errorbiztype;              //회원명
            [SerializeField] string errormessage;            //주민등록번호
            [SerializeField] string errorcustomermessage;            //휴대폰 번호
            [SerializeField] string errorpageparameters;          //회원 CI값
            [SerializeField] string ib20_err_act;             //회원 별명
            [SerializeField] string resCode;        //셩별 구분코드
            [SerializeField] string errorinstanceid;          //SNS 고유번호
            [SerializeField] string errorcode;           //로그인 구분코드

            public string GetValue()
            {
                return errorpage;
            }
        }
    }

    public Dictionary<string, string> GetDictionary()
    {


        Dictionary<string, string> Data = new Dictionary<string, string>();

        var field = typeof(JsonField_KCB).GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);


        for (int i = 0; i < field.Length; i++)
        {
            Data.Add(field[i].Name, field[i].GetValue(TBClass).ToString());

        }


        return Data;

    }

    public string[] GetKeyName()
    {
        var field = typeof(JsonField_KCB).GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
        string[] tempData = new string[field.Length];
        for (int i = 0; i < field.Length; i++)
        {
            tempData[i] = field[i].Name;

        }

        return tempData;
    }

}