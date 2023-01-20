using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class _Member_info
{
    public enum KEY_NAME
    {
        ARA_MBRS_NM,  // E
        ARA_MBRS_RRNO, // E 주민등록번호 
        ARA_MBRS_MPNO, // E 휴대폰번호
        MMT_TCCCO_DVCD, // E 통신사
        ARA_MBRS_EMLADR, // E 이메일 
        ARA_MBRS_PSWD,   // E
        KCB_MPNO,
        Agree_1,
        Agree_2,
        Agree_3,
        ARA_MBRS_CI_VAL,  // E
        ARA_MBRS_SEX_DVCD, // E
        ARA_SV_U_LEME_YN, // E
        ARA_EVNT_AND_AD_LEME_YN, // E
        ARA_MBRS_HASH_VAL,
        ARA_SNS_NATV_NO, // E
        ARA_LOGIN_DVCD, // E 로그인 구분코드
        ARA_SNS_AITM_TOKN_VAL, // E DB컬럼 나오면 수정하기
 
    }




    public enum PARENT_KEY_NAME
    {
        LWCT_AGN_NM,  // E
        ARA_MBRS_RRNO, // E 주민등록번호 
        ARA_MBRS_SEX_DVCD,
        MMT_TCCCO_DVCD, // E 통신사
        ARA_MBRS_MPNO, // E 휴대폰번호
        ARA_LWCT_AGN_CI_VAL, // E
   


    }













    public Dictionary<KEY_NAME,string> member_info = new Dictionary<KEY_NAME, string>();
    public Dictionary<PARENT_KEY_NAME, string> Parent_member_info = new Dictionary<PARENT_KEY_NAME, string>();
    public List<string> key;
    public List<string> value;


    public _Member_info()
    {
        key = new List<string>();
        value = new List<string>();
      
        member_info = new Dictionary<KEY_NAME, string>();
        Parent_member_info = new Dictionary<PARENT_KEY_NAME, string>();
        key.Clear();
        value.Clear();
        member_info.Clear(); 
        Parent_member_info.Clear();
    }

    public void init()
    {
        member_info.Clear();

    }
    public void SetValue(KEY_NAME key, string value)
    {
        if (!member_info.ContainsKey(key))
        {
            member_info.Add(key, value);
        }
        else
        {
            member_info[key] = value;
        }
        OnAfter();
    }
    public void SetValue(PARENT_KEY_NAME key, string value)
    {
        if (!Parent_member_info.ContainsKey(key))
        {
            Parent_member_info.Add(key, value);
        }
        else
        {
            Parent_member_info[key] = value;
        }
        Parent_OnAfter();
    }



    public void SetValueAppend(KEY_NAME key, string value)
    {
        if (member_info.ContainsKey(key))
        {
            if (key == KEY_NAME.ARA_MBRS_RRNO)
            {
                if (!member_info[key].Equals("●"))
                    member_info[key] = member_info[key] + value;
            }
            else
            {
                member_info[key] = member_info[key] + value;
            }
        }
        else
        {
            SetValue(key, value);
        }

        OnAfter();
    }
    public void SetValueAppend(PARENT_KEY_NAME key, string value)
    {
        if (Parent_member_info.ContainsKey(key))
        {
            if (key == PARENT_KEY_NAME.ARA_MBRS_RRNO)
            {
                if (!Parent_member_info[key].Equals("●"))
                    Parent_member_info[key] = Parent_member_info[key] + value;
            }
            else
            {
                Parent_member_info[key] = Parent_member_info[key] + value;
            }
        }
        else
        {
            SetValue(key, value);
        }

        OnAfter();
    }











    public void SetValueDisAppend(KEY_NAME key)
    {
        if (member_info.ContainsKey(key))
        {
            
            member_info[key] = member_info[key].Remove(member_info[key].Length - 1);
        }

        OnAfter();


    }
    public void SetValueDisAppend(PARENT_KEY_NAME key)
    {
        if (Parent_member_info.ContainsKey(key))
        {

            Parent_member_info[key] = Parent_member_info[key].Remove(Parent_member_info[key].Length - 1);
        }

        Parent_OnAfter();


    }















    public void SetValue( string send_key, string value)
    {
        
        if (!member_info.ContainsKey((KEY_NAME)Enum.Parse(typeof(KEY_NAME), send_key)))
        {
            member_info.Add((KEY_NAME)Enum.Parse(typeof(KEY_NAME), send_key), value);
        }
        else
        {
            member_info[(KEY_NAME)Enum.Parse(typeof(KEY_NAME), send_key)] = value;
        }
        OnAfter();
    }
    public void SetValue(string a, string send_key, string value)
    {

        if (!Parent_member_info.ContainsKey((PARENT_KEY_NAME)Enum.Parse(typeof(PARENT_KEY_NAME), send_key)))
        {
            Parent_member_info.Add((PARENT_KEY_NAME)Enum.Parse(typeof(PARENT_KEY_NAME), send_key), value);
        }
        else
        {
            Parent_member_info[(PARENT_KEY_NAME)Enum.Parse(typeof(PARENT_KEY_NAME), send_key)] = value;
        }
        Parent_OnAfter();
    }









    public int SetValueLength(KEY_NAME key)
    {
       

        return (member_info.ContainsKey(key)) ?member_info[key].Length: 0;

    }
    public int SetValueLength(PARENT_KEY_NAME key)
    {


        return (Parent_member_info.ContainsKey(key)) ? Parent_member_info[key].Length : 0;

    }









    public void SetModify(KEY_NAME key, string value)
    {
        if (member_info.ContainsKey(key))
        {
            member_info[key] = value;
        }
    }
    public void SetModify(PARENT_KEY_NAME key, string value)
    {
        if (Parent_member_info.ContainsKey(key))
        {
            Parent_member_info[key] = value;
        }
    }









    public string GetValue(KEY_NAME key)
    {

        return member_info[key];
    }
    public string GetValue(PARENT_KEY_NAME key)
    {

        return Parent_member_info[key];
    }









    public bool GetContainsKey(KEY_NAME key)
    {
        return member_info.ContainsKey(key);
    }
    public bool GetContainsKey(PARENT_KEY_NAME key)
    {
        return Parent_member_info.ContainsKey(key);
    }


    public void OnAfter()
    {
        key.Clear();
        value.Clear();
        foreach (KeyValuePair<KEY_NAME, string> item in member_info)
        {

            key.Add(Enum.GetName(typeof(KEY_NAME), item.Key));
            value.Add(item.Value);

           
        }

    }
    public void Parent_OnAfter()
    {
        key.Clear();
        value.Clear();
        foreach (KeyValuePair<PARENT_KEY_NAME, string> item in Parent_member_info)
        {

            key.Add(Enum.GetName(typeof(PARENT_KEY_NAME), item.Key));
            value.Add(item.Value);


        }

    }



    /// <summary>
    /// 
    /// </summary>
    public void DelValue(KEY_NAME key)
    {
        member_info.Remove(key);
        Debug.Log("member info : Del key:"+key);
   
        
    }
    public void DelValue(PARENT_KEY_NAME key)
    {
        Parent_member_info.Remove(key);
        Debug.Log("member info : Del key:" + key);

    }
    /// <summary>
    /// 
    /// </summary>
    /// 


    public void Del_ALLToggle()
    {
       
        member_info.Remove(KEY_NAME.Agree_1);
        member_info.Remove(KEY_NAME.Agree_2);
        member_info.Remove(KEY_NAME.Agree_3);
    }

    public List<string> GetALLKey()
    {
        List<string> temp = new List<string>();
        foreach (KeyValuePair<KEY_NAME, string> pair in member_info)
        {
            
            temp.Add(pair.Key.ToString());
        }

        return temp;
    }
    public List<string> GetParentALLKey()
    {
        List<string> temp = new List<string>();
        foreach (KeyValuePair<PARENT_KEY_NAME, string> pair in Parent_member_info)
        {

            temp.Add(pair.Key.ToString());
        }

        return temp;
    }
    public List<string> GetALLValue()
    {
        List<string> temp = new List<string>();
        foreach (KeyValuePair<KEY_NAME, string> pair in member_info)
        {

            if (pair.Key == KEY_NAME.ARA_MBRS_RRNO) temp.Add(pair.Value.Replace("-",""));
            else temp.Add(pair.Value);
        }
       

        return temp;
    }
    public List<string> GetParentALLValue()
    {
        List<string> temp = new List<string>();
        foreach (KeyValuePair<PARENT_KEY_NAME, string> pair in Parent_member_info)
        {

            if (pair.Key == PARENT_KEY_NAME.ARA_MBRS_RRNO) temp.Add(pair.Value.Replace("-", ""));
            else temp.Add(pair.Value);
        }
       
        return temp;
    }


    public List<string> GetSNS_Key()
    {
        List<string> temp = new List<string>();
        temp.Add("ARA_MBRS_CI_VAL");
        temp.Add("ARA_SNS_AITM_TOKN_VAL");
        temp.Add("ARA_SNS_NATV_NO");
        
        return temp;

    }
    public List<string> GetSNS_value()
    {
        List<string> temp = new List<string>();
        temp.Add(member_info[KEY_NAME.ARA_MBRS_CI_VAL]);
        temp.Add(member_info[KEY_NAME.ARA_SNS_AITM_TOKN_VAL]);
        temp.Add(member_info[KEY_NAME.ARA_SNS_NATV_NO]);

        return temp;

    }
    public void CreateMemberHashValue(int index)
    {
        var Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567899";
        var RandomHash = "";

        for(int i = 0; i< index; i++)
        {
            RandomHash += Characters[UnityEngine.Random.Range(0, Characters.Length-1)];
        }
        SetValue(KEY_NAME.ARA_MBRS_HASH_VAL, RandomHash);

    }
    



    public bool SubmitValueCheck()
    {
        bool isCheck = true;
        foreach (KEY_NAME key_name in Enum.GetValues(typeof(KEY_NAME)))
        {
            if (key_name != KEY_NAME.ARA_LOGIN_DVCD)
            {
                if (key_name != KEY_NAME.ARA_SNS_AITM_TOKN_VAL)
                {
                    if (key_name != KEY_NAME.ARA_SNS_NATV_NO)
                    {
                        if (!member_info.ContainsKey(key_name))
                        {
                            isCheck = false;
                            Debug.Log(key_name.ToString() + " 없음 확인바람");
                        }
                    }
                }
            }
        }

        if (isCheck)
        {
            foreach(KeyValuePair<KEY_NAME,string> pair in member_info)
            {
                if(pair.Value=="" || pair.Value == null)
                {
                    isCheck = false;
                }
            }
        }

        return isCheck;
    }
    public bool Submit_SNS_ValueCheck()
    {
        bool isCheck = true;
        foreach (KEY_NAME key_name in Enum.GetValues(typeof(KEY_NAME)))
        {
            if (key_name != KEY_NAME.ARA_MBRS_EMLADR)
            {
                if (key_name != KEY_NAME.ARA_MBRS_PSWD) {
                    if (!member_info.ContainsKey(key_name))
                    {
                        isCheck = false;
                        Debug.Log(key_name.ToString() + " 없음 확인바람");
                    }
                }
            }
        }

        if (isCheck)
        {
            foreach (KeyValuePair<KEY_NAME, string> pair in member_info)
            {
                if (pair.Value == "" || pair.Value == null)
                {
                    isCheck = false;
                }
            }
        }

        return isCheck;
    }




    /// <summary>
    /// 테스트용
    /// </summary>
    /// <param name="MenuCode"></param>
    /// <returns></returns>
    public void Test_Log()
    {
        string str = "";
        foreach (KeyValuePair<KEY_NAME, string> pair in member_info)
        {
            str += "KEY : " + pair.Key.ToString() + " VALUE : " + pair.Value+"\n";
        }
        Debug.Log(str);
    }



    public Dictionary<string, string> KCB_P_SendData()
    {

        Dictionary<string, string> temp_data = new Dictionary<string, string>();


        temp_data.Add("CMD_SVC_CD", "01");
        temp_data.Add("KCB_CUST_NM", Parent_member_info[PARENT_KEY_NAME.LWCT_AGN_NM]);
        temp_data.Add("KCB_RRNO1", Parent_member_info[PARENT_KEY_NAME.ARA_MBRS_RRNO].Split('-')[0]);
        temp_data.Add("KCB_RRNO3", Parent_member_info[PARENT_KEY_NAME.ARA_MBRS_RRNO].Split('-')[1].Substring(0, 1));

        string carrier = Parent_member_info[PARENT_KEY_NAME.MMT_TCCCO_DVCD];
        if (carrier.Contains("알뜰폰"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "55");
        }
        else if (carrier.Contains("SKT") || carrier.Equals("KT") || carrier.Equals("LG U+"))
        {
            if (carrier.Contains("SKT")) temp_data.Add("KCB_TCCCO_DVCD", "01");
            else if (carrier.Equals("KT")) temp_data.Add("KCB_TCCCO_DVCD", "02");
            else if (carrier.Equals("LG U+")) temp_data.Add("KCB_TCCCO_DVCD", "03");
        }
        else
        {
            temp_data.Add("KCB_TCCCO_DVCD", "55");
        }
        temp_data.Add("KCB_BRCH_NO", "836");
        temp_data.Add("KCB_MPNO", Parent_member_info[PARENT_KEY_NAME.ARA_MBRS_MPNO]);
        temp_data.Add("AGREE_1", "Y");
        temp_data.Add("AGREE_2", "Y");
        temp_data.Add("AGREE_3", "Y");
        temp_data.Add("KCB_PURP_SV_CD", "01");
        temp_data.Add("HSLF_CTF_RQST_RNCD", "00");

        return temp_data;

    }




    public Dictionary<string,string> KCB_SendData()
    {

        Dictionary<string, string> temp_data = new Dictionary<string, string>();

       
        temp_data.Add("CMD_SVC_CD", "01");
        temp_data.Add("KCB_CUST_NM", member_info[KEY_NAME.ARA_MBRS_NM]);
        temp_data.Add("KCB_RRNO1", member_info[KEY_NAME.ARA_MBRS_RRNO].Split('-')[0]);
        temp_data.Add("KCB_RRNO3", member_info[KEY_NAME.ARA_MBRS_RRNO].Split('-')[1].Substring(0,1));
        
        string carrier = member_info[KEY_NAME.MMT_TCCCO_DVCD];
        if (carrier.Contains("알뜰폰"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "55");
        }
        else if (carrier.Contains("SKT") || carrier.Equals("KT") || carrier.Equals("LG U+"))
        {
            if (carrier.Contains("SKT")) temp_data.Add("KCB_TCCCO_DVCD", "01");
            else if (carrier.Equals("KT")) temp_data.Add("KCB_TCCCO_DVCD", "02");
            else if (carrier.Equals("LG U+")) temp_data.Add("KCB_TCCCO_DVCD", "03");
        }
        else
        {
            temp_data.Add("KCB_TCCCO_DVCD", "55");
        }
        temp_data.Add("KCB_BRCH_NO", "836");
        temp_data.Add("KCB_MPNO",member_info[KEY_NAME.ARA_MBRS_MPNO]);
        temp_data.Add("AGREE_1", "Y");
        temp_data.Add("AGREE_2", "Y");
        temp_data.Add("AGREE_3", "Y");
        temp_data.Add("KCB_PURP_SV_CD", "01");
        temp_data.Add("HSLF_CTF_RQST_RNCD", "00");

        return temp_data;
        
    }
    public Dictionary<string, string> KCB_P_Certify_SendNum(string num, string trans_seq)
    {

        Dictionary<string, string> temp_data = new Dictionary<string, string>();


        temp_data.Add("CMD_SVC_CD", "01");
        temp_data.Add("KCB_CUST_NM", Parent_member_info[PARENT_KEY_NAME.LWCT_AGN_NM]);


        string carrier = Parent_member_info[PARENT_KEY_NAME.MMT_TCCCO_DVCD].Trim();
        if (carrier.Equals("SKT"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "01");
        }
        else if (carrier.Equals("KT"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "02");
        }
        else if (carrier.Equals("LG U+"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "03");
        }
        else
        {
            temp_data.Add("KCB_TCCCO_DVCD", "55");
        }
        temp_data.Add("KCB_RQST_NO", num);
        temp_data.Add("TRNS_SEQ", trans_seq);

        temp_data.Add("KCB_MPNO", Parent_member_info[PARENT_KEY_NAME.ARA_MBRS_MPNO]);
        temp_data.Add("KCB_BRCH_NO", "836");
        temp_data.Add("KCB_PURP_SV_CD", "01");
        temp_data.Add("ARA_FLAG", "Y");
        temp_data.Add("HSLF_CTF_RQST_RNCD", "00");

        return temp_data;

    }
    public Dictionary<string, string> KCB_Certify_SendNum(string num,string trans_seq)
    {

        Dictionary<string, string> temp_data = new Dictionary<string, string>();

        
        temp_data.Add("CMD_SVC_CD", "01");
        temp_data.Add("KCB_CUST_NM", member_info[KEY_NAME.ARA_MBRS_NM]);
        

        string carrier = member_info[KEY_NAME.MMT_TCCCO_DVCD].Trim();
        if (carrier.Equals("SKT"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "01");
        }else if (carrier.Equals("KT"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "02");
        }
        else if(carrier.Equals("LG U+"))
        {
            temp_data.Add("KCB_TCCCO_DVCD", "03");
        }
        else
        {
            temp_data.Add("KCB_TCCCO_DVCD", "55");
        }
        temp_data.Add("KCB_RQST_NO", num);
        temp_data.Add("TRNS_SEQ", trans_seq);
        
        temp_data.Add("KCB_MPNO", member_info[KEY_NAME.ARA_MBRS_MPNO]);
        temp_data.Add("KCB_BRCH_NO", "836"); 
        temp_data.Add("KCB_PURP_SV_CD", "01");
        temp_data.Add("ARA_FLAG", "Y");
        temp_data.Add("HSLF_CTF_RQST_RNCD", "00");

        return temp_data;

    }

  
}
