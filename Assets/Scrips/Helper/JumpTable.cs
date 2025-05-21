using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class JumpTable : MonoBehaviour
{
    [SerializeField] JumpPointer pointers;
    public float jumpForce = 2f;

    private void Awake()
    {
        pointers.onCollisionEvent += MakePlayerJump;
    }

    private void MakePlayerJump(Collision collision)
    {
        Debug.Log("충돌발생");
        if (collision == null ||
            !collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        Player player = collision.gameObject.GetComponent<Player>();
        player.PlayerJump(jumpForce);

    }
}
