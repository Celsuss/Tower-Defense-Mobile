using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] GameObject m_Tooltip;
	[SerializeField] BuildingShoot m_Tower;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter( PointerEventData eventData ){
		m_Tooltip.SetActive(true);
	}

	public void OnPointerExit( PointerEventData eventData ){
		m_Tooltip.SetActive(false);
    }
}