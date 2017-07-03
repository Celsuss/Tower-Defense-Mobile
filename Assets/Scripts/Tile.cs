using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	enum BoardTileType{
		Ground,
		Wall,
		Road,
		Spawn,
		End
	}

	SpriteRenderer m_SpriteRenderer;
	BoardTileType m_TileType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}