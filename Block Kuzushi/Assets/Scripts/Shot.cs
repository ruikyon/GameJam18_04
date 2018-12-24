using UnityEngine;
using System.Collections;
using System;

// プレイヤーが発射する弾を制御するコンポーネント
public class Shot : MonoBehaviour
{
    private Vector3 m_velocity; // 速度
    private Vector3 sc;
    private bool longer = false, shorter = false;
    public int fast=0;
    public bool type = false;
    public static int shotCount=0;

    // 毎フレーム呼び出される関数
    private void Update()
    {
        if ((fast == 0) && ScoreManager.sm.m_score > 2000)
        {
            m_velocity *= 1.5f;
            fast = 1;
        }
        else if (fast == 1 && ScoreManager.sm.m_score > 4000)
        {
            m_velocity *= 1.5f;
            fast = 2;
        }
        // 移動する
        //transform.localPosition += m_velocity;
        //if (Mathf.Abs(transform.localPosition.x) >= 9.5) m_velocity.x = -m_velocity.x;
        //if (transform.localPosition.y >= 5.0) m_velocity.y = -m_velocity.y;
        if (transform.localPosition.y <= -5.0)
        {
            shotCount--;
            if (shotCount == 0 && Player.m_instance.shotLeft==0) Hud.hud.GameOver();
            Debug.Log(shotCount);
            Destroy(gameObject);
        }
    }

    // 弾を発射する時に初期化するための関数
    public void Init(float angle, float speed)
    {
        // 弾の発射角度をベクトルに変換する
        var direction = Utils.GetDirection(angle + 90);

        // 発射角度と速さから速度を求める
        m_velocity = direction * speed;

        // 弾が進行方向を向くようにする
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;
        Rigidbody2D rb=GetComponent<Rigidbody2D>();
        rb.velocity= m_velocity*5;
        shotCount++;
        Debug.Log(shotCount);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        //Debug.Log("check_i");
        switch (collider.gameObject.tag) {
            case "long_bar":
                if (!longer)
                {
                    sc = Player.m_instance.GetComponent<Player>().transform.localScale;
                    sc.x *= 1.6f;
                    ScoreManager.sm.barRate *= 0.8;
                    Player.m_instance.GetComponent<Player>().transform.localScale = sc;
                    StartCoroutine(DelayMethod(6f, () =>
                    {
                        ResetLong(true);
                    }));
                    longer = true;
                }
                break;
            case "short_bar":
                if (!shorter)
                {
                    sc = Player.m_instance.GetComponent<Player>().transform.localScale;
                    sc.x *= 0.625f;
                    ScoreManager.sm.barRate *= 1.25;
                    Player.m_instance.GetComponent<Player>().transform.localScale = sc;
                    StartCoroutine(DelayMethod(4f, () =>
                    {
                        ResetLong(false);
                    }));
                    shorter = true;
                }
                break;
            case "add_shot":
                if(Player.m_instance.shotLeft < 10) Player.m_instance.shotLeft++;
                break;
            case "pow_up":
                type = true;
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                //GetComponent<Shot>().gameObject.layer = LayerMask.NameToLayer("Shot_1");
                rb.velocity *= 1.25f;
                GetComponent<Renderer>().material.color = Color.red;
                StartCoroutine(DelayMethod(8f, () =>
                {
                    ResetShot();
                }));
                break;


        }
        Destroy(collider.gameObject);        
    }

    private IEnumerator DelayMethod(float waitTime, Action action)
    {
         yield return new WaitForSeconds(waitTime);
         action();
    }

    private void ResetLong(bool flag) {
        if (flag)
        {
            var sc = Player.m_instance.GetComponent<Player>().transform.localScale;
            sc.x *= 0.625f;
            ScoreManager.sm.barRate *= 1.25;
            Player.m_instance.GetComponent<Player>().transform.localScale = sc;
            longer = false;
        }
        else
        {
            var sc = Player.m_instance.GetComponent<Player>().transform.localScale;
            sc.x *= 1.6f;
            ScoreManager.sm.barRate *= 0.8;
            Player.m_instance.GetComponent<Player>().transform.localScale = sc;
            shorter = false;
        }    
    }

    private void ResetShot() {
        type = false;
        //GetComponent<Shot>().gameObject.layer = LayerMask.NameToLayer("Default");
        GetComponent<Renderer>().material.color = Color.white;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity *= 0.8f;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") ScoreManager.sm.contRate = 1;
    }
}
