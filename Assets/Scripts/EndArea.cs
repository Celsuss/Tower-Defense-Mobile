using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EndArea : MonoBehaviour {

	BoxCollider m_Collider;

	// Use this for initialization
	void Start () {
		BoxCollider collider = transform.GetComponent<BoxCollider>();
		Assert.IsNotNull(collider);
		m_Collider = collider;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter( Collider other ){
		if( other.tag != "Enemy" ) return;

		Destroy(other.gameObject);
	}
}