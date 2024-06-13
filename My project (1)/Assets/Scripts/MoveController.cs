using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MoveController : MonoBehaviour
{
    //(Case by case��) manager, �񵿱������� ȣ���� �������� ����
    //controller, update��� ���������� ȣ���� ���� �ʴ��� Ÿ ����� �ҷ��� ����ϴ� ��찡 ����

    [Header("�÷��̾� �̵� �� ����")]
    Rigidbody2D rigid;
    CapsuleCollider2D capcol;
    Animator anim;
    Vector3 moveDir;
    float virticalVelocity = 0f;//�������� �������� ��

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    [SerializeField] bool showGroundCheck;
    [SerializeField] float groundCheckLength;//�� ���̰� ���ӿ��� �󸶸�ŭ�� ���̷� �������� �������� ������������ �˼��� ����
    [SerializeField] Color colorGroundCheck;

    [SerializeField] bool isGround;//�ν����Ϳ��� �÷��̾ �÷��� Ÿ�Ͽ� �����ߴ���

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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moving();
        checkGrounded();
    }

    private void checkGrounded()
    {
        if (gameObject.CompareTag("Player") == true)//�±״� string���� ����� �±׸� ����
        {

        }

        //Layer int�� ����� ���̾ �����Ѵ�.
        //Layer�� int�� ���������� Ȱ���ϴ� int�� �ٸ���
        //Wall Layer, Ground Layer
        RaycastHit2D hit =

         Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, LayerMask.GetMask("Ground"));

        if (hit)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
    private void moving()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;//a,L A key -1, d, R A Key 1, �ƹ��͵� �Է����� ������ 0
        moveDir.y = rigid.velocity.y;
        //���ð����� ���鶧�� ������Ʈ�� �ڵ忡 ���ؼ� �����̵��ϰ� ���������
        //������ ���ؼ� �̵�
        rigid.velocity = moveDir;//moveDir���� default���� 0,0,0�ε� y�� ���� 0�̱� ������ ����ؼ� �ʱ�ȭ��(������ �ڵ尡 ������ ����)
    }
}
