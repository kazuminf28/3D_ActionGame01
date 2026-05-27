using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public Transform PlayerPosition;
    private Timer TimeControlle;

    void Start()
    {
        TimeControlle.ResetTimer();
        Instantiate(Player, PlayerPosition.position, Quaternion.identity);
        TimeControlle.StartTimer();
    }

    public void GameClear()
    {
        TimeControlle.StopTimer();
        TimeControlle.ClearTime();
        SceneManager.LoadScene("ClearScene");
    }
}
