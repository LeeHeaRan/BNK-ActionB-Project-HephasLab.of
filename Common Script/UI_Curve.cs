using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Curve : MonoBehaviour
{
    public List<UICurveTarget> curveTarget;
    
    bool isMove = false;
    bool isClose = false;
    bool isinit = true;
    bool isEnd = false;
   
    // Update is called once per frame
    void Update()
    {
        if (isinit)
        {
            for (int i = 0; i < curveTarget.Count; i++)
            {
                curveTarget[i].start();

            }
            isinit = false;
        }
        else
        {
            if (isMove)
            {
                isEnd = false;
                for (int i =0; i<curveTarget.Count; i++)
                {
                    if (curveTarget[i].CurveToVector(Time.deltaTime))
                    {
                        curveTarget[i].EndMove();

                    }
                    else
                    {
                        isEnd = true;
                    }
                }

                if (!isEnd)
                {

                    //open 끝나는시점
                    isMove = false;
                  

                }
            }

            if (isClose)
            {
                isEnd = false;
                for (int i = 0; i < curveTarget.Count; i++)
                {
                    if (curveTarget[i].CloseCurveToVector(Time.deltaTime))
                    {
                        curveTarget[i].EndClose();
                    }
                    else
                    {
                        isEnd = true;
                    }
                }

                if (!isEnd)
                {
                    //Close 끝나는시점
                    isClose = false;
                    gameObject.SetActive(false);

                }
            }
        }
    }
    public void CloseUI()
    {
        isMove = false;
        for (int i = 0; i < curveTarget.Count; i++)
        {

            curveTarget[i].StartClose();

        }
        isClose = true;



    }


    private void OnEnable()
    {
        isClose = false;
        for (int i = 0; i < curveTarget.Count; i++)
        {
            
                curveTarget[i].StartMove();
            
        }
        isMove = true;
    }
    private void OnDisable()
    {
        for (int i = 0; i < curveTarget.Count; i++)
        {
            curveTarget[i].init();
            //curveTarget[i].target.SetActive(false);
        }
        gameObject.SetActive(false);
    }
    
        

}
