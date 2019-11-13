using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //追加


public class SelectButtonController : MonoBehaviour
{
	

	public void SelectButtonClick()
	{

		Debug.Log ("SelectButtonClick");
		switch (transform.name)
		{
		     //Debug.Log ("transform.name");
			case  "ButtonStage1":
			Debug.Log ("ButtonStage1");
			SceneManager.LoadScene ("Stage1");
			//Debug.Log("ボタンが押されました。");
				break;
			case  "ButtonStage2":
			Debug.Log("ButtonStage2");
			SceneManager.LoadScene ("Stage2");
			//Debug.Log("ボタンが押されました。");
				break;
			case  "ButtonStage3":
			Debug.Log("ButtonStage3");
			SceneManager.LoadScene ("Stage3");
			//Debug.Log("ボタンが押されました。");
				break;
			default:
				break;
		}

	}

}
