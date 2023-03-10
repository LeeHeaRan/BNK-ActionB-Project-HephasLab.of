using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_Control : MonoBehaviour
{

    public GameObject[] ActiveObject;
    public List<_ColotControl_target> ColorList = new List<_ColotControl_target>();
    public List<_MoveControl_target> MoveList = new List<_MoveControl_target>();

    bool isInitAnim = true;
    public bool isClose = false;
    bool isReturnAnim = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ColorList.Count; i++)
        {

           


        }
        for (int i = 0; i < MoveList.Count; i++)
        {
            

        }
    }
    
    // Update is called once per frame
    void Update()
    {
       
        if (isInitAnim)
        {
            isInitAnim = false;
            for (int i = 0; i < ColorList.Count; i++)
            {
                ColorList[i].StartColor();

            }
            for (int i = 0; i < MoveList.Count; i++)
            {
                MoveList[i].StartMovePos();

            }
        }

        if(!isInitAnim && isReturnAnim)
        {
            isReturnAnim = false;
            for (int i = 0; i < ColorList.Count; i++)
            {
                ColorList[i].ReturnColor();

            }
            for (int i = 0; i < MoveList.Count; i++)
            {
                MoveList[i].ReturnMovePos();
            }
        }
    }

    public void CloseUI()
    {
        if (gameObject.activeSelf && !isClose)
        {
            isReturnAnim = true;
            isClose = true;
            if (ActiveObject.Length > 0)
            {
                for (int i = 0; i < ActiveObject.Length; i++)
                {
                    ActiveObject[i].GetComponent<UI_Control>().CloseUI();

                }
            }
        }
    }
    public void CloseComplete()
    {
        gameObject.SetActive(false);
    }



    private void OnEnable()
    {

        for (int i = 0; i < ColorList.Count; i++)
        {

            if (ColorList[i].Tobject.GetComponent<ValueToUpdate>() == null)
            {
                Debug.LogError("ValueToUpdata Component Null (Color) Object name : " + ColorList[i].Tobject.name);
                ColorList[i].Tobject.SetActive(true);
            }


        }
        for (int i = 0; i < MoveList.Count; i++)
        {
            if (MoveList[i].Tobject.GetComponent<ValueToUpdate>() == null)
            {
                Debug.LogError("ValueToUpdata Component Null (Move) Object name : " + MoveList[i].Tobject.name);
                ColorList[i].Tobject.SetActive(true);
            }

        }
        for (int i = 0; i < ActiveObject.Length; i++)
        {
          //  ActiveObject[i].SetActive(true);
        }

    }
    private void OnDisable()
    {
        isInitAnim = true;
        isClose = false;


    }
}
/*#if UNITY_EDITOR
[CustomEditor(typeof(UI_Control))]
[CanEditMultipleObjects]
public class UI_Control_Editor : Editor
{

    public override void OnInspectorGUI()
    {

        EditorGUILayout.BeginVertical();
        EditorGUILayout.HelpBox("\n\nColor List, Move List ????????? Target Object??? ValueToUpdate ???????????? ????????????\n" +
                                "??????????????? ????????? UI_Control Component???????????? CloseUI() ????????????\n" +
                                "?????? ??????????????? ?????????????????? Return Value??? UI_Control ??????????????? CloseComplete????????????\n" +
                                "????????? Active(false)??? \n\n" +
                                "1. ??????????????? List ????????????\n" +
                                "2. ??????????????? ?????? ??????????????? ?????? ??????????????? ?????????\n" +
                                "3. ??? ??????????????? ????????? ValueToUpdate ????????????\n" +
                                "4. ????????? CloseUI() ??????" +
                                "5. ?????? ??????????????? ?????????????????? ReturnCallbak??? CloseComplete ????????????\n\n",MessageType.None);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        base.OnInspectorGUI();



    }
}
#endif*/
