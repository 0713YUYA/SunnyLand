using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAnimation : MonoBehaviour 
{
	//Debug.Log("OnCompleteAnimation");
	//アニメーションが終了しましたよという関数
	public void OnCompleteAnimation () 
	{
		//Debug.Log("OnCompleteAnimation");
		Destroy (this.gameObject); //アニメーションが終了したら自分自身を破壊しなさいという意味
		//Debug.Log(this.gameObject);
	}
	   
}
