using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;//메인카메라
        //카메라가 2개 이상이 되는 경우가 존재함
        //Camera.current; //현재 카메라를 가져오게 하는 코드
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
