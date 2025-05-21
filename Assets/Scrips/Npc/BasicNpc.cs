using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BasicNpc : MonoBehaviour
{
    public string name;
    public string[] dialog;
    public Sprite photo;
    [SerializeField] Animator anim;
    [SerializeField] bool isBeenTalk = false;
    [SerializeField] Quaternion targetRot;
    public Vector3 baseDirection = Vector3.forward;

    private void Update()
    {
        if (isBeenTalk)
        {
            LookPlayer();
        }
    }

    public void NowInterction(bool isStart)
    {
        anim.SetInteger("ActionNum", 1);
        isBeenTalk = true;
        if (isStart)
        {
            GetDirectionOfPlayer();
        }
        else
        {
            targetRot = Quaternion.LookRotation(baseDirection);
        }

    }

    public void GetDirectionOfPlayer()
    {
        Vector3 playerPos = new Vector3(
            UiManager.Instance.player.transform.position.x,
            0,
            UiManager.Instance.player.transform.position.z);

        Vector3 myPos = new Vector3(
            transform.position.x, 0, transform.position.z);

        Vector3 direction = (playerPos - myPos).normalized;
        targetRot = Quaternion.LookRotation(direction);

    }
    public void LookPlayer()
    {
        Quaternion nowRot = (transform.rotation);

        transform.rotation = Quaternion.Lerp(
            transform.rotation, //현재회전값
            targetRot, //목표 회전값
            Time.deltaTime * 5f); //회전 속도

        if (Quaternion.Angle(nowRot,targetRot) < 1f)
        {
            isBeenTalk = false;
            targetRot = Quaternion.identity;
            anim.SetInteger("ActionNum", 0);
        }
    }
    //public void LookOther()
    //{
    //    targ
    //    transform.rotation = Quaternion.Lerp(
    //        transform.rotation)
    //}



}
