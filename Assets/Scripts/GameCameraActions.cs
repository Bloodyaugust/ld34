using UnityEngine;
using System.Collections;

public class GameCameraActions : MonoBehaviour {

	Camera worldCam;

	// Use this for initialization
	void Start () {
		worldCam = GetComponent<Camera>();

		transform.position = new Vector3(0, worldCam.orthographicSize - 32f, -10);
	}

	// Update is called once per frame
	void Update () {

	}
}
