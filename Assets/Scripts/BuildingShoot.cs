using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BuildingShoot : MonoBehaviour {

	[SerializeField] float m_Range;
	[SerializeField] float m_Damage;
	[SerializeField] float m_FireRate;
	bool m_Shooting = false;
	[SerializeField] Projectile m_BulletPrefab;
	SphereCollider m_Collider;
	List<GameObject> m_EnemiesInRange = new List<GameObject>();
	[SerializeField] GameObject m_CurrentTarget = null;

	// Use this for initialization
	void Start () {
		m_Collider = GetComponent<SphereCollider>();
		Assert.IsNotNull( m_Collider );
		m_Collider.radius = m_Range;
	}
	
	// Update is called once per frame
	void Update () {
		if( !m_CurrentTarget && !m_Shooting )
			UpdateTarget();
	}

	void UpdateTarget(){
		if(m_EnemiesInRange.Count == 0) return;

		m_CurrentTarget = m_EnemiesInRange[0];

		if(!m_CurrentTarget){
			m_CurrentTarget = null;
			m_EnemiesInRange.RemoveAt( 0 );
			return;
		}
		StartCoroutine( Shoot() );
	}

	IEnumerator Shoot(){
		m_Shooting = true;

		EnemyHealth health = m_CurrentTarget.GetComponent<EnemyHealth>();
		Assert.IsNotNull( health );
		m_BulletPrefab.Target = health;
		
		while( m_CurrentTarget ){

			m_BulletPrefab.Damage = m_Damage;
			GameObject bullet = Instantiate( m_BulletPrefab.gameObject, transform.position, transform.rotation, transform );

			yield return new WaitForSeconds( m_FireRate );
		}
		
		m_Shooting = false;
	}

	void OnTriggerEnter( Collider other ){
		if( other.tag != "Enemy" ) return;

		m_EnemiesInRange.Add( other.gameObject );
	}

	void OnTriggerExit( Collider other ){
		if( other.tag != "Enemy" ) return;

		m_EnemiesInRange.Remove( other.gameObject );
		if( m_CurrentTarget == other.gameObject )
		m_CurrentTarget = null;
	}
}