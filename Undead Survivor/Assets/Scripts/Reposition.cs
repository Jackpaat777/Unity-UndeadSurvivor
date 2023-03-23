using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;    // Ground�� Enemy�� ��� ����ϴ� ��ũ��Ʈ�̹Ƿ� ��� Collider�� ���� �� �ֵ�����

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    // Area�� ��� ��쿡�� �Լ� ����
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        // �÷��̾��� ��ġ���� �� ��ġ���� �� ���Ͱ��� ���ϱ�
        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        // player�� ��ǲ ���Ͱ��� 1, -1�� �ƴ� ���� �ֱ� ������ �̸� �ٲپ��� (Input System������)
        dirX = dirX < 0 ? -1 : 1;
        dirY = dirY < 0 ? -1 : 1;

        Vector3 playerDir = GameManager.instance.player.inputVec;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)  // �÷��̾ Area���� ���η� �� ���� �������� ���
                    transform.Translate(Vector3.right * dirX * 40); // dirX�� �����־� �¿츦 ���� / 50�� ���ϴ� ������ �� �ϳ��� �� �ǳʶپ�� �ϱ� ����
                else                // �÷��̾ Area���� ���η� �� ���� �������� ���
                    transform.Translate(Vector3.up * dirY * 40);
                break;
            case "Enemy":               // Enemy�� �ӵ��� Player���� ������ ������ Enemy�� Area�� ��� ��쿡�� ���ġ�� �ϵ��� ��
                if(coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));    // �÷��̾��� �̵������� �������� �����ϵ��� ��
                }
                break;
        }

    }
}
