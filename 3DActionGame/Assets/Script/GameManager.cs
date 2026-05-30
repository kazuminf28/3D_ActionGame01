using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public Transform PlayerPosition;
    public Timer TimeControlle;

    private GameObject playerObj;

    void Awake()
    {
        TimeControlle.ResetTimer();

        playerObj = Instantiate(Player, PlayerPosition.position, Quaternion.identity);

        TimeControlle.StartTimer();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
        // TimeControlle.ResetTimer();

        // GameObject playerObj =
        //     Instantiate(Player, PlayerPosition.position, Quaternion.identity);

        // CameraControls camera =
        //     FindAnyObjectByType<CameraControls>();

        // GameObject playerObj = Instantiate(Player, PlayerPosition.position, Quaternion.identity);

        Camera.main.GetComponent<CameraControls>().player = playerObj.transform.Find("look_player");

        // camera.player =
        //     playerObj.transform.Find("look_player");

        TimeControlle.StartTimer();
    }

    public void GameClear()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        TimeControlle.StopTimer();
        TimeControlle.ClearTime();
        SceneManager.LoadScene("ClearScene");
    }

    public void GameOver()
    {
        Debug.Log("this = " + gameObject.name);
        Debug.Log("TimeControlle = " + TimeControlle);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (TimeControlle == null)
        {
            Debug.LogError("Timerがnull");
            return;
        }
        Debug.Log(TimeControlle);
        TimeControlle.StopTimer();
        TimeControlle.ResetTimer();
        SceneManager.LoadScene("GameOverScene");
    }
}
