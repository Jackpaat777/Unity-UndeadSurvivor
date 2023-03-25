using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;  // 따라가기 위한 목표 (rigid의 position을 사용하기 위해 Rigidbody2D로 해줌)

    bool isLive;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;  // 물리속도없이 플레이어를 따라가는 로직 (물리속도가 존재하면 움직임이 이상해질 수 있음)
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }

    // 활성화 되었을 때 사용되는 생성주기함수
    void OnEnable()
    {
        // Enemy는 프리펩이기 때문에 target(Player의 rigid)을 직접 지정해서 넣어주어야함
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();

        // 활성화 될 때 초기화 해줘야 하는 변수들
        isLive = true;
        health = maxHealth;
    }

    // Enemy의 초기상태를 SpawnData를 통해 지정
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}
