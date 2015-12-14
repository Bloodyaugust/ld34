using UnityEngine;
using System.Collections;

public class CoalHandler : MonoBehaviour {

	public float maxFallSpeed = 384f;
	public float timeToMaxFallSpeed = 2f;

	GameDispatcherHandler dispatcher;
	float fallScalar = 0f;
	float fallSpeed = 1f;
	float timeFalling = 0f;

	// Use this for initialization
	void Start () {
		dispatcher = GameObject.FindWithTag("Dispatcher").GetComponent<GameDispatcherHandler>();
	}

	// Update is called once per frame
	void Update () {
		timeFalling += Time.deltaTime;
		fallScalar = Mathf.Clamp(timeFalling / timeToMaxFallSpeed, 0f, 1f);

		fallSpeed = Mathf.Sin(fallScalar * (Mathf.PI / 2)) * maxFallSpeed;

		transform.Translate(new Vector3(0, -1f * fallSpeed * Time.deltaTime, 0), Space.World);

		if (Camera.main.WorldToScreenPoint(transform.position).y < -16) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D collidingObject) {
		if (collidingObject.tag == "Player") {
			dispatcher.Message("add-coal");
			Destroy(gameObject);
		}
	}
}
