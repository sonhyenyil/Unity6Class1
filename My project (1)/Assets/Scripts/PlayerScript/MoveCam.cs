using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [SerializeField] Transform chaseTrs;

    void Update()
    {
        Vector3 fixedPos = chaseTrs.position;

        fixedPos.z = transform.position.z;
        transform.position = fixedPos;//z����� ���󰡰� �ȴ�.
        //transform.position = chaseTrs.position;//z����� ���󰡰� �ȴ�.
    }
}
