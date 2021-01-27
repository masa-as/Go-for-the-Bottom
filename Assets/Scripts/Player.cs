﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 4f; //歩くスピード
	private Rigidbody2D rigidbody2D;
	private Animator anim;

	public float jumpPower = 700; //ジャンプ力
	public LayerMask groundLayer; //Linecastで判定するLayer

	private bool isGrounded; //着地判定

	public GameObject gameobj;

	void Start () {
		//各コンポーネントをキャッシュしておく
		anim = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update ()
	{
		//Linecastでユニティちゃんの足元に地面があるか判定
		isGrounded = Physics2D.Linecast (
			transform.position + transform.up * 1,
			transform.position - transform.up * 0.05f,
			groundLayer);
		//スペースキーを押し、
		if (Input.GetKeyDown ("space")) {
			Debug.Log (isGrounded);
			//着地していた時、
			if (isGrounded) {
				//Runアニメーションを止めて、Jumpアニメーションを実行
				anim.SetBool("Run", false);
				anim.SetTrigger("Jump");
				//着地判定をfalse
				isGrounded = false;
				//AddForceにて上方向へ力を加える
				rigidbody2D.AddForce (Vector2.up * jumpPower);
			}
		}
		//上下への移動速度を取得
		float velY = rigidbody2D.velocity.y;
		//移動速度が0.1より大きければ上昇
		bool isJumping = velY > 0.1f ? true:false;
		//移動速度が-0.1より小さければ下降
		bool isFalling = velY < -0.1f ? true:false;
		//結果をアニメータービューの変数へ反映する
		anim.SetBool("isJumping",isJumping);
		anim.SetBool("isFalling",isFalling);
	}


	void FixedUpdate ()
	{
		//左キー: -1、右キー: 1
		float x = Input.GetAxisRaw ("Horizontal");
		//左か右を入力したら
		if (x != 0) {
			//入力方向へ移動
			rigidbody2D.velocity = new Vector2 (x * speed, rigidbody2D.velocity.y);
			//localScale.xを-1にすると画像が反転する
			Vector2 temp = transform.localScale;
			temp.x = x;
			transform.localScale = temp;
			//Wait→Run
			anim.SetBool ("Run", true);
			//左も右も入力していなかったら
		} else {
			//横移動の速度を0にしてピタッと止まるようにする
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
			//Run→Wait
			anim.SetBool ("Run", false);
		}
	}
}