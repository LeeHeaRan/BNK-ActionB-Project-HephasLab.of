using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class UIControll : MonoBehaviour
{
    public AssetBunblesManager assetBunblesManager;
    //public TestviewLog text;

    public SetAR setAR_s; //초기화하고 AR로 넘어감.
    public OCRControll_02 ocrControll_02_s; //OCR로 받은 값을 inputfeild에 넣기 위함.
    public OCRControll_01 ocrControll_01_s; //OCR로 받은 값을 inputfeild에 넣기 위함.

    [SerializeField]
    private GameObject OCR_MainBtns_Page;
    private GameObject OCR_GestureGuide_Page;


    public GameObject OCRCamera, Snap_Btn; //ocr에 사용되는 카메라. ar로 넘어가면 꺼줄거임
    public GameObject PostCam, ARUICam; //포스트에 사용되는 카메라. AR로 넘어가면 켜줄꺼임.

    public GameObject UI_OCR_BlackBG, UI_OCR_CardDetectionGuide, UI_OCR_CheckInfoGuide, OCR_Default_Text, OCR_Error_Text; //가이드와 기본 설명 문구, 에러 문구
    public GameObject UI_ocr_info, UI_ocr_networkCheck, UI_ocr_DBCheck;

    //public DBControll DBControll_s;
    public ActionCardSQLManage ActionCardSQLManager_s;

    public InputField nameinput, departmentinput, rankinput;
    public Text nameInfo, departmentInfo, rankInfo; //ocr로 인식한 문자.


    /// <summary>
    /// <para>정보모달의 위로올라가있는지 확인.</para> 
    /// <para>false: 안올라간 상태 ture: 위로 올라간 상태</para>
    /// </summary>
    bool isInputField_UPCheck = false;
    public GameObject info_change_area; //정보모달이 위로 올라갈수 있게한다.

    //AR오브젝트들.
    public GameObject AR_ui, UI_AR_Guide01, AR_Guide02, UI_AR_BG;
    public GameObject UI_AR_menuButton, A_AR_Exit_Button, UI_AR_subscribe_all, UI_AR_finance_all, UI_AR_entertainment_all, UI_AR_ReMenu;
    public GameObject UI_AR_tomain;

    public GameObject OCRdetecting_Text;
    public BackControll backControll;

    public All all_s;

    public RectTransform middleCanvas, mainCanvas;

    public GameObject Particle, PostObject;
    float middle, main, ratio;

    public GameObject[] Cars = new GameObject[8];

    public ActionCardSQLManage sqlManage;

    float time = 0;

    /// <summary>
    /// [명함정보확인] 모달이 활성화되고 각 요소별로 클릭시 행동을 관리.
    /// </summary>
    /// <param name="name"></param>
    public void click_ocr_inputField(string name)
    {

        if (name.Contains("InputField"))
        {
            //모달의 inputfield를 클릭하였을 경우.

            //모바일키보드가 활성화되기에 모달을 위로 올린다.
            //모달이 위로 안올라간 상태일때.
            if (!isInputField_UPCheck)
            {
                //모달을 위로 올린다.
                iTween.ValueTo(UI_ocr_info, iTween.Hash("from", info_change_area.GetComponent<RectTransform>().anchoredPosition.y, "to", info_change_area.GetComponent<RectTransform>().anchoredPosition.y + 500f, "onupdate", "updateInfoModal", "onupdatetarget", this.gameObject, "time", 0.1f));
                isInputField_UPCheck = true; //상태변수 수정.
            }


        }
        else if (name.Contains("close_btn") || name.Contains("A_BlackPlane_Image"))
        {
            //모달의 오른쪽상단 x 버튼 혹은 모달뒤 bg를 클릭하였을 경우.

            UI_ocr_info.GetComponent<UI_Control>().CloseUI(); //모달을 닫아준다.
            isInputField_UPCheck = false; //상태변수 수정.


            if (Snap_Btn != null)
            {
                OnState_SnapBtn(); //카메라버튼을 활성화한다.
            }

            all_s.show_BottonMenu(); //하단의 카테고리바 활성화

        }
        else if (name.Equals("submit"))
        {
            if (Snap_Btn != null)
            {
                OnState_SnapBtn(); //카메라버튼을 활성화한다.
            }

            //인포창이 사라지고 
            isInputField_UPCheck = false;
            UI_ocr_info.GetComponent<UI_Control>().CloseComplete();

            click_ocr_info_successButton();
        }
    }

    public void close_Keyboard()
    {
        if (isInputField_UPCheck) //올라간 상태일때.
        {
            //아래로 내린다.
            iTween.ValueTo(UI_ocr_info, iTween.Hash("from", info_change_area.GetComponent<RectTransform>().anchoredPosition.y, "to", info_change_area.GetComponent<RectTransform>().anchoredPosition.y - 500f, "onupdate", "updateInfoModal", "onupdatetarget", this.gameObject, "time", 0.1f));
            isInputField_UPCheck = false; //내려 상태

        }
    }

    void updateInfoModal(float val) //info모달창이 키패드가 나왔을때 
    {
        info_change_area.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, val, 0f);
    }

    void Awake()
    {
        main = mainCanvas.rect.height;
        middle = middleCanvas.rect.height;
        ratio = (float)(main / middle); //0.8
    }

    void Update()
    {
        time += Time.deltaTime;

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape)) //뒤로 가기를 눌렀을때
            {
                if (TouchScreenKeyboard.visible)
                {
                    close_Keyboard();
                }
            }
        }
        if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))  //터치가 있거나 마우스 클릭 있을시.
        {
            if (EventSystem.current.currentSelectedGameObject != null && time > 2f) //null체크
            {
                if (EventSystem.current.currentSelectedGameObject.name.Contains("InputField") ||
                    EventSystem.current.currentSelectedGameObject.name.Contains("close_btn") ||
                    EventSystem.current.currentSelectedGameObject.name.Contains("A_BlackPlane_Image") ||
                    EventSystem.current.currentSelectedGameObject.name.Contains("submit"))
                {
                    //Debug.Log(EventSystem.current.currentSelectedGameObject.name + "||" + isInputField_UPCheck);
                    click_ocr_inputField(EventSystem.current.currentSelectedGameObject.name);
                }

                time = 0f;
            }
        }


        //AR화면이 시작되면 실행.
        if (!OCRCamera.activeSelf)
        {
            //AR평면이 일정크기 이상, AR오브젝트가 비활성화, AR가이드1이 보이지 않은경우.
            if (setAR_s.arPlaneSize > 0.6f && !setAR_s.isCreate && !UI_AR_Guide01.activeSelf)
            {
                AR_Guide02.SetActive(true); //두번째 가이드를 켜주고
            }

            //AR오브
            if (setAR_s.SceneARObj.activeSelf) //AR오브젝트가 생성되면 두번째 가이드를 꺼준다.
            {
                AR_Guide02.SetActive(false);
            }
        }
    }

    /// <summary>
    /// OCR회면에서 도움말 아이콘을 눌렀을때.
    /// </summary>
    public void click_ocr_help()
    {
        UI_OCR_BlackBG.SetActive(true);
        UI_OCR_CardDetectionGuide.SetActive(true); 
        UI_OCR_CheckInfoGuide.SetActive(false);
        backControll.changeStep(UI_OCR_CardDetectionGuide, BackControll.ARCARD_STEP.OCRGuide); // 가이드 있고 시작할때.
        all_s.hide_BottonMeun();//하단 메뉴 가림

    }

    //다음번 실행에서는 가이드가 안뜨게 하기 위함. 제스쳐 가이드의 마지막 확인버튼을 눌렀을때 실행.
    public void click_ocr_guideExit_button()
    {
        SplayerPrefs.PlayerPrefsSave("isARCArdGuideKey", 1); //0: 처음 실행했을 때, 1: 처음실행이 아닐때.
        all_s.show_BottonMenu();//하단 메뉴 나옴
    }

    /// <summary>
    /// OCR화면의 카메라버튼을 활성화한다.
    /// </summary>
    public void OnState_SnapBtn()
    {
        Snap_Btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.RuntimeOnly); //버튼을 활성화한다.
    }

    /// <summary>
    /// OCR 통신(Google API)이 끝난후 호출되는 콜백
    /// </summary>
    /// <param name="isSucess">명함을 인식해 이름, 직급, 부서를 정상적으로 인식하였는지 체크</param>
    public void click_ocr_camera_(bool isSucess)
    {
        StopCoroutine(ocrControll_02_s.coroutine);

        OCRdetecting_Text.SetActive(false); //"OCR처리중입니다.." 텍스트가이드 비활성화

        if (isSucess)
        {
            //이름, 직급, 부서를 인식한 경우

            //인식된 값을 넣어준다.
            nameinput.text = ocrControll_02_s.infoName;
            departmentinput.text = ocrControll_02_s.infoDept;
            rankinput.text = ocrControll_02_s.infoRank;

            OCR_Error_Text.SetActive(false); //"다시촬영해주세요"
            on_ocr_info_modal(); //정보 모달이 뜸. *****
            isSucess = false;
        }
        else
        {
            //명함_OCR_명함 정보를 못 얻음.
            //오류 메시지가 뜸."명함이 인식되지 않았습니다."
            OnState_SnapBtn(); //버튼활성화
            OCR_Error_Text.SetActive(true); //에러 텍스트와 렉트이미지
            all_s.show_BottonMenu(); //하단 메뉴 보이게
        }

    }

    //OCR의 오류메시지를 꺼준다.
    private void remove_ocr_errortext()
    {
        OCR_Error_Text.SetActive(false);
        OCR_Default_Text.SetActive(true);
    }


    public void on_ocr_info_modal() //info모달을 뜨게끔하는 함수.
    {
        UI_ocr_info.SetActive(true); //정보 모달이 뜸.
        all_s.hide_BottonMeun(); //하단 메뉴 숨김
    }

    //스키마로 들어왔을때 호출해준다.
    public void click_ocr_info_successButton(string[] empInfo)
    {

        //websend로 내역 업데이트하는곳에 ARA_STF_ENOB ARA_MBRS_CI_VAL 
        // CI값은 로그인 확인해서 비로그인시 0 로그인시 CI값으로 보내주면됨
        // 밑에껀 콜백후 실행할수있도록 해주셈


        //Debug.Log("click_ocr_info_success가 실행됨.");

        /*nameinput.text  = "김부산";
         departmentinput.text = "디지털전략부";
         rankinput.text = "과장";*/


        //직원테이블에 저장.
        ActionCardSQLManager_s.ActionCard_EmpInfo_Insert_Sql(empInfo, DeeplinkCallback);

    }

    public void DeeplinkCallback(string req, string resCode, List<Dictionary<string, string>> data)
    {
        //Debug.Log("ActionCardCallBack실행");
        string contents = "";
        Debug.Log("server call: " + req);

        if (req.Equals("fail") || resCode.Equals("99"))
        {
            if (!req.Equals("NetWork error"))  //네트워크 오류
            {
                GameObject.Find("ARCardAll_get").GetComponent<UIManager>().ErrorReport("Mobile_Certify :" + contents);

                //모달:직원정보를 찾지 못함.
                //ocr화면으로 돌아감.
            }
        }
        else if (req.Equals("Sucess") && resCode.Equals("00"))
        {

            for (int i = 0; i < data.Count; i++)
            {

                if (!data[0]["ARA_MBRS_CI_VAL"].Equals("-1"))
                {
                    Debug.Log("비회원으로 들어왔습니다.(URL행번으로 들어옴)");
                    nameinput.text = data[i]["ARA_STF_NM"];
                    departmentinput.text = data[i]["ARA_STF_DPNM"];
                    rankinput.text = data[i]["ARA_STF_JGDNM"];

                    Debug.Log("NAME: " + data[i]["ARA_STF_NM"]);
                    Debug.Log("DEPART: " + data[i]["ARA_STF_DPNM"]);
                    Debug.Log("RANK: " + data[i]["ARA_STF_JGDNM"]);

                    //모바일영업점 링크를 얻는 쿼리 실행
                    sqlManage.ActionCard_mobileBranch_Select_Sql(data[i]["MBL_SLBR_ENOB"]);




                    all_s.hide_BottonMeun(); //하단 메뉴 

                    //AR로 들어간다.
                    //text.SetLog("", false); 
                    backControll.changeStep(this.gameObject, BackControll.ARCARD_STEP.ARMain); //뒤로가기 위한 버튼

                    //혹시라도 동작하고 있는 코루틴을 중지시킨다.
                    if (ocrControll_02_s.coroutine != null)
                    {
                        all_s.stopCoroutineOcrControll_02();
                    }

                    OCRCamera.SetActive(false); //OCR 카메라를 꺼준다.
                    PostCam.SetActive(true); //PostCamera_LookARObject 카메라를 켜준다.
                    ARUICam.SetActive(true); //Camera를 켜준다.


                    Particle.SetActive(true);
                    PostObject.SetActive(true);

                }
                else
                {
                    Debug.Log("응답값이 없습니다.");

                    all_s.arCardAll_Get_s.uIManager.GetComponent<Alert_Manager>().ShowAlert("일치하는 직원정보가 없습니다.\nURL를 다시 한번 확인해주세요!");
                    //모달:직원정보를 찾지 못함.
                    //ocr화면으로 돌아감.

                }
            }
            Debug.Log("DATA" + data[0]["ARA_MBRS_CI_VAL"]);

            //UI Text에 값을 셋팅해준다.
            nameInfo.text = nameinput.text;
            departmentInfo.text = departmentinput.text;
            rankInfo.text = rankinput.text;

            //가이드 부분
            UI_AR_Guide01.SetActive(true); //AR 가이드1를 켜준다.
            AR_ui.SetActive(true);
            UI_AR_menuButton.SetActive(true);

            setAR_s.InitAR(); //초기화하고

        }
    }

    //click_ocr_inputField(string name) 함수에서 호출해준다.
    //override
    public void click_ocr_info_successButton()
    {
        //직원테이블에 저장.
        ActionCardSQLManager_s.ActionCard_EmpInfo_Insert_Sql(ActionCard_EmpInfo_CallBack);
    }


    //ocr로 들어왔을 때. 직원정보 저장 콜백
    public void ActionCard_EmpInfo_CallBack(string req, string resCode, List<Dictionary<string, string>> data)
    {
        string contents = "";
        Debug.Log("server call: " + req);


        /*for (int i = 0; i < data.Count; i++)
        {
            foreach (KeyValuePair<string, string> pair in data[i])
            {
                Debug.Log(pair.Key + " : " + pair.Value);

                contents += pair.Key + "[" + i + "] : " + pair.Value + "[" + i + "] ";

                if (pair.Key.Equals("ARA_REQ_DVCD"))  // 성공 실패 유무
                {
                    if (pair.Value == "-1")
                    {
                        Debug.Log("명함테이블에 직원정보를 저장하는데 실패 하였습니다.");
                    }
                    else if (pair.Value == "0")
                    {
                        Debug.Log("명함테이블에 직원정보를 저장하는데 성공 하였습니다.");
                    }
                }
            }
        }*/

        if (req.Equals("fail") || resCode.Equals("99"))
        {
            if (!req.Equals("NetWork error"))  //네트워크 오류
            {
                GameObject.Find("ARCardAll_get").GetComponent<UIManager>().ErrorReport("Mobile_Certify :" + contents);
            }
        }
        else if (req.Equals("Sucess") && resCode.Equals("00"))
        {
            if (data[0]["ARA_REQ_DVCD"].Equals("-1"))
            {
                Debug.Log("명함테이블에 직원정보를 저장하는데 실패 하였습니다.");
            }
            else if (data[0]["ARA_REQ_DVCD"].Equals("0"))
            {
                Debug.Log("명함테이블에 직원정보를 저장하는데 성공 하였습니다.");

                if (!data[0]["ARA_STF_ENOB"].Equals("-1"))
                {
                    Debug.Log("행번으로 조회");

                    //모바일영업점 링크를 얻는 쿼리 실행
                    ActionCardSQLManager_s.ActionCard_mobileBranch_Select_Sql(data[0]["ARA_STF_ENOB"]);
                }
           
                /*  for (int i = 0; i < data.Count; i++)
                {
                   
                }*/

            }

            all_s.hide_BottonMeun(); //하단 메뉴 

            //AR로 들어간다.
            backControll.changeStep(this.gameObject, BackControll.ARCARD_STEP.ARMain); //뒤로가기 위한 버튼

            //혹시라도 동작하고 있는 코루틴을 중지시킨다.
            if (ocrControll_02_s.coroutine != null)
            {
                all_s.stopCoroutineOcrControll_02();
            }

            OCRCamera.SetActive(false); //OCR 카메라를 꺼준다.
            PostCam.SetActive(true); //PostCamera_LookARObject 카메라를 켜준다.
            ARUICam.SetActive(true); //Camera를 켜준다.

            Particle.SetActive(true);
            PostObject.SetActive(true);



            //UI에 인풋필드에 있는 값을 UI에 뿌려준다.(ocr에서 인식한 값을 inputfelid에서 수정할 수도 있기때문에) AR화면에 뜰 데이터.
            nameInfo.text = nameinput.text;
            departmentInfo.text = departmentinput.text;
            rankInfo.text = rankinput.text;

            //가이드 부분
            UI_AR_Guide01.SetActive(true); //AR 가이드1를 켜준다.
            AR_ui.SetActive(true);
            UI_AR_menuButton.SetActive(true);

            setAR_s.InitAR(); //초기화하고
        }
    }





    //AR 가이드1에 x를 눌렀을때.
    public void click_ar_guide1_exit()
    {
        setAR_s.doDetection(); //가이드1이 있는 상태에서 플랜이 인식되면 안된다.
    }

    //하단의 메뉴 버튼을 클릭했을때
    public void click_ar_menu()
    {
        UI_AR_ReMenu.SetActive(true); //메뉴 전체를 켜준다.

        A_AR_Exit_Button.SetActive(true); //메뉴 나가기 버튼X를 켜준다.

        UI_AR_BG.SetActive(true); //백그라운드를 켜준다.

        setAR_s.SceneARObj.SetActive(false);
    }

    //메뉴의 x 버튼을 클릭했을때
    public void click_ar_menuexit()
    {
        //하단의 메뉴 버튼을 보이게 해준다.
        UI_AR_menuButton.SetActive(true);
        if (setAR_s.isCreate)
        {
            setAR_s.SceneARObj.SetActive(true);
        }
    }

    int bannerType = 0; //배너의 타입   0:subscribe   1: finance   2:entertainment 
    bool isbanner = false;

    //배너의 버튼을 클릭했을때.
    public void click_ar_banner_button(int type)
    {
        bannerType = type;
        isbanner = true;

        if (bannerType == 0)
        {
            UI_AR_subscribe_all.transform.GetChild(1).gameObject.SetActive(true); //텍스트와 캐릭터를 위로 올리리는 애니를 실행.
            UI_AR_subscribe_all.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (bannerType == 1)
        {
            UI_AR_finance_all.transform.GetChild(1).gameObject.SetActive(true); //텍스트와 캐릭터를 위로 올리리는 애니를 실행.
            UI_AR_finance_all.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (bannerType == 2)
        {
            UI_AR_entertainment_all.transform.GetChild(1).gameObject.SetActive(true); //텍스트와 캐릭터를 위로 올리리는 애니를 실행.
            UI_AR_entertainment_all.transform.GetChild(2).gameObject.SetActive(true);
        }
    }


    public void click_ar_tomain()
    {
        //메인으로 갈것인지의 모달창과 하단 메뉴버튼을 켜준다.
        UI_AR_tomain.SetActive(true);
        UI_AR_menuButton.SetActive(true);
    }

    //ActionCard AR화면에 UI만 리셋한다. 처음 가이드를 띄워준다.
    public void ar_UIReDetection()
    {
        //가이드 부분
        UI_AR_Guide01.SetActive(true); //AR 가이드1를 켜준다.
        AR_Guide02.SetActive(false); //AR가이드2는 끈다.

        //메인배너(메뉴)들을 끈다.
        UI_AR_ReMenu.SetActive(false);
        A_AR_Exit_Button.SetActive(false);

        //하단의 메인배너 버튼을 킨다.
        UI_AR_menuButton.SetActive(true);
    }

    public void ocr_infoModal_close()
    {
        UI_ocr_info.GetComponent<UI_Control>().CloseUI();
    }

    public void gestureGuide_AllClose()
    {
        if(OCR_GestureGuide_Page != null)
        {
            OCR_GestureGuide_Page.GetComponent<ocr_gestureguide_uicontrol>().closeAllGuide();
        }
    }


    public Transform[] prefabparentslist;



    /// <summary>
    /// 씬이 로드될때마다 실행.
    /// <para>번들로 받은 프리팹을 인스턴스. OCR_MainBtns_Page, UI_ocr_networkCheck, UI_ocr_DBCheck, OCR_GestureGuide_Page</para>
    /// <para>싼 로드 시 제스쳐가이드 나올지 안나올지 설정</para>
    /// <para>카메라, 파티클, 포스트, UI 등 초기화</para>
    /// </summary>
    public void InitUI() //씬이 로드될때, OCR메인으로 돌아갈때.
    {

        if (OCR_MainBtns_Page == null)
        {
            //<-인스턴스->
            OCR_MainBtns_Page = Instantiate(assetBunblesManager.GetNonCompressResources(AssetBunblesManager.BUNBLE_NAME.arcard, "OCR_MainBtns_Page"), prefabparentslist[0], false);
            OCR_MainBtns_Page.name = "OCR_MainBtns_Page";

            //인스턴스 루트 오브젝트의 스크립트에 필요한 값 할당.
            OCR_MainBtns_Page.GetComponent<ocr_mainbtn_uicontol>().all = all_s;
            OCR_MainBtns_Page.GetComponent<ocr_mainbtn_uicontol>().ocrcontroll_02 = ocrControll_02_s;
            OCR_MainBtns_Page.GetComponent<ocr_mainbtn_uicontol>().uicontroll = this.GetComponent<UIControll>();
            Snap_Btn = OCR_MainBtns_Page.transform.GetChild(0).gameObject;

        }


        if (UI_ocr_networkCheck == null)
        {
            //<-인스턴스->
            UI_ocr_networkCheck = Instantiate(assetBunblesManager.GetNonCompressResources(AssetBunblesManager.BUNBLE_NAME.arcard, "UI_ocr_networkCheck"), prefabparentslist[1], false);
            UI_ocr_networkCheck.name = "UI_ocr_networkCheck";

            //인스턴스 루트 오브젝트의 스크립트에 필요한 값 할당.
            UI_ocr_networkCheck.GetComponent<ocr_alert_uicontrol>().uicontrol = this.gameObject.GetComponent<UIControll>();
        }


        if (UI_ocr_DBCheck == null)
        {
            //<-인스턴스->
            UI_ocr_DBCheck = Instantiate(assetBunblesManager.GetNonCompressResources(AssetBunblesManager.BUNBLE_NAME.arcard, "UI_ocr_DBCheck"), prefabparentslist[2], false);
            UI_ocr_DBCheck.name = "UI_ocr_DBCheck";

            //인스턴스 루트 오브젝트의 스크립트에 필요한 값 할당.
            UI_ocr_DBCheck.GetComponent<ocr_alert_uicontrol>().uicontrol = this.gameObject.GetComponent<UIControll>();
        }


        if (OCR_GestureGuide_Page == null)
        {
            //<-인스턴스->
            OCR_GestureGuide_Page = Instantiate(assetBunblesManager.GetNonCompressResources(AssetBunblesManager.BUNBLE_NAME.guide_prefab, "OCR_GestureGuide_Page_"), prefabparentslist[3], false);
            OCR_GestureGuide_Page.name = "OCR_GestureGuide_Page";

            //인스턴스 루트 오브젝트의 스크립트에 필요한 값 할당.
            OCR_GestureGuide_Page.GetComponent<ocr_gestureguide_uicontrol>().uicontrol = this.GetComponent<UIControll>();
 
            //인스턴스된 오브젝트들의 요소들 중 UIControll.cs에서 사용할 변수를 저장.
            UI_OCR_BlackBG = OCR_GestureGuide_Page.transform.GetChild(1).gameObject; //BG넣기.
            UI_OCR_CardDetectionGuide = OCR_GestureGuide_Page.transform.GetChild(2).gameObject;
            UI_OCR_CheckInfoGuide = OCR_GestureGuide_Page.transform.GetChild(3).gameObject;
        }


        //Hierarchy에 오브젝트들 초기화
        OCRCamera.SetActive(true);
        PostCam.SetActive(false);
        ARUICam.SetActive(false);
        Particle.SetActive(false);
        PostObject.SetActive(false);

        //카메라버튼 활성화
        OnState_SnapBtn();

        //제스쳐가이드 자동 설정.
        //처음 앱을 열었을때만 제스쳐가이드 시작.
        if (SplayerPrefs.isPlayerPrefs("isARCArdGuideKey")) //한번이라도 x를 누른적이 있다면.
        {
            backControll.changeStep(this.gameObject, BackControll.ARCARD_STEP.OCRMain); //가이드 없이 시작할때.

            UI_OCR_BlackBG.SetActive(false);
            UI_OCR_CardDetectionGuide.SetActive(false);
        }
        else
        {
            backControll.changeStep(UI_OCR_CardDetectionGuide, BackControll.ARCARD_STEP.OCRGuide); // 가이드 있고 시작할때.

            UI_OCR_BlackBG.SetActive(true);
            UI_OCR_CardDetectionGuide.SetActive(true);
            all_s.hide_BottonMeun();//하단 메뉴 가림

        }

        OCR_GestureGuide_Page.GetComponent<ocr_gestureguide_uicontrol>().closeAllGuide();

        UI_AR_ReMenu.SetActive(false);
        UI_AR_menuButton.SetActive(true);
        A_AR_Exit_Button.transform.parent.gameObject.SetActive(false);

        AR_ui.SetActive(false);
        UI_AR_Guide01.SetActive(false);
        AR_Guide02.SetActive(false);
        OCR_Error_Text.SetActive(false); //"다시촬영해주세요"

        nameinput.text = "";
        departmentinput.text = "";
        rankinput.text = "";

        UI_AR_tomain.SetActive(false);

        OCRdetecting_Text.SetActive(false);
        UI_ocr_networkCheck.SetActive(false);
    }

    public void ActionCard_menu()
    {
        backControll.ToOCRMain();
    }

    //액션가면으로 이동
    public void ActionMask_meun()
    {
        all_s.arCardAll_Get_s.uIManager.GetComponent<UIManager>().ContentsMove(0);
        all_s.show_BottonMenu();
    }
    //액션그림으로 이동
    public void ActionArt_meun()
    {
        all_s.arCardAll_Get_s.uIManager.GetComponent<UIManager>().ContentsMove(1);
        all_s.show_BottonMenu();
    }
    //치카포카로 이동
    public void ChikaPoka_meun()
    {
        all_s.arCardAll_Get_s.uIManager.GetComponent<UIManager>().ContentsMove(2);
        all_s.show_BottonMenu();
    }

}
