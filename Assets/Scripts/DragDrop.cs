using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour {

	private Camera myCamera; 
	private RaycastHit2D objectEdited; 
	public bool playOrEd;
	public bool beingDrg;
	public Edit contr;

	public GameObject dragged;
	public GameObject del;


	void Start () {
		myCamera = GetComponent<Camera>(); 
		contr = FindObjectOfType<Edit>();
	}

	void FixedUpdate () {
		playOrEd = GameControl.control.edOrPlay;
		if (playOrEd) {
			//Sauvegarde de la position de la souris au début du frame
			Vector3 mousePos = myCamera.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f));

			//Si click gauche appuyé
			if (Input.GetMouseButtonDown (0)) {

				//Envoyer un rayon sur la position de la souris, et garder le résultat dans objectEdited
				objectEdited = Physics2D.Raycast (mousePos, Vector2.zero);
			}
				
			//Si click gauche maintenu
			if (Input.GetMouseButton (0)) {

				//Si le collider de l'objet touché n'est pas nul
				if (objectEdited.collider != null) {
					dragged = objectEdited.collider.gameObject;

					//Si l'objet n'est pas marqué comme fixe ni dans le mauvais layer
					if (!dragged.CompareTag ("nodrg") &&
						dragged.CompareTag(GameControl.control.layer) ||
						!dragged.CompareTag ("nodrg") &&
						GameControl.control.All) {

						//La position de l'objet tenu devient celle de la souris
						objectEdited.collider.transform.position = mousePos;
						beingDrg = true;
					}
			
				}
			}

			//Quand le click gauche est relâche, reset des objets et attributs nécessaires
			if (Input.GetMouseButtonUp (0)) {
				objectEdited = new RaycastHit2D ();
				beingDrg = false;
				Invoke ("deleteObj", 0.1f);
			}
		}
	}

	void deleteObj () {
		dragged = null;
	}

}
