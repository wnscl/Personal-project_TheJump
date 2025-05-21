using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
//이게 있는 스크립트를 달아줄 때 해당 컴포넌트가 자동추가

public abstract class TestUi : MonoBehaviour
{
    private CanvasGroup cg;
    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close(bool kill = false)//옵셔널 파라미터
        //기본값 지정
    {
        if (kill)
        {
            Destroy(gameObject);
            return;
        }
        gameObject.SetActive(false);
    }
    public void SetAlpha(float alpha)
    {
        cg.alpha = alpha;
    }
}
