using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : EntityStatController
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] Animator anim;
    [SerializeField] GameObject playerBody;

    [SerializeField] PlayerCameraController cam;

    [SerializeField] LayerMask ground;
    bool isRun = false;


    //public int Hp { get { return hp; } } //���� ī�޶� ���� ������Ƽ�� �ϸ� ���ҵ�

    private void Awake()
    {

    }

    void Start()
    {
        
    }


    void Update()
    {
        if (anim.GetBool("isJump"))
        {
            Invoke("Jumping", 0.2f);
        }

        PlayerMove();
    }
    private void LateUpdate()
    {
        cam.PlayerLook();
    }

    void Jumping()
    {
        bool isGround = CheckTheGround();
        if (isGround)
        {
            anim.SetBool("isJump", false);
        }
        else
        {
            return;
        }
    }
    public bool CheckTheGround()
    {
        Ray[] groundCheckRay = new Ray[4]
        {
            new Ray(playerBody.transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(playerBody.transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(playerBody.transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(playerBody.transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };


        for (int i = 0; i < groundCheckRay.Length; i++)
        {
            Debug.DrawRay(groundCheckRay[i].origin, groundCheckRay[i].direction * 0.05f, Color.red);
            if (Physics.Raycast(groundCheckRay[i], 0.05f, ground))
            {
                return true;
            }
        }

        return false;
    }
    public void PlayerMove()
    {
        if (moveDirection != Vector3.zero)
        {
            Vector3 direction = cam.LookForward * moveDirection.z + cam.LookRight * moveDirection.x;

            if (!isRun)
            {
                anim.SetInteger("MoveNum", 1);
                rigid.MovePosition(rigid.position + (direction * moveSpeed * Time.deltaTime));
            }
            else
            {
                anim.SetInteger("MoveNum", 2);
                rigid.MovePosition(rigid.position + (direction * runSpeed * Time.deltaTime));
            }
        }
        else
        {
            anim.SetInteger("MoveNum", 0);
        }
    }

    //�÷��̾� �� �ִ� �ڸ�


    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        moveDirection = new Vector3(input.x, 0f, input.y);

    }
    public void PlayerJump()
    {
        //������ٵ� �ֵ������� �ᱹ ���ν�Ƽ�� �ǵ帰��.
        anim.SetTrigger("Jumping");
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        bool isGround = CheckTheGround();

        if (context.performed && isGround && !anim.GetBool("isJump"))
        //.performed - �Է��� ��Ȯ�� ����� ������ (���� ����)
        //context.started - �Է��� ���۵� �������� (���������)
        //context.canceled	- �Է��� ��ҵ� ���� (��ư�� ���� ��)
        //context.ReadValue<T>() - �Էµ� ���� �� �б� ����2 �÷� �� ��
        {
            anim.SetBool("isJump", true);
            PlayerJump();
        }
        else
        {
            return ;
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRun = true;
        }
        else if (context.canceled)
        {
            isRun = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (hp <= 0)
        {
            return;
        }
        else
        {
            hp -= damage;
        }
    }

}
