using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ConstructBuildinig : MonoBehaviour {

	[SerializeField] SpriteRenderer m_TowerBlueprintSprite = null;
	DrawTowerRange m_DrawTowerRange = null;
	BuildingShoot m_Tower;
	BoardManager m_BoardManager;
	bool m_PlacingBuilding = false;

	// Use this for initialization
	void Start () {
		Assert.IsNotNull(m_TowerBlueprintSprite);

		GameObject obj = GameObject.Find("Board Manager");
		Assert.IsNotNull(obj);
		BoardManager bm = obj.GetComponent<BoardManager>();
		Assert.IsNotNull(bm);
		m_BoardManager = bm;

		m_DrawTowerRange = GetComponentInChildren<DrawTowerRange>(true);
		Assert.IsNotNull(m_DrawTowerRange);

		// Deactivate draw tower range when not showing blueprint
		m_DrawTowerRange.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.Instance.GameOver){
			StopPlacingBuilding();
			return;
		}

		GameObject tile = m_BoardManager.GetTileClosestToMouse();

		if( Input.GetMouseButtonDown( 0 ) && m_PlacingBuilding && tile && tile.tag != "Road" )
			PlaceTowerOnTile( tile );
		else if( Input.GetMouseButtonDown( 1 ) || Input.GetKeyDown( KeyCode.Escape ) )
			StopPlacingBuilding();

		ToggleTowerBlueprintEnabled( tile );
		UpdateTowerBlueprintPosition( tile );
		ToggleTowerRangeBlueprintEnabled( tile );
	}

	void PlaceTowerOnTile(GameObject tile ){
		if(m_Tower.Cost > GameManager.Instance.Gold){
			StopPlacingBuilding();
			return;
		}

		for( int i = 0; i < tile.transform.childCount; i++ ){
			if( tile.transform.GetChild( i ).tag == "Building" )
				return;
		}

		Assert.IsNotNull( m_Tower );
		Assert.IsNotNull( tile );
		GameObject newTile = Instantiate( m_Tower.gameObject, tile.transform.position, tile.transform.rotation, tile.transform );
		GameManager.Instance.Gold -= m_Tower.Cost;

		if( !Input.GetKey( KeyCode.LeftShift ) )
			StopPlacingBuilding();
	}

	void UpdateTowerBlueprintPosition( GameObject tile ){
		if( !tile ) return;
		m_TowerBlueprintSprite.transform.position = tile.transform.position;
	}

	void ToggleTowerBlueprintEnabled( GameObject tile ){
		if( !tile || tile.tag == "Road" ){
			m_TowerBlueprintSprite.enabled = false;
			m_DrawTowerRange.LineColor = Color.red;
		}
		else if(tile && !m_TowerBlueprintSprite.enabled){
			m_TowerBlueprintSprite.enabled = true;
			m_DrawTowerRange.LineColor = Color.green;
		}
	}

	void ToggleTowerRangeBlueprintEnabled( GameObject tile ){
		if( !tile && m_DrawTowerRange.gameObject.activeSelf && m_PlacingBuilding )
			m_DrawTowerRange.gameObject.SetActive( false );
		else if( tile && !m_DrawTowerRange.gameObject.activeSelf && m_PlacingBuilding )
			m_DrawTowerRange.gameObject.SetActive( true );
	}

	public void StartPlaceBuilding( BuildingShoot tower ){
		if(tower.Cost > GameManager.Instance.Gold) return;

		m_Tower = tower;
		m_PlacingBuilding = true;

		Sprite sprite = m_Tower.GetComponent<SpriteRenderer>().sprite;
		Assert.IsNotNull(sprite);
		m_TowerBlueprintSprite.sprite = sprite;

		m_DrawTowerRange.Radius = tower.Range;
		m_DrawTowerRange.gameObject.SetActive(true);
	}

	void StopPlacingBuilding(){
		m_Tower = null;
		m_TowerBlueprintSprite.sprite = null;
		m_PlacingBuilding = false;
		m_DrawTowerRange.gameObject.SetActive(false);
	}
}