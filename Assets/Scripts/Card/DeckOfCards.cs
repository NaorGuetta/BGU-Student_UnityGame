using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckOfCards
{
    public List<Card> cards;
    public List<Card> currentDeck;
    public List<List<Card>> specialCards;
    public Queue<int> followUp;
    private Queue<int> recentCards;
    private const int queueSize = 20;
    public static event System.Action OnFirstCard;

    public DeckOfCards()
    {
        cards = new List<Card>();
        currentDeck = new List<Card>();
        followUp = new Queue<int>(queueSize);
        recentCards = new Queue<int>(queueSize);
        specialCards = new List<List<Card>>();
        for (int i = 0; i < 5; i++)
        {
            specialCards.Add(new List<Card>());
        }
    }
    public void AddCard(Card card)
    {
        // add special cards
        if (card.special!=0){
            specialCards[card.special-1].Add(card);
        } // all cards 
        else { 
            cards.Add(card);
            if (!card.isfollowUp) {
                AddCardToCurrentDeck(card);
            }
        }
    }
    public void AddCardToCurrentDeck(Card card)
    {
        currentDeck.Add(card);
    }
    public int GetCardById(int id)
    {
        return cards.FindIndex(card => card.id == id);
    }
    public Card PullNextCard()
    {
        Card nextCard;
        if (!PlayerPrefs.HasKey("FirstGame"))
        {
            nextCard = GetFirstGameCard();

            PlayerPrefs.SetInt("FirstGame", 1);
            PlayerPrefs.Save();
        }
        else {
            OnFirstCard.Invoke();
            if (followUp.Count != 0) {
                Card followUpCard = cards[GetCardById(followUp.Dequeue())];
                return followUpCard;
            }
            // pick a random card and remove it from the deck
            int randomIndex = Random.Range(0, currentDeck.Count-1);
            nextCard = currentDeck[randomIndex];
            currentDeck.RemoveAt(randomIndex);
            // add the card index to the queue 
            ManageQueue(GetCardById(nextCard.id));
        }
        
        return nextCard;
    }

    private Card GetFirstGameCard()
    {
        return PullSpecialCard(4); // 4 is the index of card list were the instructions are !
    }


    public void addFollowUp(List<int> list)
    {
        foreach (int cardId in list)
        {
            followUp.Enqueue(cardId);
        }
    }

    public Card PullSpecialCard(int type) {
        //Debug.Log("specialCards[type].Count"+specialCards[type].Count);
        int randomIndex = Random.Range(0, specialCards[type].Count);
        return specialCards[type][randomIndex];
    }

    private void ManageQueue(int cardId) {
        // if the queue is full place the card back to the Deck
        if (recentCards.Count == queueSize) {
            AddCardToCurrentDeck(cards[recentCards.Dequeue()]);
        }
        recentCards.Enqueue(cardId);
    }
}
