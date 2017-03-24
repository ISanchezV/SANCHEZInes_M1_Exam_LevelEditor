using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour {

	public GameObject ply;
	public GameObject saveObjects;

	void Start() {
		ply = GameObject.FindGameObjectWithTag ("Player");
		saveObjects = GameObject.FindGameObjectWithTag ("Main");
	}

	public void OnClick() {

		//Création d'un objet tel que le prefab du même nom que le bouton
		GameObject created = (GameObject)Instantiate (Resources.Load (this.name));

		//L'objet est placé au milieu de l'écran
		created.transform.position = Camera.main.ScreenToWorldPoint
			(new Vector3(Screen.width/2, Screen.height/2, ply.transform.position.z + 10f));

		created.tag = GameControl.control.layer;

		//L'objet devient lié à un autre objet parent
		created.transform.SetParent (saveObjects.transform);
	}
}
