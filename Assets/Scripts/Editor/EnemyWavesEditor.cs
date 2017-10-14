using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor( typeof(EnemyWaves))]
public class EnemyWavesEditor : Editor {

	EnemyWaves m_Target;
	List<bool> m_ShowWave = new List<bool>();

	public override void OnInspectorGUI(){
		m_Target = (EnemyWaves)target;
		DrawDefaultInspector();
		DrawWavesInspector();
	}

	void DrawWavesInspector(){
		GUILayout.Space( 5 );
        GUILayout.Label( "Levels", EditorStyles.boldLabel );

        for( int i = 0; i < m_Target.Waves.Count; ++i )
            DrawWaves( i );

        DrawAddWaveButton();
	}

	void DrawWaves( int index ){
        if( index < 0 || index >= m_Target.Waves.Count ) return;
		
		while(m_ShowWave.Count < m_Target.Waves.Count)
			m_ShowWave.Add(false);

		m_ShowWave[ index ] = EditorGUILayout.Foldout(m_ShowWave[ index ], "Waves");
		if(m_ShowWave[ index ]){

			//SerializedProperty listIterator = serializedObject.FindProperty( "Level " + index );
			WaveData wave = m_Target.Waves[index];

			GUILayout.BeginHorizontal();{   
				GUILayout.Label( "Health Modifier", EditorStyles.label, GUILayout.Width( 90 ) );

				EditorGUI.BeginChangeCheck();

				float newHealthModifier = EditorGUILayout.FloatField(wave.HealthModifier, GUILayout.Width(30));

				//If a variable was modified, EndChangeCheck() returns true
				if( EditorGUI.EndChangeCheck() ){
					m_Target.Waves[index].HealthModifier = newHealthModifier;

					EditorUtility.SetDirty( m_Target );
				}

				DrawRemoveWaveButton( index );
			}
			GUILayout.EndHorizontal();

			GUILayout.Space(10);
			for(int i = 0; i < wave.Enemies.Count; ++i)
				DrawEnemies( i, index );

			GUILayout.Space(10);
			DrawAddEnemyButton( index );

			GUILayout.Space(20);
		}
    }

	void DrawEnemies( int index, int waveIndex ){
		if( index < 0 || index >= m_Target.Waves[ waveIndex ].Enemies.Count ) return;

		EditorGUI.BeginChangeCheck();

		WaveData wave = m_Target.Waves[waveIndex];
		EnemyWaveData enemy = wave.Enemies[ index ];

		int count;
		float spawnRate;
		GameObject prefab;

		GUILayout.BeginHorizontal(); { 
			prefab = (GameObject)EditorGUILayout.ObjectField(enemy.Prefab, typeof(GameObject), true);
		}
        GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal(); {
            GUILayout.Label( "Count", EditorStyles.label, GUILayout.Width( 40 ) );
			count = EditorGUILayout.IntField(enemy.Count, GUILayout.Width( 30 ));

			GUILayout.Label( "Spawn rate", EditorStyles.label, GUILayout.Width( 70 ) );
			spawnRate = EditorGUILayout.FloatField(enemy.SpawnRate, GUILayout.Width( 30 ));

			if( EditorGUI.EndChangeCheck() ){
				m_Target.Waves[ waveIndex ].Enemies[ index ].Count = count;
				m_Target.Waves[ waveIndex ].Enemies[ index ].Prefab = prefab;
				m_Target.Waves[ waveIndex ].Enemies[ index ].SpawnRate = spawnRate;
                EditorUtility.SetDirty( m_Target );
            }

			DrawRemoveEnemyButton( index, waveIndex );
		}
        GUILayout.EndHorizontal();
		GUILayout.Space(10);
	}

    void DrawAddWaveButton(){
        if( GUILayout.Button( "Add new Wave", GUILayout.Height( 30 ) ) ){
            m_Target.Waves.Add( new WaveData { HealthModifier = 1f } );
			m_ShowWave.Add(true);
			EditorUtility.SetDirty( m_Target );
        }
    }

	void DrawAddEnemyButton( int index ){
		if(GUILayout.Button( "Add new Enemy" ) ){
			m_Target.Waves[ index ].Enemies.Add(new EnemyWaveData { } );
		}
	}

	void DrawRemoveWaveButton( int index ){
		if( GUILayout.Button( "Remove Wave" ) ){
                EditorApplication.Beep();
				
                if( EditorUtility.DisplayDialog( "Really?", "Do you really want to remove the Wave '" + index + "'?", "Yes", "No" ) == true ){
                    Undo.RecordObject( m_Target, "Delete Wave" );
                    m_Target.Waves.RemoveAt( index );
					m_ShowWave.RemoveAt( index );
                    EditorUtility.SetDirty( m_Target );
                }
            }
	}

	void DrawRemoveEnemyButton( int index, int waveIndex ){
		if( GUILayout.Button( "Remove" ) ){
				EditorApplication.Beep();

				if( EditorUtility.DisplayDialog( "Really?", "Do you really want to remove Enemy '" + index + "'?", "Yes", "No" ) == true ){
					Undo.RecordObject( m_Target, "Delete Enemy" );
					m_Target.Waves[ waveIndex ].Enemies.RemoveAt( index );
					EditorUtility.SetDirty( m_Target );
				}
			}
	}
}