using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameDispatcherHandler : MonoBehaviour {

	public Image[] candyCanes;
	public Image[] coalImages;
	public Text scoreText;
	public float giftFillAmount = 0.5f;
	public int score = 0;

	BagHandler bag;
	int coal = 0;
	int dashes = 0;

	public void Message (string message) {
		if (message == "add-dash") {
			AddDash();
		}

		if (message == "add-fill") {
			AddFill();
		}

		if (message == "add-coal") {
			AddCoal();
		}

		if (message == "remove-dash") {
			RemoveDash();
		}
	}
	public void Message (string message, int amount) {
		if (message == "add-score") {
			AddScore(amount);
		}
	}

	// Use this for initialization
	void Start () {
		bag = GameObject.FindWithTag("Player").GetComponent<BagHandler>();

		for (int i = 0; i < candyCanes.Length; i++) {
			candyCanes[i].enabled = false;
		}
		for (int i = 0; i < coalImages.Length; i++) {
			coalImages[i].enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void AddCoal () {
		if (coal + 1 < 4) {
			bag.AddCoal(1);
			coalImages[coal].enabled = true;
			coal++;
		}
	}

	void AddDash () {
		if (dashes + 1 < 4) {
			bag.AddDash(1);
			candyCanes[dashes].enabled = true;
			dashes++;
		}
	}

	void AddFill () {
		bag.AddFill(giftFillAmount);
	}

	void AddScore (int amount) {
		score += amount;
		scoreText.text = "Score: " + score.ToString();
	}

	void RemoveDash() {
		candyCanes[dashes - 1].enabled = false;
		dashes--;
	}
}
