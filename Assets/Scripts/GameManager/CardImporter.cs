using UnityEngine;
using System.Collections.Generic;

public class CardImporter
{
    public static List<Card> ImportCardsFromResources()
    {
        List<Card> cards = new List<Card>();

        // Load all JSON files in the Resources/Cards folder
        TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("Cards");

        // Parse each JSON file and add it to the cards list
        foreach (TextAsset jsonFile in jsonFiles)
        {
            Card card = JsonUtility.FromJson<Card>(jsonFile.text);
            cards.Add(card);
        }

        return cards;
    }
}
