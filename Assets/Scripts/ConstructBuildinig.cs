using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ConstructBuildinig : MonoBehaviour {

	[SerializeField] GameObject m_Prefab;
	BoardManager m_BoardManager;

	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find("Board Manager");
		Assert.IsNotNull(obj);
		BoardManager bm = obj.GetComponent<BoardManager>();
		Assert.IsNotNull(bm);
		m_BoardManager = bm;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			AddBuuilding();
		}
	}

	void AddBuuilding(){
		GameObject tile = m_BoardManager.GetTileClosestToMouse();
		for(int i = 0; i < tile.transform.childCount; i++){
			if( tile.transform.GetChild( i ).tag == "Building" )
				return;
		}

		GameObject newTile = Instantiate( m_Prefab, tile.transform.position, tile.transform.rotation, tile.transform );
	}
}