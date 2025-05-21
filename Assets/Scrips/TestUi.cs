using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
//�̰� �ִ� ��ũ��Ʈ�� �޾��� �� �ش� ������Ʈ�� �ڵ��߰�

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

    public void Close(bool kill = false)//�ɼų� �Ķ����
        //�⺻�� ����
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
