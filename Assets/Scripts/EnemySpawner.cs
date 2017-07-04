using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] List<GameObject> m_Enemies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//TEMP
		if(transform.childCount == 0)
			SpawnEnemies();
	}

	void SpawnEnemies(){
		
	}
}