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
        // �ټ��� ������Ʈ�� �ʿ��ϹǷ� Components, �ڽ� ������Ʈ�� ����ϹǷ� InChildren
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);  // ������ gameTIme�� �������� ����

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);

        // GetComponentsInChildren���� 0�� �ڱ��ڽ��� �ǹ��ϱ� ������ �ڽ� ������Ʈ 16���� ����ϰ� �ʹٸ� 1���� Range ����
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        // enemy�� Enemy������Ʈ���� Init�Լ� ���� (level�� ���� spawnData�� �ٲ�)
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
