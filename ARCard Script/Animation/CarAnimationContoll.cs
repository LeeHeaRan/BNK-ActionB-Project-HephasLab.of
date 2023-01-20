using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 자동차 오브젝트의 움직임을 관리한다.
/// </summary>
public class CarAnimationContoll : MonoBehaviour
{
    GameObject obj;
    public GameObject target;
    GameObject rootObj;

    [SerializeField] float forwardSpeed = 0.1f; //사용자가 설정하는 값.
    float defulatForwardSpeed = 0.05f; //회전할때, 트리거에 닿았을때 속도값.
    float Speed = 0.0f;
    [SerializeField] bool isSide = false; //본인이 사이드인지 확인하는 값.
    float distance = 0.003f; //레이 길이
    float rotSpeed = 0.5f; //기본값 닿은 트리거에 따라 값이 변함.

    RaycastHit rayHit;
    Ray ray;
    Vector3 dir = Vector3.zero;

    float timer = 0.0f;
    bool isTime = false;

    public GameObject startObj; //처음으로 돌아갈 트리거.
    Vector3 startPos; //처음위치값.
    Quaternion startRot; //처음회전값.

    bool isTargetHit = false;

    void Start()
    {
        obj = this.gameObject;
        rootObj = this.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        ray = new Ray();
        Speed = forwardSpeed;
          
        //시작할때 위치와 회전값을 저장.
        startPos = this.gameObject.transform.position;
        startRot = this.gameObject.transform.rotation;
    }

    void colliderenable() //콜리더를 켜준다.
    {
        startObj.transform.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    void Update()
    {
        dir = target.transform.position - obj.transform.position; //타겟을 바라보게 회전한다.
        obj.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed); //회전과 회전하는 속도
        obj.transform.Translate(ray.direction * Time.deltaTime * Speed * 0.1f, Space.World); //앞으로 움직인다.
        distance = rootObj.transform.localScale.x * 3f; //오브젝트 크기에 따라 레이 길이 변경

        if (isTime) //트리거에 안닿은상태로 2초가 지나면 설정한 값으로 스피드로 움직인다.
        {
            //트리거에 닿으면 timer =0 이 된다.
            timer += Time.deltaTime;
            if (timer < 2f) //회전중, 트리거에 닿은후 2초가 지나기 전에는 디폴트값으로 진행한다.
            {
                Speed = defulatForwardSpeed * (rootObj.transform.localScale.x * 1000); //디폴트값
            }
            else //사용자가 설정한 값으로 움직여준다. 
            {
                isTime = false; //타이머 끝.
            }
        }
        else //트리거에 닿기 전 처음. 트리거가 안닿아 있는 상태.
        {
            Speed = forwardSpeed * (rootObj.transform.localScale.x * 1000); //설정된
        }

        //ray
        ray.origin = obj.transform.localPosition;
        ray.direction = obj.transform.forward;

        if (Physics.Raycast(obj.transform.position, ray.direction, out rayHit, distance))
        {
            if(rayHit.transform.gameObject == target)
            {
                isTargetHit = true;
            }
            else
            {
                isTargetHit = false;
            }


            if (isTargetHit)
            {
                //만약 내 상위가 CarAnimation_01이면 트리거가 인것들만 적용해준다.
                if (transform.parent.gameObject.name.Equals("CarAnimation_01")) //01경로의 차
                {
                    //처음 위치로 초기화 해준다.
                    if (rayHit.transform.gameObject == startObj.transform.gameObject) //처음 트리거에 닿았을때 자동차를 처음 위치로 이동해준다.
                    {
                        startObj.transform.gameObject.GetComponent<BoxCollider>().enabled = false; //시작포인트의 콜리더를 잠깐 꺼주고 차가 지나간 다음 다시 활성화 해준다. 
                        this.transform.position = startPos; //초기화 위치로 이동한다.
                        this.transform.rotation = startRot; //초기화 회전값으로 바꾼다.
                        Invoke("colliderenable", 2f);
                    }

                    //이동관련 스크립트.
                    if (rayHit.collider.gameObject.name.Contains("01_P"))
                    {
                        target = rayHit.collider.gameObject.GetComponent<AnimationBox>().nextTarget;
                        rotSpeed = rayHit.collider.gameObject.GetComponent<AnimationBox>().rotSpeed;
                        isTime = true;
                        timer = 0.0f;
                    }

                    if (rayHit.collider.gameObject.name.Contains("01_P_SideCollider"))
                    {
                        isTime = true;
                        timer = 0.0f;

                        if (isSide) //만약 side오브젝트라면.
                        {
                            target = rayHit.collider.gameObject.GetComponent<AnimationBox>().sideTarget;
                        }
                        else //아니라면 회전하지 않는다.
                        {
                            target = rayHit.collider.gameObject.GetComponent<AnimationBox>().nextTarget;
                        }
                    }
                }
                else //02경로의 차
                {
                    //처음 위치로 초기화 해준다.
                    if (rayHit.transform.gameObject == startObj.transform.gameObject)
                    {
                        startObj.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                        this.transform.position = startPos;
                        this.transform.rotation = startRot;
                        Invoke("colliderenable", 2f);
                    }

                    if (rayHit.collider.gameObject.name.Contains("02_P"))
                    {
                        target = rayHit.collider.gameObject.GetComponent<AnimationBox>().nextTarget;
                        rotSpeed = rayHit.collider.gameObject.GetComponent<AnimationBox>().rotSpeed;
                        isTime = true;
                        timer = 0.0f;
                    }
                    if (rayHit.collider.gameObject.name.Contains("02_P_SideCollider") && rayHit.collider.gameObject.name.Contains(""))
                    {
                        isTime = true;
                        timer = 0.0f;

                        if (isSide) //만약 side오브젝트라면.
                        {
                            target = rayHit.collider.gameObject.GetComponent<AnimationBox>().sideTarget;
                        }
                        else //아니라면 회전하지 않는다.
                        {
                            target = rayHit.collider.gameObject.GetComponent<AnimationBox>().nextTarget;
                        }
                    }
                }

                //sideVisibleCarObject에 들어있는 오브젝트가 보였다 안보였다한다.
                //if (rayHit.collider.gameObject.name == "Invisible")
                if (rayHit.collider.gameObject.name.Equals("Invisible"))
                {
                    if (this.gameObject.name.Equals("side"))
                    {
                        this.gameObject.transform.GetChild(0).gameObject.SetActive(false); //하위오브젝트를 끈다.
                    }
                    target = rayHit.collider.gameObject.GetComponent<AnimationBox>().sideTarget;
                    rotSpeed = rayHit.collider.gameObject.GetComponent<AnimationBox>().rotSpeed;
                    isTime = true;
                    timer = 0.0f;
                }

                if (rayHit.collider.gameObject.name.Equals("Visible"))
                    {
                    if (this.gameObject.name.Equals("side"))
                    {
                        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    }
                    target = rayHit.collider.gameObject.GetComponent<AnimationBox>().sideTarget;
                    rotSpeed = rayHit.collider.gameObject.GetComponent<AnimationBox>().rotSpeed;
                    isTime = true;
                    timer = 0.0f;
                }
            }
        }
    }
}
