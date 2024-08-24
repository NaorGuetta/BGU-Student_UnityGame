using UnityEngine;

[System.Serializable]
public class Card
{
    public int id;                      // Unique identifier for the card
    public string CardText;             // The main text on the card
    public string CharacterName;        // The name of the character
    public int CardImageIndex;            // The image associated with the CharacterName
    public CardAction LeftSwipeAction;  // Action to perform on left swipe
    public CardAction RightSwipeAction; // Action to perform on right swipe
    public int special;
    public bool isfollowUp;

    public string LeftSwipeText => LeftSwipeAction.actionText;   // Text shown when swiping left
    public string RightSwipeText => RightSwipeAction.actionText; // Text shown when swiping right

    // Constructor to initialize the card with actions
    public Card(int id, string cardText, string characterName, int cardImageIndex, CardAction leftSwipeAction, CardAction rightSwipeAction, int special, bool isfollowUp)
    {
        this.id = id;
        CardText = cardText;
        CharacterName = characterName;
        CardImageIndex = cardImageIndex;
        LeftSwipeAction = leftSwipeAction;
        RightSwipeAction = rightSwipeAction;
        this.special = special;
        this.isfollowUp = isfollowUp;
    }
}
