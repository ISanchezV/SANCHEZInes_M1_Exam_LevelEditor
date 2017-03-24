using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public string CurrentLevelName;
	public float currentX;
	public float currentY;
	public float currentZ;
	public bool testNewButton = false;

	public bool edOrPlay = true;
	public String layer = "Layer1";
	public bool All = false;

	void Awake () {

		//Si l'objet permanent n'existe pas
		if (control == null) {

			//Ne pas détruire l'objet en
			//changeant de scène
			DontDestroyOnLoad (gameObject);

			//L'objet permanent est celui
			//qui contient ce script
			control = this;

			//Si l'objet permanent n'est pas
			//celui qui contient ce script
		} else if (control != this) {

			//Detruire cet objet
			Destroy (gameObject);
		}
	}

	public void OnLoad() {
		GameControl.control.edOrPlay = false;
	}

}