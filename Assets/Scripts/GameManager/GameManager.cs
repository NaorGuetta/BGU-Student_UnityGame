using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public DeckOfCards deck;
    public CardSpawner cardSpawner;
    public UpperPanelManager upperPanelManager;
    public LowerPanelManager lowerPanelManager;
    public LowerPanelManager lowerPanelManagerFade;
    public PauseGamePanelManager pauseManager;
    private GameStats gameStats;
    public GameAudioManager gameAudioManager;
    private int numOfGames;


    private bool restartGame;

    private void Start()
    {
        /*//TO DO REMOVE THIS IS JUST FOR TEST AND DEBUGGING :
        PlayerPrefs.DeleteKey("FirstGame");
        PlayerPrefs.Save();*/
        gameAudioManager = GameAudioManager.instance;
        ReStartGame();
    }
    public void ReStartGame() {
        restartGame = false;
        FindObjectOfType<FadeController>().StartFadeAnimation();
    }
    public void StartGameSeq()
    {
        if (!PlayerPrefs.HasKey("NumOfGame"))
        {
            numOfGames = 1;
            PlayerPrefs.SetInt("NumOfGame", numOfGames);
        }
        else {
            numOfGames = PlayerPrefs.GetInt("NumOfGame") + 1;
            PlayerPrefs.SetInt("NumOfGame", numOfGames);
        }
        gameStats = new GameStats();
        gameStats.ResetStats(numOfGames);
        upperPanelManager.ResetStatsImages(gameStats);
        lowerPanelManager.resetDaysSurvived();
        lowerPanelManagerFade.resetDaysSurvived();

        deck = new DeckOfCards();
        CreateInitialCards();
        SpawnNextCard();
    }


    private void CreateInitialCards()
    {
        // Load the cards from the Resources folder
        List<Card> importedCards = CardImporter.ImportCardsFromResources();

        // Add imported cards to the deck
        foreach (Card card in importedCards)
        {
            deck.AddCard(card);
        }
    }

    private void SpawnNextCard()  // Added CardStats parameter to match the delegate
    {
        cardSpawner.SpawnCard(deck.PullNextCard());
    }

    private void OnEnable()
    {
        CardBehaviour.OnCardDestroyed += OnCardDestroyedHandler;
    }

    private void OnDisable()
    {
        CardBehaviour.OnCardDestroyed -= OnCardDestroyedHandler;
    }

    private void OnCardDestroyedHandler(CardAction cardAction)
    {
        if (restartGame) {
            gameAudioManager.PlaySwipeSound();
            ReStartGame();
            return;
        }
        /*Debug.Log("OnCardDestroyedHandler grades:" + cardAction.cardStats.grades);
        Debug.Log("OnCardDestroyedHandler friends: " + cardAction.cardStats.friends);
        Debug.Log("OnCardDestroyedHandler health: " + cardAction.cardStats.health);
        Debug.Log("OnCardDestroyedHandler money: " + cardAction.cardStats.money);*/
        // Modify the game stats with the card's stats
        gameStats.ApplyModification(cardAction.cardStats);
        // Update the UI with the new stats
        upperPanelManager.UpdateFrontImages(gameStats);
        // add Follow Up Cards if there is any 
        if (cardAction.followUpCardIds.Count != 0) {
            deck.addFollowUp(cardAction.followUpCardIds);
        }
        // Check if any stat has reached zero
        int endGameType = gameStats.CheckStatThreshold();
        if (endGameType > 0)
        {
            gameAudioManager.PlayResetSound();
            Debug.Log("endGameType" + endGameType);
            cardSpawner.SpawnCard(deck.PullSpecialCard(endGameType-1));
            EndGame();
        }
        else { 
            gameAudioManager.PlaySwipeSound();
            SpawnNextCard();
        }
    }

    private void EndGame()
    {
        //Debug.Log("Game Over! One of the stats reached zero.");
        restartGame = true;
    }
}
