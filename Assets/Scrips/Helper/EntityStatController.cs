using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
