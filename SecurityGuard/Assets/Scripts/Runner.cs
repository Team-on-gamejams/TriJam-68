using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour {
	[System.NonSerialized] public float speed = 2.0f;
	[System.NonSerialized] public bool isFlip = false;

	[SerializeField] SpriteRenderer srOutline;
	[SerializeField] SpriteRenderer sr;
	[SerializeField] Rigidbody2D rb;
	[SerializeField] BoxCollider2D bx;

	bool isRunning = false;

	void Start() {
		if (isFlip) {
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			transform.localEulerAngles = new Vector3(0, 0, 6.112f);
		}

		Color c = sr.color;
		c.a = 0.0f;
		sr.color = c;

		LeanTween.value(0.0f, 1.00f, 1.0f).
		setOnUpdate((float a) => {
			c = srOutline.color;
			c.a = a;
			srOutline.color = c;
		})
		.setOnComplete(() => {
			c = sr.color;
			c.a = 1.0f;
			sr.color = c;

			LeanTween.value(1.0f, 0.00f, 1.0f).
			setDelay(Random.Range(0.2f, 1.5f)).
			setOnUpdate((float a) => {
				c = srOutline.color;
				c.a = a;
				srOutline.color = c;
			}).
			setOnComplete(() => {
				isRunning = true;
			});
		});
	}

	void Update() {
		if (isRunning) {
			transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
		}
	}

	private void OnTriggerStay2D(Collider2D collision) {
		if (isRunning) {
			if (collision.CompareTag("Player")) {
				isRunning = false;
				rb.gravityScale = 1.0f;
				rb.freezeRotation = false;
				bx.enabled = false;
				bx.isTrigger = true;
				rb.AddForce(new Vector2(Random.Range(-0.45f, 0.45f), 1.0f).normalized * 1000.0f);
				rb.angularVelocity = Random.Range(-100f, 100f);

				LeanTween.value(1.0f, 0.00f, 3.0f).
				setOnUpdate((float a) => {
					Color c = sr.color;
					c.a = a;
					sr.color = c;
					transform.localScale = Vector3.one * a;
				})
				.setOnComplete(() => {
					Destroy(gameObject);
				});
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (isRunning) {
			if (collision.gameObject.CompareTag("Scene")) {
				isRunning = false;
				Debug.Log("Lose");
			}
		}
	}
}
