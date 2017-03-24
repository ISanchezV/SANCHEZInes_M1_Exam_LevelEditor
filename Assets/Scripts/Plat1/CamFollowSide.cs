using UnityEngine;
using System.Collections;

public class CamFollowSide : MonoBehaviour {

	private Vector2 velocity;

	public float smoothTimeX;
	public float smoothTimeY;

	public GameObject player;

	public bool bounds;

	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		bounds = player.GetComponent<NewController> ().bind;
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		if (bounds) {
			transform.position = new Vector3 (Mathf.Clamp (posX, minCameraPos.x, maxCameraPos.x),
				Mathf.Clamp (posY, minCameraPos.y, maxCameraPos.y), transform.position.z);
		}
	}
}