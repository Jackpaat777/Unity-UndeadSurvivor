using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리펩 보관 변수
    public GameObject[] prefabs;

    // 풀 담당 리스트
    List<GameObject>[] pools;

    void Awake()
    {
        // 프리펩의 길이만큼 리스트 생성
        pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i < pools.Length; i++)
        {
            // 각 리스트를 생성자를 통해 초기화
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀의 비활성화된 오브젝트 접근
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // 발견하면 select에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // select == null이라 해도 되지만 데이터가 없는 경우를 의미하는 !select로 써도 됨
        if (!select)
        {
            // 못 찾았을 경우 새롭게 생성하여 select에 할당
            // Hierarchy창이 아닌 PoolManager 아래에 할당하기 위해 transform이라 지정
            select = Instantiate(prefabs[index], transform);

            // 오브젝트를 새로 생성했으니 풀에 등록
            pools[index].Add(select);
        }

        return select;
    }
}
