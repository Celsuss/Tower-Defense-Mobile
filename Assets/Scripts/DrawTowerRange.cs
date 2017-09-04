using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DrawTowerRange : MonoBehaviour {

	[SerializeField] int m_VertexCount;
	[SerializeField] float m_Radius;
	LineRenderer m_LineRenderer;

	public float Radius{
		get { return m_Radius; }
		set {
			m_Radius = value;
			SetupCircle();
		}
	}

	void Awake() {
		m_LineRenderer = GetComponent<LineRenderer>();
		Assert.IsNotNull(m_LineRenderer);
	}

	// Use this for initialization
	void Start () {
		SetupCircle();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void SetupCircle(){
		Assert.IsTrue(m_VertexCount > 0);

		m_LineRenderer.positionCount = m_VertexCount;
		float deltaTheta = (2f * Mathf.PI) / (m_VertexCount-1);
		float theta = 0f;

		for(int i = 0; i < m_LineRenderer.positionCount; ++i){
			Vector3 pos = new Vector3(m_Radius * Mathf.Cos(theta), m_Radius * Mathf.Sin(theta), 0f);
			m_LineRenderer.SetPosition(i, pos);
			theta += deltaTheta;
		}
	}

	void OnDrawGizmos(){
		float deltaTheta = (2f * Mathf.PI) / (m_VertexCount-1);
		float theta = 0f;

		Vector3 pos = new Vector3(m_Radius * Mathf.Cos(theta), m_Radius * Mathf.Sin(theta), 0f);
		Vector3 oldPos = transform.position + pos;
		for(int i = 0; i < m_VertexCount; ++i){
			pos = new Vector3(m_Radius * Mathf.Cos(theta), m_Radius * Mathf.Sin(theta), 0f);
			Gizmos.DrawLine(oldPos, transform.position + pos);
			oldPos = transform.position + pos;
			theta += deltaTheta;
		}
	}
}