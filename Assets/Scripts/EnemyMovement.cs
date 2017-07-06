using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyMovement : MonoBehaviour {

	Waypoint m_TargetWaypoint;
	[SerializeField] float m_MovementSpeed = 0;

	float MovementSpeed{
		get{ return m_MovementSpeed; }
		set{ m_MovementSpeed = value; }
	}

	// Use this for initialization
	void Start () {
		Assert.IsNotNull(transform.parent);
		if(!transform.parent) return;

		EnemySpawner spawner = transform.parent.GetComponent<EnemySpawner>();
		Assert.IsNotNull(spawner);
		if(!spawner) return;
	
		m_TargetWaypoint = spawner.FirstWaypoint;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		Vector3 target = m_TargetWaypoint.transform.position;
		Vector3 delta = target - position;
		Vector3 direction = Vector3.Normalize(delta);
		transform.position += direction * m_MovementSpeed * Time.deltaTime;

		if(delta.magnitude < 0.1f){
			Waypoint waypoint = m_TargetWaypoint.NextWaypoint;
			if(waypoint){
				m_TargetWaypoint = waypoint;
			}
		}
	}
}