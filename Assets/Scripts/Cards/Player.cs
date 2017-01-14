using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IParticipant {

	bool isActive;
	int layerMask;

	[SerializeField]
	Button hitButton;
	[SerializeField]
	Button standButton;

	void Awake() {
		layerMask = LayerMask.GetMask ("Deck");
		SetActive (false);
	}

	void OnEnable() {
		hitButton.onClick.AddListener (Hit);
		standButton.onClick.AddListener (Stand);
	}

	void OnDisable() {
		hitButton.onClick.RemoveListener (Hit);
		standButton.onClick.RemoveListener (Stand);
	}

	void Update() {
		if (isActive && Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			bool isDeckClicked = Physics.Raycast (ray, 10f, layerMask);
			if (isDeckClicked) {
				Hit ();
			}
		}
	}

	public void MakeStep () 
	{
		SetActive (true);
	}

	void Hit() 
	{
		SetActive (false);
		OnHit ();
	}

	void Stand()
	{
		SetActive (false);
		OnStand ();
	}

	void SetActive(bool value) 
	{
		isActive = value;
		if(OnActivate != null) OnActivate (value);
		ActivateButtons (value);
	}

	void ActivateButtons(bool value) 
	{
		hitButton.interactable = value;
		standButton.interactable = value;
	}

	public event HitHandler OnHit;
	public event StandHandler OnStand;
	public event ActivateHandler OnActivate;
}
