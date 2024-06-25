using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 moveDir = new Vector2(1f, 0f);
    [SerializeField] float moveSpeed;
    BoxCollider2D checkGroundCol;
    CapsuleCollider2D checkflyCol;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        checkGroundCol = GetComponentInChildren<BoxCollider2D>();
        checkflyCol = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {

        if (checkflyCol.IsTouchingLayers(LayerMask.GetMask("Ground")) == false)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            return;
        }
        if (checkGroundCol.IsTouchingLayers(LayerMask.GetMask("Ground")) == false || checkGroundCol.IsTouchingLayers(LayerMask.GetMask("Wall")))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            moveDir.x *= -1;           
        }

        rigid.velocity = new Vector2(moveDir.x * moveSpeed, rigid.velocity.y);
    }
}
