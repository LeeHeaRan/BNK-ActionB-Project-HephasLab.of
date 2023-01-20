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
        EditorGUILayout.HelpBox("\n\nColor List, Move List 추가후 Target Object에 ValueToUpdate 스크립트 추가하기\n" +
                                "애니메이션 닫을땐 UI_Control Component접근하여 CloseUI() 호출하기\n" +
                                "제일 늦게닫히는 애니메이션에 Return Value에 UI_Control 타켓추가후 CloseComplete추가하면\n" +
                                "알아서 Active(false)됨 \n\n" +
                                "1. 애니메이션 List 추가하기\n" +
                                "2. 애니메이션 타켓 애니메이션 시작 초기상태로 만들기\n" +
                                "3. 각 애니메이션 타겟에 ValueToUpdate 추가하기\n" +
                                "4. 닫을땐 CloseUI() 호출" +
                                "5. 제일 늦게닫히는 애니메이션에 ReturnCallbak에 CloseComplete 추가하기\n\n",MessageType.None);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        base.OnInspectorGUI();



    }
}
#endif*/
