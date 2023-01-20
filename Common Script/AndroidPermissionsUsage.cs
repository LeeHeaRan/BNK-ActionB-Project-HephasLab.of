using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class AndroidPermissionsUsage : MonoBehaviour
{

#if UNITY_ANDROID
    public string[] WantedPermissions ;
    public UIManager uimanager;


    private string currentPermission;
    public GameObject canvas_popup;
    public GameObject canvas_rePer;
    public GameObject canvas_denied;
    bool isFirst = true;
    bool isRePer = false;
    List<string> requstPermission = new List<string>();
    int count = 0;
    int Permissions_count = 0;
    public void Start()
    {
        canvas_denied.SetActive(false);
        Permissions_count = PlayerPrefs.GetInt("Permissions");
        CallPermission();
    }

    // 최초 불려지는 스크립트. 
    // 다음씬에서 사용되기전에 버튼을 누르면 호출하거나 최초 씬의 start에서 호출하면 됨
    public void CallPermission()
    {
        bool isAnyFailed = false;

        // 한개라도 permission이 제대로 안되있다면 true를 반환함
        for (int i = 0; i < WantedPermissions.Length; i++)
        {
            if (CheckPermissions(WantedPermissions[i]) == false)
            {
               // text.text += WantedPermissions[i] + "\n";
                requstPermission.Add(WantedPermissions[i]);
                isAnyFailed = true;
                //return;
            }
        }

        if (isAnyFailed && isFirst)
        {
            Debug.LogWarning("권한이 없습니다, 권한 승인을 해주세요");
//text.text += "권한이 없습니다, 권한 승인을 해주세요\n";

         //  text.text += "Permissions PlayerPrefs:"+PlayerPrefs.GetInt("Permissions")+"\n\n";



            // 퍼미션을 왜 요청하는지 설명하는 팝업을 이 때 ON 시켜주면 됨
            // 팝업에는 버튼 하나가 있고 누르면 OnGrantButtonPress()를 호출해야함

            if (Permissions_count >= 2)
            {
                canvas_denied.SetActive(true);
            }
            else
            {
                canvas_popup.SetActive(true);
                isFirst = false;
            }


        }else if(isAnyFailed && !isFirst && !isRePer)
        {
          
           
            canvas_rePer.SetActive(true);
            isRePer = true;


        }else if (isAnyFailed && !isFirst && isRePer)
        {
            canvas_denied.SetActive(true);
        }
        else
        {
            uimanager.LoadingStart();
            // 성공한 부분. 권한을 가지고 하고싶은 일을 하면 됨
            Debug.Log("퍼미션 확인 완료..");
      //      text.text += "퍼미션 확인 완료..\n";
            ALLClose();
        }
    }


    // 퍼미션을 체크한다.
    private bool CheckPermissions(string a_permission)
    {
        // 안드로이드가 아니면 ㅍㅊ true를 리턴시킨다.
        if (Application.platform != RuntimePlatform.Android)
        {
            uimanager.LoadingStart();
            return true;
        }

        return AndroidPermissionsManager.IsPermissionGranted(a_permission);
    }
    public void OnGrantButtonPress()
    {
        Invoke("OnSetPermission", 0.5f);

    }

        // 권한 승인 버튼에 할당합시다.
        public void OnSetPermission()
    {

/*        ARCore 또는 ARFoundatioln 사용시 권한획득에 문제가 있었다.
아래 방법으로 해결.

//GPSSystem.Action action = (text, check) => Debug.Log(text);UnityEngine.XR.ARCore.ARCorePermissionManager.RequestPermission("android.permission.ACCESS_FINE_LOCATION", action);

* 2018.3부터는 아래방법으로도 가능.
//


* **2개의 권한을 획득해야 할때는 같은방법 2개는 안되고, 위에꺼 아래꺼 하나씩 적용하니 된다.*/

        
            try
            {
            /*                / AndroidPermissionsManager.RequestPermission(new[] { WantedPermissions[i] }, new AndroidPermissionCallback(
                              grantedPermission =>
                              {
                                  // 권한이 승인 되었다.
                                  CallPermission();
                              },
                              deniedPermission =>
                              {
                                  canvas_rePer.SetActive(true);
                                  // 권한이 거절되었다.
                              },
                              deniedPermissionAndDontAskAgain =>
                              { 
                                  // 권한이 거절된데다가 다시 묻지마시오를 눌러버렸다.
                                  // 안드로이드 설정창 권한에서 직접 변경 할 수 있다는 팝업을 띄우는 방식을 취해야함. 


                                  canvas_denied.SetActive(true);
                              }));*/
                for (int i = count; i < requstPermission.Count; i++)
                {
                    if (CheckPermissions(requstPermission[i]) == false)
                    {
                        if (requstPermission[i].Equals("android.permission.READ_PHONE_STATE"))
                        {
                            if (GetSDKLevel() > 29)
                            {
                                requstPermission[i] = "android.permission.READ_PHONE_NUMBERS";
                            }

                           


                        }
                        UnityEngine.XR.ARCore.ARCorePermissionManager.RequestPermission(requstPermission[i], PermissionCallBack);
                    count += 1;
                        break;
                    }
                }

            } catch (Exception e)
            {
              //  text.text += e;
            }
        
    }
    public int GetSDKLevel()
    {
        var clazz = AndroidJNI.FindClass("android.os.Build$VERSION");
        var fieldID = AndroidJNI.GetStaticFieldID(clazz, "SDK_INT", "I");
        var sdkLevel = AndroidJNI.GetStaticIntField(clazz, fieldID);
        return sdkLevel;
    }
    // 거절했다면 닫는 기능
    public void PressDeniedCanvasButton()
    {
        canvas_denied.SetActive(false);
    }
    public void ALLClose()
    {
        gameObject.SetActive(false);
        uimanager.LoadingStart();
    }



    public void PermissionCallBack(string s, bool b)
    {
 
        
            if(requstPermission.Count != count)
            {
                Invoke("OnSetPermission", 0.1f);
            }
            else
            {
                uimanager.LoadingStart();
                PlayerPrefs.SetInt("Permissions", Permissions_count + 1);
                PlayerPrefs.Save();
                CallPermission();

            }
      
    }
    public void CloseComplete()
    {
        gameObject.SetActive(false);
        uimanager.LoadingStart();
    }
#elif UNITY_IOS
    public UIManager uimanager;
    private void Start(){

        uimanager.LoadingStart();
    }

#endif

}
