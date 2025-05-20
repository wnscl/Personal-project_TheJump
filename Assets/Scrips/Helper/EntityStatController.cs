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
    public int Hp { get { return hp; } } //���� ī�޶� ���� ������Ƽ�� �ϸ� ���ҵ�
    public int MaxHp { get { return maxHp; } }

    [SerializeField] protected int sp;
    public int Sp { get { return sp; } }    
}
