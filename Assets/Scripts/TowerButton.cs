using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	bool m_ShowTooltip = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter( PointerEventData eventData ){
		m_ShowTooltip = true;
	}

	public void OnPointerExit( PointerEventData eventData ){
        m_ShowTooltip = false;
    }
}