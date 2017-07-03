using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class TileHandle : Editor {

	public static Vector3 CurrentHandlePosition = Vector3.zero;
	public static bool IsMouseInValidArea = false;
	static Vector3 m_OldHandlePosition = Vector3.zero;

	static TileHandle(){
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
	}
	
	void OnDestroy(){
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
	}

	static void OnSceneGUI(SceneView sceneView){
		UddateIsMouseInVanlidArea(sceneView.position);
		UpdateRepaint();
		UpdateHandlePosition();

		DrawTilePreview();
	}

	static void UpdateHandlePosition(){
		if( Event.current == null ) return;

        Vector2 mousePosition = new Vector2( Event.current.mousePosition.x, Event.current.mousePosition.y );

        Ray ray = HandleUtility.GUIPointToWorldRay( mousePosition );
        RaycastHit hit;
        if( Physics.Raycast( ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer( "Level" ) ))
			CurrentHandlePosition = hit.transform.position;
		else{
			IsMouseInValidArea = false;
			SceneView.RepaintAll();
		}
	}

	static void UddateIsMouseInVanlidArea(Rect sceneViewRect){
		bool isInValidArea = Event.current.mousePosition.y < sceneViewRect.height - 35;
        if( isInValidArea != IsMouseInValidArea ){
            IsMouseInValidArea = isInValidArea;
            SceneView.RepaintAll();
        }
	}

	static void UpdateRepaint(){
		if(CurrentHandlePosition != m_OldHandlePosition){
			SceneView.RepaintAll();
			m_OldHandlePosition = CurrentHandlePosition;
		}
	}

	static void DrawTilePreview(){
		if(!IsMouseInValidArea) return;

		Handles.color = Color.red;
		DrawHandles();
	}

	static void DrawHandles(){
		Vector3 center = CurrentHandlePosition;

		Vector3 p1 = center + Vector3.up * 0.5f + Vector3.right * 0.5f + Vector3.forward * 0.5f;
        Vector3 p2 = center + Vector3.up * 0.5f + Vector3.right * 0.5f - Vector3.forward * 0.5f;
        Vector3 p3 = center + Vector3.up * 0.5f - Vector3.right * 0.5f - Vector3.forward * 0.5f;
        Vector3 p4 = center + Vector3.up * 0.5f - Vector3.right * 0.5f + Vector3.forward * 0.5f;

        Vector3 p5 = center - Vector3.up * 0.5f + Vector3.right * 0.5f + Vector3.forward * 0.5f;
        Vector3 p6 = center - Vector3.up * 0.5f + Vector3.right * 0.5f - Vector3.forward * 0.5f;
        Vector3 p7 = center - Vector3.up * 0.5f - Vector3.right * 0.5f - Vector3.forward * 0.5f;
        Vector3 p8 = center - Vector3.up * 0.5f - Vector3.right * 0.5f + Vector3.forward * 0.5f;

        Handles.DrawLine( p1, p2 );
        Handles.DrawLine( p2, p3 );
        Handles.DrawLine( p3, p4 );
        Handles.DrawLine( p4, p1 );

        Handles.DrawLine( p5, p6 );
        Handles.DrawLine( p6, p7 );
        Handles.DrawLine( p7, p8 );
        Handles.DrawLine( p8, p5 );

        Handles.DrawLine( p1, p5 );
        Handles.DrawLine( p2, p6 );
        Handles.DrawLine( p3, p7 );   
        Handles.DrawLine( p4, p8 );
	}
}