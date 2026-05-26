using UnityEngine;

public class GameManeger : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
