using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class LevelEditorToolsMenu : Editor {

    public enum SelectableTools{
        None,
        Paint,
        Waypoints
    }

	public static SelectableTools SelectedTool{
        get{
            return (SelectableTools)EditorPrefs.GetInt( "SelectedEditorTool", 0 );
        }
        set{
            if( value == SelectedTool ) return;

            EditorPrefs.SetInt( "SelectedEditorTool", (int)value );

            switch( (int)value ){
            case 0:
                EditorPrefs.SetBool( "IsLevelEditorEnabled", false );

                Tools.hidden = false;
                break;
            case 1:
                EditorPrefs.SetBool( "IsLevelEditorEnabled", true );
                EditorPrefs.SetFloat( "CubeHandleColorR", Color.magenta.r );
                EditorPrefs.SetFloat( "CubeHandleColorG", Color.magenta.g );
                EditorPrefs.SetFloat( "CubeHandleColorB", Color.magenta.b );

                //Hide Unitys Tool handles (like the move tool) while we draw our own stuff
                Tools.hidden = true;
                break;
            default:
                EditorPrefs.SetBool( "IsLevelEditorEnabled", true );
                EditorPrefs.SetFloat( "CubeHandleColorR", Color.yellow.r );
                EditorPrefs.SetFloat( "CubeHandleColorG", Color.yellow.g );
                EditorPrefs.SetFloat( "CubeHandleColorB", Color.yellow.b );

                //Hide Unitys Tool handles (like the move tool) while we draw our own stuff
                Tools.hidden = true;
                break;
            }
        }
    }

	static LevelEditorToolsMenu(){
    	SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;

        //EditorApplication.hierarchyWindowChanged is a good way to tell if the user has loaded a new scene in the editor
        //EditorApplication.hierarchyWindowChanged -= OnSceneChanged;
        //EditorApplication.hierarchyWindowChanged += OnSceneChanged;
	}

	void OnDestroy(){
        SceneView.onSceneGUIDelegate -= OnSceneGUI;

        //EditorApplication.hierarchyWindowChanged -= OnSceneChanged;
    }

	static void OnSceneGUI( SceneView sceneView ){
        DrawToolsMenu( sceneView.position );
    }

	static void DrawToolsMenu( Rect position )   {
        Handles.BeginGUI();

        //Here we draw a toolbar at the bottom edge of the SceneView
        GUILayout.BeginArea( new Rect( 0, position.height - 35, position.width, 20 ), EditorStyles.toolbar );
        {
            string[] buttonLabels = new string[] { "None", "Paint", "Waypoints" };

            int selectedToolInt = (int)SelectedTool;
            SelectedTool = (SelectableTools)GUILayout.SelectionGrid(
                selectedToolInt,
                buttonLabels, 
                4,
                EditorStyles.toolbarButton,
                GUILayout.Width( 300 ) );
        }
        GUILayout.EndArea();

        Handles.EndGUI();
    }
}