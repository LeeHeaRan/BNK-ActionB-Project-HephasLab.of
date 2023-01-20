using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubScroll : MonoBehaviour
{

    Vector2 BeganPos = new Vector2();
    Vector2 UpdatePos = new Vector2();
    public GameObject CloseBtn;
    float time = 0.1f;
    float utime = 0f;
 
    bool isState = false;

    public Toggle ALL_Agree;
    public Toggle[] toggles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<iTween>() != null)
        {


            Debug.Log(gameObject.GetComponent<iTween>().time);

        }
       
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                BeganPos = touch.position;
                UpdatePos = touch.position; 
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                //    Debug.Log((BeganPos - touch.position).sqrMagnitude);
                /*    if ((BeganPos-touch.position).sqrMagnitude > 5000f)
                    {
                        Debug.Log((BeganPos - touch.position).sqrMagnitude);
                    }*/

                utime += Time.deltaTime;
                if (utime >= time)
                {
                    if (Mathf.Abs(BeganPos.y-touch.position.y) < 20f)
                    {
                        
                        BeganPos = touch.position;
                        utime = 0;
                    }
                    else
                    {
                       
                        utime = 0;
                    }
                    
                 
                  
                }
                


            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log(BeganPos.y - touch.position.y);
                float dis = (isState) ? BeganPos.y - touch.position.y : touch.position.y- BeganPos.y;
                if (dis > 70f)
                {
                    if (gameObject.GetComponent<iTween>() != null) {

                       
                        iTween.Stop(gameObject);
                        
                    }

                    float moveTime = 0.3f;
                    Vector2 targetPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
                    Hashtable ht = new Hashtable();
                    ht.Add("name", (isState) ?"down" : "up");
                  
                    ht.Add("from", targetPos);
                    ht.Add("to", (isState) ? new Vector2(targetPos.x,0) : new Vector2(targetPos.x, 88f));
                    ht.Add("easeType", iTween.EaseType.easeOutQuint);
                    ht.Add("loopType", "once");
                    ht.Add("time", moveTime);
                    ht.Add("onupdatetarget", gameObject);
                    ht.Add("onupdate", "SetMoveValue");
                    ht.Add("oncompletetarget", gameObject);
                    ht.Add("oncomplete", "anim_complete");

                    iTween.ValueTo(gameObject, ht);

                    targetPos = CloseBtn.GetComponent<RectTransform>().anchoredPosition;
                    ht = new Hashtable();
                    ht.Add("name", (isState) ? "down" : "up");

                    ht.Add("from", CloseBtn.GetComponent<RectTransform>().anchoredPosition);
                    ht.Add("to", (isState) ? new Vector2(targetPos.x, -6.4f) : new Vector2(targetPos.x, -100f));
                    ht.Add("easeType", iTween.EaseType.easeOutQuint);
                    ht.Add("loopType", "once");
                    ht.Add("time", moveTime);
                    ht.Add("onupdatetarget", gameObject);
                    ht.Add("onupdate", "SetCloseMoveValue");
       

                    iTween.ValueTo(gameObject, ht);


                    isState = (isState) ? false : true;
                }
                UpdatePos = touch.position;
                time = 0;
            }

        }
    }
    void anim_complete()
    {
       
    }
   public void SetMoveValue(Vector2 value)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = value;
    }
    public void SetCloseMoveValue(Vector2 value)
    {
        CloseBtn.GetComponent<RectTransform>().anchoredPosition = value;
    }

    public void ALL_Toggle_Active(bool isOn)
    {
        if (isOn)
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].isOn = isOn;
            }
        }
        
    }
    private void OnDisable()
    {
        ALL_Agree.isOn = false;
        for(int i = 0; i<toggles.Length; i++)
        {
            toggles[i].isOn = false;
        }
    }
   
}

