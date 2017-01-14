using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour 
{
	[SerializeField]
	GameObject player;
	CardsSet playerSet;
	[SerializeField]
	Text playerPointsText;
	[SerializeField]
	Text playerScoreText;

	[SerializeField]
	GameObject npc;
	CardsSet npcSet;
	[SerializeField]
	Text npcPointsText;
	[SerializeField]
	Text npcScoreText;

	[SerializeField]
	Croupier croupier;
	[SerializeField]
	Text winText;

	void Awake() 
	{
		playerSet = player.GetComponent<CardsSet> ();
		npcSet = npc.GetComponent<CardsSet> ();
	}

	void OnEnable() 
	{
		playerSet.OnGetCard += UpdatePlayerPoints;
		croupier.OnGameFinish += ShowWinMessage;
		winText.enabled = false;
		npcPointsText.enabled = false;
		GameController gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		playerScoreText.text = gameController.playerScore.ToString();
		npcScoreText.text = gameController.npcScore.ToString();
	}

	void OnDisable()
	{
		playerSet.OnGetCard -= UpdatePlayerPoints;
		croupier.OnGameFinish -= ShowWinMessage;
	}

	void UpdatePlayerPoints() 
	{
		playerPointsText.text = playerSet.Points.ToString();
	}

	void ShowWinMessage(WinState winState) {
		npcPointsText.enabled = true;
		npcPointsText.text = npcPointsText.text.Replace ("0", npcSet.Points.ToString());
		winText.enabled = true;
		switch (winState) {
		case WinState.Player:
			winText.text = "You win!";
			break;
		case WinState.NPC:
			winText.text = "You lose!";
			break;
		case WinState.Draw:
			winText.text = "Draw";
			break;
		default:
			break;
		}
	}
}
