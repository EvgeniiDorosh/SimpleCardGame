using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceValue : IEquatable<FaceValue> 
{

	CardSuit suit = 0;
	public CardSuit Suit 
	{
		get { return suit;}
	}
	CardTile tile = 0;
	public CardTile Tile 
	{
		get { return tile; } 
	}
	int points = 0;
	public int Points 
	{
		get { return points;}
	}

	public FaceValue (CardSuit suit, CardTile tile) 
	{
		this.suit = suit;
		this.tile = tile;
		points = CardPoints.GetPoints(tile);		
	}

	public bool Equals (FaceValue other) 
	{
		if (other == null)
			return false;
		return this.suit.Equals(other.Suit) && this.tile.Equals(other.Tile);
	}

	public override bool Equals(object obj) 
	{
		if (obj == null) return false;
		FaceValue objAsFaceValue = obj as FaceValue;
		if (objAsFaceValue == null) return false;
		else return Equals(objAsFaceValue);
	}

	public override string ToString ()
	{
		return string.Format ("[FaceValue: Suit={0}, Tile={1}, Points={2}]", suit, tile, points);
	}

	public override int GetHashCode ()
	{
		return ((int)suit + 1) * 100 + (int)tile;
	}

	public static bool operator == (FaceValue value1, FaceValue value2)
	{
		return value1.Equals(value2);
	}

	public static bool operator != (FaceValue value1, FaceValue value2)
	{
		return ! (value1.Equals(value2));
	}
}
