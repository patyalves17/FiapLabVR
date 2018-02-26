using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour {

	public Camera cameraRayCasting;
	public int distanceToMove = 100;

	private bool isMoving = false;
	private int speed = 10;
	private RaycastHit hit;
	private Vector3 startPoint;
	private Vector3 endPoint;
	private float startTime;
	private float journeyLength;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {

		Ray ray = cameraRayCasting.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));

		if (GvrControllerInput.TouchDown || Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast(ray, out hit, distanceToMove)) {
				if (hit.transform.tag == "TargetToMove") {
					Debug.Log ("Find somewhere to move"); 

					startPoint = transform.position;
					endPoint = hit.transform.position;
					startTime = Time.time;
					journeyLength = Vector3.Distance (startPoint, endPoint);
					isMoving = true;
				}
			}
		}

		if (isMoving) {
			float distanceCovered = (Time.time - startTime) * speed;
			float fracJourney = distanceCovered / journeyLength;
			Vector3 move = Vector3.Lerp (startPoint, endPoint, fracJourney);
//			this.transform.position = new Vector3(move.x, this.transform.position.y, move.z);
			this.transform.position = new Vector3(move.x, move.y, move.z);
			isMoving = (this.transform.position != endPoint);
		}
	}
}
