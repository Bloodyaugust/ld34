using UnityEngine;
using System.Collections;

public class BagHandler : MonoBehaviour {

	string leftState = "IDLE";
	string rightState = "IDLE";
	float baseSpeed = Screen.width / 3f;
	float currentDash = 0f;
	float dashAmount = 1f;
	float dashCost = 0.33f;
	float dashLength = 0.1f;
	float dashSpeed = Screen.width / 0.3f;
	float dashTrap = 0.2f;
	float lastLeft = 0f;
	float lastRight = 0f;
	float speedMultiplier = 1f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 boundedLocation = new Vector3(0, transform.position.y, transform.position.z);
		float translation = 0;

		if (Input.GetKeyUp("left") || Input.GetKeyUp("a")) {
			leftState = "IDLE";
			currentDash = 0f;
		}
		if (Input.GetKeyUp("right") || Input.GetKeyUp("d")) {
			rightState = "IDLE";
			currentDash = 0f;
		}

		if (leftState != "DASH" && rightState != "DASH") {
			if (Input.GetKeyDown("left") || Input.GetKeyDown("a")) {
				leftState = "ACTIVE";

				if (Time.time - dashTrap <= lastLeft && dashAmount >= dashCost) {
					leftState = "DASH";
					currentDash = dashLength + Time.deltaTime;
					dashAmount -= dashCost;
				}
				lastLeft = Time.time;
			}
			if (Input.GetKeyDown("right") || Input.GetKeyDown("d")) {
				rightState = "ACTIVE";

				if (Time.time - dashTrap <= lastRight && dashAmount >= dashCost) {
					rightState = "DASH";
					currentDash = dashLength + Time.deltaTime;
					dashAmount -= dashCost;
				}
				lastRight = Time.time;
			}
		}

		if (leftState == "ACTIVE") {
			translation = -1f * baseSpeed * speedMultiplier * Time.deltaTime;
		} else if (rightState == "ACTIVE") {
			translation = baseSpeed * speedMultiplier * Time.deltaTime;
		}

		if (leftState == "DASH") {
			translation = -1f * dashSpeed * speedMultiplier * Time.deltaTime;
			currentDash -= Time.deltaTime;

			if (currentDash <= 0) {
				leftState = "ACTIVE";
			}
		} else if (rightState == "DASH") {
			translation = dashSpeed * speedMultiplier * Time.deltaTime;
			currentDash -= Time.deltaTime;

			if (currentDash <= 0) {
				rightState = "ACTIVE";
			}
		}

		transform.Translate(translation, 0, 0);

		if (Camera.main.WorldToScreenPoint(transform.position).x < 32f) {
			boundedLocation = Camera.main.ScreenToWorldPoint(new Vector3(32f, 0, 0));
			boundedLocation.y = 0;
			boundedLocation.z = 0;
			transform.position = boundedLocation;
		} else if (Camera.main.WorldToScreenPoint(transform.position).x > Screen.width - 32f) {
			boundedLocation = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 32f, 0, 0));
			boundedLocation.y = 0;
			boundedLocation.z = 0;
			transform.position = boundedLocation;
		}
	}
}
