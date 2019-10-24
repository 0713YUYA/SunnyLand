using UnityEngine;

public class ItemManager: MonoBehaviour 
{
	GameManager gameManager; //スコア取得のため

	private void Start()
	{
		          //GameObject.FindでGameManagerを探して、GetComponentでGameManagerを取得する
       gameManager = GameObject.Find("GameManager").GetComponent<GameManager> (); //ヒエラルキー上からGameObjictを探してあげる　
	}

	public void GetItem()
	{
	    gameManager.AddScore(100); //破壊される前に
		Debug.Log(100);
     Destroy(this.gameObject);	
	}
}
