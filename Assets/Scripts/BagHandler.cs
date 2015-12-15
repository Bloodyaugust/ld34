using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BagHandler : MonoBehaviour {

	public Sprite[] bagImages;

	GameDispatcherHandler dispatcher;
	SpriteRenderer bagSprite;
	string leftState = "IDLE";
	string rightState = "IDLE";
	float baseSpeed = Screen.width / 1.5f;
	float currentDash = 0f;
	float dashLength = 0.1f;
	float dashSpeed = Screen.width / 0.3f;
	float dashTrap = 0.2f;
	public float fillAmount = 0f;
	float lastLeft = 0f;
	float lastRight = 0f;
	float maxFillAmount = 12f;
	float speedMultiplier = 1f;
	int currentSprite = 0;
	int dashCost = 1;
	public int coal = 0;
	public int coalMax = 3;
	public int dashes = 0;

	public void AddCoal (int amount) {
		coal += amount;
	}

	public void AddDash (int amount) {
		dashes += amount;
	}

	public void AddFill (float amount) {
		fillAmount += amount;
	}

	public void ResetBag() {
		fillAmount = 0f;
		currentSprite = 0;
		bagSprite.sprite = bagImages[0];
	}

	public void NewBag() {
		currentSprite = 0;
		bagSprite.sprite = bagImages[0];
		fillAmount = 0;
		coal = 0;
		dashes = 0;
	}

	// Use this for initialization
	void Start () {
		dispatcher = GameObject.FindWithTag("Dispatcher").GetComponent<GameDispatcherHandler>();
		bagSprite = gameObject.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 boundedLocation = new Vector3(0, transform.position.y, transform.position.z);
		float translation = 0;
		int bagFillStage = (int)Mathf.Clamp(Mathf.Round((fillAmount / maxFillAmount) * 8), 0, 8);

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

				if (Time.time - dashTrap <= lastLeft && dashes >= dashCost) {
					leftState = "DASH";
					currentDash = dashLength + Time.deltaTime;
					dashes -= dashCost;
					dispatcher.Message("remove-dash");
				}
				lastLeft = Time.time;
			}
			if (Input.GetKeyDown("right") || Input.GetKeyDown("d")) {
				rightState = "ACTIVE";

				if (Time.time - dashTrap <= lastRight && dashes >= dashCost) {
					rightState = "DASH";
					currentDash = dashLength + Time.deltaTime;
					dashes -= dashCost;
					dispatcher.Message("remove-dash");
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

		if (bagFillStage != currentSprite) {
			currentSprite = bagFillStage;
			bagSprite.sprite = bagImages[currentSprite];
		}

		if (fillAmount >= maxFillAmount) {
			dispatcher.Message("bag-full");
		}

		if (coal >= coalMax) {
			dispatcher.Message("game-lose");
		}
	}
}
