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
    [SerializeField] LayerMask npc;
    bool isRun = false;
    bool interctionMode = false;


    //public int Hp { get { return hp; } } //차후 카메라 값을 프로퍼티로 하면 편할듯

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

    //플레이어 룩 있던 자리


    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        moveDirection = new Vector3(input.x, 0f, input.y);

    }
    public void PlayerJump()
    {
        //리지드바디 애드포스는 결국 벨로시티를 건드린다.
        anim.SetTrigger("Jumping");
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        bool isGround = CheckTheGround();

        if (context.performed && isGround && !anim.GetBool("isJump"))
        //.performed - 입력이 정확히 실행된 시점에 (누른 순간)
        //context.started - 입력이 시작된 순간부터 (누르기시작)
        //context.canceled	- 입력이 취소된 순간 (버튼을 땟을 때)
        //context.ReadValue<T>() - 입력된 실제 값 읽기 벡터2 플롯 불 등
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
    public void OnInterction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShotInterction();
        }
    }
    private void ShotInterction()
    {
        Vector3 direction = cam.LookForward;

        Ray talkRay = new Ray(playerBody.transform.position + (transform.up * 1f), direction);
        Debug.DrawRay(talkRay.origin, talkRay.direction * 2f, Color.red, 4f);

        if (Physics.Raycast(talkRay, 2f, npc))
        {
            interctionMode = true;
            anim.SetInteger("MoveNum", 0);
            UiManager.Instance.PlayerUiInterctionOrder("NpcOpen");
            Debug.Log("npc와 대화");
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
