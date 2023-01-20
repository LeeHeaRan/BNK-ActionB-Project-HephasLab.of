using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
//ActionCard에 AR화면에서 AR오브젝트가 나온 후 디바이스 카메라 화면 안에 있는지 밖에 있는지 확인.
/// </summary>
public class ARReDetection_ : MonoBehaviour
{
    
    
    public Camera cam;
    public GameObject pivotObj;
    List<GameObject> target = new List<GameObject>();
    bool isOneFirst = false; //target리스트에 피벗을 처음 오브젝트를 켰을때만 실행.
    float limitTime = 20f; //제한시간. 오브젝트가 보이지 않는 시간이 20초를 지나면 리셋.
    public All all_s;

    void Update()
    {
        if (this.GetComponent<SetAR>().SceneARObj.activeSelf) //오브젝트가 켜져있을때만 실행한다.
        {
            if (!isOneFirst)
            {
                for (int i = 0; i < pivotObj.transform.childCount; i++)
                {
                    target.Add(pivotObj.transform.GetChild(i).gameObject);
                }
                isOneFirst = true;
            }

            if (cam.transform.GetChild(0).GetComponent<ARCamera_Trigger>().isInObject || checkObjectIsInCamera(target))  //둘중 하나라도 true면 오브젝트가 범위에 있는 것임.
            {
                //보임
                limitTime = 20f;
            }
            else
            {
                //안보임 카운트가 들어감.
                if (limitTime > 0f)
                {
                    limitTime -= Time.deltaTime;
                }
                else //타이머가 다 됬을때.
                {
                    limitTime = 20f;
                    all_s.resetActionCard_AR(); //actioncard의 ar부분만 리셋하는 함수.
                }
            }
            //안보이게 된 후에 시간 측정. 
        }
        else
        {
            limitTime = 20f;
        }
    }

    public bool checkObjectIsInCamera(List<GameObject> _target)
    {
        bool isIncamera = false; //오브젝트가 카메라 안에있는지 밖에 있는지 확인.
        List<Vector3> targetVector = new List<Vector3>();

        for (int i = 0; i < _target.Count; i++)
        {
            targetVector.Add(cam.GetComponent<Camera>().WorldToViewportPoint(_target[i].transform.position));
        }

        for (int i = 0; i < _target.Count; i++)
        {
            if (targetVector[i].z > 0 && targetVector[i].x > 0 && targetVector[i].x < 1 && targetVector[i].y > 0 && targetVector[i].y < 1)
            {
                isIncamera = true;
            }
        }

        targetVector.Clear();

        return isIncamera;
    }
}
