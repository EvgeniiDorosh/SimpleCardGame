using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsView : MonoBehaviour 
{
	[Serializable]
	public class PairSuitSprites 
	{
		public CardSuit suit = 0;
		public List<Sprite> sprites; 
	}

	public List<PairSuitSprites> pairs = new List<PairSuitSprites> ();
	Dictionary<CardSuit, List<Sprite>> cardsFaces = new Dictionary<CardSuit, List<Sprite>>();

	static CardsView instance = null;
	public static CardsView Instance 
	{
		get { return instance;}
	}

	void Awake() 
	{
		if (instance != null) 
		{
			Destroy (gameObject);
			return;
		}
		instance = this;
		InitializeSprites ();
	}

	void InitializeSprites()
	{
		foreach (PairSuitSprites pair in pairs) 
		{
			if (!cardsFaces.ContainsKey (pair.suit)) 
			{
				cardsFaces [pair.suit] = new List<Sprite> ();
			}
			cardsFaces [pair.suit].AddRange (pair.sprites);
		}
	}

	public Sprite GetFaceSkin(CardSuit suit, CardTile tile) 
	{
		if (cardsFaces.ContainsKey (suit)) 
			return cardsFaces [suit] [(int)tile];
		
		return null;
	}
}
