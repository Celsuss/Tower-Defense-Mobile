using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWaveData{
    public int Count;
    public GameObject Prefab;
    public float SpawnRate;
}

[System.Serializable]
public class WaveData{
	public float HealthModifier;
	public List<EnemyWaveData> Enemies = new List<EnemyWaveData>();
}

[CreateAssetMenu]
public class EnemyWaves : ScriptableObject {
    [HideInInspector] public List<WaveData> Waves = new List<WaveData>();
}