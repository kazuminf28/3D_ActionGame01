using System.Collections.Generic;
using UnityEngine;

public class RankingController : MonoBehaviour
{
    private int rankingmax = 5;
    public void UpdateRanking(float cleartime)
    {
        List<float> ranking = new List<float>();

        for(int i = 0; i < rankingmax; i++)
        {
            float time = PlayerPrefs.GetFloat("Rank" + i, -1);
            if (time >= 0)
            {
                ranking.Add(time);
            }
        }

        ranking.Add(cleartime);
        ranking.Sort();

        if (ranking.Count > rankingmax)
        {
            ranking.RemoveRange(rankingmax, ranking.Count - rankingmax);
        }

        for(int i = 0; i < ranking.Count; i++)
        {
            PlayerPrefs.SetFloat("Rank" + i, ranking[i]);
        }

        PlayerPrefs.Save();

        Debug.Log("保存完了");
    }

    public void DebugShowRanking()
    {
        Debug.Log("=== Ranking ===");

        for (int i = 0; i < rankingmax; i++)
        {
            float time = PlayerPrefs.GetFloat("Rank" + i, -1);

            if (time >= 0)
            {
                Debug.Log((i + 1) + "位 : " + time);
            }
        }
    }
}
