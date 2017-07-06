using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : MonoBehaviour {

	[SerializeField] GameObject m_WapointPrefab;
	List<Waypoint> m_Waypoints;
	Waypoint m_NextWaypoint;

	public GameObject WaypointPrefab{
		get{ return m_WapointPrefab; }
	}

	public List<Waypoint> Waypoints{
		get{ return m_Waypoints; }
	}

	public Waypoint NextWaypoint{
		get{ return m_NextWaypoint; }
		set{ m_NextWaypoint = value; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
