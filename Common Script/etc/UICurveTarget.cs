
using System;
using UnityEngine;

[Serializable]
public class UICurveTarget
{

    
    [Header("Curve Control")]
    public AnimationCurve curve;
  



    [Header("Common")]
    public GameObject target;
    public Vector2 MoveTo;
    public float Time;
    public float DelayTime;
    float temp_DelayTime=0;
    [Tooltip("닫을때 가속도 기본값 1, 1f->0f 느려짐  1f->2f 빨라짐")]
    [Range(0.0f, 2.0f)]
    public float Close_Acceleration=1f;
    float t=0;
    Vector2 StartVector;
    Vector2 NowVector;
    bool isMove=false;
    bool isClose = false;

   

    public void start()
    {
        StartVector = target.GetComponent<RectTransform>().anchoredPosition;
    }
    public void init()
    {
        temp_DelayTime = 0;
        t = 0;
        target.GetComponent<RectTransform>().anchoredPosition = StartVector;
    }
    public void SetPos(Vector2 vec2)
    {
        target.GetComponent<RectTransform>().anchoredPosition = vec2;
    }
    public Vector2 GetStartVector()
    {
        return StartVector;
    }
  
    public bool CurveToVector(float delt)
    {
        if (isMove)
        {
            if (Time >= t)
            {
                if (temp_DelayTime > DelayTime)
                {
                    NowVector = Vector2.Lerp(StartVector, MoveTo, curve.Evaluate(t / Time));
                    target.GetComponent<RectTransform>().anchoredPosition = NowVector;
                    t += delt;
                }
                else
                {
                    temp_DelayTime += delt;
                }
                return false;

            }
            else
            {

                return true;

            }
        }
        else
        {
            return true;
        }
       
    }
    public bool CloseCurveToVector(float delt)
    {
        if (isClose)
        {
            if (t >= 0)
            {
                if (temp_DelayTime > DelayTime)
                {
                    target.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(StartVector, NowVector, curve.Evaluate(t / Time));
                    t -= (Close_Acceleration>0f)? delt* Close_Acceleration :1f;

                }
                else
                {
                    temp_DelayTime += delt;
                }

                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    
    public void EndMove()
    {
        isMove = false;
        temp_DelayTime = 0;
        t = Time;
    }
    public void StartMove()
    {
        isMove = true;
        t = 0;
    }
    public void EndClose()
    {
        isClose = false;
        temp_DelayTime = 0;
        t = 0;
    }
    public void StartClose()
    {
        isClose = true;
       
    }
}
