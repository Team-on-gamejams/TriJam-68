using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSpawner : MonoBehaviour {
	[SerializeField] GameObject[] runners;
	[SerializeField] GameObject[] points;
	[Space]
	[SerializeField] float runnerTime = 2.0f;
	[SerializeField] float timePerSpawn = -0.2f;
	[Space]
	[SerializeField] float speedMin = 1.0f;
	[SerializeField] float speedMax = 2.0f;

	float currTime = 3.0f;
	int currPoint = 0;

	private void Awake() {
		points.Shuffle();
	}

	void Update() {
		currTime -= Time.deltaTime;

		if(currTime <= 0) {
			currTime += runnerTime;
			if(runnerTime > 0.5f)
				runnerTime -= timePerSpawn;

			Runner r = Instantiate(runners.Random(), points[currPoint++].transform.position, Quaternion.identity, transform).GetComponent<Runner>();
			r.isFlip = Random.Range(0, 2) == 1;
			r.speed = Random.Range(speedMin, speedMax);

			if(currPoint == points.Length) {
				currPoint = 0;
				points.Shuffle();
			}
		}
	}
}
