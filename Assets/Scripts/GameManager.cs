using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	static GameManager m_Instance = null;
	[SerializeField] int m_Life = 5;
	[SerializeField] int m_Gold;
	[SerializeField] Text m_LifeText;
	[SerializeField] Text m_GoldText;

	public static GameManager Instance {
		get {
			if( m_Instance )
				return m_Instance;
			else
				return null;
		 }
	}

	public int Life {
		get { return m_Life; }
		set {
			m_Life = value;
			m_LifeText.text = value.ToString();
		}
	}

	public int Gold {
		get{ return m_Gold; }
		set {
			m_Gold = value;
			m_GoldText.text = m_Gold.ToString();
		}
	}

	void Awake() {
		if (!m_Instance )
			m_Instance = this;
		else if(m_Instance != this)
			Destroy(gameObject);

		//DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		m_LifeText.text = m_Life.ToString();
		m_GoldText.text = m_Gold.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}