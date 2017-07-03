using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tile))]
public class TileEditor : Editor {

	Tile m_Target;
	

	public override void OnInspectorGUI(){
		m_Target = (Tile)target;

		DrawDefaultInspector();
		DrawCusomInspector();
	}

	void DrawCusomInspector(){
		DrawConvertTileButtons();
	}

	void DrawConvertTileButtons(){
		if(GUILayout.Button("Convert to Ground")){
			ConvertToGround();
		}
		if(GUILayout.Button("Convert to Road")){
			ConvertToRoad();
		}
		if(GUILayout.Button("Convert to Spawn")){

		}
		if(GUILayout.Button("Convert to Goal")){

		}
	}

	void ConvertToGround(){

	}

	void ConvertToRoad(){
		
	}

	void ConvertToSpawn(){
		
	}

	void ConvertToGoal(){
		
	}
}