using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;//����ī�޶�
        //ī�޶� 2�� �̻��� �Ǵ� ��찡 ������
        //Camera.current; //���� ī�޶� �������� �ϴ� �ڵ�
    }

    void Update()
    {
        checkAim();    
    }

    private void checkAim() 
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }
}
