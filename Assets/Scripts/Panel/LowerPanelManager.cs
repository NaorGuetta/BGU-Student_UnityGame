using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LowerPanelManager : MonoBehaviour
{
    public TextMeshProUGUI daysSurvivedCounter;
    private int daysSurvived = 0;

    private void OnEnable()
    {
        CardBehaviour.OnCardDestroyed += IncrementCounter;
    }

    private void OnDisable()
    {
        CardBehaviour.OnCardDestroyed -= IncrementCounter;
    }

    private void IncrementCounter(CardAction cardAction)
    {
        ++daysSurvived;
        if (PlayerPrefs.HasKey("HighScoreDays"))
        {
            if (PlayerPrefs.GetInt("HighScoreDays") < daysSurvived - 1)
            {
                PlayerPrefs.SetInt("HighScoreDays", daysSurvived - 1);
            }
        }
        else {
            PlayerPrefs.SetInt("HighScoreDays",0);
        }
        daysSurvivedCounter.text = daysSurvived.ToString();
    }


    public void resetDaysSurvived() {
        daysSurvived = 0;
        daysSurvivedCounter.text = "0";
    }
}
