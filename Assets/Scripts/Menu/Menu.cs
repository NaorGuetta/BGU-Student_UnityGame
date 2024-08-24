using UnityEngine;

public class Menu : MonoBehaviour
{
    public FadeControllerMenu fadeController;
    public GameObject audioManagerPrefab;  // Reference to the GameAudioManager prefab
    private GameAudioManager gameAudioManager;  // Reference to the GameAudioManager instance

    void Awake()
    {
        if (GameAudioManager.instance == null)  // Check if the GameAudioManager already exists
        {
            Instantiate(audioManagerPrefab);  // Instantiate the prefab if it doesn't exist
        }
       /* //TO DO REMOVE THIS IS JUST FOR TEST AND DEBUGGING :
        PlayerPrefs.DeleteKey("FirstGame");
        PlayerPrefs.Save();*/

        // Reassign the GameAudioManager instance
        gameAudioManager = GameAudioManager.instance;
    }

    public void PlayGame()
    {
        fadeController.FadeToScene("Gameplay Scene"); 
    }
}
