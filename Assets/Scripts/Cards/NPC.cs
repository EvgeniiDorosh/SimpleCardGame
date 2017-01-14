using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IParticipant 
{
	int threshold = 17;
	float[] chancesToRisk = new float[] {0, 0, 0.154f, 0.308f, 0.462f};

	CardsSet cardSet;

	void Awake() 
	{
		cardSet = GetComponent<CardsSet> ();
	}

	public void MakeStep () 
	{
		if(OnActivate != null) OnActivate (true);
		StartCoroutine(MakeDecision());
	}

	IEnumerator MakeDecision() 
	{
		yield return new WaitForSeconds (Random.Range (3f, 5f));
		int pointsToLose = CardPoints.maxPoints - cardSet.Points;
		if (pointsToLose > 0) {
			if (cardSet.Points < threshold) {
				OnHit ();	
			} else if (Random.value < chancesToRisk [pointsToLose]) {
				Debug.Log ("Risk chance was " + chancesToRisk [pointsToLose]);
				OnHit ();
			} else {
				OnStand ();
			}
		} else {
			OnStand ();
		}

		if(OnActivate != null) OnActivate (false);
		StopCoroutine(MakeDecision());
	}

	public event HitHandler OnHit;
	public event StandHandler OnStand;
	public event ActivateHandler OnActivate;
}
