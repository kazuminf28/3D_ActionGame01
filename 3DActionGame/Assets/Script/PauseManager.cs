using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject Controller;
    private bool isPause = false;

    void Start()
    {
        PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPause = true;
        PauseUI.SetActive(isPause);
        if (isPause)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OpenController()
    {
        isPause = false;
        PauseUI.SetActive(isPause);
        Controller.SetActive(true);
    }

    public void CloseController()
    {
        Controller.SetActive(false);
        isPause = true;
        PauseUI.SetActive(isPause);
    }

    public void ClosePause()
    {
        isPause = false;
        PauseUI.SetActive(isPause);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }

    public void QuiteGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("ゲーム終了");
    }
}
