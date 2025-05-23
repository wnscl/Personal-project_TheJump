using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

interface ICanIntroduce
{
    void Introduce();
}

public class EntityStatController : MonoBehaviour
{
    [Header("This Entity Stat")]
    protected Vector3 moveDirection = Vector3.zero;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float runSpeed;
    [SerializeField] protected float jumpPower;
    [SerializeField] protected int hp;
    [SerializeField] protected int maxHp;
    public int Hp { get { return hp; } } //차후 카메라 값을 프로퍼티로 하면 편할듯
    public int MaxHp { get { return maxHp; } }

    [SerializeField] protected int sp;
    public int Sp { get { return sp; } }
    public Coroutine nowCoroutine;


    public void OnHealPlayer()
    {
        nowCoroutine = StartCoroutine(HealPlayer());
    }
    public IEnumerator HealPlayer()
    {
        int healPower = 10;

        while (healPower > 0)
        {
            if (hp <= 0 || hp >= maxHp)
            {
                nowCoroutine = null;
                yield break;
            }

            hp += healPower;
            if (hp >= maxHp)
            {
                int temp = hp - maxHp;
                hp -= temp;
            }
            healPower--;
            yield return new WaitForSeconds(0.5f);
        }
        nowCoroutine = null;
        yield break;
    }
    public void OnSpeedUpPlayer()
    {
        nowCoroutine = StartCoroutine(SpeedUpPlayer());
    }
    public IEnumerator SpeedUpPlayer()
    {
        float speed = 6;

        while (speed > 0)
        {
            moveSpeed += speed;
            yield return new WaitForSeconds(1f);
            moveSpeed -= speed; 
            speed--;
        }
        nowCoroutine = null;
        yield break;
    }
}
