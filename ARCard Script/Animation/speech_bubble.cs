using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
//자동차에 말풍선이 띄워지는 스크립트.
//ARCardObjectr>>Trigger>>안에 오브젝트에 자동차가 닿으면 
//ARCardObjectr>>AnimationControll>>CarAnimation01>>cute_car_02>>안에 트리거와 동일한 이름의 말풍성 오브젝트들이 활성화 된다.
/// </summary>
public class speech_bubble : MonoBehaviour
{
    RaycastHit rayHit;
    Ray ray;
    Vector2 dir = Vector3.zero;
    float distance = 0.003f;

    GameObject tempObject, thisObject, colliderObject;

    float bubbleMax = 2.6f;
    bool isObject = false;

    void Start()
    {
        ray = new Ray();
    }

    void Update()
    {
        ray.origin = this.transform.localPosition;
        ray.direction = this.transform.forward;

        //자동차가 트리거에 닿으면 작동한다.
        if(Physics.Raycast(this.transform.position, ray.direction, out rayHit, distance))
        {
            if (rayHit.transform.gameObject.name.Contains("speech")) //문자열이 포함된 트리거를 맞으면.
            {
                colliderObject = rayHit.transform.gameObject;
                colliderObject.GetComponent<BoxCollider>().enabled = false; //트리거에 레이가 계속 닿음. colllider를 비활성화

                if (rayHit.transform.gameObject.name.Equals("END_speech")) //끝나는 트리거에 닿았을때.
                {
                    //Debug.Log("end");
                    //Debug.Log("OUT:" + tempObject.name);
                    isObject = false;
                    iTween.ScaleTo(tempObject, new Vector3(0f, 0f, 0f), 0.3f); //스케일 0
                    Invoke("BubbleIn_Delay", 2f); //트리거를 다시 활성화

                }

                foreach (Transform child in this.transform)
                {
                    if (rayHit.transform.gameObject.name.Equals(child.gameObject.name)) //동일한 이름의 콜리더를 맞으면.
                    {
                        thisObject = child.gameObject;

                        if(tempObject != null)
                        {
                            //temp가 빈값이 아닐때
                            //Debug.Log("OUT:" + tempObject.name + tempObject.transform.localScale.x);
                            isObject = false;
                            iTween.ValueTo(transform.gameObject, iTween.Hash("from", bubbleMax, "to", 0f, "onupdate", "updateBubbleScale", "time", 0.8f, "easetype", iTween.EaseType.easeInElastic)); 
                            Invoke("BubbleIn_Delay", 1f); 
                            
                        }
                        else
                        {
                            //temp가 빈값일때. 처음 맞은 말풍선 콜리더.
                            //isObject = false;
                            //iTween.ValueTo(transform.gameObject, iTween.Hash("from", bubbleMax, "to", 0f, "onupdate", "updateBubbleScale", "time", 0.8f, "easetype", iTween.EaseType.easeInElastic)); //수정해야함.
                            Invoke("BubbleIn_Delay", 1f);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 말풍선이 커지면서 나오는 스크립트. 트리거를 다시 활성화 하는 스크립트 스케일 2.6f
    /// </summary>
    void BubbleIn_Delay()
    {
        //Debug.Log(thisObject.transform.localScale.x);
        //Debug.Log("IN:" + thisObject.transform.parent.name + thisObject.transform.localScale.x);
        
        if (colliderObject.name.Equals("END_speech"))
        {
            tempObject = null;
            thisObject = null;
            colliderObject.GetComponent<BoxCollider>().enabled = true; //콜리더 활성화
        }
        else
        {
            isObject = true;
            iTween.ValueTo(transform.gameObject, iTween.Hash("from", 0f, "to", bubbleMax, "onupdate", "updateBubbleScale", "time", 0.8f, "easetype", iTween.EaseType.easeOutElastic));
            tempObject = thisObject;
            colliderObject.GetComponent<BoxCollider>().enabled = true; //콜리터 활성화
        }
    }

    /// <summary>
    ///버블의 스케일을 조정하는 함수. itween에 onupdate에서 호출되는 함수.
    /// </summary>
    /// <param name="val"></param>
    public void updateBubbleScale(float val)
    {
        //Debug.Log(val);
        if (isObject) //isObject가 true이고 thisObject가 비어있지 않을 경우.
        {
            //Debug.Log("this");
            if(thisObject != null)
            {
                thisObject.transform.localScale = new Vector3(val, val, val);
            }
        }
        else
        {
            //Debug.Log("temp");
            if (tempObject != null)
            { 
                tempObject.transform.localScale = new Vector3(val, val, val);
            }
        }
    }
}

