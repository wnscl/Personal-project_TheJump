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
    //public int Hp { get { return hp; } } //���� ī�޶� ���� ������Ƽ�� �ϸ� ���ҵ�



    //���̸���� �迭ó�� ������ 
    //�÷��̾� ĳ���Ϳʹ� ���̰� �浹�� �ȵǰ�

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

            //�ű״�Ʃ�带 �̿��ϴ� ������� ������
            //-�ǵ��� Ȯ���ϴ�.
            //-���ʿ��� ������ ���δ�.

        //magnitude�� ���Ϳ� ��������μ� ���⺤���� ���̸� ���� �� �ִ�
        //�� ��� ���⺤���� ���̸� ����� ĳ���Ͱ� ������ �� �� ������ ���� ����
        //��? ���⺤���� ���� 1�̱� ������ ������ �Է��ϴ� �������� �� ���ǹ��� ���
        //�Է��� ���ٸ� 0�̱� ������ else���� ����
        //�� �Է��ߴ��� ���ߴ��ĸ� �Ǻ��ع���
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
            //���ν�Ƽ�� �ʼ� �� ��ü�̱� ������ ��ŸŸ���� �����ָ� �ȵȴ�.
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
        //��ǲ�ý����� �ƴ� ���� ����� -1 ~ 1 �̳��� ���� ����
        //�׷��� ��ǲ�ý����� 1920, 1080���� ���� ��û���� ������ ������ �ʰ� ȸ���� �ݿ��� �ް��� Ʀ
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
        //eulerAngles ����
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
        //���⼭ �̹� �Է��� ���� ����ȭ�ؼ� ������ ��
        //�׷��⿡ �밢�� �̵��� ���� ����ó���� ���� �ʾƵ� ��

        //Vector2 moveWithDirectionAndSpeed = inputVec * moveSpeed * Time.deltaTime;
        //���ؿ� ���⺤�� �� �����ӹ����� moveSpeed(�̵��ӵ�)�� ���ؼ� 
        //������ ����� ũ�⸦ �� �Ը���� �����.
        // -> �ᱹ ���ν�Ƽ�� ���� ������ٵ��� �����ý����� �̿��Ͽ� �����̴� ��������
        // ���ν�Ƽ�� ������ �ϴ� ���� ������ִ� ���̴�.

        //rigid.velocity = moveWithDirectionAndSpeed;
        //velocity = ����x�ӵ� �� ���⼺�� �ӵ��� ũ�⸦ ��� ���� ����
        //������ ����,ũ�� ���͸� ���� �־��ָ� �̵� �ý���

    }
    public void PlayerJump()
    {
        //������ٵ� �ֵ������� �ᱹ ���ν�Ƽ�� �ǵ帰��.
        anim.SetTrigger("Jumping");
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
    public void OnJump(InputAction.CallbackContext context)
    //����Ƽ�Է¿��� ��ư����� �������� �� ���� ����Ű�� ���� �󸶳� ������ ����
    //���ʰ� �������� ���� ������ �Լ��� ��Ƽ� �����ִ� ���� CallbackContext
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
