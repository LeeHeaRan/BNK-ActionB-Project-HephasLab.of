using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnityExample;
using System.Xml;
using OpenCVForUnity.ArucoModule;
using System.Linq;
using System.IO;




/// <summary>
//Google의 OCR API와 통신한다.
//전달받은 데이터를 가공(영역제한)하여 명함의 이름, 직급, 부서를 추출한다.
//GetFindNameAndRank()에서 이름과 직급을 찾는다. 그외의 값은 부서 값으로 저장된다.
/// </summary>
public class OCRControll_02 : MonoBehaviour
{

    [Header("UIControll")]
    public UIControll uiControll;
    public BackControll backControll;

    public string url = "https://vision.googleapis.com/v1/images:annotate?key="; //google api url
    public string apiKey = "AIzaSyDT9Ic1xltpStKLuaBY9OxDnGRhy93oEz4"; //google cloud vision api key
    public int requestedWidth = 640; //뽑히는 이미지의 크기 지정.
    public int requestedHeight = 480;
    public int maxResults = 10;

    //device
    public Texture2D textureOCR; //OCRControll01.cs에서 전달받은 텍스쳐.
    public bool isSnap = false; //캡쳐버튼을 눌렀을때 false -> true로 전환.
    public bool isGetTexture = false;
    public bool isGetTextureFine = false;

    public Vector2 startPoint_, endPoint_;

    //UI에 들어갈 정보들.
    List<textAnnotationsClass> infoList = new List<textAnnotationsClass>();//찾은 정보들.

    public string infoName, infoDept, infoRank; //UIControll_cs로 전달되는 값. 
    //OCR인식이 이름, 직급, 부서를 찾았는지 확인하는 값. -> info 모달을 띄워준다.
    public bool isSuccessDB = false; //DB에 값이 정상적으로 있을경우. ->  AR로 넘어간다.

    public IEnumerator coroutine;

    //public TestviewLog text;

    //구글 클라우드에 설정될 값들 클래스.
    [System.Serializable]
    public class AnnotateImageRequests
    {
        public List<AnnotateImageRequest> requests;
    }

    [System.Serializable]
    public class AnnotateImageRequest
    {
        public Image image;
        public List<Feature> features;
    }

    [System.Serializable]
    public class Image
    {
        public string content;
    }

    [System.Serializable]
    public class Feature
    {
        public string type;
        public int maxResults;
    }

    void Update()
    {
        //isGetture 이미지 넣음이 실행되고. 
        if (isSnap && !isGetTexture)
        {
            Capture(); //isSnap = false 넣어줌
        }
    }

    struct webData //웹캠에서 받은 데이터
    {
        public byte[] bytes;
        public Texture2D texture;
        public int width;
        public int height;
    }


    //네트워크 에러 메시지를 지워주는 함수.
    void removeNetworkError()
    {
        uiControll.Snap_Btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
    }


    public string GenerateId()
    {
        long i = 1;
        foreach (byte b in Guid.NewGuid().ToByteArray())
        {
            i *= ((int)b + 1);
        }
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }

    //isgettexture true, issnap true

    /// <summary>
    /// 구글 API에 이미지를 보내고 ocr인식된 값을 받는다.
    /// </summary>
    /// <returns></returns>
    private bool Capture()
    {
        //사진을 한장만 넘기고 중지.
        isSnap = false;

        //api가 비어있으면 반환
        if (this.apiKey == null)
        {
            uiControll.Snap_Btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
            return false;
        }

        byte[] jpg = textureOCR.EncodeToJPG(); //텍스쳐를 받아온다.
        string base64 = System.Convert.ToBase64String(jpg);

        //CloudVisionAPI
        AnnotateImageRequests requests = new AnnotateImageRequests();
        requests.requests = new List<AnnotateImageRequest>();

        AnnotateImageRequest request = new AnnotateImageRequest();
        request.image = new Image();
        request.image.content = base64;
        request.features = new List<Feature>();

        Feature feature = new Feature();
        feature.type = "TEXT_DETECTION";
        feature.maxResults = this.maxResults;
        request.features.Add(feature);
        requests.requests.Add(request);

        string jsonData = JsonUtility.ToJson(requests, false); //이미지 정보가 포함된 정보.

        isSuccessDB = false;
        infoList.Clear();
        infoName = "";
        infoDept = "";
        infoRank = "";

        //보내는 이미지가 있을때 수행.
        //Debug.Log("짜란.");
        if (jsonData != string.Empty)
        {
            //Debug.Log("이미지 있음.");

            string url = this.url + this.apiKey; //GET방식일때 씀. url + apikey
            byte[] postData = System.Text.Encoding.Default.GetBytes(jsonData); //이미지 데이터를 byte[]로.

            //실행할때 마다 초기화하고 ocr을 한다.

            //코루틴 전 네트워크 확인
            if (Application.internetReachability == NetworkReachability.NotReachable) //네트워크 연결이 안되어 있을경우.
            {
                Invoke("removeNetworkError", 2f); //2초후에 UI제거, 카메라 버튼을 활성화 한다.
                //수동으로 정보를 입력할 수 있도록 한다.
                uiControll.UI_ocr_networkCheck.SetActive(true);
                backControll.changeStep(uiControll.UI_ocr_networkCheck, BackControll.ARCARD_STEP.OCRModal);

            }
            else //네트워크 연결이 되어있을경우.
            {

                uiControll.OCRdetecting_Text.SetActive(true); //"OCR처리중입니다.." 텍스트가이드 활성화 
                StartCoroutine(ProcessRequest(url, postData, uiControll.click_ocr_camera_)); //Post 구글에 데이터를 보냄 OCR부분.
                coroutine = ProcessRequest(url, postData, uiControll.click_ocr_camera_);
            }
        }


        jpg = null;
        base64 = null;
        //Debug.Log("초기화");

        return true;
    }

    
    /// <summary>
    /// BNK클래스들. API로 받은 Json을 받기위해 필요.
    /// </summary>
    [System.Serializable]
    public class TotalResult
    {
        public resultDataClass[] result;
    }

    [System.Serializable]
    public class resultDataClass
    {
        public textAnnotationsClass[] textAnnotations;
    }

    [System.Serializable]
    public class textAnnotationsClass
    {
        public string description;
        public boundingPolyClass boundingPoly;
    }

    [System.Serializable]
    public class boundingPolyClass
    {
        public verticesClass[] vertices;
    }

    [System.Serializable]
    public class verticesClass
    {
        public string x;
        public string y;
    }

    /// <summary>
    /// Google API로 전달받은 값을 통해 이름, 직급, 부서를 추출한다.
    /// <para>추축과정: API로 받은 값에서 BNK문구가 있는지 확인하여 BNK명함인지 판단한다. 
    /// 명함의 이름, 직급, 부서가 있는 영역안에 있는 값들만 추출하고 해당 영역안에 인식된 값들은 렉트값(4개의 포인트)을 가진다. </para>
    /// <para>(GetFindNameAndRank()함수)각 렉트중 세로의 길이가 가장 큰것이 이름이고 구분자 | 를 이용하여 부서와 직급을 추출한다.</para>
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    private IEnumerator ProcessRequest(String url, byte[] data, Action<bool> callback)
    {

        //using (UnityWebRequest request1 = UnityWebRequest.Post(url, datastr)) //주소만 => 데이터 안들어감.
        using (UnityWebRequest request1 = UnityWebRequest.Get(url)) //주소만, 이미지 데이터
        {
            request1.method = "POST";
            request1.SetRequestHeader("Content-Type", "application/json; charset=UTF-8"); //헤더를 넣어준다.
            request1.SetRequestHeader("key", "AIzaSyDT9Ic1xltpStKLuaBY9OxDnGRhy93oEz4");
            request1.uploadHandler = new UploadHandlerRaw(data); //데이터를 보내줌

            yield return request1.SendWebRequest(); //답장이 올때까지 기다림.

            if (request1.result == UnityWebRequest.Result.ConnectionError) //서버와 통신하지 못함.
            {
                //text.SetLog("서버와 통신하지 못함.");
                callback(false);
                yield break;
            }
            else if (request1.result == UnityWebRequest.Result.ProtocolError) //서버에서 오류 응답을 반환. 
            {
                //text.SetLog("서버에서 오류 응답을 받음.");
                callback(false);
                yield break;

            }
            else if (request1.result == UnityWebRequest.Result.Success) //성공적으로 응답을 받음.
            {

                string str = request1.downloadHandler.text;
                int count = 0; //bnk갯수
                int tempY = 0; //처음 인식되는 bnk값 저장. 다른 bnk와 비교하기 위해 사용.
                TotalResult Object_;

                Debug.Log("전체 인식된 값: " + str);
                if (!str.Contains("textAnnotations")) //데이터가 넘어오긴 했지만 인식된 텍스트가 없을때. 
                {
                    callback(false);
                    yield break; //코틀린 끝
                }
                else //데이터 값이 있을때.,
                {
                    string textAnnotations = str.Substring(str.IndexOf("\"textAnnotations"), str.LastIndexOf("fullTextAnnotation") - 40);
                    textAnnotations = "[{\n" + textAnnotations + "\n}]"; //형식맞추기

                    //중간에 필요없는 부분을 찾아냄
                    int startCutIndex = textAnnotations.IndexOf("\"locale") - 12; //textAnnotations에서 자를 시작인덱스를 찾는다. -20은 {까지 포함하기 위함.
                    string delStr = textAnnotations.Substring(startCutIndex); //앞을 자른다.
                    int endCutIndex = delStr.IndexOf("\"description\"", 50) - 12; //startCutIndex에서 부터 끝 인덱스를 찾는다. -10은 }까지만 포함하기 위함.

                    delStr = delStr.Substring(0, endCutIndex); //제거할 문자열을 얻는다.

                    //최종적으로 사용할 데이터 String만을 클래스로 만든다.
                    string resultRequest = textAnnotations.Replace(delStr, "");
                    Object_ = JsonUtility.FromJson<TotalResult>("{\"result\":" + resultRequest + "}"); //클래스에 넣는다.

                    //1차로 BNK를 찾는다. BNK의 인덱스를 얻는다.
                    try
                    {
                        //시작지점을 고름. 모든 BNK를 찾는다.
                        foreach (var word in Object_.result[0].textAnnotations)
                        {
                            if (word.description.Contains("BNK")) //전체 인식된 문자열에서 bnk가 포함되어있는게 있다면. 0620
                            {
                                if (count == 0)
                                {
                                    tempY = int.Parse(word.boundingPoly.vertices[3].y); //처음 인식된bnk의 y값.

                                    startPoint_.x = int.Parse(word.boundingPoly.vertices[3].x); //height 
                                    startPoint_.y = int.Parse(word.boundingPoly.vertices[3].y) - 20;
                                    count++;
                                }
                                else //만약 2번째 인식됨. 
                                {
                                    if (int.Parse(word.boundingPoly.vertices[3].y) < tempY) //만약 더 왼쪽에 있는 bnk를 찾았다.
                                    {
                                        startPoint_.x = int.Parse(word.boundingPoly.vertices[3].x);
                                        startPoint_.y = int.Parse(word.boundingPoly.vertices[3].y) - 20;
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        //text.SetLog(e.ToString(), false);

                    }

                    if (count == 0) //BNK 문구가 인식되지 않았을때 
                    {
                        //text.SetLog("BNK명함을 찾을 수 없습니다.", false);

                        callback(false);
                        yield break;
                    }
                    else //BNK문구가 인식되었을때. count가 0이상.
                    {

                        //끝나는 지점을 찾는다.
                        foreach (var word in Object_.result[0].textAnnotations)
                        {
                            if (word.description.Equals("문현동") || word.description.Equals("본점"))
                            {
                                endPoint_.x = int.Parse(word.boundingPoly.vertices[1].x);
                                endPoint_.y = int.Parse(word.boundingPoly.vertices[1].y);
                            }
                        }

                        //2차 다시 탐색한다. 지정된 영역에서 부서, 직급, 이름만 인식한다. 3~4개
                        try
                        {
                            foreach (var word_ in Object_.result[0].textAnnotations)
                            {
                                if (int.Parse(word_.boundingPoly.vertices[0].x) <= startPoint_.x && int.Parse(word_.boundingPoly.vertices[0].y) >= startPoint_.y && int.Parse(word_.boundingPoly.vertices[2].x) >= endPoint_.x && int.Parse(word_.boundingPoly.vertices[2].y) <= endPoint_.y)
                                {
                                    try
                                    {
                                        infoList.Add(word_); //인식된 값들
                                        //text.SetLog("인식단어:" + word_.description + "\n");
                                        Debug.Log("영역에 인식된 값: " + word_.description);
                                        Debug.Log("영역에 인식된 값(ASKI): " + Convert.ToInt32(word_.description));


                                    }
                                    catch (Exception e)
                                    {
                                        //text.SetLog("부서, 직급, 이름을 인식하는데 오류." + e.ToString()); //없음. 값 안나옴.
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            //text.SetLog("!!!!" + e.ToString() + "!!!!");

                        }


                        if (infoList.Count < 2)  //infolist.Count 0,1,2 
                        {

                            //text.SetLog("3개(이름, 직급, 부서)의 값이 입력되지 못하였습니다. 다시 찍어주세요", false);

                            callback(false);
                            yield break;
                        }
                        else if (infoList.Count >= 2 && infoList.Count <= 10) //3~6개
                        {

                            if (GetFindNameAndDept(infoList, "FindName") != -1) //이름을 찾았으면 인덱스 값이 못찾았으면 -1이 반환된다.
                            {
                                int findDeptResult = GetFindNameAndDept(infoList, "FindDept"); //직급을 찾았으면 인덱스 값이 못찾았으면 -1이 반환된다.

                                if (findDeptResult != -1) 
                                {
                                    foreach (var word_ in infoList)
                                    {
                                        infoRank += word_.description;
                                    }

                                    if (infoRank.Length > 0)// 빈값이 아니면
                                    {
                                        callback(true); //직급까지 정상적으로 처리되면 보여준다. 통신하는데 시간 걸림.

                                        Debug.Log("INFO Rank:" + infoRank);

                                        yield break;
                                    }
                                    else
                                    {
                                        //text.SetLog(" 인식되지 않았습니다.");

                                        callback(false);
                                        yield break;
                                    }
                                }
                                else //값이 -1일 경우.
                                {
                                    //text.SetLog("이름을 제외한 인식된 값에서 구분자가 인식되지 않았습니다.. 다시 인식해주세요");
                                    callback(false);
                                    yield break;
                                }

                            }
                            else
                            {
                                //명함_OCR_명함 정보를 못 얻음.
                                callback(false);
                                //text.SetLog("인식된 이름이 1글자 입니다. 다시 인식해주세요.");

                                yield break;
                            }
                        }
                        else
                        {
                            callback(false);
                            // text.SetLog("StopCoroutine");

                            yield break;
                            //지정된 범위에서 글자렉트를 찾았는지. 
                        }
                    } //BNK문자를 찾았는지 확인.
                }//통신으로 값은 넘어왔지만 인식된 값이 있는지 확인.
            }
            else //서버와 성공적인 통신도 오류도 발생하지 않았을때,.
            {
                //isGetTexture = false;
                callback(false);
                yield break;
            }
            request1.Dispose();
            callback(false);
            yield break;
        }
    }


    /// <summary>
    /// 매개변수에 따라 이름 혹은 직급을 추출한다
    /// </summary>
    /// <param name="list_"></param>
    /// <param name="get"></param>
    /// <returns></returns>
    int GetFindNameAndDept(List<textAnnotationsClass> list_, string get)
    {
        if (get.Equals("FindName"))
        {
            List<int> rectHeight = new List<int>(); //인식된 렉트의 세로길이를 담을 리스트.

            for (int i = 0; i < list_.Count; i++)
            {
                rectHeight.Add(int.Parse(list_[i].boundingPoly.vertices[0].x) - int.Parse(list_[0].boundingPoly.vertices[3].x));
            }

            int maxindex = rectHeight.IndexOf(rectHeight.Max()); //세로의 최대 값의 인덱스를 찾는다.
            rectHeight.Clear();

            if (list_[maxindex].description.Length > 1) //오류처리. 이름이 1개로 인식됬을 때.
            {
                infoName = list_[maxindex].description; //UI에 들어갈 내용. //이름이 있는 인덱스를 가지고 infoList에서 값을 찾음. 이름정보가 들어가 있다.

                infoList.RemoveAt(maxindex); //원본 리스트에서 값을 지운다.
                return maxindex; //이름이 있는 인덱스를 반환
            }
            else
            {
                return -1;
            }

        }
        else if (get.Equals("FindDept")) //****부서를 찾는다. 
        {
            int separatorIndex = -1;
            //이름을 제외한 인식된 글자에서 구분자가 포함되어있는지 확인한다.
            //(부서) (구분자) (직급) 형태로 남는다.
            for (int i = 0; i < list_.Count; i++)
            {
                if (list_[i].description.Contains("l") || list_[i].description.Contains("1") || list_[i].description.Contains("|") ||
                     list_[i].description.Contains("]") || list_[i].description.Contains("[") || list_[i].description.Contains("/") ||
                     list_[i].description.Contains("|") || list_[i].description.Contains("!") || list_[i].description.Contains(":") ||
                     list_[i].description.Contains("(") || list_[i].description.Contains(")") || list_[i].description.Contains(";") ||
                     list_[i].description.Contains("/"))
                {
                    separatorIndex = i;
                    //구분자가 있는 인덱스 번호를 저장.
                    Debug.Log("구분자:" + list_[i].description + "index: " + separatorIndex);
                    break;
                }
                else
                {
                    separatorIndex = -1;
                }
            }
            string aa = "";
            if (separatorIndex != -1)
            {
                for (int i = 0; i < separatorIndex; i++)
                {
                    Debug.Log("i=" + i);
                    Debug.Log("description=" + list_[i].description);

                    infoDept += list_[i].description;
                }

                Debug.Log("INFO Dept:" + infoDept);
                Debug.Log("separatorIndex:" + separatorIndex);

                for (int i = 0; i <= separatorIndex; i++)  //구분자 까지 지운다.
                {
                    infoList.RemoveAt(0);
                }

                string bb = "";
                for (int i = 0; i < infoList.Count; i++)
                {
                    bb += infoList[i].description;

                    Debug.Log("#i=" + i);
                    Debug.Log("#description=" + list_[i].description);
                }
                Debug.Log("INFO Rank:" + bb);

                return separatorIndex;
            }
            else
            {
                return separatorIndex;
            }
        }
        else
        {
            //  text.SetLog(get + " : 잘못된 매개변수입니다. GetMat(List<textAnnotationsClass> list, string get).");
            //잘못된 매개변수입니다.
            return -1;
        }
    }

    string SnapshotName(webData wb1)
    {
        return string.Format("{0}/Contents/ARCard/Develop/Snapshots/snap_{1}x{2}_{3}.jpg",
            Application.dataPath,
            wb1.width,
            wb1.height,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    //OCR카메라 촬영버튼 클릭.
    public void SnapshotOCR()
    {
        //한번 실행.
        isGetTexture = true;
        uiControll.Snap_Btn.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        isSnap = true;
    }

}
