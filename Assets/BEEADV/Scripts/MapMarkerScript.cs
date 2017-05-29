using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MapMarkerScript : MonoBehaviour, IPointerDownHandler {

	public void OnPointerDown(PointerEventData eventData) {
		SceneManager.LoadScene("FlowerTapping5.15");
	}

}
