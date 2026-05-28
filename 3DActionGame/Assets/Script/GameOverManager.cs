using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
