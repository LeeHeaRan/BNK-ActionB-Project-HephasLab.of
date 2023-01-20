using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

public class AssetBunblesManager: MonoBehaviour
{

    public Dictionary<BUNBLE_NAME, AssetBundle> UsingAssetBundle = new Dictionary<BUNBLE_NAME, AssetBundle>();
    int ss = 2;
 
    public enum BUNBLE_NAME
    {
        video,
        guide_prefab,
        common_ui,
        login_ui,
        play_ui,
        common_image,
        paint,
        action_mask,
        arcard,
        chika


    }

    private string assetBundleDirectory="";
    private void Awake()
    {
        if (assetBundleDirectory == "")
        {
            #if UNITY_EDITOR
                        assetBundleDirectory = Application.streamingAssetsPath + "/bundle/";
            #elif UNITY_ANDROID || UNITY_IPHONE
                         assetBundleDirectory = Application.persistentDataPath+"/bundle/";
            #endif
         }
    }
    private void Start()
    {
     
    }

    public void GetVideo(BUNBLE_NAME bunbleName, List<string> objectName,Action<List<VideoClip>> callback)
    {
        if (assetBundleDirectory == "")
        {
            #if UNITY_EDITOR
                        assetBundleDirectory = Application.streamingAssetsPath + "/bundle/";
            #elif UNITY_ANDROID || UNITY_IPHONE
                                assetBundleDirectory = Application.persistentDataPath+"/bundle/";
            #endif
        }
        StartCoroutine(GetVideosAsync(bunbleName, objectName, callback));
    }

    IEnumerator GetVideosAsync(BUNBLE_NAME bunbleName, List<string> objectName,Action<List<VideoClip>> callback)
    {
        
        List<VideoClip> videoClips = new List<VideoClip>();




        AssetBundleCreateRequest asyncBundleRequest = AssetBundle.LoadFromFileAsync(assetBundleDirectory + Enum.GetName(typeof(BUNBLE_NAME), bunbleName));
        yield return asyncBundleRequest;

        AssetBundle tempBunble = asyncBundleRequest.assetBundle;


        for (int i =0; i<objectName.Count; i++)
        {

            videoClips.Add(tempBunble.LoadAsset<VideoClip>(objectName[i]));
        }


        tempBunble.Unload(false);

        callback(videoClips);

    }

public GameObject GetNonCompressResources(BUNBLE_NAME bunbleName, string objectName)
    {
        if (assetBundleDirectory == "")
        {
            #if UNITY_EDITOR
                        assetBundleDirectory = Application.streamingAssetsPath + "/bundle/";
            #elif UNITY_ANDROID || UNITY_IPHONE
                        assetBundleDirectory = Application.persistentDataPath+"/bundle/";
            #endif
        }
        Debug.Log(assetBundleDirectory + Enum.GetName(typeof(BUNBLE_NAME), bunbleName));
        AssetBundle tempBunble = AssetBundle.LoadFromFile(assetBundleDirectory + Enum.GetName(typeof(BUNBLE_NAME), bunbleName));

        if (tempBunble != null)
        {
            GameObject prefab = tempBunble.LoadAsset<GameObject>(objectName);
            tempBunble.Unload(false);
            return prefab;
        }
        else
        {

            return null;
        }


    }
    public GameObject GetResources(BUNBLE_NAME bunbleName,string objectName)
    {
        if (UsingAssetBundle.ContainsKey(bunbleName))
        {
            if (Enum.GetName(typeof(BUNBLE_NAME), bunbleName).Equals(UsingAssetBundle[bunbleName].name))
            {

                GameObject prefab = UsingAssetBundle[bunbleName].LoadAsset<GameObject>(objectName);

                return prefab;
            }
            else
            {
                return null;
            }

        }
        else
        {
            AssetBundle tempBunble = AssetBundle.LoadFromFile(assetBundleDirectory + Enum.GetName(typeof(BUNBLE_NAME), bunbleName));
            UsingAssetBundle.Add(bunbleName,tempBunble);
            GameObject prefab = UsingAssetBundle[bunbleName].LoadAsset<GameObject>(objectName);

            return prefab;

        }
        
        
    }
    public void BundleUnlode(BUNBLE_NAME bunbleName)
    {
        if (UsingAssetBundle.ContainsKey(bunbleName))
        {
            UsingAssetBundle[bunbleName].Unload(false);
            UsingAssetBundle.Remove(bunbleName);
        }
    }
    public void BunblesLoad(BUNBLE_NAME bunbleName,bool async)
    {
        if (UsingAssetBundle.ContainsKey(bunbleName))
        {
            if(!Enum.GetName(typeof(BUNBLE_NAME), bunbleName).Equals(UsingAssetBundle[bunbleName].name)) {

                UsingAssetBundle[bunbleName].Unload(false);
                if (async) {

                    StartCoroutine(BunblesLoadAsync(bunbleName));
                }
                else
                {
                    AssetBundleCreateRequest asyncBundleRequest = AssetBundle.LoadFromFileAsync(assetBundleDirectory + Enum.GetName(typeof(BUNBLE_NAME), bunbleName));
                    AssetBundle tempBunble = asyncBundleRequest.assetBundle;
                    UsingAssetBundle[bunbleName] = tempBunble;
                }
            }
        }
        else
        {
            if (async)
            {

                StartCoroutine(BunblesLoadAsync(bunbleName));
            }
            else
            {
                AssetBundleCreateRequest asyncBundleRequest = AssetBundle.LoadFromFileAsync(assetBundleDirectory + Enum.GetName(typeof(BUNBLE_NAME), bunbleName));
                AssetBundle tempBunble = asyncBundleRequest.assetBundle;
                UsingAssetBundle[bunbleName] = tempBunble;
            }
        }
    }
    IEnumerator BunblesLoadAsync(BUNBLE_NAME bunbleName)
    {
        
        AssetBundleCreateRequest asyncBundleRequest = AssetBundle.LoadFromFileAsync(assetBundleDirectory + Enum.GetName(typeof(BUNBLE_NAME), bunbleName));
        yield return asyncBundleRequest;

        AssetBundle tempBunble = asyncBundleRequest.assetBundle;
        if(tempBunble == null)
        {
            yield break;
        }
        UsingAssetBundle[bunbleName] = tempBunble;
   
    }
   
}
