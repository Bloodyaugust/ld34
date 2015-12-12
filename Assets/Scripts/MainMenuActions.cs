using UnityEngine;
using System.Collections;

public class MainMenuActions : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void StartGame () {
		Application.LoadLevel("Game");
	}

	public void Stats () {
		Application.LoadLevel("Stats");
	}
}
