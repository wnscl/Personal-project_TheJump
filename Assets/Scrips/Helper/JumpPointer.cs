using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointer : MonoBehaviour
{
    [SerializeField] private BoxCollider jumpCol;
    public float jumpForce = 30f;
    public event Action<Collision> onCollisionEvent;


    private void OnCollisionEnter(Collision collision)

    {
        onCollisionEvent?.Invoke(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ContactPoint cont = collision.contacts[0];
            Vector3 pos = cont.point;
            Vector3 normal = cont.normal;
            Debug.Log("�浹 ����: " + pos);
            Debug.Log("���� ����: " + normal);
            Debug.Log($"���� ����2: {cont.normal}");
            Debug.DrawRay(cont.point, cont.normal, Color.green, 2f);
            Debug.DrawRay(cont.point, -cont.normal, Color.red, 2f);

            Debug.Log(collision.contacts[0].point);

            
            Transform otherTrans = collision.transform;
            Vector3 otherPos = otherTrans.position;
            Vector3 incomingDirection = (cont.point - otherPos).normalized;

            Rigidbody rb = collision.rigidbody;
            Debug.Log("�浹����: " + incomingDirection);
            Debug.Log("�浹ü ����: " + rb.position);


        }


    }
}
