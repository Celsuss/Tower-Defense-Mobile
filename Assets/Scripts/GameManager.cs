using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField] int m_Life = 1;

	public int Life{
		get{ return m_Life; }
		set{ m_Life = value; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
