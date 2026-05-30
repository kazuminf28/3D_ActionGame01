using System.Collections.Generic;
using UnityEngine;

public class EnemyListManager : MonoBehaviour
{
    public List<Transform> EnemyList = new List<Transform>();


    void Update()
    {
        for(int i = 0; i < EnemyList.Count; i++)
        {
            for(int j = i + 1; j < EnemyList.Count; j++)
            {
                if (EnemyList[i] == EnemyList[j])
                {
                    EnemyList.RemoveAt(j);
                }
            }
            if (!EnemyList[i])
            {
                EnemyList.RemoveAt(i);
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            EnemyList.Add(collider.gameObject.transform);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (EnemyList[i] == collider.gameObject.transform)
                {
                    EnemyList.RemoveAt(i);
                }
            }
        }
    }
}
