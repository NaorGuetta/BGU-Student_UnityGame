using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDescriptionDisplay : MonoBehaviour
{

	private static readonly List<CardDescriptionDisplay> _changeListeners = new List<CardDescriptionDisplay>();

	public TextMeshProUGUI cardText;
	public TextMeshProUGUI characterNameText;
	public Image arrow_Img;

	private void Awake()
	{
		arrow_Img.enabled = !PlayerPrefs.HasKey("FirstGame");
		if (gameObject.scene.IsValid())
		{
			_changeListeners.Add(this);
			ResetDescription();
		}
	}

	public static void SetDescription(string cardCaption, string characterName)
	{
		SetAllDisplays(cardCaption, characterName);
	}

	public static void ResetDescription()
	{
		SetDescription("", "");
	}

	private static void SetAllDisplays(string cardCaption, string characterName)
	{
		for (int i = 0; i < _changeListeners.Count; i++)
		{
			if (_changeListeners[i] == null)
			{
				_changeListeners.RemoveAt(i);
			}
			else
			{
				_changeListeners[i].SetDisplay(cardCaption, characterName);
			}
		}
	}

	private void SetDisplay(string cardCaption, string characterName)
	{
		cardText.text = cardCaption;
		if (characterName.Equals("החלק כדי לנסות שוב"))
		{
			characterNameText.enabled = true;
			characterNameText.text = characterName;
		}
		else {
			characterNameText.enabled = false;
		}
    }

	private void OnEnable()
	{
		DeckOfCards.OnFirstCard += HandleFirstCardEvent;
	}

	private void OnDisable()
	{
		DeckOfCards.OnFirstCard -= HandleFirstCardEvent;
	}

	private void HandleFirstCardEvent()
	{
		arrow_Img.enabled = false;
	}
}
