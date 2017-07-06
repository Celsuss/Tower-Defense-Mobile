using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
	[SerializeField] GameObject m_TilePrefab;
	[HideInInspector] public int m_Width, m_Height;
	[HideInInspector] public List<GameObject> m_Board = new List<GameObject>();
	public GameObject TilePrefab { get{ return m_TilePrefab; }}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject GetTile( Vector3 position ){
		for( int i = 0; i < transform.childCount; ++i ){
            float distanceToBlock = Vector3.Distance( transform.GetChild(i).transform.position, position );
            if( distanceToBlock < 0.1f ){
                return transform.GetChild(i).gameObject;
            }
        }
		return null;
	}

	public GameObject GetTileClosestToMouse(){
		GameObject obj = null;
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit hit;
        if( Physics.Raycast( ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer( "Level" ) ))
			obj = GetTile( hit.transform.position );
		return obj;
	}
}