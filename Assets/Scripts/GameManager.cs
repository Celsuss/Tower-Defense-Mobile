using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	[SerializeField] int m_Life = 5;
	[SerializeField] Text m_LifeText;

	public int Life{
		get { return m_Life; }
		set {
			m_Life = value;
			m_LifeText.text = "Health: " + value.ToString();
		}
	}

	// Use this for initialization
	void Start () {
		m_LifeText.text = "Health: " + m_Life.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}