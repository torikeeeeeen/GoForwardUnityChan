using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{

    // アニメーションするためのコンポーネントを入れる
    Animator animator;

    // Unityちゃんを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;

    // 地面の位置
    private float groundLevel = -3.0f;

    // ジャンプの速度の減衰
    private float dump = 0.8f;
    // ジャンプの速度(privateは？)
    float jumpVelocity = 20;

    // ゲームオーバーになる位置
    private float deadLine = -9;


    // Start is called before the first frame update
    void Start()
    {
        // Animatorのコンポーネントを取得する
        this.animator = GetComponent<Animator>();
        // Rigidbody2Dのコンポーネントを取得する
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 走るアニメーションを再生するためにAnimatorのパラメータを調整する
        this.animator.SetFloat("Horizontal", 1);

        // 着地しているかどうかを調べる
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        this.animator.SetBool("isGround", isGround);

        // ジャンプ状態のときは足音のボリュームを0にする
        GetComponent<AudioSource>().volume = (isGround) ? 1 : 0; 

        // 着地状態でクリックされた場合
        if (Input.GetMouseButtonDown(0) && isGround)
        {
            // 上方向の速度を与える
            this.rigid2D.velocity = new Vector2(0, this.jumpVelocity);
        }

        // クリックをやめたら上方向への速度を減衰する
        if (Input.GetMouseButton(0) == false)
        {
            if(this.rigid2D.velocity.y > 0)
            {
                this.rigid2D.velocity *= this.dump;
            }
        }

        // deadLineを超えた場合にゲームオーバーにする
        if (transform.position.x < this.deadLine)
        {
            // UIcontrollerのGameOver関数を呼び出して画面上にGameOverを表示
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

            // ユニティちゃんを破棄する
            Destroy(gameObject);
        }
    }
}
