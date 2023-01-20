using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

//LightMap Setting
using System.IO;
using UnityEngine.XR.ARFoundation;
using System;

/// <summary>
/// ARObject의 회전과 크기조절을 관리한다.
//1. 생성된 AR오브젝트를 arObj에 저장.
//2. 화면을 눌렀을때 타이머가 리셋되고 누른 포지션값을 저장.
//3. 화면을 누른채로 움직일때 오브젝트를 회전. 손가락을 움직인 거리를 임계값을 기준으로 회전 속도 제한.
//4. 화면을 뗏을때 움직인 거리와 시간을 기준으로 터치와 드래그를 구분.
//5. 터치 시 tag가 있는 특정 오브젝트를 클릭했는지 확인. 드래그 시 스르륵 멈추는 StopDrag iTween 실행.
/// </summary>
public class ARControll : MonoBehaviour
{

   


    public SetAR SetAR_s;

    //컨트롤할 ARObject
    public GameObject arObj;

    //클릭 및 드래그
    private Vector2 startV, endV, dirV;
    private float velocity = 2f; //드래그 속도
    private float time = 0f; //클릭(1f미만), 드래그(1f이상)를 확인하기 위한 시간.
    private Vector2 touchPos; //터치 시 position 값.
    private float acc = 0f; //클릭(100f미만), 드래그(100f이상), 임계값(200f)을 확인하기 위한 값.
    private float minV = 1f; //최소 회전속도
    private float maxV = 30f; //최대 회전속도

    private bool isClick = false;
    private bool isDrag = false;
    float hitDistance = 0f;
    float arobjSize = 0f;

    //낮 밤 변화

    public Toggle lightChange_Toggle;
    public GameObject[] nightWindow = new GameObject[5]; //밤에 켜지는 오브젝트.

    //라이팅 맵을 밤낮으로 바꾸는 부분  
    //1.리소스 폴더에 라이팅맵 주소 잘 확인하고. 잘 받아오는지 확인. 
    //2.라이팅맵에 구성요소 다 들어가있는지 확인. 정상적인 소스들로 구성되어있는지. 
    public Texture2D[] dayColorArr, dayDirArr, daySMArr, nightColorArr, nightDirArr, nightSMArr;
    public LightmapData[] lightmapdataNightArr, lightmapdataDayArr, TempData;

    //오브젝트의 스케일을 조정하는 부분.
    private float initialDeistance;
    private Vector3 initialScale;
    private RaycastHit hit;

    void Start()
    {
        nightWindow[0].SetActive(false); 
        nightWindow[1].SetActive(false); 
        nightWindow[2].SetActive(false); 
        nightWindow[3].SetActive(false); 
        nightWindow[4].SetActive(false); 

        TempData = LightmapSettings.lightmaps;

        lightmapdataNightArr = TempData;
        lightmapdataDayArr = TempData;
        SystemIOFileLoad();
    }

    private void Awake()
    {
        arobjSize = arObj.transform.localScale.x; //init이 실행되기 전에 awake로 값을 가지고 있어야한다.
    }

    //처음 실행했을때 첫 라이팅셋팅의 라이트맵의 인덱스 값을 넣어준다. 씬을 이동할때 라이트맵의 갯수가 증가한다. 1로 맞춘다.
    public void SaveMapData()
    {
        TempData = LightmapSettings.lightmaps;

        lightmapdataNightArr = TempData;
        lightmapdataDayArr = TempData;
        SystemIOFileLoad();
        //text.SetLog("Sys1:" + LightmapSettings.lightmaps.Length.ToString(), false); //1

    }


    public void nullSave() //장면전환 전에 모든 라이드맵 데이터를 지워줘야한다. 안그러면 기전에 묻은 맵이 남아있어서 맵이 겹쳐진다.(이상하게 나옴.-더 밝게)
    {
        LightmapData[] NoneLightMapping;
        NoneLightMapping = LightmapSettings.lightmaps;
        LightmapsMode NoneLightMapping_ = LightmapSettings.lightmapsMode;

        for (int i = 0; i < NoneLightMapping.Length; i++)
        {
            NoneLightMapping[i].lightmapColor = null;
            NoneLightMapping[i].lightmapDir = null;
            NoneLightMapping[i].shadowMask = null;

        }
        LightmapSettings.lightmaps = NoneLightMapping;
        LightmapSettings.lightProbes = null;
        LightmapSettings.lightmaps = null;

        for (int i = 0; i < LightmapSettings.lightmaps.Length; i++)
        {
            Destroy(LightmapSettings.lightmaps[i].lightmapColor);
            Destroy(LightmapSettings.lightmaps[i].lightmapDir);
        }
        //text.SetLog("Sys2:" + LightmapSettings.lightmaps.Length.ToString());
    }
    void Update()
    {
        //드래그를 따라서 오브젝트를 회전시키는 부분.
        if (isDrag)
        {
            //arObj.transform.Rotate(new Vector3(0f, -(touchPos.x * velocity * Time.deltaTime), 0f));
            arObj.transform.Rotate(new Vector3(0f, (touchPos.y * velocity * Time.deltaTime), 0f));
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            try
            {
                if (touch.phase == TouchPhase.Began)
                {
                    //Debug
                    //debugText01.text += "\n화면을 누름";

                    //회면을 눌렀을때 한번 호출
                    startV = touch.position;
                    time = 0f;
                    //touchPos.x = 0f;
                    touchPos.y = 0f;
                    iTween.Stop(arObj.gameObject);
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    //Debug
                    //debugText01.text += "\n화면을 드래그 중";

                    //화면을 누른채로 움직이고 있는 동안 호출
                    isDrag = true;
                    endV = touch.position;
                    dirV = startV - endV;
                    acc += Mathf.Abs(dirV.magnitude); //처음 클릭한 곳과 클릭이 끝난 곳 사이의 거리값.
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    //손을 뗏을때 한번 호출
                    if (acc < 100f && time < 1f) //조금 움직이고 클릭한 시간이 1초 미만일경우. (클릭 시)
                    {
                        //Debug
                        //text.SetLog("클릭이 발생");

                        isClick = true;
                        isDrag = false;

                    }
                    else //드래그하고 손을 뗏을 시
                    {
                        //debugText01.text += "\n";
                        //text.SetLog("드래그 후 손을 뗌");

                        isClick = false;
                        isDrag = true;
                        acc = 0f;

                        //float timeV = (Mathf.Abs(dirV.x) * 0.001f) * 5;
                        float timeV = (Mathf.Abs(dirV.y) * 0.001f) * 5;
                        //iTween.ValueTo(transform.gameObject, iTween.Hash("from", touchPos.x, "to", 0f, "onupdate", "StopDrag", "time", timeV)); //드래그하고 손을 놓았을때 서서히 멈추도록 한다.
                        iTween.ValueTo(arObj.gameObject, iTween.Hash("from", touchPos.y, "to", 0f, "onupdate", "StopDrag", "onupdatetarget", this.gameObject, "time", timeV)); //드래그하고 손을 놓았을때 서서히 멈추도록 한다. //수정해야함.
                    }
                }

            }
            catch (Exception e)
            {
                //text.SetLog(e.ToString(), false);
            }

            if (acc > 200f) //회전 속도제한. 드래그가 발생 했을때 최소회전 속도와 최대 회전속도를 제한한다. 
            {
                touchPos = Input.GetTouch(0).deltaPosition;

                //if (Mathf.Abs(touchPos.x) < minV)
                if (Mathf.Abs(touchPos.y) < minV)
                {
                    touchPos.y = .0f;
                }
                if (Mathf.Abs(touchPos.y) > maxV)
                {
                    touchPos.y = (touchPos.y < 0) ? -maxV : maxV;
                }
            }
        }

        //오브젝트의 스케일을 조정하는 부분.
        double tempFactor;
        //X
        if (Input.touchCount == 2)
        {
            try
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                if (Physics.Raycast(ray, out hit))
                {
                    hitDistance = hit.distance;
                }

                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                    touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                {
                    return;
                }

                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    initialDeistance = Vector2.Distance(touchZero.position, touchOne.position);
                    initialScale = arObj.transform.localScale;
                }
                else
                {
                    var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    if (Mathf.Approximately(initialDeistance, 0)) //근사한지 비교한다.
                    {
                        return;
                    }

                    var factor = currentDistance / initialDeistance;
                    tempFactor = (initialScale * factor).x;

                    if ((initialScale * factor).x < 0.0005652389) //0.0002413458->0.0005652389 떨리는걸 줄이기 위해 최소 사이즈 지정 + 오브젝트가 뒤집어 지는거 방지
                    {
                        tempFactor = factor;
                    }
                    else
                    {
                        arObj.transform.localScale = initialScale * factor;
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }

    /// <summary>
    /// 오브젝트 회전 후 서서히 멈추도록하는 함수.
    /// </summary>
    /// <param name="val"></param>
    public void StopDrag(float val)
    {
        touchPos.y = val;
    }


    /// <summary>
    /// 리소스폴더에서 라이팅맵 데이터를 받아온다.
    /// </summary>
    private void SystemIOFileLoad()
    {

        int lightMapNumber = 0;
        nightColorArr = new Texture2D[lightmapdataNightArr.Length];
        nightDirArr = new Texture2D[lightmapdataNightArr.Length];
        nightSMArr = new Texture2D[lightmapdataNightArr.Length];

        dayColorArr = new Texture2D[lightmapdataNightArr.Length];
        dayDirArr = new Texture2D[lightmapdataNightArr.Length];
        daySMArr = new Texture2D[lightmapdataNightArr.Length];

        for (int i = 0; i < lightmapdataNightArr.Length; i++)
        {
            nightColorArr[i] = Resources.Load<Texture2D>("BusanARtest/LightMapping/night/Lightmap-" + lightMapNumber + "_comp_light");
            nightDirArr[i] = Resources.Load<Texture2D>("BusanARtest/LightMapping/night/Lightmap-" + lightMapNumber + "_comp_dir");
            lightMapNumber++;
        }

        lightMapNumber = 0;
        for (int i = 0; i < lightmapdataDayArr.Length; i++)
        {
            dayColorArr[i] = Resources.Load<Texture2D>("BusanARtest/LightMapping/day/Lightmap-" + lightMapNumber + "_comp_light");
            dayDirArr[i] = Resources.Load<Texture2D>("BusanARtest/LightMapping/day/Lightmap-" + lightMapNumber + "_comp_dir");
            lightMapNumber++;
        }
    }

    /// <summary>
    /// AR오브젝트의 낮밤 토글을 클릭시 실행.
    /// </summary>
    public void changeLight()
    {
        //명함_AR_낮/밤 전환.
        if (lightChange_Toggle.GetComponent<Toggle>().isOn)//달->해
        {
            TodayLight();
        }
        else
        {
            TonightLight();
        }

    }

    public void TonightLight() //밤으로 라이트맵을 바꾸는 함수.
    {
        try
        {
            for (int i = 0; i < lightmapdataNightArr.Length; i++)
            {
                lightmapdataNightArr[i].lightmapColor = nightColorArr[i];
                lightmapdataNightArr[i].lightmapDir = nightDirArr[i];

            }
            LightmapSettings.lightmaps = lightmapdataNightArr;
        }
        catch (Exception e)
        {
            //text.SetLog(e.ToString());
        }

        nightWindow[0].SetActive(true); 
        nightWindow[1].SetActive(true);
        nightWindow[2].SetActive(true);
        nightWindow[3].SetActive(true);
        nightWindow[4].SetActive(true);
    }

    public void TodayLight() //낮으로 라이트맵을 바꾸는 함수.
    {
        for (int i = 0; i < lightmapdataDayArr.Length; i++)
        {
            lightmapdataDayArr[i].lightmapColor = dayColorArr[i];
            lightmapdataDayArr[i].lightmapDir = dayDirArr[i];
        }
        LightmapSettings.lightmaps = lightmapdataDayArr;

        nightWindow[0].SetActive(false); 
        nightWindow[1].SetActive(false); 
        nightWindow[2].SetActive(false); 
        nightWindow[3].SetActive(false); 
        nightWindow[4].SetActive(false); 
    }


    /// <summary>
    /// <para>AR오브젝트의 클릭 및 드래그 정지</para>
    /// <para>AR오브젝트의 크기를 초기화</para>
    /// <para>AR오브젝트의 창문그룹을 초기화</para>
    /// </summary>
    public void initControllAR()
    {
        isClick = false;
        isDrag = false;

        arObj.transform.localScale = new Vector3(arobjSize, arobjSize, arobjSize);

        nightWindow[0].SetActive(false); 
        nightWindow[1].SetActive(false); 
        nightWindow[2].SetActive(false); 
        nightWindow[3].SetActive(false); 
        nightWindow[4].SetActive(false); 
    }
}
