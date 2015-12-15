using UnityEngine;
using System.Collections;

public class SpawnerHandler : MonoBehaviour {

	public GameObject candyCanePrefab;
	public GameObject coalPrefab;
	public GameObject giftPrefab;
	public GameObject snowflakePrefab;
	public float baseMaxSpawnRate = 0.8f;
	public float maxSpawnRate = 0.3f;
	public float minSpawnRate = 1.5f;
	public float baseSpawnRateCandyCane = 0.2f;
	public float spawnRateCandyCane = 0.2f;
	public float baseSpawnRateCoal = 0.1f;
	public float spawnRateCoal = 0.1f;
	public float timeToMaxSpawnRate = 120f;
	public float lastSnowflakeSpawn = 0f;
	public float snowflakeSpawnInterval = 0.2f;

	float difficultyModifier = 1f;
	float spawnRate = 0f;
	float timeSpawning = 0f;
	float timeToNextSpawn = 0f;
	string state = "SPAWN";

	public void SetSpawn(bool spawnOn) {
		if (spawnOn) {
			state = "SPAWN";
		} else {
			state = "IDLE";
		}
	}

	public void ResetSpawn (float diffMod) {
		difficultyModifier = diffMod;
		spawnRate = minSpawnRate;
		maxSpawnRate = Mathf.Clamp(baseMaxSpawnRate - (difficultyModifier * 0.1f), 0.1f, baseMaxSpawnRate);
		spawnRateCandyCane = Mathf.Clamp(baseSpawnRateCandyCane - (difficultyModifier * 0.05f), 0.05f, baseSpawnRateCandyCane);
		spawnRateCoal = Mathf.Clamp(baseSpawnRateCoal + (difficultyModifier * 0.05f), 0.1f, 0.5f);
		timeSpawning = 0f;
		timeToNextSpawn = 0f;
	}

	// Use this for initialization
	void Start () {
		spawnRate = minSpawnRate;
	}

	// Update is called once per frame
	void Update () {
		if (state != "IDLE") {
			timeSpawning += Time.deltaTime;
			timeToNextSpawn -= Time.deltaTime;
			lastSnowflakeSpawn += Time.deltaTime;

			if (timeToNextSpawn <= 0) {
				spawnRate = minSpawnRate - ((timeSpawning / timeToMaxSpawnRate) * (minSpawnRate - maxSpawnRate));

				if (spawnRate < maxSpawnRate) {
					spawnRate = maxSpawnRate;
				}

				timeToNextSpawn = spawnRate;
				Spawn();
			}

			if (lastSnowflakeSpawn >= snowflakeSpawnInterval) {
				lastSnowflakeSpawn = 0f;
				Instantiate(snowflakePrefab, Camera.main.ScreenToWorldPoint(new Vector3(Mathf.Clamp(Random.value * Screen.width, 16, Screen.width - 16), Screen.height + 16, 9)), Quaternion.identity);
			}
		}
	}

	void Spawn () {
		float spawnDecider = Random.value;

		if (spawnDecider <= spawnRateCoal) {
			Instantiate(coalPrefab, Camera.main.ScreenToWorldPoint(new Vector3(Mathf.Clamp(Random.value * Screen.width, 16, Screen.width - 16), Screen.height + 16, 9)), Quaternion.identity);
		} else if (spawnDecider > spawnRateCoal && spawnDecider <= spawnRateCoal + spawnRateCandyCane) {
			Instantiate(candyCanePrefab, Camera.main.ScreenToWorldPoint(new Vector3(Mathf.Clamp(Random.value * Screen.width, 16, Screen.width - 16), Screen.height + 16, 9)), Quaternion.identity);
		} else {
			Instantiate(giftPrefab, Camera.main.ScreenToWorldPoint(new Vector3(Mathf.Clamp(Random.value * Screen.width, 16, Screen.width - 16), Screen.height + 16, 9)), Quaternion.identity);
		}
	}
}
