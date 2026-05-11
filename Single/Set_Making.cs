using UnityEngine;
using System.Collections;

public class Set_Making : MonoBehaviour {
    public GameObject block;
    public GameObject[] item;
    GameObject[] blockset = new GameObject[6];
    // Use this for initialization
    void Start()
    {
        int level = Game_Manager.level/2;
        int a = Random.Range(0, 100);
        int p;
        if (a < 80-level)
        {
            p = Random.Range(100, 200);
            if (p < 145)
                blockset[Random.Range(0, 5)] = item[0];     // 공 추가
            else if (p < 160)
                blockset[Random.Range(0, 5)] = item[1];     // 공 추가 2
            else if (p < 175)
                blockset[Random.Range(0, 5)] = item[2];     // 바 길게
            else if (p < 195)
                blockset[Random.Range(0, 5)] = item[6];     // 쉴드
            else if (p < 200)
                blockset[Random.Range(0, 5)] = item[3];     // 공 데미지 x2
        }
        else {
            p = Random.Range(100, 200);
            if (p < 140)
                blockset[Random.Range(0, 5)] = item[4];     // 바 짧게
            else if (p < 180)
                blockset[Random.Range(0, 5)] = item[7];     // 슬로우
            else
                blockset[Random.Range(0, 5)] = item[5];     // 공 투명하게
        }
        for (int i = 0; i < 6; i++)
        {
            if (blockset[i] == null)
            {
                blockset[i] = block;
            }
            Instantiate(blockset[i], new Vector2(transform.position.x - 10 + 4 * i, 18.3f), Quaternion.identity);
        }
    }
}
