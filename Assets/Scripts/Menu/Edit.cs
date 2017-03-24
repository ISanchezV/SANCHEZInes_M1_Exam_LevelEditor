using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Edit : MonoBehaviour {

	public GameObject butt;
	public Button[] layers;

	public void OnClick () {
		if (!GameControl.control.edOrPlay) {
			butt.GetComponentInChildren<Text> ().text = "Jouer";
			GameControl.control.edOrPlay = true;
			FindObjectOfType<NewController> ().startPos = FindObjectOfType<NewController> ().transform.position;
		} else {
			butt.GetComponentInChildren<Text> ().text = "Editer";
			GameControl.control.edOrPlay = false;
			FindObjectOfType<NewController> ().transform.position = FindObjectOfType<NewController> ().startPos;
		}
	}

	public void Layer() {
		GameControl.control.layer = EventSystem.current.currentSelectedGameObject.name;
		GameControl.control.All = false;
		for (int i = 0; i < 4; i++) {
			if (layers [i].name != GameControl.control.layer)
				layers [i].GetComponent<Image> ().color = Color.gray;
			else {
				layers [i].GetComponent<Image> ().color = Color.white;
			}
		} 
	}

	public void AllLayers() {
		if (!GameControl.control.All) {
			GameControl.control.All = true;
			layers [3].GetComponent<Image> ().color = Color.white;
			for (int i = 0; i < 3; i++) {
				layers [i].GetComponent<Image> ().color = Color.gray;
			}
		}
	}
}
