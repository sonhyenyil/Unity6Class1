using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;
    private Vector2 mouseWPCheckVal; //마우스값 위치를 표시하기 위한 저장Vector값
    [SerializeField] Transform trsHand;
    [SerializeField] GameObject objThrowWeapons;
    [SerializeField] Transform trsWeapons;
    [SerializeField] Transform TrsDynamic;
    [SerializeField] Vector2 throwForce = new Vector2(10f, 0f);

    private void Start()
    {
        mainCam = Camera.main;//메인카메라
        //카메라가 2개 이상이 되는 경우가 존재함
        //Camera.current; //현재 카메라를 가져오게 하는 코드
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
        //fixedPos.x > 0 또는 trasnform.localScale.x -1 => 오른쪽, fixedPos.x < 0 또는, transform.localScale.x 1 => 왼쪽

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
