

static class URLs 
{
    public static string Certify_Request = "/ib20/act/MWPCMN010000A60P";
    public static string Certify_Num= "/ib20/act/MWPCMN010000A50P";


    // 로그인 간편조회
    /// <summary>
    /// <para>CI_MemberInfo [ 로그인 간편조회(CI값, 고유번호, 해쉬값)]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_EMLADR (이메일주소)</para>
    /// <para>ARA_SNS_NATV_MGNO(비밀번호)</para>
    /// <para>ARA_SNS_NATV_NO(SNS고유번호)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA11PRC00001101V00_REC</para>
    /// <para>ARA_MBRS_NATV_MGNO(고유번호)</para>
    /// <para>ARA_MBRS_NM(이름)</para>
    /// <para>ARA_MBRS_CI_VAL(CI값)</para>
    /// <para>ARA_MBRS_HASH_VAL(HASH값)</para>
    /// <para>ARA_MBRS_STCD(회원상태)</para>
    /// </summary>
    public static string CI_MemberInfo = "/ib20/act/MBPARA001101A00M";

    // 회원정보 고유번호 기준으로 조회
    /// <summary>
    /// <para>MemberInfo [회원정보 고유번호 기준으로 조회]</para>
    /// <para>key:</para>
    /// </summary>
    public static string MemberInfo = "/ib20/act/MBPARA001002A00M";

    //회원 이메일/SNS 로그인 검증
    /// <summary>
    /// <para>Join_IDCheck [로그인공통 SNS,이메일 (키값에따라 달라짐)]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_EMLADR (이메일주소)</para>
    /// <para>ARA_MBRS_PSWD(비밀번호)</para>
    /// <para>ARA_SNS_NATV_NO(SNS고유번호)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA11PRC00001101V00_REC</para>
    /// <para>ARA_MBRS_NATV_MGNO(고유번호)</para>
    /// <para>ARA_MBRS_EMLADR(이메일주소)</para>
    /// <para>ARA_MBRS_NM(이름)</para>
    /// <para>ARA_MBRS_RRNO(주민번호)</para>
    /// <para>ARA_MBRS_MPNO(전화번호)</para>
    /// <para>MMT_TCCCO_DVCD</para>
    /// <para>ARA_MBRS_CI_VAL(CI값)</para>
    /// <para>ARA_MBRS_NNM(별명)</para>
    /// <para>ARA_MBRS_SEX_DVCD(성별)</para>
    /// <para>ARA_SNS_NATV_NO(SNS고유번호)</para>
    /// <para>ARA_LOGIN_DVCD(로그인구분)</para>
    /// <para>ARA_MBNK_LNK_YN(모뱅연동여부)</para>
    /// <para>ARA_EVNT_AND_AD_LEME_YN(이벤트알림동의여부)</para>
    /// <para>ARA_MBRS_HASH_VAL(HASH값)</para>
    /// <para>ARA_MBRS_STCD(회원상태)</para>
    /// </summary>
    public static string MemberLogin = "/ib20/act/MBPARA001000A00M";

    // 이메일 회원 아이디찾기, 회원가입 중복검증
    /// <summary>
    /// <para>MemberCheckOrIDFind [이메일 회원 아이디찾기, 회원가입 중복검증]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_NM(이름)</para>
    /// <para>ARA_MBRS_CI_VAL(CI값)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_MBRS_EMLADR(이메일주소)</para>
    /// <para>ARA_MBRS_MPNO(전화번호)</para>
    /// <para>ARA_LOGIN_DVCD(로그인구분)</para>
    /// </summary>
    public static string MemberCheckOrIDFind = "/ib20/act/MBPARA001001A00M";

    // 회원 비밀번호 변경을 위한 회원여부 확인
    /// <summary>
    /// <para>MemberPasswordMemberCheck [회원 비밀번호 변경을 위한 회원여부 확인]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_EMLADR(이메일주소)</para>
    /// <para>ARA_MBRS_CI_VAL(CI값)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_MBRS_NATV_MGNO(고유번호)</para>
    /// <para>ARA_MBRS_CI_VAL(CI값)</para>
    /// <para>ARA_MBRS_EMLADR(이메일주소)</para>
    /// <para>ARA_MBRS_PSWD(비밀번호)</para>
    /// </summary>
    public static string MemberPasswordMemberCheck = "/ib20/act/MBPARA001004A00M";

    // 회원 비밀번호 변경
    /// <summary>
    /// <para>MemberPasswordMemberCheck [회원 비밀번호 변경]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_EMLADR(이메일주소)</para>
    /// <para>ARA_MBRS_CI_VAL(CI값)</para>
    /// /// <para>ARA_MBRS_PSWD(비밀번호)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD(성공/실패)</para>
    /// </summary>
    public static string MemberPasswordChange = "/ib20/act/MBPARA001300A00M";

    // 회원가입
    /// <summary>
    /// <para>Join_Submit [회원가입]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (CI값)</para>
    /// <para>ARA_MBRS_EMLADR(이메일주소)</para>
    /// <para>ARA_MBRS_PSWD (비밀번호)</para>
    /// <para>ARA_MBRS_NM(이름)</para>
    /// <para>ARA_MBRS_RRNO(주민번호)</para>
    /// <para>MMT_TCCCO_DVCD (통신사구분코드)</para>
    /// <para>ARA_MBRS_MPNO(전화번호)</para>
    /// <para>ARA_MBRS_NNM (별명)</para>
    /// <para>ARA_MBRS_SEX_DVCD (회원 성별 구분코드)</para>
    /// <para>ARA_SNS_NATV_NO (SNS 고유번호)</para>
    /// <para>ARA_SNS_AITM_TOKN_VAL (로그인토큰)</para>
    /// <para>ARA_LOGIN_DVCD (로그인 구분코드)</para>
    /// <para>ARA_MBNK_LNK_YN (모바일뱅킹 연동 여부)</para>
    /// <para>ARA_SV_U_LEME_YN (서비스 이용 알림 여부)</para>
    /// <para>ARA_EVNT_AND_AD_LEME_YN (이벤트 및 광고 알림 여부)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD (성공/실패)</para>
    /// </summary>
    public static string Join_Submit = "/ib20/act/MBPARA001100A00M"; // 회원가입 submit

    // 회원가입
    /// <summary>
    /// <para>JoinParentInfo_Submit [회원가입]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL(자식 CI값)</para>
    /// <para>ARA_LWCT_AGN_CI_VAL (CI값)</para>
    /// <para>LWCT_AGN_NM(이름)</para>
    /// <para>ARA_MBRS_RRNO(주민번호)</para>
    /// <para>MMT_TCCCO_DVCD (통신사구분코드)</para>
    /// <para>ARA_MBRS_MPNO(전화번호)</para>
    /// <para>ARA_MBRS_NNM (별명)</para>
    /// <para>ARA_MBRS_SEX_DVCD (회원 성별 구분코드)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD (성공/실패)</para>
    /// </summary>
    public static string JoinParentInfo_Submit = "/ib20/act/MBPARA006200A00M"; // 회원가입 submit

    // 회원탈퇴
    /// <summary>
    /// <para>MemberOut [회원탈퇴]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL(CI값)</para>
    /// <para>ARA_MBRS_STCD(회원상태코드)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD (성공/실패)</para>
    /// </summary>
    public static string MemberOut = "/ib20/act/MBPARA001302A00M"; // 회원가입 submit

    // SNS고유번호 입력
    /// <summary>
    /// <para>SNSID_Add [SNS고유번호 입력]</para>
    /// <para>------------key-----------</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// </summary>
    public static string SNSID_Add = "/ib20/act/MBPARA001301A00M";

    // 회원 동의여부 변경(통합)
    /// <summary>
    /// <para>MemberAgree [회원 동의여부 변경(통합)]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBNK_LNK_YN(모뱅연동여부)</para>
    /// <para>ARA_SV_U_LEME_YN (서비스 이용 알림 여부)</para>
    /// <para>ARA_EVNT_AND_AD_LEME_YN (이벤트 및 광고 알림 여부)</para>
    /// 
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD</para>
    /// </summary>
    public static string MemberAgree = "/ib20/act/MBPARA001003A00M";  //ARA_REQ_DVCD?

    // CI값 기준 통신사 및 전화번호 조회
    /// <summary>
    /// <para>CI_PhoneInfo [CI값 기준 통신사 및 전화번호 조회]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (CI값)</para>
    /// 
    /// <para>------request value-------</para>
    /// <para>MMT_TCCCO_DVCD (통신사구분코드)</para>
    /// <para>ARA_MBRS_MPNO(전화번호)</para>
    /// </summary>
    public static string CI_PhoneInfo = "/ib20/act/MBPARA001007A00M";

    // 전화번호 변경
    /// <summary>
    /// <para>MemberPhoneChange [CI값 기준 통신사 및 전화번호 조회]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (CI값)</para>
    /// <para>ARA_MBRS_MPNO(전화번호)</para>
    /// 
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD</para>
    /// </summary>
    public static string MemberPhoneChange = "/ib20/act/MBPARA001304A00M";

    /// <summary>
    /// <para>Join_IDCheck [회원가입 ID 유무체크]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_EMLADR (이메일주소)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_MBRS_NATV_MGNO(고유번호)</para>
    /// <para></para>
    /// </summary>
    public static string Join_IDCheck = "/ib20/act/MBPARA001005A00M";

    // 자동로그인
    /// <summary>
    /// <para>AutoLogin [자동로그인]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_HASH_VAL (자동로그인해시)</para>
    /// <para>ARA_MBRS_CI_VAL (CI값)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_MBRS_CI_VAL (CI값)</para>
    /// <para>ARA_MBRS_NATV_MGNO(고유번호)</para>
    /// <para>ARA_MBRS_EMLADR(이메일주소)</para>
    /// <para>ARA_MBRS_NNM (별명)</para>
    /// <para>ARA_MBRS_SEX_DVCD (회원 성별 구분코드)</para>
    /// <para>ARA_SNS_NATV_NO (SNS 고유번호)</para>
    /// <para>ARA_MBNK_LNK_YN (모바일뱅킹 연동 여부)</para>
    /// <para>ARA_SV_U_LEME_YN (서비스 이용 알림 여부)</para>
    /// <para>ARA_EVNT_AND_AD_LEME_YN (이벤트 및 광고 알림 여부)</para>
    /// <para></para>
    /// </summary>
    public static string AutoLogin = "/ib20/act/MBPARA001006A00M";

    // Board
    /// <summary>
    /// <para>Board [게시판]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_ITG_NB_DVCD (게시판구분코드)</para>
    /// <para>PAGE_NUM (페이지넘버)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para> FST_REG_DTTI </para>
    /// <para> LT_CHPR_ID </para>
    /// <para> LT_CH_DTTI </para>
    /// <para> ARA_ITG_NB_MGNO </para>
    /// <para> ARA_NB_TIT </para>
    /// <para> ARA_NB_CNTN </para>
    /// <para> NUMROW </para>
    /// <para> FST_RGPR_ID </para>
    /// <para> ARA_ITG_NB_DV_VAL2 </para>
    /// <para> ARA_ITG_NB_DV_VAL1 </para>
    /// <para> ARA_MBRS_CI_VAL </para>
    /// <para></para>
    /// <para></para>
    /// </summary>
    public static string Board = "/ib20/act/MBPARA009100A00M";

    // 아이디찾기 (주민,이름반환)
    /// <summary>
    /// <para>Board [아이디찾기 (주민,이름반환) ]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_MPNO (휴대폰번호)</para>
    /// <para>MMT_TCCCO_DVCD (통신사)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_MBRS_NM(이름)</para>
    /// <para>ARA_MBRS_RRNO(주민번호)</para>
    /// <para></para>
    /// </summary>
    public static string ID_Find_DataRequest = "/ib20/act/MBPARA001102A00M";

    // 양치 총 카운트 
    /// <summary>
    /// <para>RewardALLCount [양치 총 카운트 ]</para>
    /// <para>------------key-----------</para>
    /// <para>COUNT_DVCD (1 : 일 1회씩 카운트 / 2 : 일 최대 3회 카운트)</para>
    /// <para>ARA_MBRS_CI_VAL (CI값)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>COUNTRESULT (카운트)</para>
    /// <para></para>
    /// </summary>
    public static string RewardALLCount = "/ib20/act/MBPARA003101A00M";

    // 양치 월별 성공/실패여부
    /// <summary>
    /// <para>Reward_MonthSelect [월별 성공/실패여부 ]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (CI값)</para>
    /// <para>ASELECT_DATE('YYYYMMDD'형식 ex: 20220701)</para>
    /// <para>SELECT_DATE2(조회월일 'YYYYMMDD'형식 ex : 20220801)</para>
    /// <para>------request value-------</para>
    /// <para>ARA_BHVR_COMPT_YN()</para>
    /// <para>- ARA_MBRS_CI_VAL()</para>
    /// <para>- LT_DATE()</para>
    /// <para>- ARA_BHVR_COMPT_YN</para>
    /// <para></para>
    /// </summary>
    public static string Reward_MonthSelect = "/ib20/act/MBPARA003102A00M";

    // 액션가면 아바타 생성 및 수정 (통신 결과값 반환)
    /// <summary>
    /// <para>Board [액션가면 아바타 생성 및 수정]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (회원 CI값)</para>
    /// <para>ARA_CRCTR_SEX_DVCD (액션가면 성별 코드)</para>
    /// <para>ARA_CRCTR_SKIN_DVCD (액션가면 피부 코드)</para>
    /// <para>ARA_CRCTR_SKIN_COLR_CD (액션가면 피부 색상 코드)</para>
    /// <para>ARA_CRCTR_MAKEUP_DVCD (액션가면 화장 코드)</para>
    /// <para>ARA_CRCTR_HAIR_DVCD (액션가면 헤어 코드)</para>
    /// <para>ARA_CRCTR_HAIR_COLR_CD (액션가면 헤어 색상 코드)</para>
    /// <para>ARA_CRCTR_CLOTH_DVCD (액션가면 옷 코드)</para>
    /// <para>ARA_CRCTR_ACC_CD (액션가면 악세서리 코드)</para>
    /// <para>ARA_CRCTR_PSR_INFO_VAL (액션가면 아바타 정보값)</para> 
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD(결과 리턴값)</para>
    /// <para></para>
    /// </summary>
    public static string CRCTR_Make_And_Update_Avatar = "/ib20/act/MBPARA002200A00M";

    // 액션가면 아바타 조회 (아바타 정보 반환)
    /// <summary>
    /// <para>Board [액션가면 아바타 조회]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (회원 CI값)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_MBRS_CI_VAL (회원 CI값)</para>
    /// <para>ARA_CRCTR_SEX_DVCD (액션가면 성별 코드)</para>
    /// <para>ARA_CRCTR_SKIN_DVCD (액션가면 피부 코드)</para>
    /// <para>ARA_CRCTR_SKIN_COLR_CD (액션가면 피부 색상 코드)</para>
    /// <para>ARA_CRCTR_MAKEUP_DVCD (액션가면 화장 코드)</para>
    /// <para>ARA_CRCTR_HAIR_DVCD (액션가면 헤어 코드)</para>
    /// <para>ARA_CRCTR_HAIR_COLR_CD (액션가면 헤어 색상 코드)</para>
    /// <para>ARA_CRCTR_CLOTH_DVCD (액션가면 옷 코드)</para>
    /// <para>ARA_CRCTR_ACC_CD (액션가면 악세서리 코드)</para>
    /// <para>ARA_CRCTR_PSR_INFO_VAL (액션가면 아바타 정보값)</para>
    /// <para></para>
    /// </summary>
    public static string CRCTR_Select_Avatar = "/ib20/act/MBPARA002100A00M";

    // 치카포카 시작시 데이터 입력
    /// <summary>
    /// <para>Board [치카포카 시작시 데이터 입력]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (회원 CI값)</para>
    /// <para>ARA_BHVR_ITEM_CD (선택아이템 값)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD(결과 리턴값</para>
    /// <para>ARA_BHVR_NATV_MGNO(양치 관리번호)</para>
    /// <para></para>
    /// </summary>
    public static string BHVR_Start = "/ib20/act/MBPARA003200A00M";

    // 치카포카 완료시 데이터 업데이트
    /// <summary>
    /// <para>Board [치카포카 완료시 데이터 업데이트]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (회원 CI값)</para>
    /// <para>ARA_BHVR_ITEM_CD (선택아이템 값)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD(결과 리턴값)</para>
    /// <para></para>
    /// </summary>
    public static string BHVR_Complete = "/ib20/act/MBPARA003300A00M";

    // 치카포카 컨텐츠 총 성공 횟수 조회
    /// <summary>
    /// <para>Board [치카포카 컨텐츠 총 성공 횟수 조회]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (회원 CI값)</para>
    /// <para>COUNT_DVCD (카운트 구분 코드 1 : 1회 / 2 : 3회)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>COUNTRESULT(결과 리턴값)</para>
    /// <para></para>
    /// </summary>
    public static string BHVR_CompleteCount = "/ib20/act/MBPARA003101A00M";

    //명함정보 입력
    /// <summary>
    /// <para>Board [명함정보 입력]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (회원CI값)</para>
    /// <para>ARA_STF_NM (사원명)</para>
    /// <para>ARA_STF_JGDNM (직급명)</para>
    /// <para>ARA_STF_DPNM (부서명)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD(성공/실패)</para>
    /// <para></para>
    /// </summary>
    public static string Card_Save = "/ib20/act/MBPARA004200A00M";

    //아바타이미지저장
    /// <summary>
    /// <para>ActionMask_ImageSend [아바타이미지저장]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (회원CI값)</para>
    /// <para>imgData (이미지 데이터)</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD(성공/실패)</para>
    /// <para></para>
    /// </summary>
    public static string ActionMask_ImageSend = "/ib20/act/MBPARA002400A00M";

    //알림조회
    /// <summary>
    /// <para>Notice_Select [알림조회]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (회원CI값)</para>
    /// <para>PAGE_NUM (페이지넘버)<para>
    ///<para>PAGE_COUNT (페이지카운트)<para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>NUMROW</para>
    /// <para>ARA_LEME_NATV_MGNO</para>
    /// <para>ARA_LEME_DVCD</para>
    /// <para>ARA_LEME_LRG_TIT</para>
    /// <para>ARA_LEME_DTL_ENTN</para>
    /// <para>ARA_STRT_DTTI</para>
    /// <para>ARA_END_DTTI</para>
    /// 
    /// <para></para>
    /// </summary>
    public static string Notice_Select = "/ib20/act/MBPARA007100A00M";

    //알림확인
    /// <summary>
    /// <para>Notice_Check [알림확인]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL (회원CI값)</para>
    /// <para>ARA_LEME_NATV_MGNO (넘버)<para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_REG_DVCD (0 : 정상 , -1 : 오류)</para>
    /// </summary>
    public static string Notice_Check = "/ib20/act/MBPARA008200A00M";

    //명함 서브배너 이미지 URL 조회
    /// <summary>
    /// <para>Board [명함서브배너 이미지URL 조회]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_BNCD_BANNER_KND_DVCD [AR앱 명함배넌 종류 구분코드]</para>
    /// <para>ARA_BNCD_BANNER_LOCAT_DVCD [중복제거 뷰 수]</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_BNCD_BANNER_MGNO(AR앱 명함배너 관리번호)</para>
    /// <para>ARA_BNCD_BANNER_TIT(AR앱 명함배너 제목)</para>
    /// <para>ARA_BNCD_BANNER_KND_DVCD(AR앱 명함배넌 종류 구분코드)</para>
    /// <para>ARA_BNCD_BANNER_LOCAT_DVCD(중복제거 뷰 수)</para>
    /// <para>IMG_URL(이미지URL)</para>
    /// <para>EVNT_TYPE_DVCD(이벤트유형구분코드)</para>
    /// <para>SV_RQST_URL(서비스요청URL)</para>
    /// <para></para>
    /// </summary>
    public static string ActionCard_SubImageSelect = "/ib20/act/MBPARA101000A00M";

    //명함 메인배너 정보 조회 
    /// <summary>
    /// <para>Board [명함메인배너 정보 조회]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_BNCD_BANNER_KND_DVCD [AR앱 명함배넌 종류 구분코드]</para>
    /// <para>ARA_BNCD_BANNER_LOCAT_DVCD [중복제거 뷰 수]</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_BNCD_BANNER_MGNO(AR앱 명함배너 관리번호)</para>
    /// <para>ARA_BNCD_BANNER_TIT(AR앱 명함배너 제목)</para>
    /// <para>ARA_BNCD_BANNER_KND_DVCD(AR앱 명함배넌 종류 구분코드)</para>
    /// <para>ARA_BNCD_BANNER_LOCAT_DVCD(중복제거 뷰 수)</para>
    /// <para>IMG_URL(이미지URL)</para>
    /// <para>EVNT_TYPE_DVCD(이벤트유형구분코드)</para>
    /// <para>SV_RQST_URL(서비스요청URL)</para>
    /// <para></para>
    /// </summary>

    public static string ActionCard_MainImageSelect = "/ib20/act/MBPARA101000A00M"; //문서는 MBPARA102000A00로 되어있음. 문서 수정 필요.

    //알림등록
    /// <summary>
    /// <para>Notice_create [알림등록]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_MBRS_CI_VAL [CI_VAL]</para>
    /// <para>ARA_LEME_CRCT_DVCD [1:이벤트완료페이지 2:활동 3:기타]</para>
    /// <para>ARA_LEME_LRG_TIT [제목]</para>
    /// <para>ARA_LEME_DTL_CNTN [내용]</para>
    /// <para>EVNT_TYPE_DVCD [이벤트완료페이지 2:없음 3:URL]</para>
    /// <para>SV_RQST_URL [EVNT_TYPE_DVCD : 3일경우 넣음]</para>
    /// <para>ARA_LEME_RDNG_DTTI [ ]</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_REQ_DVCD [성공 0 실패 -1]</para>
    /// </summary>
    public static string Notice_create = "/ib20/act/MBPARA007200A00M";


    //페이지뷰 데이터갱신
    /// <summary>
    /// <para>PageViewLog [페이지뷰 데이터갱신]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_CTS_DVCD [01 액션가면 02 치카포카 03 액션명함]</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_BNCD_BANNER_MGNO(AR앱 명함배너 관리번호)</para>
    /// <para>ARA_BNCD_BANNER_TIT(AR앱 명함배너 제목)</para>
    /// <para>ARA_BNCD_BANNER_KND_DVCD(AR앱 명함배넌 종류 구분코드)</para>
    /// <para>ARA_BNCD_BANNER_LOCAT_DVCD(중복제거 뷰 수)</para>
    /// <para>IMG_URL(이미지URL)</para>
    /// <para>EVNT_TYPE_DVCD(이벤트유형구분코드)</para>
    /// <para>SV_RQST_URL(서비스요청URL)</para>
    /// <para></para>
    /// </summary>
    public static string PageViewLog = "/ib20/act/MBPARA104000A00M";


    //더보기 배너
    /// <summary>
    /// <para>OhterPageBanner [더보기 배너]</para>
    /// <para>------------key-----------</para>
    /// <para>ARA_PRCDG_BANNER_LOCAT_DVCD </para>
    /// <para>ARA_PRCDG_BANNER_PROCE_DVCD </para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>ARA_PRCDG_BANNER_RESULT_LIST</para>
    /// <para>ARA_PRCDG_BANNER_MGNO</para>
    /// <para>IMG_URL</para>
    /// <para>ARA_PRCDG_BANNER_LOCAT_DVCD</para>
    /// <para>ARA_PRCDG_BANNER_PROCE_DVCD</para>
    /// <para>ARA_PRCDG_BANNER_TIT</para>
    /// <para></para>
    /// </summary>
    public static string OhterPageBanner = "/ib20/act/MBPARA102000A00M";


    public static string Point_GetUpdate = "/ib20/act/MBPARA003301A00M";

    // PROCESS_DVCD : 01 - 조회(CI값) / 02 - 조회(전화번호) / 03 - 입력(insert) /
    //                04 - 수정+적립(update) / 05 - 수정+차감(update) / 06 - 수정(merge) / 07 - 선물하기
    //포인트관련
    /// <summary>
    /// <para>Point_control_01 [포인트조회(CI값)][필수키 : (K)</para>
    /// <para>------------key-----------</para>
    /// <para>PROCESS_DVCD (K)(프로세스 구분 코드 : 01) </para>
    /// <para>ARA_MBRS_CI_VAL (K)(회원 CI값) </para>
    /// <para>ARA_MBRS_NATV_MGNO (회원 고유 번호) </para>
    /// <para>ARA_MBRS_MPNO (회원 휴대전화번호)</para>
    /// <para>------request value-------</para>
    /// <para>ARA_MBRS_NM (회원명) </para>
    /// <para>USE_PSBL_PNT_AMT (사용가능 포인트)</para>
    /// <para>USE_PNT_ACAMT (누적된 포인트)</para>
    /// <para>PES_PNT_AMT (사용된 포인트)</para>
    /// <para>RESULT_CODE (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// <para>RESULT (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// </summary>
    public static string Point_control_Select = "/ib20/act/MBPARA009000A00M";

    //포인트관련
    /// <summary>
    /// <para>Point_control_02 [포인트조회(전화번호)]</para>
    /// <para>------------key-----------</para>
    /// <para>PROCESS_DVCD (K)(프로세스 구분 코드 : 02) </para>
    /// <para>ARA_MBRS_CI_VAL (회원 CI값) </para>
    /// <para>ARA_MBRS_NATV_MGNO (회원 고유 번호) </para>
    /// <para>ARA_MBRS_MPNO (K)(회원 휴대전화번호)</para>
    /// <para>------request value-------</para>
    /// <para>ARA_MBRS_NM (회원명) </para>
    /// <para>USE_PSBL_PNT_AMT (사용가능 포인트)</para>
    /// <para>USE_PNT_ACAMT (누적된 포인트)</para>
    /// <para>PES_PNT_AMT (사용된 포인트)</para>
    /// <para>RESULT_CODE (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// <para>RESULT (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// </summary>
    public static string Point_control_Select_MPNO = "/ib20/act/MBPARA009000A00M";

    //포인트관련
    /// <summary>
    /// <para>Point_control_03 [포인트입력]</para>
    /// <para>------------key-----------</para>
    /// <para>PROCESS_DVCD (K)(프로세스 구분 코드 : 03)</para>
    /// <para>ARA_MBRS_CI_VAL (K)(회원 CI값)</para>
    /// <para>ARA_MBRS_NATV_MGNO (K)(회원 고유 번호)</para>
    /// <para>ARA_MBRS_NM (회원명))</para>
    /// <para>MMT_TCCCO_DVCD (통신사 구분코드 : SK/KT/LG 등)</para>
    /// <para>ARA_MBRS_MPNO (회원 휴대전화번호)</para>
    /// <para>USE_PSBL_PNT_AMT (사용가능 포인트)</para>
    /// <para>ARA_EVNT_DVCD (이벤트 구분코드 (01 : 치카포카? 랜덤or지정발행?)</para>
    /// <para>ARA_EVNT_TYPE_DVCD (이벤트 유형 구분코드 (00 : 임의 포인트 지급 / 01:포인트 / 02:경품)</para>
    /// <para>ARA_EVNT_TMRD (이벤트 회차 (1, 2, 3 형식) </para>
    /// <para>------request value-------</para>
    /// <para>RESULT_CODE (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// <para>RESULT (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// </summary>
    public static string Point_control_Insert = "/ib20/act/MBPARA009000A00M";

    //포인트관련
    /// <summary>
    /// <para>Point_control_04 [포인트수정+적립]</para>
    /// <para>------------key-----------</para>
    /// <para>PROCESS_DVCD (K)(프로세스 구분 코드 : 04)</para>
    /// <para>ARA_MBRS_CI_VAL (K)(회원 CI값)</para>
    /// <para>ARA_MBRS_NATV_MGNO (K)(회원 고유 번호)</para>
    /// <para>ARA_MBRS_MPNO (회원 휴대전화번호)</para>
    /// <para>USE_PSBL_PNT_AMT (사용가능 포인트)</para>
    /// <para>ARA_EVNT_DVCD (이벤트 구분코드 (01 : 치카포카? 랜덤or지정발행?)</para>
    /// <para>ARA_EVNT_TYPE_DVCD (이벤트 유형 구분코드 (00 : 임의 포인트 지급 / 01:포인트 / 02:경품)</para>
    /// <para>ARA_EVNT_TMRD (이벤트 회차 (1, 2, 3 형식) </para>
    /// <para>------request value-------</para>
    /// <para>RESULT_CODE (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// <para>RESULT (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// <para>BE_PICKED (포인트 당첨 여부)</para>
    /// <para>GET_POINT (획득 포인트)</para>
    /// </summary>
    public static string Point_control_AddOnly = "/ib20/act/MBPARA009000A00M";

    //포인트관련
    /// <summary>
    /// <para>Point_control_05 [포인트수정+적립]</para>
    /// <para>------------key-----------</para>
    /// <para>PROCESS_DVCD (K)(프로세스 구분 코드 : 05)</para>
    /// <para>ARA_MBRS_CI_VAL (K)(회원 CI값)</para>
    /// <para>ARA_MBRS_NATV_MGNO (K)(회원 고유 번호)</para>
    /// <para>ARA_MBRS_MPNO (회원 휴대전화번호)</para>
    /// <para>USE_PSBL_PNT_AMT (K)(차감할 포인트)</para>
    /// <para>ARA_EVNT_DVCD (이벤트 구분코드 (01 : 치카포카? 랜덤or지정발행?)</para>
    /// <para>ARA_EVNT_TYPE_DVCD (이벤트 유형 구분코드 (00 : 임의 포인트 지급 / 01:포인트 / 02:경품)</para>
    /// <para>ARA_EVNT_TMRD (이벤트 회차 (1, 2, 3 형식) </para>
    /// <para>------request value-------</para>
    /// <para>RESULT_CODE (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// <para>RESULT (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// </summary>
    public static string Point_control_Sub = "/ib20/act/MBPARA009000A00M";

    //포인트관련
    /// <summary>
    /// <para>Point_control_06 [포인트 수정(merge)]</para>
    /// <para>------------key-----------</para>
    /// <para>PROCESS_DVCD (K)(프로세스 구분 코드 : 06)</para>
    /// <para>ARA_MBRS_CI_VAL (K)(회원 CI값)</para>
    /// <para>ARA_MBRS_NATV_MGNO (K)(회원 고유 번호)</para>
    /// <para>ARA_MBRS_NM (회원명))</para>
    /// <para>MMT_TCCCO_DVCD (통신사 구분코드 : SK/KT/LG 등)</para>
    /// <para>ARA_MBRS_MPNO (회원 휴대전화번호)</para>
    /// <para>USE_PSBL_PNT_AMT (사용가능 포인트)</para>
    /// <para>ARA_EVNT_DVCD (이벤트 구분코드 (01 : 치카포카? 랜덤or지정발행?)</para>
    /// <para>ARA_EVNT_TYPE_DVCD (이벤트 유형 구분코드 (00 : 임의 포인트 지급 / 01:포인트 / 02:경품)</para>
    /// <para>ARA_EVNT_TMRD (이벤트 회차 (1, 2, 3 형식) </para>
    /// <para>------request value-------</para>
    /// <para>RESULT_CODE (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// <para>RESULT (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// <para>BE_PICKED (포인트 당첨 여부)</para>
    /// <para>GET_POINT (획득 포인트)</para>
    /// </summary>
    public static string Point_control_Add = "/ib20/act/MBPARA009000A00M";

    //포인트관련
    /// <summary>
    /// <para>Point_control_07 [포인트선물]</para>
    /// <para>------------key-----------</para>
    /// <para>PROCESS_DVCD (K)(프로세스 구분 코드 : 07)</para>
    /// <para>ARA_MBRS_CI_VAL1 (K)(보내는 회원 CI값)</para>
    /// <para>ARA_MBRS_NATV_MGNO1 (K)(보내는 회원 고유 번호)</para>
    /// <para>ARA_MBRS_NM1 (보내는 회원명))</para>
    /// <para>MMT_TCCCO_DVCD1 (보내는 회원 통신사 구분코드 : SK/KT/LG 등)</para>
    /// <para>ARA_MBRS_MPNO1 (K)(보내는 회원 휴대전화번호)</para>
    /// 
    /// <para>ARA_MBRS_CI_VAL2 (받는 회원 CI값)</para>
    /// <para>ARA_MBRS_NATV_MGNO2 (받는 회원 고유 번호)</para>
    /// <para>ARA_MBRS_NM2 (받는 회원명))</para>
    /// <para>MMT_TCCCO_DVCD2 (받는 회원 통신사 구분코드 : SK/KT/LG 등)</para>
    /// <para>ARA_MBRS_MPNO2 (K)(받는 회원 휴대전화번호)</para>
    /// <para>USE_PSBL_PNT_AMT (K)(선물할 포인트)</para>
    /// <para>------request value-------</para>
    /// <para>RESULT_CODE (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// <para>RESULT (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// </summary>
    public static string Point_control_Gift = "/ib20/act/MBPARA009000A00M";


    //포인트관련
    /// <summary>
    /// <para>Point_control_08 [포인트선물]</para>
    /// <para>------------key-----------</para>
    /// <para>PROCESS_DVCD (K)(프로세스 구분 코드 : 08)</para>
    /// <para>ARA_MBRS_CI_VAL (K)(보내는 회원 CI값)</para>
    /// <para>ARA_MBRS_NATV_MGNO1 (K)(보내는 회원 고유 번호)</para>
    /// <para>ARA_MBRS_MPNO1 (K)(보내는 회원 휴대전화번호)</para>
    /// <para>------request value-------</para>
    /// <para>REG_DTTI                  (등록일시)</para>
    /// <para>EVNT_SEQ                  (이벤트 일련번호)</para>
    /// <para>ARA_EVNT_DVCD             (AR앱 이벤트 구분코드)</para>
    /// <para>ARA_EVNT_TYPE_DVCD        (AR앱 이벤트 유형 구분코드)</para>
    /// <para>ARA_EVNT_PBL_DVCD         (AR앱 이벤트 발행 구분코드)</para>
    /// <para>ARA_EVNT_TMRD             (AR앱 이벤트 회차)</para>
    /// <para>CPON_NM                   (쿠폰명)</para>
    /// <para>CPON_CNTN_IMG_URL_ADDR    (쿠폰 내용 이미지 URL 주소)</para>
    /// <para>ARA_MBRS_CI_VAL           (AR앱 회원 CI값)</para>
    /// <para>ARA_MBRS_NATV_MGNO1       (AR앱 회원 고유 관리번호1)</para>
    /// <para>ARA_MBRS_NM1              (AR앱 회원 명1)</para>
    /// <para>MMT_TCCCO_DVCD1           (이동통신사 구분코드1)</para>
    /// <para>ARA_MBRS_MPNO1            (AR앱 회원 휴대전화번호1)</para>
    /// <para>ARA_MBRS_CI_VAL2          (AR앱 회원 CI값2)</para>
    /// <para>ARA_MBRS_NATV_MGNO2       (AR앱 회원 고유 관리번호2)</para>
    /// <para>ARA_MBRS_NM2              (AR앱 회원 명2)</para>
    /// <para>MMT_TCCCO_DVCD2           (이동통신사 구분코드2)</para>
    /// <para>ARA_MBRS_MPNO2            (AR앱 회원 휴대전화번호2)</para>
    /// <para>USE_RQST_PNT              (사용 요청 포인트)</para>
    /// <para>USE_CAN_PNT               (사용 취소 포인트)</para>
    /// <para>RV_PNT                    (적립 포인트)</para>
    /// <para>RV_CAN_PNT                (적립 취소 포인트)</para>
    /// <para>RESULT_CODE               (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// <para>RESULT                    (0:SUCCESS / 1:EMPTY / 9999:ERROR)</para>
    /// </summary>
    public static string Point_control_History = "/ib20/act/MBPARA009000A00M";

    //명함 모바일영업점 조회
    /// <summary>
    /// <para>Board [명함 모바일영업점 조회]</para>
    /// <para>------------key-----------</para>
    /// <para>MBL_SLBR_ENOB [직원행번]</para>
    /// <para></para>
    /// <para>------request value-------</para>
    /// <para>MBL_SLBR_ENOB(직원행번)</para>
    /// <para>MBL_SLBR_URL_GN_VAL(??)</para>
    /// <para>CG_BIZ_DVCD(??)</para>
    /// <para>SUB_PSR_NM(??)</para>
    /// <para>MBL_SLBR_STS_NM(??)</para>
    /// <para></para>
    /// </summary>
    public static string ActionCard_MobileBranch_Select = "/ib20/act/MBPARA106000A00M";



    static public class Menu
    {
        public static string Join = "MBPCMN1200ARA10";
        public static string Member = "MBPCMN1100ARA10";
        public static string KCB = "MWPCMN2040CMN10";
        public static string Reward = "MBPCMN0300ARA10";
        public static string Card = "MBPCMN0400ARA10";
        public static string board = "MBPCMN0900ARA10"; 
        public static string notice = "MBPCMN0700ARA10";
        public static string noticeCheck = "MBPCMN0800ARA10";
        public static string noticeCreate = "MBPCMN0700ARA10";
        public static string pageViewLog = "MBPCMN1100ARA10";
        public static string otherPagebanner = "MBPCMN1100ARA10";
    }
}


// 이메일 회원 아이디찾기, 회원가입 중복검증
/// <summary>
/// <para>MemberCheckOrIDFind [이메일 회원 아이디찾기, 회원가입 중복검증]</para>
/// <para>------------key-----------</para>
/// <para>ARA_MBRS_NM(이름)</para>
/// <para>ARA_MBRS_CI_VAL(CI값)</para>
/// <para></para>
/// <para>------request value-------</para>
/// <para>ARA_MBRS_EMLADR(이메일주소)</para>
/// <para>ARA_MBRS_MPNO(전화번호)</para>
/// <para>ARA_LOGIN_DVCD(로그인구분)</para>
/// </summary>
/*    요청 데이터
/// <para>ARA_MBRS_CI_VAL (CI값)</para>
/// <para>ARA_MBRS_EMLADR(이메일주소)</para>
/// <para>ARA_MBRS_PSWD (비밀번호)</para>
/// <para>ARA_MBRS_NM(이름)</para>
/// <para>ARA_MBRS_RRNO(주민번호)</para>
/// <para>MMT_TCCCO_DVCD (통신사구분코드)</para>
/// <para>ARA_MBRS_MPNO(전화번호)</para>
/// <para>ARA_MBRS_NNM (별명)</para>
/// <para>ARA_MBRS_SEX_DVCD (회원 성별 구분코드)</para>
/// <para>ARA_SNS_NATV_NO (SNS 고유번호)</para>
/// <para>ARA_SNS_AITM_TOKN_VAL (로그인토큰)</para>
/// <para>ARA_LOGIN_DVCD (로그인 구분코드)</para>
/// <para>ARA_MBNK_LNK_YN (모바일뱅킹 연동 여부)</para>
/// <para>ARA_SV_U_LEME_YN (서비스 이용 알림 여부)</para>
/// <para>ARA_EVNT_AND_AD_LEME_YN (이벤트 및 광고 알림 여부)</para>

응답 데이터
ARA_REQ_DVCD (성공/실패)*/
