using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;  // ���󰡱� ���� ��ǥ (rigid�� position�� ����ϱ� ���� Rigidbody2D�� ����)

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
        rigid.velocity = Vector2.zero;  // �����ӵ����� �÷��̾ ���󰡴� ���� (�����ӵ��� �����ϸ� �������� �̻����� �� ����)
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }

    // Ȱ��ȭ �Ǿ��� �� ���Ǵ� �����ֱ��Լ�
    void OnEnable()
    {
        // Enemy�� �������̱� ������ target(Player�� rigid)�� ���� �����ؼ� �־��־����
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();

        // Ȱ��ȭ �� �� �ʱ�ȭ ����� �ϴ� ������
        isLive = true;
        health = maxHealth;
    }

    // Enemy�� �ʱ���¸� SpawnData�� ���� ����
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}
