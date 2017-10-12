using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] GameObject m_Tooltip;
	[SerializeField] BuildingShoot m_Tower;
	Transform m_TooltipAnchor;

	// Use this for initialization
	void Start () {
		m_TooltipAnchor = GameObject.Find("Tower Tooltip Anchor").transform;
	}
	
	// Update is called once per frame
	void Update () {
		m_Tooltip.transform.position = new Vector3(m_TooltipAnchor.position.x, transform.position.y, transform.position.z);;
	}

	public void OnPointerEnter( PointerEventData eventData ){
		m_Tooltip.SetActive(true);
	}

	public void OnPointerExit( PointerEventData eventData ){
		m_Tooltip.SetActive(false);
    }
}