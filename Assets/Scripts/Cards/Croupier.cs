using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croupier : MonoBehaviour 
{
	public delegate void OnGameFinishHandler(WinState winState);
	public event OnGameFinishHandler OnGameFinish;

	public GameObject player;
	CardsSet playerSet;

	public GameObject npc;
	CardsSet npcSet;

	GameObject currentHand;

	public Deck deck;
	public GameObject cardPrefab;

	int standValue = 2;

	void Awake() 
	{
		playerSet = player.GetComponent<CardsSet> ();
		npcSet = npc.GetComponent<CardsSet> ();
		currentHand = player;
		SetState (GameState.Dealing);
	}

	void OnEnable() 
	{
		IParticipant playerParticipant = player.GetComponent<IParticipant> ();
		playerParticipant.OnHit += OnParticipantHit;
		playerParticipant.OnStand += OnParticipantStand;
		IParticipant npcParticipant = npc.GetComponent<IParticipant> ();
		npcParticipant.OnHit += OnParticipantHit;
		npcParticipant.OnStand += OnParticipantStand;
	}

	void GiveCard(CardsSet cardSet, bool isHidden) 
	{
		GameObject cardClone = Instantiate (cardPrefab, deck.transform.position, Quaternion.identity);
		Card card = cardClone.GetComponent<Card> ();
		card.CardFaceValue = deck.GetCard ();
		if (isHidden)
			card.Flip (true);
		cardSet.AddCard (cardClone);
	}

	void SetState(GameState state) 
	{
		switch (state) 
		{
		case GameState.Dealing:
			StartCoroutine (DealStartCards());
			break;
		case GameState.Action:			
			WaitingForAction ();
			break;
		case GameState.Revealing:
			StartCoroutine (Revealing());
			break;
		case GameState.Finishing:
			Finish ();
			break;
		default:
			break;
		}
	}

	IEnumerator DealStartCards() 
	{
		WaitForSeconds waiting = new WaitForSeconds (1f);
		for (int i = 0; i < 2; i++) 
		{
			yield return waiting;
			GiveCard (npcSet, true);
			yield return waiting;
			GiveCard (playerSet, false);
		}
		StopCoroutine (DealStartCards ());
		SetState (GameState.Action);
	}

	void WaitingForAction() 
	{
		if (IsGameOver) {
			SetState (GameState.Revealing);
		} else {
			currentHand.GetComponent<IParticipant>().MakeStep ();
		}
	}

	void OnParticipantHit() 
	{
		GiveCard (currentHand.GetComponent<CardsSet> (), currentHand == npc);
		SwitchHand ();
		SetState (GameState.Action);
	}

	void OnParticipantStand() 
	{
		SwitchHand ();
		standValue--;
		SetState (GameState.Action);
	}

	void SwitchHand() 
	{
		if (standValue < 2)
			return;
		currentHand = (currentHand == player) ? npc : player;
	}

	IEnumerator Revealing()
	{
		WaitForSeconds waiting = new WaitForSeconds (1f);
		yield return waiting;
		foreach (Card card in npcSet.Cards) 
		{
			card.Flip ();
			yield return waiting;
		}
		StopCoroutine (Revealing ());
		SetState (GameState.Finishing);
	}

	void Finish()
	{
		WinState state = GetWinner ();
		OnGameFinish (state);
	}

	WinState GetWinner() {
		if ((playerSet.IsOverfull && npcSet.IsOverfull) || playerSet.Points == npcSet.Points) {
			return WinState.Draw;			
		} else if (!playerSet.IsOverfull && (playerSet.Points > npcSet.Points || npcSet.IsOverfull)) {
			return WinState.Player;
		} else {
			return WinState.NPC;
		}
	}

	bool IsGameOver 
	{
		get {
			if (playerSet.IsOverfull || npcSet.IsOverfull) {
				return true;
			} else if (standValue == 0) {
				return true;
			} else {
				return false;
			}
		}	
	}
}
