using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //追加

public class StageNumber : MonoBehaviour 
{
	private Text stageNumberText;

	// Use this for initialization
	void Start () 
	{
		stageNumberText = this.gameObject.GetComponent<Text>();

		//追加
		//現在のシーンの名前を取得してtextプロパティにセットする（ポイント）
		stageNumberText.text = SceneManager.GetActiveScene ().name;
	}
	
	// Update is called once per frame
	void Update () 
	{
		stageNumberText.color = Color.Lerp (stageNumberText.color, new Color (1, 1, 1, 0), 0.5f * Time.deltaTime);
	}
}
