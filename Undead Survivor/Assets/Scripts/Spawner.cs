using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    void Awake()
    {
        // 다수의 컴포넌트가 필요하므로 Components, 자식 오브젝트를 사용하므로 InChildren
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);  // 레벨은 gameTIme이 지날수록 오름

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);

        // GetComponentsInChildren에서 0은 자기자신을 의미하기 때문에 자식 오브젝트 16개를 사용하고 싶다면 1부터 Range 설정
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        // enemy의 Enemy컴포넌트에서 Init함수 실행 (level에 따라 spawnData가 바뀜)
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}
