using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piy : MonoBehaviour
{


    private Vector3 movement = Vector3.zero;
    private Rigidbody rb;
    private Animator animator;

    [HeaderAttribute("회전 속도")]
    public float turnSpeed = 20;
    private Quaternion rotation = Quaternion.identity;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //movement.Set : 벡터에 값을 저장
        movement.Set(horizontal, 0f, vertical);
        //movement.Normalize() : 벡터 정규화
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        //근삿값으로 정지인지 움직이는지 판별
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //가로 세로로 둘 다 안움직인다면 isWalking false로 설정

        animator.SetBool("isWalking", isWalking);
        //Time.fixedDeltaTime: fixedupdate 한 프레임당 걸리는 시간
        //Time.deltaTime - Update랑 짝임
        Vector3 desiredForword = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.fixedDeltaTime, 0f);
        //Quaternion : 방향벡터를 직접 쳐다보게 만든다
        rotation = Quaternion.LookRotation(desiredForword);
    }

    private void OnAnimatorMove()
    {
        //rb.MovePosition : RigidBody를 통한 위치 이동
        //애니메이션당 한 발자국
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        //rb.MoveRotation : RigidBody를 통한 회전
        rb.MoveRotation(rotation);
    }
}
