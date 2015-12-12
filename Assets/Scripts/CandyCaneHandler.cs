using UnityEngine;
using System.Collections;

public class CandyCaneHandler : MonoBehaviour {

	public float maxFallSpeed = 256f;
	public float maxRotationSpeed = 64f;
	public float timeToMaxFallSpeed = 1.5f;

	GameDispatcherHandler dispatcher;
	float fallSpeed = 1f;
	float timeFalling = 0f;
	float rotationDirection = 0f;
	float rotationSpeed = 0f;

	// Use this for initialization
	void Start () {
		rotationSpeed = Random.value * maxRotationSpeed;

		if (Random.value <= 0.5f) {
			rotationDirection = -1f;
		} else {
			rotationDirection = 1f;
		}

		dispatcher = GameObject.FindWithTag("Dispatcher").GetComponent<GameDispatcherHandler>();
	}

	// Update is called once per frame
	void Update () {
		timeFalling += Time.deltaTime;

		fallSpeed = (timeFalling / timeToMaxFallSpeed) * maxFallSpeed;

		transform.Rotate(new Vector3(0, 0, rotationSpeed * rotationDirection * Time.deltaTime));
		transform.Translate(new Vector3(0, -1f * fallSpeed * Time.deltaTime, 0), Space.World);

		if (Camera.main.WorldToScreenPoint(transform.position).y < -16) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D collidingObject) {
		if (collidingObject.tag == "Player") {
			dispatcher.Message("add-dash");
			Destroy(gameObject);
		}
	}
}
