using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameDispatcherHandler : MonoBehaviour {

	public GameObject gameOverPanel;
	public Image[] candyCanes;
	public Image[] coalImages;
	public Text[] highscoresText;
	public Text scoreText;
	public Text multiplierText;
	public float giftFillAmount = 0.5f;
	public int score = 0;

	BagHandler bag;
	SpawnerHandler spawner;
	float difficultyMultiplier = 1;
	float scoreMultiplier = 1;
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

		if (message == "bag-full") {
			BagFull();
		}

		if (message == "game-lose") {
			GameLose();
		}

		if (message == "main-menu") {
			MainMenu();
		}

		if (message == "play-again") {
			GameStart();
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
		spawner = GameObject.FindWithTag("Spawner").GetComponent<SpawnerHandler>();

		for (int i = 0; i < candyCanes.Length; i++) {
			candyCanes[i].enabled = false;
		}
		for (int i = 0; i < coalImages.Length; i++) {
			coalImages[i].enabled = false;
		}

		GameStart();
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
		score += amount * (int)scoreMultiplier;
		scoreText.text = "Score: " + score.ToString();
	}

	void BagFull () {
		bag.ResetBag();

		scoreMultiplier++;
		difficultyMultiplier++;
		multiplierText.text = "(x" + scoreMultiplier.ToString() + ")";
		spawner.ResetSpawn(difficultyMultiplier);
		spawner.SetSpawn(true);
		gameOverPanel.SetActive(false);
	}

	void GameLose () {
		scoreMultiplier = 1;
		difficultyMultiplier = 1f;
		spawner.SetSpawn(false);
		gameOverPanel.SetActive(true);
	}

	void GameStart () {
		dashes = 0;
		coal = 0;
		score = 0;
		for (int i = 0; i < coalImages.Length; i++) {
			coalImages[i].enabled = false;
		}
		for (int i = 0; i < candyCanes.Length; i++) {
			candyCanes[i].enabled = false;
		}
		bag.NewBag();

		multiplierText.text = "(x" + scoreMultiplier.ToString() + ")";
		scoreText.text = "Score: " + score.ToString();
		spawner.SetSpawn(true);
		spawner.ResetSpawn(difficultyMultiplier);
		gameOverPanel.SetActive(false);
	}

	void MainMenu () {
		Application.LoadLevel("MainMenu");
	}

	void RemoveDash() {
		candyCanes[dashes - 1].enabled = false;
		dashes--;
	}
}
