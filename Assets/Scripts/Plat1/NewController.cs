using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewController : MonoBehaviour {

	const int STATE_IDLE = 0;
	const int STATE_CORRER = 1;

	public bool isGrounded;
	public bool bind;
	public IsGrounded tester;
	public Rigidbody2D rgb;
	public bool status;
	public GameObject SelectMenu;

	public float speed = 5f;
	public float moveX;
	public float moveY;
	public float scroll;
	public float jumpHeight;
	public float gravityStore;
	public Vector3 startPos;

	public bool inLadder;
	public GameObject cam, bg;
	public Animator anim;
	public bool dir = true;
	public int state;
	public int blinkTime;
	public GameObject animate;

	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		//moveY = 0;
		gravityStore = rgb.gravityScale;
		status = GameControl.control.edOrPlay;
		startPos = transform.position;

		//anim = animate.GetComponent<Animator>();
		bg = Camera.main.gameObject.transform.GetChild(0).gameObject;
	}

	// Update is called once per frame
	void Update () {
		status = GameControl.control.edOrPlay;
		if (bg == null)
			bg = Camera.main.gameObject.transform.GetChild(0).gameObject;

		//Si le jeu est en mode jouer, alors le personnage peut se déplacer 
		if (!status) {
			//Reset des propriétés qui ont été changées pendant le mode édition
			GetComponent<Rigidbody2D> ().isKinematic = false;
			if (Camera.main.orthographicSize != 5) {
				Camera.main.orthographicSize = 5;
				float wScreenHeight = Camera.main.orthographicSize * 1.35F;
				float wScreenWidth = wScreenHeight / Screen.height * Screen.width;
				bg.transform.localScale = new Vector3 (wScreenWidth / bg.GetComponent<SpriteRenderer> ().sprite.bounds.size.x,  
					wScreenHeight / bg.GetComponent<SpriteRenderer> ().sprite.bounds.size.y, 0);
			}
			bind = true;
			if (SelectMenu != null && SelectMenu.activeInHierarchy)
				SelectMenu.SetActive (false);

			//Si le joueur est tombé de l'écran
			if (transform.position.y < -15) {
				transform.position = startPos;
			}

			//isGrounded dit si le personnage est sur le sol
			isGrounded = tester.grounded;
			moveX = Input.GetAxis ("Horizontal");

			//Si le personnage est sur le sol, alors il peut se déplacer et sauter
			if (isGrounded) {
				if (Input.GetKeyDown (KeyCode.Space)) {
					rgb.velocity = new Vector2 (rgb.velocity.x, jumpHeight);
				} else
					rgb.velocity = new Vector2 (moveX * speed, rgb.velocity.y);
				if (moveX > 0) {
					//changeState (STATE_CORRER);
					if (!dir) {
						dir = true;
						changeDirection (dir);
					}
				} else if (moveX < 0) {
					//changeState (STATE_CORRER);
					if (dir) {
						dir = false;
						changeDirection (dir);
					}
				}

				if (moveX > 0) {
					//changeState (STATE_IDLE);

				} else if (moveX < 0) {
					//changeState (STATE_IDLE);
				}
			}
		} else {
			bind = false;
			GetComponent<Rigidbody2D> ().isKinematic = true;
			rgb.gravityScale = 0f;
			if (SelectMenu != null && !SelectMenu.activeInHierarchy)
				SelectMenu.SetActive (true);

			//Mouvement de la caméra et zoom pour pouvoir voir l'ensemble du niveau pendant l'édition
			moveX = 3 * Input.GetAxis ("Horizontal");
			moveY = 3 * Input.GetAxis ("Vertical");
			scroll = -50 * Input.GetAxis ("MouseScroll");
			cam.transform.parent = null;
			cam.transform.Translate (new Vector3 (moveX * Time.deltaTime, moveY * Time.deltaTime, 0f));
			Camera.main.orthographicSize += scroll * Time.deltaTime;

			//La taille de l'image de fond reste constante avec les zooms
			float wScreenHeight = Camera.main.orthographicSize * 1.35F;
			float wScreenWidth = wScreenHeight / Screen.height * Screen.width;
			bg.transform.localScale = new Vector3 (wScreenWidth / bg.GetComponent<SpriteRenderer> ().sprite.bounds.size.x,  
				wScreenHeight/ bg.GetComponent<SpriteRenderer> ().sprite.bounds.size.y, 0);
		}

		//Si le personnage est dans une échelle, alors il peut bouger sur l'axe verticale
		if (inLadder) {
			//La gravité est nulle dans l'échelle
			rgb.gravityScale = 0f;

			rgb.velocity = new Vector2 (moveX * speed, Input.GetAxis ("Vertical") * speed);

			//Si le personnage monte, son collisioneur disparait afin de pouvoir passer à  
			//travers les plateformes comme s'il était derrière elles
			if (Input.GetAxis("Vertical") > 0)
				GetComponent<BoxCollider2D> ().isTrigger = true;
		} else {
			rgb.gravityScale = gravityStore;
			GetComponent<BoxCollider2D> ().isTrigger = false;
		}

	}

	//Collision avec des objets qui causent game over instant
	void OnTriggerEnter2D(Collider2D c) {
		if (!status && c.gameObject.name == "Piques") {
			Debug.Log ("ouch");
			blinkTime = 8;
			InvokeRepeating ("GameOver", 0, 0.5f);
		}
	}

	//Changer l'animation
	/*void changeState (int state) {
		switch (state) {
		case STATE_IDLE:
			anim.SetInteger ("State", STATE_IDLE);
			break;
		case STATE_CORRER:
			anim.SetInteger ("State", STATE_CORRER);
			break;
		}
	}*/

	//Changer la direction du sprite
	void changeDirection(bool dir) {
		if (dir) 
			transform.Rotate (0, -180, 0);
		else if (!dir)
			transform.Rotate (0, 180, 0);
	}

	//Blink puis game over
	void GameOver() {
		if (blinkTime > 0) {
			animate.GetComponent<Renderer> ().enabled = !animate.GetComponent<Renderer> ().enabled;
			blinkTime--;
		}
		else if (blinkTime == 0) {
			transform.position = startPos;
			animate.GetComponent<Renderer> ().enabled = true;
			blinkTime = -1;
		}
	}

}

