using UnityEngine;
using System.Collections.Generic;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;  // The card prefab to spawn
    public RectTransform cardPanel;  // Reference to the CardPanel
    public Sprite[] characterImages;  // Array of sprites for different characters

    public void SpawnCard(Card cardData)
    {
        // Instantiate the new card within the CardPanel
        GameObject newCard = Instantiate(cardPrefab, cardPanel);

        // Initialize the card with data
        CardBehaviour cardBehaviour = newCard.GetComponent<CardBehaviour>();
        //Debug.Log("cardData.CardImageIndex"+ cardData.CardImageIndex +"ID"+ cardData.id);
        cardBehaviour.Initialize(cardData, characterImages[cardData.CardImageIndex-1]);
    }

}
