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
}
