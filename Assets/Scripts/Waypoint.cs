using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	Waypoint m_NextWaypoint;

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
