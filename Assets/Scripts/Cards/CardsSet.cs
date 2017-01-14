using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardsSet : MonoBehaviour 
{
	private List<Card> cards = new List<Card> ();
	public List<Card> Cards 
	{
		get {return cards;}
	}

	public Transform initialPosition;
	private float offsetX = 1f;
	private float moveDuration = 1f;

	public delegate void GetCardHandler();
	public event GetCardHandler OnGetCard;

	public void AddCard(GameObject card) 
	{
		StartCoroutine (MoveCard (card));
	}

	IEnumerator MoveCard(GameObject card) 
	{
		AddCard (card.GetComponent<Card> ());
		Transform cardTransform = card.transform;
		Vector3 startPosition = cardTransform.position;
		Vector3 endPosition = initialPosition.position + new Vector3(cards.Count * offsetX, 0, 0);
		float elapsedTime = 0;
		while (elapsedTime < moveDuration) 
		{
			cardTransform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		card.transform.position = endPosition;
		StopCoroutine (MoveCard(card));
	}

	void AddCard(Card card)
	{
		cards.Add (card);
		if(OnGetCard != null) OnGetCard ();
	}

	public int Points 
	{
		get	{ 
			int sum = 0;
			foreach (Card card in cards) {
				sum += card.CardFaceValue.Points;
			}

			return sum;		
		}
	}

	public bool IsOverfull 
	{
		get { return Points > CardPoints.maxPoints;}
	}
}

