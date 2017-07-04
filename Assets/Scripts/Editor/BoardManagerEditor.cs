using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardManager))]
public class BoardManagerEditor : Editor {
	BoardManager m_Target;

	public override void OnInspectorGUI(){
		m_Target = (BoardManager)target;

		DrawDefaultInspector();
		DrawCusomInspector();
	}	

	void DrawCusomInspector(){
		GUILayout.Space(10);
		GUILayout.Label("BoardCreator", EditorStyles.boldLabel);
		DrawBoardSizeIntFields();
		DrawGenerateBoardButton();
		DrawClearBoardButton();
	}

	void DrawBoardSizeIntFields(){
		//SerializedProperty widthProperty = serializedObject.FindProperty("m_Width");
		//if(widthProperty.isInstantiatedPrefab)

		GUILayout.BeginHorizontal();
		GUILayout.Label("Width", EditorStyles.label);
		m_Target.m_Width = EditorGUILayout.IntField(m_Target.m_Width);
		GUILayout.Label("Height", EditorStyles.label);
		m_Target.m_Height = EditorGUILayout.IntField(m_Target.m_Height);

		GUILayout.EndHorizontal();
	}

	void DrawGenerateBoardButton(){
		if(GUILayout.Button("Generate Board")){

			if(m_Target.transform.childCount > 0){
				EditorApplication.Beep();
				if( EditorUtility.DisplayDialog( "Really?", "Do you really want generate a new board?", "Yes", "No" ) == true ){
					ClearBoard();
					EditorUtility.SetDirty( m_Target );
				}
				else
					return;
			}
			
			GenerateBoard();
		}
	}

	void DrawClearBoardButton(){
		if(GUILayout.Button("Clear Board")){
			EditorApplication.Beep();
			if( EditorUtility.DisplayDialog( "Really?", "Do you really want to clear the board?", "Yes", "No" ) == true ){
				ClearBoard();
				EditorUtility.SetDirty( m_Target );
			}
		}
	}

	void GenerateBoard(){
		int width = m_Target.m_Width;
		int height = m_Target.m_Height;
		Vector3 pos = new Vector3(-(width/2), -(height/2), 0);

		for(int i = 0; i < width; i++){
			for(int j = 0; j < height; j++){
				//m_Target.m_Board.Add(Instantiate(m_Target.TilePrefab, pos, m_Target.transform.rotation, m_Target.transform));
				Instantiate(m_Target.TilePrefab, pos, m_Target.transform.rotation, m_Target.transform);
				++pos.y;
			}
			++pos.x;
			pos.y = -(width/2);
		}
	}

	void ClearBoard(){
		while(m_Target.transform.childCount > 0){
			DestroyImmediate(m_Target.transform.GetChild(0).gameObject);
		}
	}
}