using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 moveDir = new Vector2(1f, 0f);
    [SerializeField] float moveSpeed;
    BoxCollider2D checkGroundCol;
    int GroundCheckCount = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        checkGroundCol = GetComponentInChildren<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
        if (checkGroundCol.IsTouchingLayers(LayerMask.GetMask("Ground")) == false)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            moveDir.x *= -1;           
        }

        rigid.velocity = new Vector2(moveDir.x * moveSpeed, rigid.velocity.y);
    }
}
