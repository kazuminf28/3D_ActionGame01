using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    private RankingController rank;
    float cleartime;
    private int rankingmax = 5;
    public TextMeshProUGUI RankingText;
    public TextMeshProUGUI ClearTimeText;
    void Start()
    {
        cleartime = PlayerPrefs.GetFloat("ClearTime", -1);
        ShowClearTime();
        RankUpdate();
        ShowRanking();
    }

    void ShowClearTime()
    {
        ClearTimeText.text = "クリアタイム: ";
        if (cleartime >= 0)
        {
            ClearTimeText.text = FormatTime(cleartime);
        }
    }

    void RankUpdate()
    {
        if (cleartime >= 0)
        {
            rank.UpdateRanking(cleartime);
            PlayerPrefs.DeleteKey("ClearTime");
            Debug.Log("更新!!");
        }
    }

    void ShowRanking()
    {
        RankingText.text = "";
        for (int i = 0; i < rankingmax; i++)
        {
            float time = PlayerPrefs.GetFloat("Rank" + i, -1);
            if (time >= 0)
            {
                RankingText.text += (i + 1) + "位 " + FormatTime(time) + "\n";
            }
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 100) % 100);

        return minutes.ToString("00") + ":" +
                        seconds.ToString("00") + ":" +
                        milliseconds.ToString("00");
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
