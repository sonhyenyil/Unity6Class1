using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapone : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 force;
    bool right;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    
   
    }
    void Start()
    {
        rigid.AddForce(force, ForceMode2D.Impulse);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 
            right == true ? -360f : 360f)* Time.deltaTime);
    }
}
