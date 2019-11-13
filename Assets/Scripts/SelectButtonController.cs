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
			//SelectButtonClick.WriteLine("Stage1");
				break;
			case  "ButtonStage2":
			Debug.Log("ButtonStage2");
			//SelectButtonClick.WriteLine("Stage2");
				break;
			case  "ButtonStage3":
			Debug.Log("ButtonStage3");
			//SelectButtonClick.WriteLine("Stage3");
				break;
			default:
				break;
		}

	}

}
