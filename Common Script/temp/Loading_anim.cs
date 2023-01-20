using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading_anim : MonoBehaviour
{
    public float time;

    public float delay;
    bool isPlay = false;
    Hashtable ht = new Hashtable();
    // Start is called before the first frame update
    void Start()
    {



    }
    private void Update()
    {
        if (!isPlay)
        {
            Vector2 pos = gameObject.GetComponent<RectTransform>().anchoredPosition;
            Vector2 topos = pos;
            topos.y += 10;
            
            ht.Add("from", pos);
            ht.Add("to", topos);
            ht.Add("easeType", iTween.EaseType.linear);
            ht.Add("loopType", "pingpong");
          
            ht.Add("time", time);
            ht.Add("onupdatetarget", gameObject);
            ht.Add("onupdate", "setvalue");

            Invoke("play",delay);
            isPlay = true;
          
        }

    }
    public void play()
    {
        iTween.ValueTo(gameObject, ht);
    }

    public void setvalue(Vector2 pos)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
    }
    private void OnDisable()
    {
        iTween.Pause(gameObject);
    }
    private void OnEnable()
    {
        iTween.Resume(gameObject);
    }



}
