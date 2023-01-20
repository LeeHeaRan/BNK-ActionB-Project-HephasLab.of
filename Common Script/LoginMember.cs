using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMember
{

    public LoginMember()
    {
        ARA_TBL_NATV_MGNO="";      
        ARA_MBRS_EMLADR = "";
        ARA_MBRS_PSWD = "";
        ARA_MBRS_NM = "";
        ARA_MBRS_RRNO = "";
        ARA_MBRS_MPNO = "";
        ARA_MBRS_CI_VAL = "";
        ARA_MBRS_NNM = "";
        ARA_MBRS_SEX_DVCD = "";
        ARA_SNS_NATV_NO = "";
        ARA_LOGIN_DVCD = "";
        ARA_MBNK_LNK_YN = "";
        ARA_SV_U_LEME_YN = "";
        ARA_EVNT_AND_AD_LEME_YN = "";
        ARA_MBRS_HASH_VAL = "";
        ARA_MBRS_REG_DTTI = "";
        ARA_LT_LOGIN_DTTI = "";
        ARA_MBRS_STCD = "";


    }


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



    public string GetUidValue()
    {
        return (ARA_TBL_NATV_MGNO!=null && ARA_TBL_NATV_MGNO!="")?ARA_TBL_NATV_MGNO:"0";
    }

}
