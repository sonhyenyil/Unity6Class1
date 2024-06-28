using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D weaponCol;
    Vector2 force;
    bool right;
    bool isDone = true;

    [Header("무기 설정")]
    [SerializeField] float weaponSpinSpeed = 2.0f; //무기의 회전속도를 지정하는 변수
    //[SerializeField, Tooltip("무기 유지시간 벽이나 바닥에 닿으면 설정된 시간후 파괴됨")] float weaponTime = 1.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        weaponCol = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        rigid.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDone = false;
        if (weaponCol.IsTouchingLayers(LayerMask.GetMask("Monster")))
        {
            Destroy(gameObject);
        }

    }
    void Update()
    {
        if (isDone == false)
        {
            return;
        }
        transform.Rotate(new Vector3(0, 0,
            right == true ? -360f : 360f) * Time.deltaTime * weaponSpinSpeed);
    }

    public void SetForce(Vector2 _force, bool _isRight)
    {
        force = _force;
        right = _isRight;
    }

}
