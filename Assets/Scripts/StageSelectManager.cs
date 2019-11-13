using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{

	[SerializeField] string[] stageName; //ステージ名　11/8追加

	public int CurrentstageNo = 0;

	StageSelectManager stageselectManager;

	//下のところから追加11/8
	//次のステージに進む処理
	public void NextStageThisScene()
	{
		Debug.Log ("NextStageThisScene");
		CurrentstageNo += 1;
		//コルーチンを実行
		StartCoroutine(WaitForLoadScene());
	}

	//シーンの読み込みと待機を行うコルーチン
	IEnumerator WaitForLoadScene()
	{
		//シーンを非同期で読込し、読み込まれるまで待機する
		yield return SceneManager.LoadSceneAsync(stageName[CurrentstageNo]);
	}

	//プレイヤーが当たり判定に入った時の処理
	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("OntriggerEnter");
		if(other.gameObject.tag == "Player")
		{
			stageselectManager.NextStageThisScene();

		}
	}
}
