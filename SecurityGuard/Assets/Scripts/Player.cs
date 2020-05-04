using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	[SerializeField] float speed = 0;
	[HideInInspector] [SerializeField] Rigidbody2D rb;
	[HideInInspector] [SerializeField] Animator anim;

	Vector2 moveVector;
	bool isCanMove = true;

#if UNITY_EDITOR
	private void OnValidate() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
#endif

	void FixedUpdate() {
		if (isCanMove && moveVector.magnitude >= 0.05f) {
			rb.velocity = moveVector.normalized * speed;
			anim.SetBool("IsRun", true);
		}
		else {
			rb.velocity = Vector2.zero;
			anim.SetBool("IsRun", false);
		}
	}

	public void OnMove(InputAction.CallbackContext context) {
		moveVector = context.ReadValue<Vector2>();
	}

	public void OnLose() {
		isCanMove = false;
	}
}
