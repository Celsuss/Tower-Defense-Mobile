using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BoardDesignerWindow : EditorWindow {

	[MenuItem("Window/Board Designer")]
	static void OpenWindow(){
		BoardDesignerWindow window = (BoardDesignerWindow)GetWindow(typeof(BoardDesignerWindow));
		window.minSize = new Vector2(100, 100);
		window.Show();
	}

	void OnEnable(){

	}

	void OnGUI(){
		if(GUILayout.Button("Create board")){
			
		}
	}
}