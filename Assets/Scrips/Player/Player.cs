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
    public void PlayerJump(float jumpP)
    {
        //리지드바디 애드포스는 결국 벨로시티를 건드린다.
        anim.SetTrigger("Jumping");
        rigid.AddForce(Vector3.up * (jumpPower * jumpP), ForceMode.Impulse);
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
        //쏘는 광선 방향성과 시작점을 가짐
        RaycastHit target;
        //광선이 오브젝트에 닿으면 그 결과를 저장하는 구조체
        //무엇에 닿았는가 어디에 닿았는가 무엇을 통해 닿았는가
        //target.point      광선이 맞은 월드 좌표
        //target.normal     맞은 표면의 법선 벡터
        //target.distance   발사점으로부터 충돌 지점까지의 거리
        //target.collider   맞은 오브젝트의 Collider
        //target.transform  맞은 오브젝트의 Transform
        //target.rigidbody  맞은 오브젝트의 Rigidbody (있다면)
        Debug.DrawRay(talkRay.origin, talkRay.direction * 2.5f, Color.red, 4f);
         
        if (Physics.Raycast(talkRay,out target, 2.5f, npc))
        {
            interctionMode = true;
            anim.SetInteger("MoveNum", 0);
            interctionTarget = target.collider.GetComponent<BasicNpc>();
            UiManager.Instance.PlayerUiInterctionOrder("NpcOpen", interctionTarget);
            Debug.Log("npc와 대화");
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

        //wasPressedThisFrame 이번 프레임에서 처음 눌렸는가 ? (Edge - triggered)
        //isPressed   지금 눌려져 있는 상태인가? (누르고 있으면 계속 true)
        //wasReleasedThisFrame 이번 프레임에서 뗐는가?
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
