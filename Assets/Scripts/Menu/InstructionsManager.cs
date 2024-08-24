using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject UsPanel;
    public GameObject backUsButton;
    public GameObject LayStart;
    public GameObject LayBack;
    public GameObject backButton;
    public GameObject playButton;
    public GameObject usButton;
    public GameObject instructionsButton;

    private void Start()
    {
        instructionsPanel.SetActive(false);
        UsPanel.SetActive(false);
    }

    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
        backButton.SetActive(true);
        LayBack.SetActive(true);
        playButton.SetActive(false);
        instructionsButton.SetActive(false);
        usButton.SetActive(false);
        LayStart.SetActive(false);
        UsPanel.SetActive(false);
        backUsButton.SetActive(false);
}

    public void HideInstructions()
    {
        instructionsPanel.SetActive(false);
        backButton.SetActive(false);
        LayBack.SetActive(false);
        playButton.SetActive(true);
        instructionsButton.SetActive(true);
        usButton.SetActive(true);
        LayStart.SetActive(true);
        UsPanel.SetActive(false);
        backUsButton.SetActive(false);
    }

    public void ShowUs()
    {
        instructionsPanel.SetActive(false);
        backButton.SetActive(false);
        LayBack.SetActive(false);
        playButton.SetActive(false);
        instructionsButton.SetActive(false);
        usButton.SetActive(false);
        LayStart.SetActive(false);
        UsPanel.SetActive(true);
        backUsButton.SetActive(true);
    }

    public void HideUs()
    {
        instructionsPanel.SetActive(false);
        backButton.SetActive(false);
        LayBack.SetActive(false);
        playButton.SetActive(true);
        instructionsButton.SetActive(true);
        usButton.SetActive(true);
        LayStart.SetActive(true);
        UsPanel.SetActive(false);
        backUsButton.SetActive(false);
    }
}
