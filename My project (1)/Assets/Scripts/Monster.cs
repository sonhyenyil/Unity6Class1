using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 moveDir = new Vector2(1f, 0f);
    [SerializeField] float moveSpeed;
    [SerializeField] bool checkGizmos = false;
    [SerializeField] float gizmosRange = 0.67f;
    BoxCollider2D checkGroundCol;
    CircleCollider2D checkWallCol;
    Physics2D physic;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        checkGroundCol = GetComponentInChildren<BoxCollider2D>();
        checkWallCol = GetComponentInChildren<CircleCollider2D>();
    }

    void Update()
    {
        monsterMove();
    }
    private void OnDrawGizmos()
    {
        if (checkGizmos == true)
        {
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, gizmosRange), Color.red);
        }
    }

    private void monsterMove()
    {
        Vector3 scale = transform.localScale;

        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, 0.68f, LayerMask.GetMask("Ground"));


        if (hitGround)
        {
            if (scale.x < 0)
            {
                moveDir.x = -1;
            }
            else if (scale.x > 0)
            {
                moveDir.x = 1;
            }
            if (checkGroundCol.IsTouchingLayers(LayerMask.GetMask("Ground")) == false || checkWallCol.IsTouchingLayers(LayerMask.GetMask("Wall")))
            {
                scale.x *= -1;
                transform.localScale = scale;
                moveDir.x *= -1;
            }
        }
        else
        {
            moveDir.x = 0;
        }
        rigid.velocity = new Vector2(moveDir.x * moveSpeed, rigid.velocity.y);
    }
}
