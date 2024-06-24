using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 force;
    bool right;
    bool isDone = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        rigid.AddForce(force, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDone = false;
    }

    void Update()
    {
        if (isDone == false) 
        {
            return;
        }
        transform.Rotate(new Vector3(0, 0,
            right == true ? -360f : 360f) * Time.deltaTime);
    }

    public void SetForce(Vector2 _force, bool _isRight)
    {
        force = _force;
        right = _isRight;
    }
}
