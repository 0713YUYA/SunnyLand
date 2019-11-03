using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //ゲーム管理
using UnityEngine.SceneManagement; //シーンを再読み込み

public class GameManager : MonoBehaviour 
{
	[SerializeField] GameObject gameClearTextObj;
	[SerializeField] GameObject gameOverTextObj;
	[SerializeField] Text scoreText;

	const int MAX_SCORE = 9999; //MAXのスコアを決める

	int score = 0; //スコアの数値

	public int stageNo;  //ステージナンバー

	public void AddScore(int val) //AddScoreはItemManagerで取得する
	{
		score += val;
		if (score > MAX_SCORE) //スコアを増やす　
		{
			score = MAX_SCORE;
		}
		scoreText.text = score.ToString(); //scoreTextの値を反映させる。文字にさせるため、score.Tostring();にする
	}
	//SE 効果音
	[SerializeField]AudioClip clearSE;
	[SerializeField]AudioClip overSE;
	AudioSource audioSource;

	private void Start()
	{
		
	audioSource = GetComponent<AudioSource>();

    }

	public void GameClear()
	{
		gameClearTextObj.SetActive(true);
		audioSource.PlayOneShot(clearSE); //ゲームクリア用のBGMを鳴らす
		Invoke ("ReStartThisScene", 1.5f); //発動関数
		//ReStartThisScene(); //このReStartThisScene();を後につける事によって、先に来ている動作の速さが意味を無くす
	}
	public void GameOver()
	{
		gameOverTextObj.SetActive(true);
		audioSource.Stop (); //ゲームオーバーの時にBGMを止める(鳴らす順番がある)
		audioSource.PlayOneShot(overSE); //ゲームオーバー用のBGMを鳴らす
		Invoke ("ReStartThisScene", 1.5f); //発動関数
		//ReStartThisScene(); //このReStartThisScene();を後につける事によって、先に来ている動作の速さが意味を無くす
	}

	void ReStartThisScene()  //リスタートの関数
	{
		Scene thisScene = SceneManager.GetActiveScene (); //現在のシーンの取得
		SceneManager.LoadScene (thisScene.name); //シーンをロードする
	}
}
