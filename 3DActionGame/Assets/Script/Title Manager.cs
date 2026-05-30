using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject TitleObject;
    public GameObject ConfirmPanel;
    public GameObject CreditObject;
    public GameObject ControllerObject;
    public GameObject GameExp;
    public RankingController rank;
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("ゲーム終了");
    }

    public void OpenConfirm()
    {
        TitleObject.SetActive(false);
        ConfirmPanel.SetActive(true);
    }

    public void RankRest()
    {
        rank.RankReset();
        ConfirmPanel.SetActive(false);
        TitleObject.SetActive(true);
    }

    public void CloseConfirm()
    {
        ConfirmPanel.SetActive(false);
        TitleObject.SetActive(true);
    }

    public void OpenCredit()
    {
        TitleObject.SetActive(false);
        CreditObject.SetActive(true);
    }

    public void CloseCredit()
    {
        CreditObject.SetActive(false);
        TitleObject.SetActive(true);
    }

    public void OpenController()
    {
        TitleObject.SetActive(false);
        ControllerObject.SetActive(true);
    }

    public void CloseController()
    {
        ControllerObject.SetActive(false);
        TitleObject.SetActive(true);
    }

    public void OpenGameExp()
    {
        TitleObject.SetActive(false);
        GameExp.SetActive(true);
    }

    public void CloseGameExp()
    {
        GameExp.SetActive(false);
        TitleObject.SetActive(true);
    }
}
