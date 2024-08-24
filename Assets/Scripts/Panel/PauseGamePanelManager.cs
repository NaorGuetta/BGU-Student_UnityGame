using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGamePanelManager : MonoBehaviour
{
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        HidePausePanel();
    }

    public void ShowPausePanel() {
        pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void EndGameToMenu()
    {
        PlayerPrefs.DeleteKey("GamesInARow");
        SceneManager.LoadScene("MenuScene");
    }
}
