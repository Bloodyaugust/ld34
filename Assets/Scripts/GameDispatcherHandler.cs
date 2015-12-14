using UnityEngine;
using System.Collections;

public class GameDispatcherHandler : MonoBehaviour {

	public float giftFillAmount = 0.5f;

	BagHandler bag;

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
	}

	// Use this for initialization
	void Start () {
		bag = GameObject.FindWithTag("Player").GetComponent<BagHandler>();
	}

	// Update is called once per frame
	void Update () {

	}

	void AddCoal () {
		bag.AddCoal(1);
	}

	void AddDash () {
		bag.AddDash(1);
	}

	void AddFill () {
		bag.AddFill(giftFillAmount);
	}
}
