using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //追加

public class SelectButtonController : MonoBehaviour {

	public void ButtonClick() {
		switch (transform.name) {
		case "Button1":
			Debug.Log ("3.1415");
			break;
		case "Button2":
			Debug.Log("2.7181");
			break;
		case "Button3":
			Debug.Log("6.022 x 10^23");
			break;
		default:
			break;
		}
	}

}
