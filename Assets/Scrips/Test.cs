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
        Debug.Log($"10�� ���ԵǾ��ִ°�? {isIn = studyList.Contains(10)}");
        Debug.Log($"����Ʈ�� ù��° ��Ҵ�? {studyList[0]}");
        studyList[0] = 9999;

        Debug.Log($"����Ʈ�� ù��° ��Ҵ�? {studyList[0]}");

        //UiManager.Instance.Open<TheUi>();
    }

    
}
