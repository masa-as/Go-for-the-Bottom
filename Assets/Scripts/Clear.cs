using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour {

	Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		//UnityChanとぶつかった時
		if (col.gameObject.tag == "Player") {
			//LifeScriptのLifeDownメソッドを実行
			SceneManager.LoadScene ("GameClear");
		}
	}

}
