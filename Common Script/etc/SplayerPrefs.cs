
using System;
using UnityEngine;

static class SplayerPrefs
{
    
   static public void PlayerPrefsSave(string key, string value)
    {
      
        PlayerPrefs.SetString(key, value);
        ALLKey(key);
        PlayerPrefs.Save();
    }
    static public void PlayerPrefsSave(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        ALLKey(key);
        PlayerPrefs.Save();
    }
    static public void PlayerPrefsSave(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        ALLKey(key);
        PlayerPrefs.Save();
    }


    static public bool isPlayerPrefs(string key)
    {
        return PlayerPrefs.HasKey(key);
    }



    static public string GetPlayerPrefs_string(string key)
    {
        return (isPlayerPrefs(key))?PlayerPrefs.GetString(key):null;
    }
    static public Nullable<int> GetPlayerPrefs_int(string key)
    {
        return (isPlayerPrefs(key)) ? PlayerPrefs.GetInt(key) : (int?)null;
    }
    static public bool GetPlayerPrefs_bool(string key)
    {
      
        return (isPlayerPrefs(key)) ? Convert.ToBoolean(PlayerPrefs.GetInt(key)) : false;
    }
    static public Nullable<float> GetPlayerPrefs_float(string key)
    {
        return (isPlayerPrefs(key)) ? PlayerPrefs.GetFloat(key) : (float?)null;
    }

    static public void Del_PlayerPrefs(string key)
    {
        if (isPlayerPrefs(key))
        {
            PlayerPrefs.DeleteKey(key);

            string allkey = PlayerPrefs.GetString("ALLKEY", "");
            int index = allkey.IndexOf(key, 0, allkey.Length);
            allkey = allkey.Replace((index!=0)?",":""+key, "");
            PlayerPrefsSave("ALLKEY", allkey);

            PlayerPrefs.Save();
        }
    }
    static public void MemberDelete()
    {
        PlayerPrefs.DeleteKey("AutoLogin");
        PlayerPrefs.DeleteKey("MemberHash");
        PlayerPrefs.DeleteKey("CI_VAL");
        PlayerPrefs.DeleteKey("ARA_MBRS_NATV_MGNO");
        PlayerPrefs.Save();
    }

    static public void ALLDelete()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    static void ALLKey(string value)
    {
      // string allkey = PlayerPrefs.GetString("ALLKEY","");

      // PlayerPrefsSave("ALLKEY", allkey + "," + value);

    }
}
