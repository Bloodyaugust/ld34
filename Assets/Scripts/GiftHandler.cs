using UnityEngine;
using System.Collections;

public class GiftHandler : MonoBehaviour {

	public float maxFallSpeed = 128f;
	public float maxRotationSpeed = 64f;
	public float timeToMaxFallSpeed = 2f;

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
}
