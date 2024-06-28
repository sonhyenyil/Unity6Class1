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

    [Header("���� ����")]
    [SerializeField] float weaponSpinSpeed = 2.0f; //������ ȸ���ӵ��� �����ϴ� ����
    //[SerializeField, Tooltip("���� �����ð� ���̳� �ٴڿ� ������ ������ �ð��� �ı���")] float weaponTime = 1.0f;

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
