using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //追加

public class ButtonController : MonoBehaviour 
{

	public void PushAAAButton()
	{
		SceneManager.LoadScene ("Stage1");
		//Debug.Log("ボタンが押されました。");
	}
	// Update is called once per frame
	void Update()
	{
		
	}
     public void GameClear()
    {
		Debug.Log ("GameClear");
		if (Input.GetKeyDown ("space"))
		{

			SceneManager.LoadScene ("Scene_Stage2");
		}
    }
}
