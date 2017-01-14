using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
	
	private FaceValue cardFaceValue;
	public FaceValue CardFaceValue {
		get {return cardFaceValue;}
		set { cardFaceValue = value;
			SetFace (value);
		}
	}

	bool isHidden = false;
	public bool IsHidden {
		get { return isHidden;}
	}
	bool flipping = false;
	float flipDuration = 1f;
	Quaternion rotationAngle = Quaternion.Euler(0, 180f, 0);

	public SpriteRenderer faceRenderer;

	void SetFace(FaceValue faceValue) {
		faceRenderer.sprite = CardsView.Instance.GetFaceSkin(faceValue.Suit, faceValue.Tile);
	}

	public void Flip(bool immediately = false) {
		if (flipping) {
			return;
		}

		if (immediately) {
			transform.Rotate (0, 180f, 0f);
			isHidden = !isHidden;
		} else {
			StartCoroutine (Flipping ());
		}
	}

	IEnumerator Flipping() {
		flipping = true;
		Quaternion startRotation = transform.rotation;
		Quaternion targetRotation = startRotation * rotationAngle;
		float elapsedTime = 0;
		while (elapsedTime < flipDuration) {
			transform.rotation = Quaternion.Slerp (startRotation, targetRotation, elapsedTime / flipDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.rotation = targetRotation;
		StopCoroutine (Flipping ());
		isHidden = !isHidden;
		flipping = false;
	}
}
