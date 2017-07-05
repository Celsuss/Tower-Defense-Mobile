using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class LevelEditorEditWaypoints : Editor {

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

	static WaypointsManager m_WaypointsManager;
	public static WaypointsManager WaypointsManager{
		get{
			if( !m_WaypointsManager ){
				GameObject obj = GameObject.Find("Waypoints Manager");
				if( obj ){
					WaypointsManager wpm = obj.GetComponent<WaypointsManager>();
					if( wpm )
						m_WaypointsManager = wpm;
					else
						Debug.LogError("Can't find Comonent Waypoints Manager");
				}
				else
					Debug.LogError("Can't find GameObject Waypoints Manager");
			}
			
			return m_WaypointsManager;
		}
	}

	static LevelEditorEditWaypoints(){
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
	}

	void OnDestroy(){
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }

	static void OnSceneGUI( SceneView sceneView ){
		if(LevelEditorToolsMenu.SelectedTool != LevelEditorToolsMenu.SelectableTools.Waypoints) return;

		DrawWaypoints();
		DrawPath();
        HandleWaypointsPlacement();
	}

	static void DrawWaypoints(){
		/*foreach(GameObject wp in WaypointsManager.Waypoints){
			Handles.DrawSolidDisc(wp.transform.position, Vector3.forward, 0.10f);
		}*/
	}
	
	static void DrawPath(){
		Handles.color = Color.yellow;
        //Handles.BeginGUI();
		
		
		Waypoint wp = WaypointsManager.NextWaypoint;
		if(!wp)
			return;

		Vector3 p1 = WaypointsManager.transform.position;
		Vector3 p2 = wp.transform.position;
		while(wp.NextWaypoint){
			p1 = wp.transform.position;
			p2 = wp.NextWaypoint.transform.position;
			Handles.DrawDottedLine(p1, p2, 1);
			wp = wp.NextWaypoint;
		}

        //Handles.EndGUI();
    }

	static void HandleWaypointsPlacement(){
		int controlId = GUIUtility.GetControlID( FocusType.Passive );

        if( Event.current.type == EventType.mouseDown &&
            Event.current.button == 0 &&
            Event.current.alt == false &&
            Event.current.shift == false &&
            Event.current.control == false ){
            if( TileHandle.IsMouseInValidArea == true ){
                AddWaypoint(TileHandle.CurrentHandlePosition, WaypointsManager.WaypointPrefab);
            }
        }

        if( Event.current.type == EventType.keyDown &&
            Event.current.keyCode == KeyCode.Escape ){
            LevelEditorToolsMenu.SelectedTool = 0;
        }

        //Add our controlId as default control so it is being picked instead of Unitys default SceneView behaviour
        HandleUtility.AddDefaultControl( controlId );
	}

    public static void AddWaypoint( Vector3 position, GameObject prefab ){
		if(!prefab) return;

		GameObject tile = GetTile(position);
		if(!tile) return;

		Transform oldWaypoint = tile.transform.Find("Waypoint");
		if(oldWaypoint){
			WaypointsManager.Waypoints.Remove(oldWaypoint.gameObject);
			Undo.DestroyObjectImmediate( oldWaypoint.gameObject );
			UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
			return;
		}

		GameObject newWaypoint = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
		newWaypoint.transform.parent = tile.transform;
		newWaypoint.transform.position = tile.transform.position;
		WaypointsManager.Waypoints.Add(newWaypoint);

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