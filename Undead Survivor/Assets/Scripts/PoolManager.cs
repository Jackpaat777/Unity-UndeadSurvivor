using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ������ ���� ����
    public GameObject[] prefabs;

    // Ǯ ��� ����Ʈ
    List<GameObject>[] pools;

    void Awake()
    {
        // �������� ���̸�ŭ ����Ʈ ����
        pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i < pools.Length; i++)
        {
            // �� ����Ʈ�� �����ڸ� ���� �ʱ�ȭ
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� ��Ȱ��ȭ�� ������Ʈ ����
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // �߰��ϸ� select�� �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // select == null�̶� �ص� ������ �����Ͱ� ���� ��츦 �ǹ��ϴ� !select�� �ᵵ ��
        if (!select)
        {
            // �� ã���� ��� ���Ӱ� �����Ͽ� select�� �Ҵ�
            // Hierarchyâ�� �ƴ� PoolManager �Ʒ��� �Ҵ��ϱ� ���� transform�̶� ����
            select = Instantiate(prefabs[index], transform);

            // ������Ʈ�� ���� ���������� Ǯ�� ���
            pools[index].Add(select);
        }

        return select;
    }
}
