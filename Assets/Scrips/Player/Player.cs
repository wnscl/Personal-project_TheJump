using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : EntityStatController
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] Animator anim;
    [SerializeField] Transform camPivot;
    [SerializeField] Transform realCam;
    [SerializeField] GameObject playerBody;
    [SerializeField] LayerMask ground;

    public Vector2 viewPos;
    bool isRun = false;
    //public int Hp { get { return hp; } } //차후 카메라 값을 프로퍼티로 하면 편할듯



    //레이만들고 배열처럼 가지고 
    //플레이어 캐릭터와는 레이가 충돌이 안되게

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
        PlayerLook();
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

            //매그니튜드를 이용하는 방법보다 좋은점
            //-의도가 확실하다.
            //-불필요한 연산을 줄인다.

        //magnitude를 벡터에 사용함으로서 방향벡터의 길이를 얻을 수 있다
        //이 경우 방향벡터의 길이를 사용해 캐릭터가 움직일 때 안 움직일 때를 구분
        //왜? 방향벡터의 값은 1이기 때문에 방향을 입력하는 시점에서 이 조건문이 사용
        //입력이 없다면 0이기 때문에 else문이 사용됨
        //즉 입력했느냐 안했느냐를 판별해버림
        {
            Vector3 lookForward = new Vector3(camPivot.forward.x, 0f, camPivot.forward.z).normalized;
            Vector3 lookRight = new Vector3(camPivot.right.x, 0f, camPivot.right.z).normalized;
            Vector3 direction = lookForward * moveDirection.z + lookRight * moveDirection.x;

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
            //rigid.velocity = new Vector3(moveDirection.x * moveSpeed, rigid.velocity.y, moveDirection.z * moveSpeed);
            //rigid.velocity += moveDirection * moveSpeed * Time.deltaTime;
            //벨로시티는 초속 그 자체이기 때문에 델타타임을 곱해주면 안된다.
        }
        else
        {
            anim.SetInteger("MoveNum", 0);
        }
    }
    public void PlayerLook()
    {
        //Vector3 camAngle = realCam.rotation.eulerAngles;
        //float camAngleX = camAngle.x - viewPos.y;
        //realCam.rotation = Quaternion.Euler(camAngle.x - viewPos.y, camAngle.y + viewPos.x, camAngle.z);
        //float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //인풋시스템이 아닌 옛날 방식은 -1 ~ 1 이내로 값이 나옴
        //그러나 인풋시스템은 1920, 1080같이 값이 엄청나기 때문에 곱하지 않고 회전에 반영시 급격이 튐
        Vector3 camAngle = camPivot.rotation.eulerAngles;
        float camAngleX = camAngle.x - mouseDelta.y;

        if (camAngleX < 180f)
        {
            camAngleX = Mathf.Clamp(camAngleX, -15f, 45f);

        }
        else
        {
            camAngleX = Mathf.Clamp(camAngleX, 335f, 361f);
        }


        camPivot.rotation = Quaternion.Euler(camAngleX, camAngle.y + mouseDelta.x, camAngle.z);

        //Vector3 viewDir = (transform.position - camPivot.position).normalized;

        float angle = camPivot.eulerAngles.y;
        //eulerAngles 공부
        playerBody.transform.rotation = Quaternion.Euler(0, angle, 0);

        //Vector2 playerPos = new Vector2(playerBody.transform.position.x, playerBody.transform.position.z);
        //Vector2 camPos = new Vector2(realCam.transform.position.x, realCam.transform.position.z);
        //moveDirection = playerPos - camPos;
        


        //float angle = Mathf.Atan2(viewDir.z, viewDir.x);
        //transform.rotation = Quaternion.Euler(0, angle, 0);
        //transform.rotation = Quaternion.LookRotation(viewDir);


        //transform.rotation = Quaternion.Euler(0, angle, 0);
        //Quaternion targetRot = Quaternion.Euler(0, angle, 0);
        //if (moveDirection != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        //}
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 10f * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context) //InputValue value)
    {
        Vector2 input = context.ReadValue<Vector2>();

        moveDirection = new Vector3(input.x, 0f, input.y);
        //Vector3 forward = realCam.forward;
        //Debug.Log(forward);
        //Vector3 right = realCam.right;
        //Debug.Log(right);
        //forward.y = 0;
        //Debug.Log(forward);
        //right.y = 0;
        //Debug.Log(right);
        //forward.Normalize();
        //right.Normalize();
        //Vector3 moveInput = new Vector3(input.x, 0, input.y);

        //moveDirection = (forward * moveInput.z + right * moveInput.x).normalized;

        //Vector3 playerPos = new Vector3(playerBody.transform.position.x, 0 , playerBody.transform.position.z);
        //Vector3 camPos = new Vector3(realCam.transform.position.x, 0 ,realCam.transform.position.z);
        //moveDirection = (playerPos - camPos).normalized;

        //moveDirection = new Vector3(input.x, 0, input.y);
        //direction = value.Get<Vector2>();
        //여기서 이미 입력한 값을 정규화해서 가지고 옴
        //그렇기에 대각선 이동에 관한 예외처리를 하지 않아도 됨

        //Vector2 moveWithDirectionAndSpeed = inputVec * moveSpeed * Time.deltaTime;
        //구해온 방향벡터 즉 움직임방향을 moveSpeed(이동속도)와 곱해서 
        //벡터의 방향과 크기를 내 입맛대로 만든다.
        // -> 결국 벨로시티를 통해 리지드바디의 물리시스템을 이용하여 움직이는 것임으로
        // 벨로시티가 가져야 하는 값을 만들어주는 것이다.

        //rigid.velocity = moveWithDirectionAndSpeed;
        //velocity = 방향x속도 즉 방향성과 속도의 크기를 모두 갖는 벡터
        //가져온 방향,크기 벡터를 집어 넣어주면 이동 시스템

    }
    public void PlayerJump()
    {
        //리지드바디 애드포스는 결국 벨로시티를 건드린다.
        anim.SetTrigger("Jumping");
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
    public void OnJump(InputAction.CallbackContext context)
    //유니티입력에서 버튼방식을 선택했을 때 내가 누른키가 뭐고 얼마나 누르고 땟고
    //몇초간 눌럿는지 같은 정보를 함수로 담아서 보내주는 것이 CallbackContext
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
