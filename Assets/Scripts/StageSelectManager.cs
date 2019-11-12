using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour {

	public void OnStartSelectButtonClicled()
	{
		SceneManager.LoadScene ("Stage1");
	}
}
