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
    [SerializeField] BasicNpc interctionTarget;
    [SerializeField] PlayerInventory inventory;

    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask npc;
    bool isRun = false;
    public bool interctionMode = false;


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
        if (!interctionMode)
        {
            PlayerMove();
        }
    }
    private void LateUpdate()
    {
        if (!interctionMode)
        {
            cam.PlayerLook();
        }
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
    public void PlayerJump(float jumpP)
    {
        //������ٵ� �ֵ������� �ᱹ ���ν�Ƽ�� �ǵ帰��.
        anim.SetTrigger("Jumping");
        rigid.AddForce(Vector3.up * (jumpPower * jumpP), ForceMode.Impulse);
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
            PlayerJump(1);
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
    public void OnInterction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShotInterction();
        }
    }
    private void ShotInterction()
    {
        Vector3 direction = cam.CamPivot.forward;

        Ray talkRay = new Ray(playerBody.transform.position + (transform.up * 1f), direction);
        //��� ���� ���⼺�� �������� ����
        RaycastHit target;
        //������ ������Ʈ�� ������ �� ����� �����ϴ� ����ü
        //������ ��Ҵ°� ��� ��Ҵ°� ������ ���� ��Ҵ°�
        //target.point      ������ ���� ���� ��ǥ
        //target.normal     ���� ǥ���� ���� ����
        //target.distance   �߻������κ��� �浹 ���������� �Ÿ�
        //target.collider   ���� ������Ʈ�� Collider
        //target.transform  ���� ������Ʈ�� Transform
        //target.rigidbody  ���� ������Ʈ�� Rigidbody (�ִٸ�)
        Debug.DrawRay(talkRay.origin, talkRay.direction * 2.5f, Color.red, 4f);
         
        if (Physics.Raycast(talkRay,out target, 2.5f, npc))
        {
            interctionMode = true;
            anim.SetInteger("MoveNum", 0);
            interctionTarget = target.collider.GetComponent<BasicNpc>();
            UiManager.Instance.PlayerUiInterctionOrder("NpcOpen", interctionTarget);
            Debug.Log("npc�� ��ȭ");
        }
    }
    public void OnItemUse(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var keyboard = Keyboard.current;

        if (keyboard.digit1Key.wasPressedThisFrame)
            inventory.UseAndRemoveItem(1);
        else if (keyboard.digit2Key.wasPressedThisFrame)
            inventory.UseAndRemoveItem(2);
        else if (keyboard.digit3Key.wasPressedThisFrame)
            inventory.UseAndRemoveItem(3);
        else if (keyboard.digit4Key.wasPressedThisFrame)
            inventory.UseAndRemoveItem(4);
        else if (keyboard.digit5Key.wasPressedThisFrame)
            inventory.UseAndRemoveItem(5);

        //wasPressedThisFrame �̹� �����ӿ��� ó�� ���ȴ°� ? (Edge - triggered)
        //isPressed   ���� ������ �ִ� �����ΰ�? (������ ������ ��� true)
        //wasReleasedThisFrame �̹� �����ӿ��� �ô°�?
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
