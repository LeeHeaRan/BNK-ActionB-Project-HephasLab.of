using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_control : MonoBehaviour
{
    public List<GameObject> GameObjects = new List<GameObject>();
    public List<int> ActiveIndex = new List<int>();
    bool isCloseing = false;
    GameObject parent;

    private void Update()
    {
        if (isCloseing)
        {
            bool isCheck = false;
            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i].activeSelf)
                {
                    isCheck = true;
                }
            }

            if (!isCheck)
            {
                
                gameObject.SetActive(false);
      
            }
        }
    }


    public void SetActive(int index)
    {
        ActiveIndex.Add(index);
        GameObjects[index].SetActive(true);
    }
   

    public void UnActive()
    {
        if (!isCloseing)
        {
            isCloseing = true;
            for (int i = 0; i < ActiveIndex.Count; i++)
            {
                if (GameObjects[ActiveIndex[i]].GetComponent<UI_Control>() != null)
                {
                    GameObjects[ActiveIndex[i]].GetComponent<UI_Control>().CloseUI();


                }
                else
                {
                    GameObjects[ActiveIndex[i]].SetActive(false);
                    gameObject.SetActive(false);
                }
            }
        }
    }
    public void UnActive(GameObject _parent)
    {
        if (!isCloseing)
        {
            parent = _parent;
            isCloseing = true;
            for (int i = 0; i < ActiveIndex.Count; i++)
            {
                if (GameObjects[ActiveIndex[i]].GetComponent<UI_Control>() != null)
                {
                    GameObjects[ActiveIndex[i]].GetComponent<UI_Control>().CloseUI();


                }
                else
                {
                    GameObjects[ActiveIndex[i]].SetActive(false);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnDisable()
    {
        isCloseing = false;
        ActiveIndex.Clear();
        if (parent != null)
        {
            parent.SetActive(false);
            parent = null;
        }
        for (int i = 0; i < GameObjects.Count; i++)
        {
            GameObjects[i].SetActive(false);
        }
    }

}
