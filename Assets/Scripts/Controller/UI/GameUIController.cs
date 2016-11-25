using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameUIController : MonoBehaviour {

	public static GameUIController singleton;

	void Awake(){
		singleton = this;
	}

	public void ReturnToEditor(){
		SceneManager.LoadScene (0);
	}
}
