using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	
	static GameController instance = null;
	public static GameController Instance 
	{
		get { return instance;}
	}

	public int playerScore = 0;
	public int npcScore = 0;

	void Awake() 
	{
		if (instance != null) 
		{
			Destroy (gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		Croupier cropier = GameObject.FindGameObjectWithTag ("Croupier").GetComponent<Croupier> ();
		cropier.OnGameFinish += OnGameFinish;
	}

	void OnGameFinish (WinState winState)
	{
		switch (winState) 
		{
		case WinState.Player:
			playerScore++;
			break;
		case WinState.NPC:
			npcScore++;
			break;
		default:
			break;
		}
		Invoke ("ReloadScene", 3f);
	}

	void ReloadScene()
	{
		CancelInvoke ();
		Croupier cropier = GameObject.FindGameObjectWithTag ("Croupier").GetComponent<Croupier> ();
		cropier.OnGameFinish -= OnGameFinish;
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
	}
}
