using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnityExample;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//AR관련 라이브러리 사용.
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


/// <summary>
/// AR 생성, 초기화를 관리하는 소스코드.
/// </summary>
public class SetAR : MonoBehaviour //ARcamGetTexture
{
    //1. rayhit 클릭한 곳에 AR오브젝트를 생성한다.
    //2. AR오브젝트가 생성되고 ARPlane 감지를 멈추고 스르륵(RemovePlane iTween) 사라지게 한다. => 감지를 멈추면 안됨 기아앱.
    //3. AR Init. init 버튼 클릭 시 ARSession을 초기화 한다.
    //4. ARControll Script의 init함수를 가져와서 실행한다.

    public ARRaycastManager m_RaycastManager;
    public List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    //public GameObject createAR; //생성된 ARObject를 저장.

    public ARPlaneManager planeManager; //ARPlane Manager

    bool isremovePlane; //평면이 사라지는 효과 true 실행, false 중지.
    public Material planeM; //평면에 적용될 머터리얼
    public Color planeCol;

    public ARSession session; //ARSession

    public ARControll ARControll_s; //init

    public GameObject SceneARObj;

    public Animator arAppear;

    //public TestviewLog text;

    public bool isCreate = false;
    //public Text debugText01;

    public GameObject Cars;
    public ParticleSystem arAppearParticle;

    public CarAnimationContoll[] carAnimations = new CarAnimationContoll[8];

   // public TEST test;


    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        //planeManager.requestedDetectionMode = PlaneDetectionMode.None;
        doNotDetection();
        //planeManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;
    }

    private void Start()
    {
        //Remove plane
        isremovePlane = false;
        planeCol = planeM.GetColor("_TexTintColor");
        SceneARObj.SetActive(false);
        //SceneARObj.SetActive(true);

    }

    //RemovePlane iTween 함수들
    private void StartRemovePlane()
    {
        //Debug
        //debugText01.text += "\n평면이 스르륵 사라지는 RemovePlane iTween 시작";
        isremovePlane = false;
    }

    private void UpdateRemovePlane(float _val)
    {
        planeCol.a = _val;
        foreach (var plane in planeManager.trackables)
        {
            //Debug
            //debugText01.text += "\n평면 머터리얼의 알파값 변경 중";

            //모든 평면에 머터리얼 값을 업데이트한다. 알파값을 0으로.
            plane.gameObject.GetComponent<Renderer>().material.SetColor("_TexTintColor", planeCol);
        }
        //Debug
        //debugText01.text += "\n평면이 스르륵 사라지는 RemovePlane iTween 실행 중";
    }

    RaycastHit hit;
    public float arPlaneSize = 0.0f;
    public Camera arCam;

    void Update()
    {
        //text.SetLog(SceneARObj.transform.position.x.ToString() + "|"+ SceneARObj.transform.position.y.ToString() + SceneARObj.transform.position.z.ToString(), false);

        //레이를쏴서 플랜오브젝트가 생성되었는지 확안하고 일정부분이상 생성되었다면 토스트메시지를 띄워준다.

        Ray planeRay = arCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //회면 중앙에서 레이쏘기.
        Debug.DrawLine(planeRay.origin, hit.point, Color.red);

        // text.SetLog("update", false);
        //레이를 계속 쏘면서 플랜이있는지 확인한다.
        if (Physics.Raycast(planeRay, out hit))
        {
            if (hit.transform.gameObject.name.Contains("ARPlane")) //플랜에 맞고 있고
            {
                if (isCreate) //ar이 생성된 상태.
                {
                    doNotDetection(); //있어도 없어도 흔들림에 변화없음.
                }
                else
                {
                    //ar가이드1을 꺼주면 인식을 시작함.
                    arPlaneSize = hit.transform.GetComponent<Collider>().bounds.size.x; //ar이 생성되기 전 상태.
                }
            }
        }
        else if (isCreate) //ar이 생성되어 있고. 아무것도 맞지 않고 있을때.
        {
            doDetection();
            UpdateRemovePlane(0); //안보이게 한다.
            //text.SetLog("평면 인식중...(플랜에 안맞음.)", false);
        }


        //클릭한 곳에 AR오브젝트가 생성되게한다.
        if (Input.touchCount > 0 && arPlaneSize > 0.6f)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            //text.SetLog("오브젝트상태: "+ Post.activeSelf);

            if (m_RaycastManager.Raycast(touchPos, s_Hits, TrackableType.PlaneWithinPolygon)) //평면에 hit이 되면.
            {
                var hitPose = s_Hits[0].pose;

                //if (createAR != null) //오브젝트 하나만 생성하기 위한 카운트
                //if (!SceneARObj.activeSelf) //오브젝트 하나만 생성하기 위한 카운트
                if (!isCreate) //오브젝트 하나만 생성하기 위한 카운트
                {

                    SceneARObj.SetActive(true);
                    //DebugLOG
                    //명함_AR_오브젝트 생성.

                    //Debug
                    //debugText01.text = "보여라!";

                    //createAR = Instantiate(m_RaycastManager.raycastPrefab, hitPose.position, new Quaternion(hitPose.rotation.x, hitPose.rotation.y - 0.3f, hitPose.rotation.z, hitPose.rotation.w)); //AR오브젝트 생성.
                    SceneARObj.transform.position = hitPose.position;
                    SceneARObj.transform.rotation = new Quaternion(hitPose.rotation.x, hitPose.rotation.y - 0.3f, hitPose.rotation.z, hitPose.rotation.w);
                    //createAR = SceneARObj;

                    //doNotDetection();

                    //평면이 스르륵 사라지는 부분 실행. 
                    isremovePlane = true;

                    arAppear.Play("appear"); //등장애니메이션
         


                    iTween.ScaleTo(SceneARObj.transform.GetChild(0).gameObject, new Vector3(50f, 50f, 50f), 1f); //그림자 크기 애니

                    arAppearParticle.gameObject.transform.position = new Vector3(SceneARObj.transform.position.x, SceneARObj.transform.position.y + 0.1f, SceneARObj.transform.position.z);
                    arAppearParticle.Play();
                    isCreate = true;
                }
                //평면이 스르륵 사라지는 RemovePlane iTween을 한번만 실행한다. isremovePlane = true 일때 실행
                if (isremovePlane)
                {
                     iTween.ValueTo(transform.gameObject, iTween.Hash("from", planeCol.a, "to", 0f, "time", 0.2f, "onstart", "StartRemovePlane", "onupdate", "UpdateRemovePlane")); //수정해야함.
                }
            }
        }

        if (isCreate && !Cars.activeSelf)
        {
            //등장애니메이션이 끝나면 자동차를 보여준다.
            if (arAppear.GetCurrentAnimatorStateInfo(0).IsName("appear"))
            {
                if (arAppear.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f) //등장애니메이션이 끝남.
                {
                    Cars.SetActive(true);
                    //Debug.Log(arAppear.GetCurrentAnimatorStateInfo(0).normalizedTime);
                }
            }
        }

    }

   /// <summary>
   /// 평면감지를 시작
   /// </summary>
    public void doDetection()
    {
        //Debug
        //debugText01.text += "\n평면감지를 시작";

        //DebugLOG
        //명함_AR_평면감지 시작
        planeManager.requestedDetectionMode = PlaneDetectionMode.Horizontal; //평면감지를 시작한다.
        this.GetComponent<ARPointCloudManager>().enabled = true;

    }

    /// <summary>
    /// 평면감지를 중단
    /// </summary>
    public void doNotDetection()
    {
        //Debug
        //debugText01.text += "\n평면감지 중지";

        //DebugLOG
        //명함_AR_평면감지 중지.
        planeManager.requestedDetectionMode = PlaneDetectionMode.None; //평면감지를 중지한다.
        this.GetComponent<ARPointCloudManager>().enabled = false;

    }

    /// <summary>
    /// 자동차 오브젝트들을 보이게 한다. AR오브젝트 생성 애니메이션이 끝나고 호출. 
    /// </summary>
    public void endAppear()
    {
        Cars.SetActive(true);//자동차 보이게하기
    }



    /// <summary>
    /// AR의 모든 변수와 Object를 초기화하는 함수. 초기화 버튼에 들어간다.
    /// </summary>
    public void InitAR() //초기화한다.
    {

        //DebugLOG
        //명함_AR_InitAR실행.
        arPlaneSize = 0.0f;
        Cars.SetActive(false);

        //ARControll_cs, CamControll_s의 초기화 함수
        ARControll_s.initControllAR();

        //SetAR_cs의 초기화 내용
        SceneARObj.SetActive(false);
        //createAR = null;


        session.Reset();
        doNotDetection();

        //removePlane 아이트윈을 멈출뿐
        if (gameObject.GetComponent<iTween>() != null)
        {
            iTween.Stop(gameObject);
        }


        foreach(var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }

        //바닥 색상 초기화
        planeCol = planeM.GetColor("_TexTintColor");
        planeM.SetColor("_TexTintColor", planeCol);

        isCreate = false;

    }

    //애니메이션 실행 함수 Debug.
    /* public void AniButton()
     {
         isCreate = true;
         arAppear.Play("appear"); //등장애니메이션
         iTween.ScaleTo(SceneARObj.transform.GetChild(0).gameObject, new Vector3(32.5f, 32.5f, 32.5f), 1f);
         arAppearParticle.gameObject.transform.position = new Vector3(SceneARObj.transform.position.x, SceneARObj.transform.position.y+0.1f, SceneARObj.transform.position.z);
         arAppearParticle.Play();
     }*/

}
