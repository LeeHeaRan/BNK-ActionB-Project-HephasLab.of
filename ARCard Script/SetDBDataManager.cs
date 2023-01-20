using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


/// <summary>
/// 모바일 영업점과 배너들의 URL을 서버에서 받아오는 함수를 호출하는 클래스.
/// 모바일 영업점 활성화 비활성화 관리.
/// </summary>
public class SetDBDataManager : MonoBehaviour
{
    public Texture default_subbannerImage;
    public Material boardImage_m; //서브배너 이미지가 적용될 머터리얼
    public string subbanner_imageData;
    public UIControll uicontroll;

    public BackControll backControll;


    public string[] Title = new string[9]; //디폴트 값을 가지고 있는다. 0~8
    public string[] Type = new string[9];
    public string[] URL = new string[9];

    public Text[] textArr = new Text[9];

    public string mobilebranchURL = ""; //모바일 영업점 URL
    public GameObject mobileBranch_button;

    //활성화 비활성화 버튼 이미지를 저장.
    public Sprite[] mBranchImage = new Sprite[2];

    /* 배너 위치정보
    01 : 하위배너 1-1 / 02 : 하위밴너 1-2 / 03 : 하위배너 1-3
    04 : 하위배너 2-1 / 05 : 하위배너 2-2 / 06 : 하위배너 2-3
    07 : 하위배너 3-1 / 08 : 하위배너 3-2 / 09 : 하위배너 3-3
    */

    //이미지 주소를 웹통신하는 부분.
    IEnumerator getTexture(string url) //서브배너의 url을 받아온다. web통신.
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.result);
            boardImage_m.mainTexture = default_subbannerImage;
            yield break;
        }
        else
        {
            Texture texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            boardImage_m.mainTexture = texture;
            //raw.texture = texture;
            yield break;
        }
    }

    //Url을 넣어서 사용한다.
    public void getWebImage(string url)
    {
        StartCoroutine(getTexture(url)); //인터넷에 있는 url사용.
    }


    private void Start()
    {
        //시작시 모바일영업점은 비활성화 상태. 
        //직원행번으로 조회가 끝나고 모바일영업점링크를 찾으면 활성화된다.
        mobileBranch_OnOffButton(false);

    }

    public void mainbanner(int val) //val는 위치값.
    {
    
        //타입변경
        switch (Type[val]) //해당 위치값에 맞는 type[위치값]을 찾음.
        {
            case "01":
                uicontroll.ActionMask_meun();
                break;
            case "02":
                uicontroll.ActionCard_menu();
                break;
            case "03":
                uicontroll.ChikaPoka_meun();
                break;
            case "04":
                uicontroll.ActionArt_meun();
                break;
            case "05":
                Application.OpenURL(URL[val]); //url 변경 해당위치값에 맞는 url을 넣는다.
                break;
            default:
                Debug.Log("잘못된 타입.");
                break;
        }
    }

    //모바일 영업점연결 링크
    public void mobileBranch_btn()
    {
        if (!mobilebranchURL.Equals(""))
        {
            Application.OpenURL(mobilebranchURL);
            uicontroll.all_s.resetActionCard_AR();
        }
    }

    //모바일영업점 버튼을 활성화/비활성화 하는 함수
    public void mobileBranch_OnOffButton(bool isOnOff)
    {
        //이미지와 색상 변경.
        if (isOnOff)
        {
            //활성화
            mobileBranch_button.transform.GetChild(0).GetComponent<Image>().sprite = mBranchImage[0];
            mobileBranch_button.transform.GetChild(1).GetComponent<Text>().color = Color.white;
        }
        else
        {
            mobileBranch_button.transform.GetChild(0).GetComponent<Image>().sprite = mBranchImage[1];
            mobileBranch_button.transform.GetChild(1).GetComponent<Text>().color = Color.gray;
            //비활성화
        }
    }

}
