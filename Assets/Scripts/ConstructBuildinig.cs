using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ConstructBuildinig : MonoBehaviour {

	[SerializeField] SpriteRenderer m_TowerBlueprintSprite = null;
	GameObject m_Tower;
	BoardManager m_BoardManager;
	bool m_PlacingBuilding = false;

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
		GameObject tile = m_BoardManager.GetTileClosestToMouse();

		if( Input.GetMouseButtonDown( 0 ) && m_PlacingBuilding && tile )
			AddBuuilding( tile );
		else if( Input.GetMouseButtonDown( 1 ) || Input.GetKeyDown( KeyCode.Escape ) )
			StopPlacingBuilding();

		ToggleTowerBlueprintEnabled( tile );
		UpdateTowerBlueprintPosition( tile );
	}

	void AddBuuilding (GameObject tile ){
		for( int i = 0; i < tile.transform.childCount; i++ ){
			if( tile.transform.GetChild( i ).tag == "Building" )
				return;
		}

		Assert.IsNotNull( m_Tower );
		Assert.IsNotNull( tile );
		GameObject newTile = Instantiate( m_Tower, tile.transform.position, tile.transform.rotation, tile.transform );

		if( !Input.GetKey( KeyCode.LeftControl ) )
			StopPlacingBuilding();
	}

	void UpdateTowerBlueprintPosition( GameObject tile ){
		if( !tile ) return;
		m_TowerBlueprintSprite.transform.position = tile.transform.position;
	}

	void ToggleTowerBlueprintEnabled( GameObject tile ){
		if(!tile)
			m_TowerBlueprintSprite.enabled = false;
		else if(tile && !m_TowerBlueprintSprite.enabled)
			m_TowerBlueprintSprite.enabled = true;
	}

	public void StartPlaceBuilding( GameObject tower ){
		m_Tower = tower;
		m_TowerBlueprintSprite.sprite = m_Tower.GetComponent<SpriteRenderer>().sprite;
		m_PlacingBuilding = true;
	}

	void StopPlacingBuilding(){
		m_Tower = null;
		m_TowerBlueprintSprite.sprite = null;
		m_PlacingBuilding = false;
	}
}