using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWeb : MonoBehaviour
{

    public enum SEND_URL
    {
        Busan=0,
        COCOA=1
    }

    // string baseUrl = "https;//";
    public string[] baseUrl;
    public GameObject ScreenLoading;
    List<Dictionary<string, string>> ResultData = new List<Dictionary<string, string>>();


    

    // Start is called before the first frame update
    void Start()
    {
     
     
    }
    public bool WebSend(SEND_URL send_url_Type, string SendUrl, string[] key, string[] value, Action<string, List<Dictionary<string, string>>> callback)
    {
        return false;
    }
    public bool WebSend(TB_TYPE type, SEND_URL send_url_Type, string SendUrl, string[] key, string[] value, Action<string, List<Dictionary<string, string>>> callback)
    {
        return false;
    }
    public bool WebSend(TB_TYPE type, SEND_URL send_url_Type, string SendUrl, string mnu, List<string> key, List<string> value, Action<string, List<Dictionary<string, string>>> callback)
    {
        return false;
    }
        /// <summary>
        ///  key,value는 쌍으로 작성한다.
        ///  배열길이가 매칭되지 않을 경우 false 리턴 
        /// </summary>
        /// <param name="SendUrl">baseUrl+SendUrl -- /url/target 형태로 작성 '/' 첫 슬러시 포함 작성</param>
        /// <param name="key">전달 키값</param>
        /// <param name="value">전달 키와 쌍으로 전달할 값</param>
        /// <param name="callback">통신후 전달받을 함수</param>
        /// <returns></returns>
  
    public bool WebSend(TB_TYPE type, SEND_URL send_url_Type, string SendUrl,string mnu, List<string> key, List<string> value, Action<string, string, List<Dictionary<string, string>>> callback)
    {


        if (ScreenLoading != null) ScreenLoading.SetActive(true);
        key.Insert(0,"ib20_cur_mnu");
        value.Insert(0,mnu);
        if (key.Count != value.Count)
        {
            
            return false;
        }
        else
        {
            StartCoroutine(eWebSend(type, send_url_Type, SendUrl, key.ToArray(), value.ToArray(), callback));

            return true;
        }
    }
    public bool WebSend(TB_TYPE type, SEND_URL send_url_Type, string SendUrl, string mnu, List<string> key, List<string> value, List<string> data_key, List<byte[]> data_value, Action<string, string, List<Dictionary<string, string>>> callback)
    {


        if (ScreenLoading != null) ScreenLoading.SetActive(true);
        key.Insert(0, "ib20_cur_mnu");
        value.Insert(0, mnu);
        if (key.Count != value.Count)
        {

            return false;
        }
        else
        {
            StartCoroutine(eWebSend(type, send_url_Type, SendUrl, key.ToArray(), value.ToArray(),data_key.ToArray(),data_value.ToArray(), callback));

            return true;
        }
    }
    public bool WebGetTexture(SEND_URL send_url_Type, List<string> url, Action<List<Texture>> callback)
    {
        StartCoroutine(GetTexture(send_url_Type, url, callback));
        return true;
    }




    IEnumerator eWebSend(TB_TYPE type, SEND_URL send_url_Type,string SendUrl,string[] key, string[] value,Action<string, string,List<Dictionary<string,string>>> callback)
    {
        WWWForm form = new WWWForm();
        string testUrl = baseUrl[(int)send_url_Type] + SendUrl;
        for (int i =0; i<key.Length; i++)
        {
            form.AddField(key[i], value[i],System.Text.Encoding.UTF8);
            testUrl += ((i == 0) ? "?" : "&") + key[i] + "=" + value[i];
        }

        UnityWebRequest www = UnityWebRequest.Post(baseUrl[(int)send_url_Type] + SendUrl, form);
        Debug.Log(testUrl);
        Dictionary<string, string> Temp = new Dictionary<string, string>();
        yield return www.SendWebRequest();

        string requestText = UnityWebRequest.UnEscapeURL(www.downloadHandler.text, System.Text.Encoding.UTF8);

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            gameObject.GetComponent<Alert_Manager>().ShowAlert("네트워크상태를 체크해주세요");
            Temp.Add("fail", requestText);
            ResultData.Add(Temp);
            if (ScreenLoading != null) ScreenLoading.SetActive(false);
            callback("NetWork error","99", ResultData);

        }else if(www.result == UnityWebRequest.Result.ProtocolError)
        {
            
            gameObject.GetComponent<Alert_Manager>().ShowAlert("[CODE:"+ SendUrl.Substring(5)+"] 관리자에게 문의해주세요!");
            Temp.Add("fail", requestText);
            ResultData.Add(Temp);
            if (ScreenLoading != null) ScreenLoading.SetActive(false);
            callback("HTTP ProtocolError", "99", ResultData);

        }else if(www.result == UnityWebRequest.Result.Success)
        {
           
            JsonField jsonField = new JsonField(type, requestText);
            ResultData = jsonField.GetDictionary();
            if (ScreenLoading != null) ScreenLoading.SetActive(false);
            callback("Sucess", jsonField.GetResCode(), ResultData);

            jsonField = null;
        }
        else
        {
            Temp.Add("fail", requestText);
            ResultData.Add(Temp);
            if (ScreenLoading != null) ScreenLoading.SetActive(false);
            callback("unknown", "99", ResultData);
        }
        www.Dispose();
        ResultData = new List<Dictionary<string, string>>();

    }

    IEnumerator eWebSend(TB_TYPE type, SEND_URL send_url_Type, string SendUrl, string[] key, string[] value,string[] data_key,byte[][] data_byte, Action<string, string, List<Dictionary<string, string>>> callback)
    {
        WWWForm form = new WWWForm();
        string testUrl = baseUrl[(int)send_url_Type] + SendUrl;
        for (int i = 0; i < key.Length; i++)
        {
            form.AddField(key[i], value[i], System.Text.Encoding.UTF8);
            testUrl += ((i == 0) ? "?" : "&") + key[i] + "=" + value[i];
        }
        for (int i = 0; i < data_key.Length; i++)
        {
            form.AddField(data_key[i], System.Convert.ToBase64String(data_byte[i]));
            testUrl += ((i == 0) ? "?" : "&") + data_key[i] + "=" + System.Convert.ToBase64String(data_byte[i]);
        }
        UnityWebRequest www = UnityWebRequest.Post(baseUrl[(int)send_url_Type] + SendUrl, form);
        Debug.Log(testUrl);
        Dictionary<string, string> Temp = new Dictionary<string, string>();
        yield return www.SendWebRequest();

        string requestText = UnityWebRequest.UnEscapeURL(www.downloadHandler.text, System.Text.Encoding.UTF8);

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            gameObject.GetComponent<Alert_Manager>().ShowAlert("네트워크상태를 체크해주세요");
            Temp.Add("fail", requestText);
            ResultData.Add(Temp);
            if (ScreenLoading != null) ScreenLoading.SetActive(false);
            callback("NetWork error", "99", ResultData);

        }
        else if (www.result == UnityWebRequest.Result.ProtocolError)
        {

            gameObject.GetComponent<Alert_Manager>().ShowAlert("[CODE:" + SendUrl.Substring(5) + "] 관리자에게 문의해주세요!");
            Temp.Add("fail", requestText);
            ResultData.Add(Temp);
            if (ScreenLoading != null) ScreenLoading.SetActive(false);
            callback("HTTP ProtocolError", "99", ResultData);

        }
        else if (www.result == UnityWebRequest.Result.Success)
        {

            JsonField jsonField = new JsonField(type, requestText);
            ResultData = jsonField.GetDictionary();
            if (ScreenLoading != null) ScreenLoading.SetActive(false);
            callback("Sucess", jsonField.GetResCode(), ResultData);

            jsonField = null;
        }
        else
        {
            Temp.Add("fail", requestText);
            ResultData.Add(Temp);
            if (ScreenLoading != null) ScreenLoading.SetActive(false);
            callback("unknown", "99", ResultData);
        }
        www.Dispose();
        ResultData = new List<Dictionary<string, string>>();

    }



    IEnumerator GetTexture(SEND_URL send_url_Type, List<string> url, Action<List<Texture>> callback)
    {
        List<Texture> textures = new List<Texture>();
        for (int i = 0; i < url.Count; i++)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(baseUrl[(int)send_url_Type] + url[i]);

            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {

                
                textures.Add(((DownloadHandlerTexture)www.downloadHandler).texture);

               
            }
        }
        callback(textures);
    }
}
