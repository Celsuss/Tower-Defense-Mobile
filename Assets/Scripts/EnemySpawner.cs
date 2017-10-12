using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] EnemyWaves m_EnemyWaves;
	[SerializeField] Waypoint m_FirstWaypoint;
	Text m_CountdownText;
	Text m_WaveText;
	List<GameObject> m_CurrentWave;
	[SerializeField] float m_WaveCountdown;
	[SerializeField] float m_TimeUntillCountdown;
	int m_CurrentWaveIndex = -1;
	bool m_WaveActive = false;
	bool m_CountdownActive = false;

	public Waypoint FirstWaypoint{
		get{ return m_FirstWaypoint; }
		set{ m_FirstWaypoint = value; }
	}

	// Use this for initialization
	void Start () {
		m_CountdownText = GameObject.Find("Countdown Text").GetComponent<Text>();
		m_CountdownText.enabled = false;

		m_WaveText = GameObject.Find("Wave Text").GetComponent<Text>();
		m_WaveText.text = 0 + "/" + m_EnemyWaves.Waves[ 0 ].Enemies.Count;
	}
	
	// Update is called once per frame
	void Update () {
		if( !m_WaveActive && !m_CountdownActive )
			StartCoroutine(WaveCountdown());
	}

	void SpawnEnemies( ){
		if( m_CurrentWaveIndex < 0 || m_CurrentWaveIndex >= m_EnemyWaves.Waves.Count) return;

		WaveData wave = m_EnemyWaves.Waves[ m_CurrentWaveIndex ];
		float healthModifiser = wave.HealthModifier;

		for(int i = 0; i < wave.Enemies.Count; ++i){
			EnemyWaveData enemyData = wave.Enemies[ i ];
			StartCoroutine( SpawnWave( enemyData ) );
		}
		m_WaveText.text = (m_CurrentWaveIndex+1) + "/" + wave.Enemies.Count;
	}

	IEnumerator SpawnWave( EnemyWaveData enemyData ){
		for( int i = 0; i < enemyData.Count; ++i ){
			if(GameManager.Instance.GameOver)
				break;

			yield return new WaitForSeconds( 1 );
			Instantiate( enemyData.Prefab, transform.position, Quaternion.identity, transform );
		}
		m_WaveActive = true;
	}

	IEnumerator WaitUntillWaveCountdown(){
		m_CountdownActive = true;
		yield return new WaitForSeconds( m_TimeUntillCountdown );
	}

	IEnumerator WaveCountdown(){
		m_CountdownActive = true;
		m_CountdownText.enabled = true;
		for( int i = 0; i < m_WaveCountdown+1; ++i) {
			m_CountdownText.text = (m_WaveCountdown-i).ToString();
			yield return new WaitForSeconds( m_WaveCountdown/m_WaveCountdown );
		}

		++m_CurrentWaveIndex;
		m_CountdownText.enabled = false;
		SpawnEnemies();
	}
}