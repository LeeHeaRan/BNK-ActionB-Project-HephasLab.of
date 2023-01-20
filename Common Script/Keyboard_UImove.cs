using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_UImove : MonoBehaviour
{
    public float StartLimitY;
    public float[] MoveList;
    public Vector2 OrigPos;
    bool isMoved = false;
    public TestviewLog test;
    public void StartMove(int index)
    {
      //  test.SetLog("\nStartMove in : " + isMoved+ "\n");
        if (StartLimitY > gameObject.GetComponent<RectTransform>().anchoredPosition.y)
        {
            if (!isMoved)
            {
                OrigPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(OrigPos.x, MoveList[index]);
                isMoved = true;
            }
            else
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(OrigPos.x, MoveList[index]);
            }
        }
    }
    public void BackMove()
    {
        if (StartLimitY > OrigPos.y)
        {
            if (!gameObject.GetComponent<RectTransform>().anchoredPosition.Equals(OrigPos))
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = OrigPos;
                isMoved = false;
            //    test.SetLog("\nisMoved BackMove : " + isMoved + "\n");

            }
        }
    }
    private void OnDisable()
    {
        isMoved = false;
    }
}
