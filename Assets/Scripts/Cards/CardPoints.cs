using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class CardPoints {

	public static int maxPoints = 21;

	enum Points {
		Ace = 11,
		Two = 2,
		Three = 3,
		Four = 4,
		Five = 5,
		Six = 6,
		Seven = 7,
		Eight = 8,
		Nine = 9,
		Ten = 10,
		Jack = 2,
		Queen = 3,
		King = 4
	}

	static Dictionary<string, int> pointValues = new Dictionary<string, int>();

	static CardPoints() {
		string[] values = Enum.GetNames(typeof(Points));
		foreach (string name in values) {
			pointValues[name] = (int)Enum.Parse(typeof(Points), name);
		}
	}

	public static int GetPoints(CardTile tile) {
		return pointValues [tile.ToString ()];
	}
}

