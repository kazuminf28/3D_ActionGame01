using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private RankingController rank;

    private float timer;
    private bool isStop = false;

    // Update is called once per frame
    void Update()
    {
        if (isStop) return;
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int milliseconds = Mathf.FloorToInt((timer * 100) % 100);

        timerText.text = minutes.ToString("00") + ":" +
                        seconds.ToString("00") + ":" +
                        milliseconds.ToString("00");
    }

    public void ResetTimer()
    {
        timer = 0;
    }
    public void ClearTime()
    {
        rank.SaveClearTime(timer);
    }

    public void StopTimer()
    {
        isStop = true;
    }

    public void StartTimer()
    {
        isStop = false;
    }
}
