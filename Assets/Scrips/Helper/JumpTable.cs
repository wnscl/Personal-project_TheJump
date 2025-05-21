using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class JumpTable : MonoBehaviour
{
    [SerializeField] JumpPointer pointers;

    private void Awake()
    {
        pointers.onCollisionEvent += PrintCollision;
    }

    private void PrintCollision(Collision collision)
    {
        Debug.Log("충돌발생");
    }
}
