/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum TB_TYPE { 

    MBRS_INFO  = 1, //회원테이블
    CRCTR_ITMZ = 2, //액션가면 테이블

    KCB=10

}


[SerializeField]
public class JsonField
{

    List<Dictionary<string, string>> Data = new List<Dictionary<string, string>>();
 


    public JsonField(TB_TYPE tbType, string JsonType){

        Data.Clear();
        //  object tempData;

        bool isSignType = JsonType.Contains("[");
        switch (tbType)
        {

            case TB_TYPE.MBRS_INFO:
                try
                {

                    
                    if (isSignType)
                    {
                        JsonType = "{\"TB\":" + JsonType + "}";
                        var tempData = JsonUtility.FromJson<GET_CALSS_ARRAY<TB_ISBM_ARA_MBRS_JN_INFO>>(JsonType);
                        for (int i = 0; i < tempData.TB.Length; i++)
                        {
                            Data.Add(tempData.TB[i].GetDictionary());
                        }
                    }
                    else
                    {
                        var tempData = JsonUtility.FromJson<TB_ISBM_ARA_MBRS_JN_INFO>(JsonType);
                       
                            Data.Add(tempData.GetDictionary());
                        
                    }
                   
                    
               
                   
                 

                }
                catch (Exception e)
                {
                    Debug.Log("json error  "+e);
                }
                break;

            case TB_TYPE.CRCTR_ITMZ:

                try
                {

                    if (isSignType)
                    {
                        JsonType = "{\"TB\":" + JsonType + "}";
                        var tempData = JsonUtility.FromJson<GET_CALSS_ARRAY<TB_ISBI_ARA_BHVR_ITMZ>>(JsonType);
                        for (int i = 0; i < tempData.TB.Length; i++)
                        {
                            Data.Add(tempData.TB[i].GetDictionary());
                        }
                    }
                    else
                    {
                        var tempData = JsonUtility.FromJson<TB_ISBI_ARA_BHVR_ITMZ>(JsonType);

                        Data.Add(tempData.GetDictionary());

                    }


                  
                }
                catch(Exception e)
                {

                    Debug.Log("json error   "+e);
                }
              
                break;


            case TB_TYPE.KCB:

                try
                {

                    JsonType = JsonType.Replace(".", "");
                   JsonType = JsonType.Replace("Sucess :", "").Trim();
                    var tempData = JsonUtility.FromJson<JsonField_KCB>(JsonType);

                        Data.Add(tempData.GetDictionary());

                }
                catch (Exception e)
                {

                    Debug.Log("json error   " + e);
                }

                break;


        }



    }
    public List<Dictionary<string,string>> GetDictionary()
    {
        return Data;
    }


    /// <summary>
    /// TB_ISBM_ARA_MBRS_JN_INFO 테이블 관련
    /// </summary>
    /// 
 
    [Serializable]
    private class GET_CALSS_ARRAY<T>
    {
        public T[] TB;


    }
    [Serializable]
    private class TB_ISBM_ARA_MBRS_JN_INFO
    {

        public object TBClass;

        TB_ISBM_ARA_MBRS_JN_INFO()
        {
            TBClass = this;
        }
        /// <summary>
        /// 회원관련 변수
        /// </summary>
        [SerializeField] string ARA_TBL_NATV_MGNO;        //교유관리번호
        [SerializeField] string ARA_MBRS_EMLADR;          //EMAIL 주소(아이디로 사용)
        [SerializeField] string ARA_MBRS_PSWD;            //회원 비밀번호
        [SerializeField] string ARA_MBRS_NM;              //회원명
        [SerializeField] string ARA_MBRS_RRNO;            //주민등록번호
        [SerializeField] string ARA_MBRS_MPNO;            //휴대폰 번호
        [SerializeField] string ARA_MBRS_CI_VAL;          //회원 CI값
        [SerializeField] string ARA_MBRS_NNM;             //회원 별명
        [SerializeField] string ARA_MBRS_SEX_DVCD;        //셩별 구분코드
        [SerializeField] string ARA_SNS_NATV_NO;          //SNS 고유번호
        [SerializeField] string ARA_LOGIN_DVCD;           //로그인 구분코드
        [SerializeField] string ARA_MBNK_LNK_YN;          //모바일밴깅 연동 여부
        [SerializeField] string ARA_SV_U_LEME_YN;         //서비스 이용 알림 여부
        [SerializeField] string ARA_EVNT_AND_AD_LEME_YN;  //이벤트 및 광고 알림 여부
        [SerializeField] string ARA_MBRS_HASH_VAL;        //AR앱회원해쉬값
        [SerializeField] string ARA_MBRS_REG_DTTI;        //AR앱 회원 등록 일시
        [SerializeField] string ARA_LT_LOGIN_DTTI;        //AR앱 최종 로그인 일시
        [SerializeField] string ARA_MBRS_STCD;            //AR앱 회원 상태코드



        public Dictionary<string,string> GetDictionary()
        {
           

            Dictionary<string, string> Data = new Dictionary<string, string>();

            var field = typeof(TB_ISBM_ARA_MBRS_JN_INFO).GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

          
                for (int i = 0; i < field.Length; i++)
                {
                    Data.Add(field[i].Name, field[i].GetValue(TBClass).ToString());

                }
            

            return Data;

        }

        public string[] GetKeyName()
        {
            var field = typeof(TB_ISBM_ARA_MBRS_JN_INFO).GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            string[] tempData = new string[field.Length];
            for (int i = 0; i < field.Length; i++)
            {
                tempData[i] = field[i].Name;

            }

            return tempData;
        }

    }



    /// <summary>
    /// TB_ISBI_ARA_BHVR_ITMZ 테이블 관련
    /// </summary>
    private class TB_ISBI_ARA_BHVR_ITMZ
    {

        public object TBClass;

        TB_ISBI_ARA_BHVR_ITMZ()
        {

            TBClass = this;
        }
        /// <summary>
        /// 액션가면관련 
        ///  string ARA_TBL_NATV_MGNO; 키
        /// </summary>
        [SerializeField] string ARA_CRCTR_NATV_MGNO;        //교유관리번호
        [SerializeField] string ARA_TBL_NATV_MGNO;        //회원테이블 키
        [SerializeField] string ARA_CRCTR_SEX_DVCD;         //성별 구분 코드
        [SerializeField] string ARA_CRCTR_SKIN_DVCD;              //피부 구분 코드
        [SerializeField] string ARA_CRCTP_SKIN_COLR_CD;            //피부 색상 코드
        [SerializeField] string ARA_CRCTR_MAKEUP_DVCD;            //화장 구분 코드
        [SerializeField] string ARA_CRCTR_HAIR_DVCD;          //헤어 구분 코드
        [SerializeField] string ARA_CRCTR_HAIR_COLR_CD;             //헤어 색상 코드
        [SerializeField] string ARA_CRCTR_CLOTH_DVCD;        //옷 구분 코드
        [SerializeField] string ARA_CRCTR_ACC_CD;          //악세서리 코드
        [SerializeField] string ARA_CRCTR_FST_REG_DTTI;           //최초 등록일시
        [SerializeField] string ARA_CRCTR_LT_CH_DTTI;          //최종 변경일시

        public Dictionary<string, string> GetDictionary()
        {

            Dictionary<string, string> Data = new Dictionary<string, string>();

            var field = typeof(TB_ISBI_ARA_BHVR_ITMZ).GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            for (int i = 0; i < field.Length; i++)
            {
                Data.Add(field[i].Name, field[i].GetValue(TBClass).ToString());

            }

            return Data;

        }
        public string[] GetKeyName()
        {
            var field = typeof(TB_ISBI_ARA_BHVR_ITMZ).GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            string[] tempData = new string[field.Length];
            for (int i = 0; i < field.Length; i++)
            {
                tempData[i] = field[i].Name;

            }

            return tempData;
        }

    }







}
*/