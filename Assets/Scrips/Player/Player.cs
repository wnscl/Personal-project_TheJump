using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] Animator anim;

    Vector3 moveDirection = Vector3.zero;
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpPower;
    bool isJump = false;
    bool isRun = false;

    private void Awake()
    {

    }

    void Start()
    {
        
    }


    void Update()
    {
        PlayerMove();
        PlayerLook();
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
            if (!isRun)
            {
                anim.SetInteger("MoveNum", 1);
                rigid.MovePosition(rigid.position + (moveDirection * moveSpeed * Time.deltaTime));
            }
            else
            {
                anim.SetInteger("MoveNum", 2);
                rigid.MovePosition(rigid.position + (moveDirection * runSpeed * Time.deltaTime));
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
        float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, angle, 0);
        Quaternion targetRot = Quaternion.Euler(0, angle, 0);
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 10f * Time.deltaTime);
    }
    public void OnMove(InputAction.CallbackContext context) //InputValue value)
    {
        Vector2 input = context.ReadValue<Vector2>();

        moveDirection = new Vector3(input.x, 0, input.y);
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
        if (context.performed)
        //.performed - �Է��� ��Ȯ�� ����� ������ (���� ����)
        //context.started - �Է��� ���۵� �������� (���������)
        //context.canceled	- �Է��� ��ҵ� ���� (��ư�� ���� ��)
        //context.ReadValue<T>() - �Էµ� ���� �� �б� ����2 �÷� �� ��
        {
            isJump = true;
            PlayerJump();
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

}
