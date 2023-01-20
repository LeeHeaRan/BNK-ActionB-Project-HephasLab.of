using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpace : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        
        gameObject.GetComponent<Text>().text = gameObject.GetComponent<Text>().text.Replace(' ', '\u00A0');
    }
    private void OnEnable()
    {
        gameObject.GetComponent<Text>().text = gameObject.GetComponent<Text>().text.Replace(' ', '\u00A0');
    }


}
