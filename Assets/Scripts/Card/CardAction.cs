using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardAction
{
    public CardStats cardStats;
    public string actionText;
    public List<int> followUpCardIds;

    public CardAction(CardStats stats, string text, List<int> followUpIds)
    {
        cardStats = stats;
        actionText = text;
        followUpCardIds = followUpIds;
    }

    // Default constructor for flexibility
    public CardAction()
    {
        cardStats = new CardStats();
        followUpCardIds = new List<int>();
    }
}
