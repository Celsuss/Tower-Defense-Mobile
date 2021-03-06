﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	[SerializeField] float m_Health;
	[SerializeField] int m_GoldValue;

	public float Health{
		get{ return m_Health; }
		set{ m_Health = value; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(m_Health <= 0)
			KillEnemy();
	}

	void KillEnemy(){
		GameManager.Instance.Gold += m_GoldValue;
		Destroy( gameObject );
	}
}