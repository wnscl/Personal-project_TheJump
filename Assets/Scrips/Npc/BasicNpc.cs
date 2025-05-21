using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BasicNpc : MonoBehaviour
{
    public string name;
    public string[] dialog;
    public Sprite photo;
    [SerializeField] Animator anim;
    [SerializeField] bool talkMode = false;
    [SerializeField] Quaternion targetRot;

    private void Update()
    {
        if (talkMode)
        {
            LookPlayer();
        }
    }

    public void InterctionStart()
    {
        anim.SetInteger("ActionNum", 1);
        talkMode = true;
        GetDirectionOfPlayer();
    }

    public void GetDirectionOfPlayer()
    {
        Vector3 playerPos = new Vector3(
            UiManager.Instance.transform.position.x,
            0,
            UiManager.Instance.transform.position.z);

        Vector3 myPos = new Vector3(
            transform.position.x, 0, transform.position.z);

        Vector3 direction = (playerPos - myPos).normalized;

        targetRot = Quaternion.LookRotation(direction);

    }
    public void LookPlayer()
    {
        Quaternion nowRot = (transform.rotation);

        transform.rotation = Quaternion.Lerp(
            transform.rotation, //����ȸ����
            targetRot, //��ǥ ȸ����
            Time.deltaTime * 5f); //ȸ�� �ӵ�

        if (nowRot == targetRot)
        {
            talkMode = false;
            targetRot = Quaternion.identity;
        }
    }



}
