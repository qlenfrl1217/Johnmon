using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piy : MonoBehaviour
{


    private Vector3 movement = Vector3.zero;
    private Rigidbody rb;
    private Animator animator;

    [HeaderAttribute("ȸ�� �ӵ�")]
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
        //movement.Set : ���Ϳ� ���� ����
        movement.Set(horizontal, 0f, vertical);
        //movement.Normalize() : ���� ����ȭ
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        //�ٻ����� �������� �����̴��� �Ǻ�
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //���� ���η� �� �� �ȿ����δٸ� isWalking false�� ����

        animator.SetBool("isWalking", isWalking);
        //Time.fixedDeltaTime: fixedupdate �� �����Ӵ� �ɸ��� �ð�
        //Time.deltaTime - Update�� ¦��
        Vector3 desiredForword = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.fixedDeltaTime, 0f);
        //Quaternion : ���⺤�͸� ���� �Ĵٺ��� �����
        rotation = Quaternion.LookRotation(desiredForword);
    }

    private void OnAnimatorMove()
    {
        //rb.MovePosition : RigidBody�� ���� ��ġ �̵�
        //�ִϸ��̼Ǵ� �� ���ڱ�
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        //rb.MoveRotation : RigidBody�� ���� ȸ��
        rb.MoveRotation(rotation);
    }
}
