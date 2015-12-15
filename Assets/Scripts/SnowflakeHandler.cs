using UnityEngine;
using System.Collections;

public class SnowflakeHandler : MonoBehaviour {

	public Sprite[] snowflakeImages;
	public float maxFallSpeed = 256f;
	public float maxRotationSpeed = 64f;
	public float timeToMaxFallSpeed = 2f;

	SpriteRenderer snowflakeSprite;
	float fallScalar = 0f;
	float fallSpeed = 1f;
	float timeFalling = 0f;
	float rotationDirection = 0f;
	float rotationSpeed = 0f;

	// Use this for initialization
	void Start () {
		int selectedSprite = (int)(Random.value * (float)(snowflakeImages.Length - 1));

		rotationSpeed = Random.value * maxRotationSpeed;

		if (Random.value <= 0.5f) {
			rotationDirection = -1f;
		} else {
			rotationDirection = 1f;
		}

		snowflakeSprite = gameObject.GetComponent<SpriteRenderer>();

		snowflakeSprite.sprite = snowflakeImages[selectedSprite];
	}

	// Update is called once per frame
	void Update () {
		timeFalling += Time.deltaTime;
		fallScalar = Mathf.Clamp(timeFalling / timeToMaxFallSpeed, 0f, 1f);

		fallSpeed = Mathf.Sin(fallScalar * (Mathf.PI / 2)) * maxFallSpeed;

		transform.Rotate(new Vector3(0, 0, rotationSpeed * rotationDirection * Time.deltaTime));
		transform.Translate(new Vector3(0, -1f * fallSpeed * Time.deltaTime, 0), Space.World);

		if (Camera.main.WorldToScreenPoint(transform.position).y < -16) {
			Destroy(gameObject);
		}
	}
}
