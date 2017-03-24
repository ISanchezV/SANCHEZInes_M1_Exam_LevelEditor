using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Delete : MonoBehaviour {

	public bool beingDragged;
	public DragDrop drg;
	public GameObject objDragged;
	private Color col;

	public GameObject ply;

	// Use this for initialization
	void Start () {
		drg = FindObjectOfType<DragDrop> ();

	}

	// Update is called once per frame
	void Update () {
		beingDragged = drg.beingDrg;
		objDragged = drg.dragged;
	}

	void OnTriggerStay2D (Collider2D other) {

		//Si l'objet n'est pas notre avatar
		if (other.gameObject != ply) {

			//Si nous sommes en train de déplacer un objet
			if (beingDragged) {

				//La couleur de la zone devient rouge (feedback)
				GetComponent<Image> ().color = new Color (1f, 0, 0, 0.5f);

				//Si l'objet est relâché et qu'il n'est pas la zone elle même
			} else if (!beingDragged && objDragged != this.gameObject) {

				//L'objet est détruit et la couleur redevient l'initiale
				Destroy (other.gameObject);
				GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.3f);
			}
		}
	}

	//La couleur du bouton redevient normale
	void OnTriggerExit2D (Collider2D other) {
		GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.3f);
	}
}
