﻿using UnityEngine;
using System.Collections;

public class SpawnerHandler : MonoBehaviour {

	public GameObject giftPrefab;
	public float maxSpawnRate = 0.3f;
	public float minSpawnRate = 3f;
	public float timeToMaxSpawnRate = 120f;

	float spawnRate = 0f;
	float timeSpawning = 0f;
	float timeToNextSpawn = 0f;

	// Use this for initialization
	void Start () {
		spawnRate = minSpawnRate;
	}

	// Update is called once per frame
	void Update () {
		timeSpawning += Time.deltaTime;
		timeToNextSpawn -= Time.deltaTime;

		if (timeToNextSpawn <= 0) {
			spawnRate = minSpawnRate - ((timeSpawning / timeToMaxSpawnRate) * (minSpawnRate - maxSpawnRate));

			if (spawnRate < maxSpawnRate) {
				spawnRate = maxSpawnRate;
			}

			timeToNextSpawn = spawnRate;
			Spawn();
		}
	}

	void Spawn () {
		Instantiate(giftPrefab, Camera.main.ScreenToWorldPoint(new Vector3(Random.value * Screen.width, Screen.height + 16, 9)), Quaternion.identity);
	}
}