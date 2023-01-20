using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueToUpdate : MonoBehaviour
{
    public enum ComponentType
    {
        none,
        Text,
        Image,
       
    }
    public enum MoveType
    {
        none,
        Move,

    }
    public ComponentType componentType;
    public MoveType moveType;
    private Color m_StartColor;
    public Color StartColor
    {
        get => m_StartColor;
        set => m_StartColor = value;
    }

    private Vector3 m_StartPos;
    public Vector3 StartPos
    {
        get => m_StartPos;
        set => m_StartPos = value;
    }

    private Vector3 m_NowPos;
    public Vector3 NowPos
    {
        get => m_NowPos;
        set => m_NowPos = value; 
    }

    private Color m_NowColor;
    public Color NowColor
    {
        get => m_NowColor;
        set => m_NowColor = value;
    }

    private void OnEnable()
    {
      
            switch (componentType)
            {
                case ComponentType.Text:

                    StartColor = gameObject.GetComponent<Text>().color;

                    break;

                case ComponentType.Image:

                    StartColor = gameObject.GetComponent<Image>().color;

                    break;

          
            }

        if(moveType == MoveType.Move)
        {
             
           StartPos = gameObject.GetComponent<RectTransform>().anchoredPosition3D;

        }
        
    }

    public void SetColor(Color color)
    {
        switch (componentType)
        {
            case ComponentType.Text:

                gameObject.GetComponent<Text>().color = color;
                NowColor = color;
                break;

            case ComponentType.Image:

                gameObject.GetComponent<Image>().color = color;
                NowColor = color;
                break;
        }
       

    }
    public void SetPos(Vector3 pos)
    {
        if (moveType == MoveType.Move)
        {
   
                gameObject.GetComponent<RectTransform>().anchoredPosition3D = pos;
                NowPos = pos;

        }


    }
    private void OnDisable()
    {
        switch (componentType)
        {
            case ComponentType.Text:

                gameObject.GetComponent<Text>().color = StartColor;
               
                break;

            case ComponentType.Image:

                gameObject.GetComponent<Image>().color = StartColor;

                break;
          
        }
        if (moveType == MoveType.Move)
        {

            gameObject.GetComponent<RectTransform>().anchoredPosition3D = StartPos;


        }
    }
}
