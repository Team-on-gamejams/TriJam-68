using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {
	[SerializeField] CanvasGroup loseGroup;

	public void ShowLose() {
		LeanTweenEx.ChangeCanvasGroupAlpha(loseGroup, 1.0f, 0.2f);
		loseGroup.interactable = loseGroup.blocksRaycasts = true;
	}

	public void Restart() {
		SceneManager.LoadScene(0);
	}
}
