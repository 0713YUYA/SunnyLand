using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] GameManager gameManager;
	[SerializeField] LayerMask blockLayer;
	Rigidbody2D rigidbody2D;

	float speed = 0;        //動く時のスピード

	float JumpPower = 600;  //ジャンプ力

	Animator animator;     //アニメーション

	bool isGrounded;//着地しているのか判定

	//SE
	[SerializeField] AudioClip getItemSE;
	[SerializeField] AudioClip jumpSE;
	[SerializeField] AudioClip  satmpSE;
	AudioSource audioSource;

	public enum MOVE_DIRECTION
	{
		STOP,    
		RIGHT,   
		LEFT,
	}
	MOVE_DIRECTION moveDirection = MOVE_DIRECTION.STOP;

	// Use this for initialization
	void Start () 
	{
		//コンポーネントの取得
		rigidbody2D = GetComponent<Rigidbody2D> ();

		animator    = GetComponent<Animator> ();

		audioSource = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		float x = Input.GetAxis("Horizontal");
		animator.SetFloat("speed", Mathf.Abs (x)); //歩く動作のアニメーション
		if (x == 0) 
		{
			//止まる
			moveDirection = MOVE_DIRECTION.STOP;
		}
		else if (x < 0)
		{
			//右に移動
			moveDirection = MOVE_DIRECTION.RIGHT;
		} 
		else if (x > 0)
		{
			//左に移動
			moveDirection = MOVE_DIRECTION.LEFT;
		}
		//地面に着地した時
		if (IsGround ()) 
		{
			//スペースキーを押してジャンプする
			if (Input.GetKeyDown ("space"))
			{
				Jump ();
				animator.SetBool ("isJumping", true); //ジャンプ動作をするアニメーション
				audioSource.PlayOneShot(jumpSE);      //ジャンプした時の効果音
			}
			else {
				animator.SetBool ("isJumping", false); //ジャンプ動作をしないアニメーション	
			}
		}
	}
	private void FixedUpdate()
	{
		switch (moveDirection)
		{
		case MOVE_DIRECTION.STOP:
			speed = 0;
			break;
		case MOVE_DIRECTION.RIGHT:
			transform.localScale = new Vector3 (-5, 5, 1);  //プレイヤーが右に向くようにする
			speed = -3;
			break;
		case MOVE_DIRECTION.LEFT:
			transform.localScale = new Vector3 (5, 5, 1);   //プレイヤーが左に向くようにする
			speed = 3;
			break;
		}
		rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
	}

	void Jump()
	{
		//上方向へ力を加える
		rigidbody2D.AddForce (Vector2.up * JumpPower);

		//地面から離れるのでfalseにする
		isGrounded = false;

	}
	bool IsGround()
	{
		return Physics2D.Linecast(transform.position - transform.right * 0.3f,transform.position - transform.up * 0.1f,blockLayer)
		      || Physics2D.Linecast(transform.position + transform.right * 0.3f,transform.position - transform.up * 0.1f,blockLayer);
	}

	private void OnTriggerEnter2D(Collider2D collision) //オン、トリガーを発動したら
	{
		if (collision.gameObject.tag == "Finish") //もし、ぶつかった時のタグがFinishだったら
		{
 			gameManager.GameClear();
			Debug.Log ("Finish");
		}
		if (collision.gameObject.tag == "Trap") //もし、ぶつかった時のタグがGameOverだったら
		{
			gameManager.GameOver();
			Debug.Log ("Trap");
		}
		if (collision.gameObject.tag == "Item") //もし、ぶつかった時のタグがItemだったら
		{
			collision.gameObject.GetComponent<ItemManager> ().GetItem (); //GetItem(アイテムの関数を呼ぶ出す)
			Debug.Log ("GetItem");
		}
	}
}