using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    // キューブの移動速度
    private float speed = -12;

    // 消滅位置
    private float deadLine = -10;

    // ブロック衝突時の効果音
    public AudioClip SE;

    // AudioSource格納用
    AudioSource audiosource;


    // Start is called before the first frame update
    void Start()
    {

        this.audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        // キューブを移動させる
        transform.Translate(this.speed * Time.deltaTime, 0, 0);

        // 画面外に出たら破棄する
        if(transform.position.x < this.deadLine)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        // 発展：GroundまたはCubeと衝突したときに効果音を鳴らす
        if (other.gameObject.tag == "GroundTag" || other.gameObject.tag == "CubeTag")
        {
            // 音を一回だけ再生する（ボリュームは3割で）
            this.audiosource.PlayOneShot(SE,0.3F);
        }
    }
}
