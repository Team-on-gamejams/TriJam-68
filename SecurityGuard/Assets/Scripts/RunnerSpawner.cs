using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSpawner : MonoBehaviour {
	[SerializeField] CanvasGroup cg = null;
	[SerializeField] Player pl = null;
	[SerializeField] GameMenu gm = null;
	[Space]
	[SerializeField] GameObject[] runners;
	[SerializeField] GameObject[] points;
	[Space]
	[SerializeField] float runnerTime = 2.0f;
	[SerializeField] float timePerSpawn = -0.2f;
	[Space]
	[SerializeField] float speedMin = 1.0f;
	[SerializeField] float speedMax = 2.0f;

	float currTime = 8.0f;
	int currPoint = 0;

	private void Awake() {
		points.Shuffle();

		LeanTween.delayedCall(gameObject, currTime, () => {
			LeanTween.moveLocalY(cg.gameObject, 100f, 0.2f);
			LeanTweenEx.ChangeCanvasGroupAlpha(cg, 0.0f, 0.2f);
		});
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
			r.rs = this;

			if(currPoint == points.Length) {
				currPoint = 0;
				points.Shuffle();
			}
		}
	}

	public void OnLose() {
		currTime = float.MaxValue;
		pl.OnLose();
		gm.ShowLose();
	}
}
