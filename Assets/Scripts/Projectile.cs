using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] float m_Speed;
	[SerializeField] float m_Damage;
	[SerializeField] EnemyHealth m_Target;

	public float Damage{
		get{ return m_Damage; }
		set{ m_Damage = value; }
	}

	public EnemyHealth Target{
		get{ return m_Target; }
		set{ m_Target = value; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!m_Target) {
			Destroy(gameObject);
			return;
		}

		Vector3 position = transform.position;
		Vector3 target = m_Target.transform.position;
		Vector3 delta = target - position;
		Vector3 direction = Vector3.Normalize(delta);
		transform.position += direction * m_Speed * Time.deltaTime;

		if(delta.magnitude < 0.1f){
			m_Target.Health -= m_Damage;
			Destroy(gameObject);
		}
	}
}
