using UnityEngine;
using UnityEngine.UI;

public class MuteButtonToggle : MonoBehaviour
{
    public Sprite unmuteSprite;
    public Sprite muteSprite;
    public Button muteButton;

    private void Start()
    {
        // Ensure the button starts with the correct image based on the audio state
        UpdateButtonImage();
    }

    public void ToggleAudio()
    {
        // Toggle the mute state in the GameAudioManager
        GameAudioManager.instance.ToggleMute();

        // Update the button's image based on the new state
        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        // Get the current audio state from the GameAudioManager
        bool isAudioEnabled = GameAudioManager.instance.GetIsAudioEnabled();

        // Update the button's image based on the audio state
        if (isAudioEnabled)
        {
            muteButton.image.sprite = muteSprite;
        }
        else
        {
            muteButton.image.sprite = unmuteSprite;
        }
    }
}
