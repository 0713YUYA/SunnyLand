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

	bool isDead; //負けたらプレイヤーを動作させないようにする

	bool isGrounded;//着地しているのか判定

	private bool isLeftButtonDown = false;//左ボタン押下の判定（追加）11/3

	private bool isRightButtonDown = false;//右ボタン押下の判定（追加）11/3

	private bool goJump = false; //ジャンプしたか否か

	//private bool can Jump = false; //ブロックに設置しているか否か

	private bool usingButtons = false; //ボタンを押しているか否か
	//SE
	[SerializeField] AudioClip getItemSE;
	[SerializeField] AudioClip jumpSE;
	[SerializeField] AudioClip stampSE;
	AudioSource audioSource;

	public enum MOVE_DIRECTION
	{
		STOP,    
		RIGHT,   
		LEFT,
		JUMP,
	}
	MOVE_DIRECTION moveDirection = MOVE_DIRECTION.STOP;

	// Use this for initialization
	void Start () 
	{
		//コンポーネントの取得
		rigidbody2D = GetComponent<Rigidbody2D> ();

		animator    = GetComponent<Animator> ();

		audioSource = GetComponent<AudioSource> ();

		isDead = false; //最初は、プレイヤーが死んでいないので

	}
	
	// Update is called once per frame
	void Update () 
	{
		//死んだら、何も処理しない
		if (isDead)
		{
			return;
		}
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
				audioSource.PlayOneShot(jumpSE);//ジャンプした時の効果音
			}
			else {
				animator.SetBool ("isJumping", false); //ジャンプ動作をしないアニメーション	
			}
		}
	}
	private void FixedUpdate()
	{
		//死んだら、何も処理しない
		if (isDead)
		{
			return;
		}

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
		return Physics2D.Linecast (transform.position - transform.right * 0.3f, transform.position - transform.up * 0.1f, blockLayer)
		|| Physics2D.Linecast (transform.position + transform.right * 0.3f, transform.position - transform.up * 0.1f, blockLayer);
	}
	private void OnTriggerEnter2D(Collider2D collision) //オン、トリガーを発動したら
	{
		//死んだら、何も処理しない
		if (isDead)
		{
			return;
		}

		if (collision.gameObject.tag == "Finish") //もし、ぶつかった時のタグがFinishだったら
		{
 			gameManager.GameClear();
			//Debug.Log ("Finish"); //スラッシュを外したらデバック確認ができる
		}
		if (collision.gameObject.tag == "Trap") //もし、ぶつかった時のタグがGameOverだったら
		{
			PlayerDeath();
			//Debug.Log ("Trap");   //スラッシュを外したらデバック確認ができる
		}
		if (collision.gameObject.tag == "Item") //もし、ぶつかった時のタグがItemだったら
		{
			collision.gameObject.GetComponent<ItemManager> ().GetItem (); //GetItem(アイテムの関数を呼ぶ出す)
			//Debug.Log ("GetItem");  //スラッシュを外したらデバック確認ができる
			audioSource.PlayOneShot(getItemSE);//アイテムをとったら、BGMを鳴らす
		}
		if (collision.gameObject.tag == "Enemy") //もし、ぶつかった時のタグがGameOverだったら
	    {
			EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager> (); //Enemyのコンポーネントを取得
			//Debug.Log ("Enemy");  //スラッシュを外したらデバック確認ができる
			//Debug.Log(this.transform.position.y > enemy.transform.position.y); 下のif文のデバック確認
			if(this.transform.position.y > enemy.transform.position.y)//プレイヤーと敵の位置判定で上で当ったら敵を倒す。もし正面だったらプレイヤーが負ける（プレイヤー側）
			{
				//踏んだら
				//プレイヤーをジャンプさせる
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,0);
				Jump ();
				//Enemyが消える
				enemy.DestroyEnemy();
				//Debug.Log("enemy.DestroyEnemy"); 
				audioSource.PlayOneShot(stampSE);//ジャンプで敵を踏んだら、BGMを鳴らす
			}
			else
			{
				// 正面からぶつかったらプレイヤーの負け  
				PlayerDeath();
			}
	    }
	}
	void PlayerDeath() //プレイヤーがやられた時のアニメーション
	{
		isDead = true;//ここにも動作させないように、判定をつける
		animator.Play("PlayerDeathAnimation");
		rigidbody2D.velocity = new Vector2(0,0); //プレイヤーが負けたら、速度を０にして、ジャンプさせる
		Jump ();
		CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>(); //プレイヤーが負けたら、落ちたり上がったりする動作
		Destroy (capsuleCollider2D);
		gameManager.GameOver();
	}
	//左ボタンを押した
	public void PushLeftButton()
	{
		//Debug.Log("PushLeftButton");
		moveDirection = MOVE_DIRECTION.LEFT;
		//Debug.Log("LEFT");
		usingButtons = true;
		Debug.Log ("true");
	}
	//右ボタンを押した
	public void PushRightButton()
	{
		//Debug.Log ("PushRihtButton");
		moveDirection = MOVE_DIRECTION.RIGHT;
		//Debug.Log ("RIGHT");
		usingButtons = true;
		Debug.Log ("true");

	}
	//移動ボタンを放した
	public void ReleaseMoveButton()
	{
		//Debug.Log ("ReleaseMoveButton");
		moveDirection = MOVE_DIRECTION.STOP;
		//Debug.Log ("STOP");
		usingButtons = false;
		Debug.Log ("false");
	}
	//ジャンプボタンを押した
	public void PushJumpButton()
	{	
		//Debug.Log("PushjumpButton");
		moveDirection = MOVE_DIRECTION.JUMP;
		//Debug.Log("JUMP");
		goJump = true;
		Debug.Log ("true");
		//if (this.transform.position.y < 0.5f)
		//{
			//this.Rigidbody2D.AddForce (this.transform.up * this.upForce);
		//}
		//プレイヤーを矢印キーまたはボタンに応じて左右に移動させる（追加）
		//if ((Input.GetKey (KeyCode.LeftArrow) || this.isLeftButtonDown) && -this.movableRange < this.transform.position.x) 
		//{
			//左に移動
			//this.Rigidbody2D.AddForce (-this.turnForce, 0, 0);
		//} else if ((Input.GetKey (KeyCode.RightArrow) || this.isRightButtonDown) && this.transform.position.x < this.movableRange) {
			//右に移動
			//this.Rigidbody2D.AddForce (this.turnForce, 0, 0);
		//} 
	}

}