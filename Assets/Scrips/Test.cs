using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    void Start()
    {
        List<int> numbers = new List<int>();

        Study<int> studyList = new Study<int>();
        studyList.Add(1);
        studyList.Add(5);
        studyList.Add(10);

        studyList.Remove(1);
        studyList.ShowInfo();

        bool isIn;
        Debug.Log($"10이 포함되어있는가? {isIn = studyList.Contains(10)}");
        Debug.Log($"리스트의 첫번째 요소는? {studyList[0]}");
        studyList[0] = 9999;

        Debug.Log($"리스트의 첫번째 요소는? {studyList[0]}");

        //UiManager.Instance.Open<TheUi>();
    }

    
}
