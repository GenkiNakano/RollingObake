using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour
{
    //出現位置の変数
    private float positionX;
    //private float positionY;

    //パーリンノイズに変化を与える変数
    private float rdm;

    //光を当てた時にその場に残らせるための設定変数
    public float moveSetX;
    //public float moveSetY;
    private float firstMoveSet;

    //動くスピード
    public float noiseSpeedLow;
    public float noiseSpeedHigh;

    //光が当たったことを調べる変数
    public bool hitCheck;
    //光が外れたことを調べる変数
    public bool outCheck;

    // Start is called before the first frame update
    void Start()
    {
        //初期位置設定変数。スクリーンの幅のサイズ内でランダムな値をとり、スケール間を縮めるために30で割っている。
        positionX = Random.Range(-Screen.width / 2, Screen.width / 2) / 30;

        //最初に設定したfuwaSetの値を記憶しておく
        firstMoveSet = moveSetX;

        //パーリンノイズ用変数
        rdm = Random.Range(noiseSpeedLow, noiseSpeedHigh);

        hitCheck = false;
        outCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        //前の座標と今の座標を保存する変数を宣言
        Vector3 oldPos = transform.position;
        Vector3 newPos = transform.position;
        //おばけを左に向かせるか右に向かせるかをチェックする変数を宣言
        float check = 0.0f;

        //ライトが当たった時に自然に停止するように徐々に減らしていっている
        if (hitCheck == true && moveSetX > 0)
        {
            moveSetX -= 0.2f;

        }
        else
        {
            hitCheck = false;
        }

        //ライトが外れた時に自然にパーリンノイズに戻れるように徐々に足していっている
        if (outCheck == true && moveSetX < firstMoveSet)
        {
            moveSetX += 0.01f;
            
        }
        else
        {
            outCheck = false;
        }

        //パーリンノイズで動きを指定
        newPos.x = positionX + (Mathf.PerlinNoise(Time.time / rdm, 0) - 0.5f) * moveSetX;


        //横方向のパーリンノイズの値をそのまま回転に使うことで、進行方向と回転方向が一致する
        check = newPos.x - oldPos.x;
        Vector3 rotation = this.transform.eulerAngles;
        rotation.z = -check * 100f;
        if (0 <= check)
        {
            transform.Rotate(rotation);
        }
        else
        {
            transform.Rotate(rotation);
        }
        transform.position = newPos;

    }
}
