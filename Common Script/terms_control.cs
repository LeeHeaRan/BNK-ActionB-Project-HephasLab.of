using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terms_control : MonoBehaviour
{

    public GameObject[] TermsPrefabs;
    GameObject Terms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TermsShow(int index)
    {
        if (Terms == null)
        {
            Terms = Instantiate(TermsPrefabs[index],gameObject.transform); //오브젝트 생성.
            Terms.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            Terms.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        }



    }
    public void TermsClose()
    {
        Debug.Log("TermsClose");
        if (Terms != null)
        {
            Destroy(Terms);
        }
    }

}
