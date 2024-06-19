using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class MoveController : MonoBehaviour
{
    //(Case by case��) manager, �񵿱������� ȣ���� �������� ����
    //controller, update��� ���������� ȣ���� ���� �ʴ��� Ÿ ����� �ҷ��� ����ϴ� ��찡 ����

    [Header("�÷��̾� �̵� �� ����")]
    Rigidbody2D rigid;
    CapsuleCollider2D capcol;
    BoxCollider2D boxcol;
    Animator anim;
    Vector3 moveDir;
    float virticalVelocity = 0f;//�������� �������� ��

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    [SerializeField] bool showGroundCheck;
    [SerializeField] float groundCheckLength;//�� ���̰� ���ӿ��� �󸶸�ŭ�� ���̷� �������� �������� ������������ �˼��� ����
    [SerializeField] Color colorGroundCheck;

    [SerializeField] bool isGround;//�ν����Ϳ��� �÷��̾ �÷��� Ÿ�Ͽ� �����ߴ���
    bool isJump;
    float verticalVelocity;

    Camera camMain;

    [Header("������")]
    [SerializeField] bool touchWall;
    bool isWallJump;
    [SerializeField] float wallJumpTime = 0.3f;
    float wallJumpTimer = 0.0f;//�������� ������ ���ۺҰ� Ÿ�̸�


    [Header("���")]
    [SerializeField] private float dashTime = 0.3f;//��ø� �ϴ� �ð�
    [SerializeField] private float dashSpeed = 20.0f;//����� �ӵ�
    float dashTimer = 0.0f;//��� �ð��� üũ�ϱ� ���� Ÿ�̸�
    [SerializeField] private float dashCoolTime = 5.0f;//��� ��Ÿ���� ����
    float dashCoolTimer = 0.0f;//��� ���� �ð��� üũ�ϱ����� Ÿ�̸�
    //��� ����Ʈ

    //[SerializeField] KeyCode dashKey; Ư�� Ű���� �޾ƿͼ� �����ϰ� �Ϸ��� Ű���� ���������� �޾� �� �� ����

    //Unity Editor������ ����� �����ϴ�.
    private void OnDrawGizmos()
    {
        if (showGroundCheck == true)
        {
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLength), colorGroundCheck);
        }

        //Debug.DrawLine(); ����׵� üũ�뵵�� �� ī�޶� ���� �׷��ټ� ����
        //Gizmos.DrawSphere(); ����׺��� �� ���� �ð�ȿ���� ����(Gizmos.Draw)
        //Handles.DrawWireArc(); Gizmos���� �� ���� �ð�ȿ���� �����Ѵ�(Handles.Draw) �� Unity Editor����̹Ƿ� ���ӿ��� ������ �� �� ����.
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))//������ �ݶ��̴��� ������, ���� �����Ų���� ��
    //    {
    //        touchWall = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
    //    {
    //        touchWall = false;
    //    }
    //}
    public void TriggerEnter(HitBox.ehitboxType _type, Collider2D _collision)
    {
        if (_type == HitBox.ehitboxType.WallCheck)
        {
            if (_collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                touchWall = true;
            }
        }
    }

    public void TriggerExit(HitBox.ehitboxType _type, Collider2D _collision)
    {
        if (_type == HitBox.ehitboxType.WallCheck)
        {
            if (_collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                touchWall = false;
            }
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxcol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        camMain = Camera.main;
    }

    void Update()
    {
        checkTimers();

        checkGrounded();

        dash();

        moving();

        checkAim();

        jump();

        checkGravity();

        doAnim();
    }

    private void dash()
    {
        if (dashCoolTimer != 0.0f)
        {
            return;
        }
        if (dashTimer == 0.0f && Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.F))
        {
            dashTimer = dashTime;
            dashCoolTimer = dashCoolTime;
            verticalVelocity = 0.0f;
            //    if (transform.localScale.x > 0)//����
            //    {
            //        rigid.velocity = new Vector2(-dashSpeed, verticalVelocity);
            //    }
            //    else//������
            //    {
            //        rigid.velocity = new Vector2(dashSpeed, verticalVelocity);
            //    }
            //
            //rigid.velocity = transform.localScale.x > 0 ? new Vector2(-dashSpeed, verticalVelocity) : new Vector2(dashSpeed, verticalVelocity); //���׽����� üũ
            rigid.velocity = new Vector2(transform.localScale.x > 0 ? -dashSpeed : dashSpeed, 0.0f);
        }
    }

    private void checkTimers()
    {
        if (wallJumpTimer > 0.0f)
        {
            wallJumpTimer -= Time.deltaTime;
            if (wallJumpTimer < 0.0f)
            {
                wallJumpTimer = 0.0f;
            }
        }

        if (dashTimer > 0.0f)
        { 
            dashTimer -= Time.deltaTime;
            if(dashTimer < 0.0f) 
            {
                dashTimer = 0.0f;
            }
        }

        if (dashCoolTimer > 0.0f)
        { 
            dashCoolTimer -= Time.deltaTime;
            if (dashCoolTimer < 0.0f)
            {
                dashCoolTimer = 0.0f;
            }
        }
    }

    private void checkGrounded()
    {
        isGround = false;

        if (verticalVelocity > 0f)
        {
            return;
        }
        //if (gameObject.CompareTag("Player") == true)//�±״� string���� ����� �±׸� ����
        //{
        //}
        //Layer int�� ����� ���̾ �����Ѵ�.
        //Layer�� int�� ���������� Ȱ���ϴ� int�� �ٸ���
        //Wall Layer, Ground Layer
        #region �� �ϳ��� ���������� �ϴ� �ڵ�
        RaycastHit2D hit =

        // Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, LayerMask.GetMask("Ground"));

        //if (hit)
        //{
        //    isGround = true;
        //}
        #endregion
        Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0f, Vector2.down, groundCheckLength, LayerMask.GetMask("Ground"));

        if (hit)
        {
            isGround = true;
        }
    }

    private void moving()
    {
        if (wallJumpTimer > 0.0f || dashTimer > 0.0f)
        {
            return;
        }
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;//a,L A key -1, d, R A Key 1, �ƹ��͵� �Է����� ������ 0
        moveDir.y = rigid.velocity.y;
        //���ð����� ���鶧�� ������Ʈ�� �ڵ忡 ���ؼ� �����̵��ϰ� ���������
        //������ ���ؼ� �̵� ������ ���ؼ� �̵��ɶ��� time.deltaTime���� �� �ʿ䰡 ����.
        rigid.velocity = moveDir;//moveDir���� default���� 0,0,0�ε� y�� ���� 0�̱� ������ ����ؼ� �ʱ�ȭ��(������ �ڵ尡 ������ ����)
    }

    private void checkAim()
    {
        #region Ű���带 ���� ������ȯ ���(�ּ�ó����)
        //Vector3 scale = transform.localScale;
        //if (moveDir.x < 0 && scale.x != 1.0f)//����
        //{
        //    scale.x = 1.0f;
        //    transform.localScale = scale;
        //    Debug.Log("<color=red>����</color>");
        //}
        //else if (moveDir.x > 0 && scale.x != -1.0f)//������
        //{
        //    scale.x = -1.0f;
        //    transform.localScale = scale;
        //    Debug.Log("<color=blue>����</color>");
        //}
        #endregion

        Vector2 mouseWorldPos = camMain.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 fixedPos = mouseWorldPos - playerPos;

        Vector3 playerScale = transform.localScale;
        if (fixedPos.x > 0 && playerScale.x != -1.0f)
        {
            playerScale.x = -1.0f;
        }
        else if (fixedPos.x < 0 && playerScale.x != 1.0f)
        {
            playerScale.x = 1.0f;
        }
        transform.localScale = playerScale;
    }


    private void jump()
    {
        //if (isGround == true && isJump == false  Input.GetKeyDown(KeyCode.Space)) 
        //{
        //    rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);//������ �̴���
        //}

        if (isGround == false)//������ ���ִ� ���¶��
        {
            //���� �پ��ְ�, �׸��� �������� �÷��̾ ����Ű�� ������ �ִµ� ����Ű�� �����ٸ�
            if (touchWall == true && moveDir.x != 0f && Input.GetKeyDown(KeyCode.Space))
            {
                isWallJump = true;
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            isJump = true;
        }
        else if (isGround == true)
        {
            verticalVelocity = 0;
        }
    }

    private void checkGravity()
    {
        if (dashTimer > 0.0f)
        {
            return;
        }
        else if (isWallJump == true)
        {
            isWallJump = false;
            Vector2 dir = rigid.velocity;
            dir.x *= -1f;// �ݴ������ �Է��ϰ� ����
            rigid.velocity = dir;

            verticalVelocity = jumpForce * 0.5f;
            //�����ð� ������ �Է��Ҽ� ����� ���� �߷��� x���� �� �� ����
            //�ԷºҰ� Ÿ�̸Ӹ� �۵����Ѿ���
            wallJumpTimer = wallJumpTime;
        }
        else if (isGround == false)//���߿� ���ִ� ����
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;//-9.81 ����Ƽ ��ü �߷����� ����ϸ� ���������� �����̻� ����ؼ� ������ �ϰԵȴ�.

            if (verticalVelocity < -10f)
            {
                verticalVelocity = -10f;
            }
        }
        else if (isJump == true)
        {
            isJump = false;
            verticalVelocity = jumpForce;
        }


        rigid.velocity = new Vector2(rigid.velocity.x, verticalVelocity);
    }

    private void doAnim()
    {
        anim.SetInteger("Horizontal", (int)moveDir.x);
        anim.SetBool("isGround", isGround);
    }
}
