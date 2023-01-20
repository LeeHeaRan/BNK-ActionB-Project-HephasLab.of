using Defective.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public enum TB_TYPE {

    /// <summary>
    /// <para>ARA_MBRS_NATV_MGNO---------: 교유관리번호</para>
    /// <para>ARA_MBRS_EMLADR-----------: EMAIL 주소(아이디로 사용) </para>
    /// <para>ARA_MBRS_PSWD-------------: 회원 비밀번호 </para>
    /// <para>ARA_MBRS_NM---------------: 회원명</para>
    /// <para>ARA_MBRS_RRNO-------------: 주민등록번호</para>
    /// <para>ARA_MBRS_MPNO-------------: 휴대폰 번호</para>
    /// <para>MMT_TCCCO_DVCD------------: 통신사구분코드</para>
    /// <para>ARA_MBRS_CI_VAL-----------: 회원 CI값</para>
    /// <para>ARA_MBRS_NNM--------------: 회원 별명</para>
    /// <para>ARA_MBRS_SEX_DVCD---------: 셩별 구분코드</para>
    /// <para>ARA_SNS_NATV_NO-----------: SNS 고유번호</para>
    /// <para>ARA_LOGIN_DVCD------------: 로그인 구분코드</para>
    /// <para>ARA_MBNK_LNK_YN-----------: 모바일밴깅 연동 여부</para>
    /// <para>ARA_SV_U_LEME_YN----------: 서비스 이용 알림 여부</para>
    /// <para>ARA_EVNT_AND_AD_LEME_YN---: 이벤트 및 광고 알림 여부</para>
    /// <para>ARA_MBRS_HASH_VAL---------: AR앱회원해쉬값</para>
    /// <para>ARA_MBRS_REG_DTTI---------: AR앱 회원 등록 일시</para>
    /// <para>ARA_LT_LOGIN_DTTI---------: AR앱 최종 로그인 일시</para>
    /// <para>ARA_MBRS_STCD-------------: AR앱 회원 상태코드</para>
    /// </summary>
    MBRS_INFO = 1,  //회원테이블


    /// <summary>
    /// <para>ARA_CRCTR_NATV_MGNO--------: 교유관리번호</para>
    /// <para>ARA_TBL_NATV_MGNO----------: 회원테이블 키</para>
    /// <para>ARA_CRCTR_SEX_DVCD---------: 성별 구분 코드</para>
    /// <para>ARA_CRCTR_SKIN_DVCD--------: 피부 구분 코드</para>
    /// <para>ARA_CRCTP_SKIN_COLR_CD-----: 피부 색상 코드</para>
    /// <para>ARA_CRCTR_MAKEUP_DVCD------: 화장 구분 코드</para>
    /// <para>ARA_CRCTR_HAIR_DVCD--------: 헤어 구분 코드</para>
    /// <para>ARA_CRCTR_HAIR_COLR_CD-----: 헤어 색상 코드</para>
    /// <para>ARA_CRCTR_CLOTH_DVCD-------: 옷 구분 코드</para>
    /// <para>ARA_CRCTR_ACC_CD-----------: 악세서리 코드</para>
    /// <para>ARA_CRCTR_FST_REG_DTTI-----: 최초 등록일시</para>
    /// <para>ARA_CRCTR_LT_CH_DTTI-------: 최종 변경일시</para>
    /// </summary>
    CRCTR_ITMZ = 2, //액션가면 테이블

    /// <summary>
    /// <para>ARA_BHVR_NATV_MGNO---------: 양치 고유관리번호</para>
    /// <para>ARA_MBRS_CI_VAL------------: 회원테이블 키</para>
    /// <para>ARA_BHVR_STRT_DTTI---------: 양치 시작 일시</para>
    /// <para>ARA_BHVR_COMPT_YN----------: 양치 성공 여부</para>
    /// <para>ARA_BHVR_ITEM_CD-----------: 양치 아이템</para>
    /// </summary>
    BHVR_ITMZ = 3, //양치 테이블

    /// <summary>
    /// <para>(((((  인증번호 신청 )))))   </para>
    /// <para>_msg_--------------------: --</para>
    /// <para>_common_-----------------: --</para>
    /// <para>_body_-------------------: --</para>
    /// <para>errorpage----------------: (오류)</para>
    /// <para>errorbiztype-------------: (오류)</para>
    /// <para>errormessage-------------: (오류)</para>
    /// <para>errorcustomermessage-----: (오류)</para>
    /// <para>errorpageparameters------: (오류)</para>
    /// <para>ib20_err_act-------------: (오류)</para>
    /// <para>resCode------------------: (공용)</para>
    /// <para>errorinstanceid----------: (오류)</para>
    /// <para>errorcode----------------: (공용)</para>
    /// </summary>
    /// 

    /// <summary>
    /// <para>(((((  직원테이블 정보 삽입 )))))   </para>
    /// <para>ARA_STF_ENOB--------: AR앱직원행번</para>
    /// <para>ARA_STF_NM----------: AR앱 직원 이름</para>
    /// <para>ARA_STF_JGDNM---------: AR앱 직원 직급</para>
    /// <para>ARA_STF_DPNM--------: AR앱 직원 부서</para>
    /// <para>ARA_BNCD_DED_DTTI-----: AR앱명함실행일시</para>
    /// </summary>
    BNCD_INFO = 4,

    //명함 배너정보 테이블
    /// <summary>
    /// <para>(((((  명함 배너정보 테이블 )))))   </para>
    /// <para>ARA_BNCD_BANNER_MGNO--------: AR앱 명함배너 관리번호</para>
    /// <para>ARA_BNCD_BANNER_TIT----------: AR앱 명함배너 제목</para>
    /// <para>ARA_BNCD_BANNER_KND_DVCD---------: AR앱 명함배넌 종류 구분코드</para>
    /// <para>ARA_BNCD_BANNER_LOCAT_DVCD--------: 중복제거 뷰 수</para>
    /// <para>STRDT-----: 시작일자</para>
    /// <para>EDT-----: 종료일자</para>
    /// <para>IMG_URL-----: 이미지URL</para>
    /// <para>EVNT_TYPE_DVCD-----: 이벤트유형구분코드</para>
    /// <para>SV_RQST_URL-----: 서비스요청URL</para>
    /// </summary>
    BNCD_BANNER_INFO = 5,

    //명함 모바일영업점 조회 테이블
    /// <para>(((((  명함 모바일영업점 조회 테이블 )))))   </para>
    /// <para>MBL_SLBR_ENOB--------: 행번</para>
    /// <para>MBL_SLBR_URL_GN_VAL-----: ??</para>
    /// <para>CG_BIZ_DVCD-----: ??</para>
    /// <para>SUB_PSR_NM-----: ??</para>
    /// <para>MBL_SLBR_STS_NM-----: ??</para>
    /// </summary>
    BNCD_MOBILEBRANCH = 6,



    /*요청
    CMD_SVC_VD - 조회방식(01:생년월일 / 02:주민등록번호)
    KCB_CUST_NM - 이름
    KCB_RRNO1 - 주민번호 앞 6자리
    KCB_RRNO3 - 주민번호 뒷 1자리
    KCB_TCCCO_DVCD - 통신사 구분코드( 01 : SK / 02 : KT / 03 : LGT / 55 : 이동통신3사)
    KCB_MPNO - 전화번호 (하이픈 '-' 없이 11자리)
    AGREE_1 - 약관동의여부 (Y / N)
    AGREE_2 - 약관동의여부 (Y / N)
    AGREE_3 - 약관동의여부 (Y / N)
    KCB_BRCH_NO - 관리점번호 (없을 경우 836으로 설정됨)
    KCB_PURP_SV_CD - 추가인증목적 서비스코드(01 : 아이디관리 / 02 : 공인인증서 / 03 : 단말기 지정 관리 / 04 : 단말기 등록 관리 / 05 : 미지정 단말 인터넷뱅킹 이체 / 06 : 고객정보변경 / 07 : 서비스관리)
    HSLF_CTF_RQST_RNCD - 인증요청 사유코드(00 : 회원가입 / 01 : 회원정보수정 / 02 : 비밀번호찾기 / 03 : 금융거래 / 99 : 기타)





    응답
    TRNS_SEQ (인증확인시 필요함.)*/
    KCB = 3

}


[SerializeField]
public class JsonField
{
    
    List<Dictionary<string, string>> Data = new List<Dictionary<string, string>>();
    string resCode = "";


    public JsonField(TB_TYPE type, string JsonType){

        Debug.Log(JsonType);
        switch (type)
        {
            case TB_TYPE.KCB:
            //    JsonType = JsonType.Replace(".", "");
           //     JsonType = JsonType.Replace("Sucess :", "").Trim();
            break;
        }

        

        Data.Clear();
        try
        {
            var jsonObject = new JSONObject(JsonType);
            string SerchType = "";
            SerchType = (JsonType.Contains("resCode\":\"99")) ? "_common_" : "_body_";

            if (JsonType.Contains("resCode\":\"99"))
            {
                resCode = "99";
            }
            else if (JsonType.Contains("resCode\":\"00"))
            {
                resCode = "00";
            }
            Debug.Log("SerchType:" + SerchType);

            Data = AccessData(jsonObject, SerchType);
        }
        catch (Exception e)
        {
            Debug.Log("json error  "+e);
        }
    }
    public string GetResCode()
    {
        return resCode;
    }
    public List<Dictionary<string, string>> GetDictionary()
    {

        return Data;
    }





    List<Dictionary<string, string>> AccessData(JSONObject jsonObject,string SerchType)
    {
        List<Dictionary<string, string>> _Data = new List<Dictionary<string, string>>();
        Dictionary<string, string> keyValues = new Dictionary<string, string>();


        switch (jsonObject.type)
        {
            case JSONObject.Type.Object:
                for (var i = 0; i < jsonObject.list.Count; i++)
                {
                    var jvalue = jsonObject.list[i];

                    if(jvalue.type != JSONObject.Type.Object)
                    {
                        keyValues.Add(jsonObject.keys[i], TypeSearch(jvalue));

                    }
                    else
                    {
                 
                          

                        _Data.AddRange(SubAccessData(jvalue, SerchType));



                    }
                  

                }
               if(keyValues.Count!=0) _Data.Add(keyValues);
                break;
            case JSONObject.Type.Array:
                foreach (JSONObject element in jsonObject.list)
                {
                  //  _Data.Add(SubAccessData(element));
                }
                break;

        }

        return _Data;

    }
    List<Dictionary<string, string>> SubAccessData(JSONObject jsonObject, string SerchType)
    {
        List<Dictionary<string, string>> keyValuesList = new List<Dictionary<string, string>>();
        Dictionary<string, string> keyValues = new Dictionary<string, string>();
        switch (jsonObject.type)
        {
            case JSONObject.Type.Object:
                for (var i = 0; i < jsonObject.list.Count; i++)
                {
                    var jvalue = jsonObject.list[i];
                   

                    if (jvalue.type != JSONObject.Type.Object)
                    {
                        keyValues.Add(jsonObject.keys[i], TypeSearch(jvalue));

                    }
                    else
                    {
                        if (jsonObject.keys[i].Equals(SerchType))
                        {
                          
                            keyValuesList.AddRange(ValueListAccessData(jvalue));
                        }


                    }

                    if(keyValues.Count!=0)keyValuesList.Add(keyValues);

                }
               
                break;
        }

        return keyValuesList;
    }
    List<Dictionary<string, string>> ValueListAccessData(JSONObject jsonObject)
    {
        List<Dictionary<string, string>> keyValuesList = new List<Dictionary<string, string>>();
        Dictionary<string, string> keyValues = new Dictionary<string, string>();
        switch (jsonObject.type)
        {
            case JSONObject.Type.Object:
                for (var i = 0; i < jsonObject.list.Count; i++)
                {
                    var jvalue = jsonObject.list[i];

                    if (jvalue.type == JSONObject.Type.Array)
                    {
                        foreach (JSONObject element in jvalue.list)
                        {
                           Dictionary<string, string> temp = ValueAccessData(element);
                          if(temp.Count!=0) keyValuesList.Add(temp);
                        }
                      
                    }
                    else if (jvalue.type != JSONObject.Type.Object)
                    {
                        keyValues.Add(jsonObject.keys[i], TypeSearch(jvalue));
                      
                    }
                    else
                    {
                        
                         
                            foreach (KeyValuePair<string, string> pair in ValueAccessData(jvalue))
                            {
                                keyValues.Add(pair.Key, pair.Value);
                            }
                        


                    }

                   

                }
                if (keyValues.Count != 0) keyValuesList.Add(keyValues);
                break;
            case JSONObject.Type.Array:
                foreach (JSONObject element in jsonObject.list)
                {
                    Dictionary<string, string> temp = ValueAccessData(element);
                    if (temp.Count != 0) keyValuesList.Add(temp);
                }
             

                break;
        }

        return keyValuesList;
    }
    Dictionary<string, string> ValueAccessData(JSONObject jsonObject)
    {
        
        Dictionary<string, string> keyValues = new Dictionary<string, string>();
        switch (jsonObject.type)
        {
            case JSONObject.Type.Object:
                for (var i = 0; i < jsonObject.list.Count; i++)
                {
                    var jvalue = jsonObject.list[i];


                    if (jvalue.type != JSONObject.Type.Object)
                    {
                        keyValues.Add(jsonObject.keys[i], TypeSearch(jvalue));

                    }
                    else
                    {

                        keyValues.Add(jsonObject.keys[i], "JSONObject");
                        foreach (KeyValuePair<string, string> pair in ValueAccessData(jvalue))
                        {
                            keyValues.Add(pair.Key, pair.Value);
                        }



                    }

                   

                }
                break;

            case JSONObject.Type.Array:
                for (var i = 0; i < jsonObject.list.Count; i++)
                {
                    var jvalue = jsonObject.list[i];


                    if (jvalue.type != JSONObject.Type.Object)
                    {
                        keyValues.Add(jsonObject.keys[i], TypeSearch(jvalue));

                    }
                    else
                    {

                        foreach (KeyValuePair<string, string> pair in ValueAccessData(jvalue))
                        {
                            keyValues.Add(pair.Key, pair.Value);
                        }



                    }



                }
                break;

        }

        return keyValues;
    }

    string TypeSearch(JSONObject value)
    {
        string tempValue ="";
        switch (value.type)
        {
            case JSONObject.Type.String: 
                tempValue = value.stringValue.ToString();
                break;
            case JSONObject.Type.Number:
                tempValue = value.floatValue.ToString();
                break;
            case JSONObject.Type.Bool:
                tempValue = value.boolValue.ToString();
                break;
            case JSONObject.Type.Null:
                tempValue = "";
                break;
            case JSONObject.Type.Baked:
                tempValue = value.stringValue.ToString();
                break;
        }
        return tempValue;
    }
    


}
