using UnityEngine;
using System.Collections;

public class GameDispatcherHandler : MonoBehaviour {

	public float candyCaneDashAmount = 0.33f;
	public float giftFillAmount = 0.5f;

	BagHandler bag;

	public void Message (string message) {
		if (message == "add-dash") {
			AddDash();
		}

		if (message == "add-fill") {
			AddFill();
		}
	}

	// Use this for initialization
	void Start () {
		bag = GameObject.FindWithTag("Player").GetComponent<BagHandler>();
	}

	// Update is called once per frame
	void Update () {

	}

	void AddDash () {
		bag.AddDash(candyCaneDashAmount);
	}

	void AddFill () {
		bag.AddFill(giftFillAmount);
	}
}
