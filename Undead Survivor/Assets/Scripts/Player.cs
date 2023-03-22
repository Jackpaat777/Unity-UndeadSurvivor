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
        // 다음으로 나아가야 할 벡터
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;

        // MovePosition을 통한 이동 구현
        rigid.MovePosition(rigid.position + nextVec);
    }

    // LateUpdate : 프레임이 종료되기 직전에 실행되는 생명주기함수
    void LateUpdate()
    {
        // Flip X
        // 움직이고 있을 때만 좌우반전 실행
        if (inputVec.x != 0)
        {
            // true와 false로 실행되기 때문에 연산을 직접 값에 적용
            spriter.flipX = inputVec.x < 0;
        }

        anim.SetFloat("Speed", inputVec.magnitude);
    }

    // Input System을 이용한 플레이어 이동
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
