using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] EnemyWaves m_EnemyWaves;
	List<GameObject> m_CurrentWave;

	// Use this for initialization
	void Start () {
		SpawnEnemies(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnEnemies( int waveIndex ){
		if( waveIndex < 0 || waveIndex >= m_EnemyWaves.Waves.Count) return;

		WaveData wave = m_EnemyWaves.Waves[ waveIndex ];
		float healthModifiser = wave.HealthModifier;

		for(int i = 0; i < wave.Enemies.Count; ++i){
			EnemyWaveData enemyData = wave.Enemies[ i ];
			StartCoroutine(SpawnWave(enemyData));
		}
	}

	IEnumerator SpawnWave( EnemyWaveData enemyData ){
		for(int i = 0; i < enemyData.Count; ++i){
			yield return new WaitForSeconds(1);
			Instantiate(enemyData.Prefab, transform.position, Quaternion.identity, transform);
		}
	}
}