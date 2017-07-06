using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class LevelEditorEditWaypoints : Editor {

	static Transform m_BoardManager;
	static bool m_DragingPath = false;
	static Waypoint m_DragingWaypoint = null;
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
		DrawDragPath( sceneView );
        HandleWaypointsPlacement();
	}

	static void DrawWaypoints(){
		/*foreach(Waypoint wp in WaypointsManager.Waypoints){
			Handles.DrawSolidDisc(wp.transform.position, Vector3.forward, 0.10f);
		}*/
	}
	
	static void DrawPath(){
		Handles.color = Color.yellow;

		if(!WaypointsManager) return;

		foreach(Waypoint wp in WaypointsManager.Waypoints){
			if(wp.NextWaypoint){
				Vector3 p1 = wp.transform.position;
				Vector3 p2 = wp.NextWaypoint.transform.position;
				Handles.DrawDottedLine(p1, p2, 1);
			}
		}
    }

	static void DrawDragPath( SceneView sceneView ){
		if(!m_DragingPath) return;
		if(!m_DragingWaypoint) return;

		Handles.color = Color.yellow;
		
		Vector3 mousePos = Event.current.mousePosition;
		mousePos.y = sceneView.camera.pixelHeight - mousePos.y;
   		mousePos = sceneView.camera.ScreenToWorldPoint(mousePos);

		Vector3 p1 = mousePos;
		Vector3 p2 = m_DragingWaypoint.transform.position;
		Handles.DrawLine( p1, p2 );
	}

	static void HandleWaypointsPlacement(){
		int controlId = GUIUtility.GetControlID( FocusType.Passive );

		if(TileHandle.IsMouseInValidArea){
			if( Event.current.type == EventType.mouseUp &&
				!m_DragingPath && Event.current.button == 0 ){
					AddOrRemoveWaypoint( TileHandle.CurrentHandlePosition, WaypointsManager.WaypointPrefab );
					
			}
			else if( Event.current.type == EventType.mouseDrag &&
					!m_DragingPath && Event.current.button == 0  ){
					BeginDragPath( TileHandle.CurrentHandlePosition );
				}
			else if( Event.current.type == EventType.mouseUp &&
				m_DragingPath && Event.current.button == 0){
					EndDragPath( TileHandle.CurrentHandlePosition );
				}
		}

        if( Event.current.type == EventType.keyDown &&
            Event.current.keyCode == KeyCode.Escape ){
            LevelEditorToolsMenu.SelectedTool = 0;
        }

        //Add our controlId as default control so it is being picked instead of Unitys default SceneView behaviour
        HandleUtility.AddDefaultControl( controlId );
	}

	static void AddOrRemoveWaypoint( Vector3 position, GameObject prefab ){
		if(!GetTileWaypoint( position ))
			AddWaypoint( position, prefab );
		else
			RemoveWaypoint( position );
	}

    public static void AddWaypoint( Vector3 position, GameObject prefab ){
		if(!prefab) return;

		GameObject tile = GetTile(position);
		if(!tile) return;

		Transform oldWaypointTransform = tile.transform.Find("Waypoint");
		if (oldWaypointTransform ) return;

		GameObject newWaypointObj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
		newWaypointObj.transform.parent = tile.transform;
		newWaypointObj.transform.position = tile.transform.position;
		Waypoint newWaypoint = newWaypointObj.GetComponent<Waypoint>();
		WaypointsManager.Waypoints.Add( newWaypoint );

		EnemySpawner spawner = tile.GetComponentInChildren<EnemySpawner>();
		if(spawner){
			spawner.FirstWaypoint = newWaypoint;
			EditorUtility.SetDirty(spawner);
		}

		EditorUtility.SetDirty(WaypointsManager);
        UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
    }

	static void RemoveWaypoint( Vector3 position ){
		GameObject tile = GetTile(position);
		if(!tile) return;

		GameObject wpObj = tile.transform.Find("Waypoint").gameObject;
		if(!wpObj) return;

		Waypoint waypoint = wpObj.GetComponent<Waypoint>();
		if(!waypoint) return;

		WaypointsManager.Waypoints.Remove(waypoint);
		Undo.DestroyObjectImmediate( waypoint.gameObject );
		UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
	}

	static void BeginDragPath( Vector3 tilePosition ){
		Waypoint wp = GetTileWaypoint( tilePosition ); 
		if( !wp ) return;

		m_DragingWaypoint = wp;
		m_DragingPath = true;
	}

	static void EndDragPath( Vector3 tilePosition ){
		m_DragingPath = false;
		Waypoint wp = GetTileWaypoint( tilePosition ); 
		if( !wp ) return;

		m_DragingWaypoint.NextWaypoint = wp;
		EditorUtility.SetDirty(m_DragingWaypoint);
		UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
	}

	static GameObject GetTile( Vector3 position ){
		for( int i = 0; i < BoardManager.childCount; ++i ){
            float distanceToBlock = Vector3.Distance(BoardManager.GetChild(i).transform.position, position );
            if( distanceToBlock < 0.1f ){
                return BoardManager.GetChild(i).gameObject;
            }
        }
		return null;
	}

	static Waypoint GetTileWaypoint( Vector3 position ){
		GameObject tile = GetTile(position);
		if(!tile) return null;

		Transform wpObj = tile.transform.Find("Waypoint");
		if(!wpObj) return null;

		Waypoint waypoint = wpObj.GetComponent<Waypoint>();
		if(!waypoint) return null;

		return waypoint;
	}
}