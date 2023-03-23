using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;    // Ground와 Enemy를 모두 사용하는 스크립트이므로 모든 Collider를 받을 수 있도록함

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    // Area를 벗어난 경우에만 함수 실행
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        // 플레이어의 위치값과 내 위치값을 뺀 벡터값을 구하기
        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        // player의 인풋 벡터값이 1, -1이 아닐 수도 있기 때문에 이를 바꾸어줌 (Input System때문에)
        dirX = dirX < 0 ? -1 : 1;
        dirY = dirY < 0 ? -1 : 1;

        Vector3 playerDir = GameManager.instance.player.inputVec;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)  // 플레이어가 Area에서 가로로 더 많이 움직였을 경우
                    transform.Translate(Vector3.right * dirX * 40); // dirX를 곱해주어 좌우를 구별 / 50을 곱하는 이유는 맵 하나를 더 건너뛰어야 하기 때문
                else                // 플레이어가 Area에서 세로로 더 많이 움직였을 경우
                    transform.Translate(Vector3.up * dirY * 40);
                break;
            case "Enemy":               // Enemy의 속도가 Player보다 느리기 때문에 Enemy가 Area를 벗어난 경우에도 재배치를 하도록 함
                if(coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));    // 플레이어의 이동방향의 맞은편에서 등장하도록 함
                }
                break;
        }

    }
}
