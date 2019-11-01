using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	//[SerializeField] GameManager gameManager;
	[SerializeField] LayerMask blockLayer;
	[SerializeField] GameObject deathEffect;  //デスアニメーションを取得
	Rigidbody2D rigidbody2D;

	float speed = 0;        //動く時のスピード


	Animator animator;     //アニメーション

	bool isGrounded;//着地しているのか判定

	//SE
	[SerializeField] AudioClip getItemSE;
	//[SerializeField] AudioClip jumpSE;
	[SerializeField] AudioClip  satmpSE;
	AudioSource audioSource;

	public enum MOVE_DIRECTION
	{
		STOP, //STOPになっていたのをRIGHT   
		RIGHT,   
		LEFT,
	}
	                               //STOPをRIGHTに変更にして右向きから変更
	MOVE_DIRECTION moveDirection = MOVE_DIRECTION.RIGHT;

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
		//Debug.Log(IsGround()); スラッシュを外すとif(IsGround));のデバックを調べられる
		if (!IsGround ()) //if(IsGround())はfalseになるのに対し、if(!IsGround())の！を入れる事によってfalseがtrueに変わる
		{
			//向きを変える
			ChangeDirection();
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
			transform.localScale = new Vector3 (-5, 5, 1);  //敵が右に向くようにする
			speed = -3;
			break;
		case MOVE_DIRECTION.LEFT:
			transform.localScale = new Vector3 (5, 5, 1);   //敵が左に向くようにする
			speed = 3;
			break;
		}
		rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
	}

	bool IsGround() //地面に対しての当たり判定で、地面から落ちないように左右に動くように敵の鼻から下の地面に線を引く
	{
		Vector3 startVec = transform.position + transform.right * 0.16f* transform.localScale.x;
		Vector3 endVec = startVec - transform.up * 1f;
		Debug.DrawLine(startVec,endVec);
		return Physics2D.Linecast(startVec,endVec,blockLayer);
		}

	void ChangeDirection()//向きを変える関数
	{
		if (moveDirection == MOVE_DIRECTION.RIGHT) //もし、右向きだったら 
		{
			//左に移動
			moveDirection = MOVE_DIRECTION.LEFT;
		}
		else
		{
		//右に移動
		    moveDirection = MOVE_DIRECTION.RIGHT;
		}

	}
	public void DestroyEnemy()//プレイヤーが敵に当ったら敵が消える。publicはプレイヤーの方から呼び出す
	{
		//Debug.Log("DestroyEnemy");
		Instantiate (deathEffect, this.transform.position, this.transform.rotation); //Instantiate(意味は生成)
		//Debug.Log("Instantiate");
		Destroy(this.gameObject); //自分自身を破壊しなさいという意味
		//Debug.Log("this.gameObject");
	}
}