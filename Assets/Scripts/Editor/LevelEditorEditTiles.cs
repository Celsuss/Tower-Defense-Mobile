using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class LevelEditorEditTiles : Editor {
    
	static Vector2 m_ScrollPosition = Vector2.zero;
	static Transform m_BoardManager;
    public static Transform BoardManager{
        get{
            if( !m_BoardManager ){
                GameObject obj = GameObject.Find( "Board Manager" );
                if( obj )
                    m_BoardManager = obj.transform;
            }

            return m_BoardManager;
        }
    }

	public static int SelectedTile{
        get{
            return EditorPrefs.GetInt( "SelectedEditorTile", 0 );
        }
        set{
            EditorPrefs.SetInt( "SelectedEditorTile", value );
        }
    }	

	static LevelTiles m_LevelTiles;

	static LevelEditorEditTiles(){
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;

		string levelTilesPath = "Assets/Scriptable Objects/Level Tiles.asset";
		m_LevelTiles = AssetDatabase.LoadAssetAtPath<LevelTiles>( levelTilesPath );
		if(!m_LevelTiles)
			Debug.LogError("Failed to find Level Tiles at: " + levelTilesPath);
	}

	void OnDestroy(){
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }

	static void OnSceneGUI( SceneView sceneView ){
		if(!m_LevelTiles) return;

		DrawCustomBlockButtons( sceneView );
        HandleLevelEditorPlacement();
	}

	static void DrawCustomBlockButtons( SceneView sceneView ){
		if(LevelEditorToolsMenu.SelectedTool != LevelEditorToolsMenu.SelectableTools.Paint) return;

        Handles.BeginGUI();

		Rect rect = new  Rect( 0, 0, 125, sceneView.position.height - 35 );
        GUI.Box( rect, GUIContent.none, EditorStyles.textArea );
		m_ScrollPosition = GUI.BeginScrollView(rect, m_ScrollPosition, new Rect(0, 0, 0, 135 * m_LevelTiles.Tiles.Count));
		
        for( int i = 0; i < m_LevelTiles.Tiles.Count; ++i ){
            DrawCustomTileButton( i, sceneView.position );
        }

		GUI.EndScrollView();
        Handles.EndGUI();
    }

	static void DrawCustomTileButton( int index, Rect sceneViewRect ){
        bool isActive = false;

        if( LevelEditorToolsMenu.SelectedTool == LevelEditorToolsMenu.SelectableTools.Paint && index == SelectedTile ){
            isActive = true;
        }

        //By passing a Prefab or GameObject into AssetPreview.GetAssetPreview you get a texture that shows this object
        Texture2D previewImage = AssetPreview.GetAssetPreview( m_LevelTiles.Tiles[ index ].Prefab );
        GUIContent buttonContent = new GUIContent( previewImage );

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.black;
        //style.fontStyle = FontStyle.Bold;
        GUI.Label( new Rect( 5, index * 128 + 5, 100, 20 ), m_LevelTiles.Tiles[ index ].Name, style );
        bool isToggleDown = GUI.Toggle( new Rect( 5, index * 128 + 25, 100, 100 ), isActive, buttonContent, GUI.skin.button );

        //If this button is clicked but it wasn't clicked before (ie. if the user has just pressed the button)
        if( isToggleDown && !isActive ){
            SelectedTile = index;
            LevelEditorToolsMenu.SelectedTool = LevelEditorToolsMenu.SelectableTools.Paint;
        }
    }

	static void HandleLevelEditorPlacement(){
		if(LevelEditorToolsMenu.SelectedTool == LevelEditorToolsMenu.SelectableTools.None) return;

		int controlId = GUIUtility.GetControlID( FocusType.Passive );

        if( Event.current.type == EventType.mouseDown &&
            Event.current.button == 0 &&
            Event.current.alt == false &&
            Event.current.shift == false &&
            Event.current.control == false ){
            if( TileHandle.IsMouseInValidArea == true ){
                if( LevelEditorToolsMenu.SelectedTool == LevelEditorToolsMenu.SelectableTools.Paint ){
                    AddBlock( TileHandle.CurrentHandlePosition, m_LevelTiles.Tiles[ SelectedTile ].Prefab );
                }
            }
        }

        if( Event.current.type == EventType.keyDown &&
            Event.current.keyCode == KeyCode.Escape ){
            LevelEditorToolsMenu.SelectedTool = 0;
        }

        //Add our controlId as default control so it is being picked instead of Unitys default SceneView behaviour
        HandleUtility.AddDefaultControl( controlId );
	}

    public static void AddBlock( Vector3 position, GameObject prefab ){
		if(!prefab) return;

		GameObject oldTile = GetTile(position);
		if(!oldTile) return;

		GameObject newTile = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
		newTile.transform.parent = oldTile.transform.parent;
		newTile.transform.position = oldTile.transform.position;
		Undo.DestroyObjectImmediate( oldTile );

        UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
    }

	static GameObject GetTile(Vector3 position){
		for( int i = 0; i < BoardManager.childCount; ++i ){
            float distanceToBlock = Vector3.Distance(BoardManager.GetChild(i).transform.position, position );
            if( distanceToBlock < 0.1f ){
                return BoardManager.GetChild(i).gameObject;
            }
        }
		return null;
	}
}
