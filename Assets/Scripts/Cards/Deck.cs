using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour {
	
	private Queue<FaceValue> cards;

	void Awake () {
		cards = CreateDeck ();
	}

	Queue<FaceValue> CreateDeck() {
		Queue<FaceValue> deck = new Queue<FaceValue> ();
		List<FaceValue> cardsList = GetAllCards ();
		int index;
		int count = cardsList.Count;
		while (count != 0) {
			index = Random.Range (0, count);
			deck.Enqueue (cardsList [index]);
			cardsList.RemoveAt (index);
			count--;
		}

		return deck;
	}

	public static List<FaceValue> GetAllCards() {
		List<FaceValue> cardsList = new List<FaceValue> ();
		FaceValue card = null;
		Array suitValues = Enum.GetValues (typeof(CardSuit));
		Array tileValues = Enum.GetValues (typeof(CardTile));
		foreach (CardSuit suit in suitValues) {
			foreach (CardTile tile in tileValues) {
				card = new FaceValue(suit, tile);
				cardsList.Add (card);
			}
		}

		return cardsList;
	}

	public FaceValue GetCard() {
		return cards.Dequeue ();
	}
}
