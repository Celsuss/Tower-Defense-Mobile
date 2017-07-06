using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EndArea : MonoBehaviour {

	BoxCollider m_Collider;
	GameManager m_GameManager;

	// Use this for initialization
	void Start () {
		BoxCollider collider = transform.GetComponent<BoxCollider>();
		Assert.IsNotNull(collider);
		m_Collider = collider;

		GameObject obj = GameObject.Find("Game Manager");
		Assert.IsNotNull(obj);
		GameManager gm = obj.GetComponent<GameManager>();
		Assert.IsNotNull(gm);
		m_GameManager = gm;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter( Collider other ){
		if( other.tag != "Enemy" ) return;

		m_GameManager.Life--;
		Destroy(other.gameObject);
	}
}