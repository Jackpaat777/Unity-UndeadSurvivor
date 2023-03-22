using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    Vector2 inputVec;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // �������� ���ư��� �� ����
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;

        // MovePosition�� ���� �̵� ����
        rigid.MovePosition(rigid.position + nextVec);
    }

    // LateUpdate : �������� ����Ǳ� ������ ����Ǵ� �����ֱ��Լ�
    void LateUpdate()
    {
        // Flip X
        // �����̰� ���� ���� �¿���� ����
        if (inputVec.x != 0)
        {
            // true�� false�� ����Ǳ� ������ ������ ���� ���� ����
            spriter.flipX = inputVec.x < 0;
        }

        anim.SetFloat("Speed", inputVec.magnitude);
    }

    // Input System�� �̿��� �÷��̾� �̵�
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
