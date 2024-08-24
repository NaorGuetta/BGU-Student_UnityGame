using UnityEngine;
using UnityEngine.UI;

public class UpperPanelManager : MonoBehaviour
{
    public Image moneyFront;
    public Image studyFront;
    public Image friendsFront;
    public Image healthFront;

    private Color32 gameOverColor = new Color32(244, 67, 54, 255); // #F44336 - Red of some type
    private Color32 defaultColor = new Color32(255, 255, 255, 255); // White

    public void UpdateFrontImages(GameStats gameStats)
    {
        // Update the fronts based on the stats
        UpdateFront(moneyFront, gameStats.MoneyPercentage);
        UpdateFront(friendsFront, gameStats.FriendsPercentage);
        UpdateFront(studyFront, gameStats.GradesPercentage);
        UpdateFront(healthFront, gameStats.HealthPercentage);
    }

    private void UpdateFront(Image front, float percentage)
    {
        if (percentage <= 0)
        {
            front.fillAmount = 1.0f;
            front.color = gameOverColor;
        }
        else
        {
            front.fillAmount = Mathf.Clamp01(percentage);
        }
    }

    public void ResetStatsImages(GameStats gameStats) {
        ResetFront(moneyFront, gameStats.MoneyPercentage);
        ResetFront(friendsFront, gameStats.FriendsPercentage);
        ResetFront(studyFront, gameStats.GradesPercentage);
        ResetFront(healthFront, gameStats.HealthPercentage);
    }

    private void ResetFront(Image front, float percentage)
    {
        front.fillAmount = Mathf.Clamp01(percentage);
        front.color = defaultColor;
    }
}
