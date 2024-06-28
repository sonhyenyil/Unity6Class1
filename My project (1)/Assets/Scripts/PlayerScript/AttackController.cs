using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;
    private Vector2 mouseWPCheckVal; //���콺�� ��ġ�� ǥ���ϱ� ���� ����Vector��
    [SerializeField] Transform trsHand;
    [SerializeField] GameObject objThrowWeapons;
    [SerializeField] Transform trsWeapons;
    [SerializeField] Transform TrsDynamic;
    [SerializeField] Vector2 throwForce = new Vector2(10f, 0f);

    private void Start()
    {
        mainCam = Camera.main;//����ī�޶�
        //ī�޶� 2�� �̻��� �Ǵ� ��찡 ������
        //Camera.current; //���� ī�޶� �������� �ϴ� �ڵ�
    }

    void Update()
    {
        checkAim();
        checkCreate();
    }

    private void checkAim()
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 plyerPos = transform.position;
        Vector2 fixedPos = mouseWorldPos - plyerPos;
        //fixedPos.x > 0 �Ǵ� trasnform.localScale.x -1 => ������, fixedPos.x < 0 �Ǵ�, transform.localScale.x 1 => ����

        float angle = Quaternion.FromToRotation(
            transform.localScale.x < 0? Vector3.right : Vector3.left,
            fixedPos).eulerAngles.z;

        trsHand.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void checkCreate() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            createWeapons();
        }
        
    }

    private void createWeapons()
    {
       GameObject go = Instantiate(objThrowWeapons, trsWeapons.position, trsWeapons.rotation, TrsDynamic);
        ThrowWeapon goSc = go.GetComponent<ThrowWeapon>();
        bool isRight = transform.localScale.x < 0 ? true : false;
        Vector2 fixedThrowForce = throwForce;
        if (isRight == false)
        {
            fixedThrowForce = -throwForce;
        }
        goSc.SetForce(trsWeapons.rotation * fixedThrowForce, isRight);
    }
}
