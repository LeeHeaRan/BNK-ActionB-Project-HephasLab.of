using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBox_ReSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        Debug.Log(gameObject.GetComponent<RectTransform>().rect);
        float Select_width = gameObject.GetComponent<RectTransform>().rect.width;
        if(Select_width < 200)
        {
            Vector2 SelectAnch = gameObject.GetComponent<RectTransform>().offsetMin;
            Vector2 SelectSize = gameObject.GetComponent<RectTransform>().sizeDelta;

            float value = Select_width - 200f;
            gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(value, SelectAnch.y);

        }
    }
    private void OnDisable()
    {
        gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0, gameObject.GetComponent<RectTransform>().offsetMin.y);
    }
}
