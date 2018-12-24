using UnityEngine;
using System.Collections;

// プレイヤーを制御するコンポーネント
public class Player : MonoBehaviour
{
    public float m_speed; // 移動の速さ
    public Shot m_shotPrefab; // 弾のプレハブ
    public float m_shotSpeed; // 弾の移動の速さ
    public bool begin = false;
    private int angleCount = 0;
    public int shotLeft=0;
    //public int shotCount = 1;
    //public int m_score=0;

   
    // プレイヤーのインスタンスを管理する static 変数
    public static Player m_instance;
  

    public int ScreenWidth;
    public int ScreenHeight;

    // ゲーム開始時に呼び出される関数
    private void Awake()
    {
        // PC向けビルドだったらサイズ変更
        /*
        if (Application.platform == RuntimePlatform.WindowsPlayer ||
        Application.platform == RuntimePlatform.OSXPlayer ||
        Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.SetResolution(ScreenWidth, ScreenHeight, false);
        }
        */
        // 他のクラスからプレイヤーを参照できるように
        // static 変数にインスタンス情報を格納する
        m_instance = this;
      
    }

    private void Start()
    {
        //Shoot();
    }

    // 毎フレーム呼び出される関数
    private void Update()
    {
        // 矢印キーの入力情報を取得する
        var h = Input.GetAxis("Horizontal");
        // 矢印キーが押されている方向にプレイヤーを移動する
        var velocity = new Vector3(h, 0) * m_speed;
        transform.localPosition += velocity;
        // プレイヤーが画面外に出ないように位置を制限する
        transform.localPosition = Utils.ClampPosition(transform.localPosition);

        if (Input.GetMouseButtonDown(0) && angleCount < 4)
        {
            //Debug.Log("left");
            angleCount++;
            transform.Rotate(new Vector3(0, 0, 10f));
        }
        else if (Input.GetMouseButtonDown(1) && angleCount > -4)
        {
            //Debug.Log("right");
            angleCount--;
            transform.Rotate(new Vector3(0, 0, -10f));
        }
        else if (Input.GetMouseButtonDown(2) && shotLeft > 0) {
            Shoot();
        }

        /*
         // プレイヤーのスクリーン座標を計算する
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // プレイヤーから見たマウスカーソルの方向を計算する
        var direction = Input.mousePosition - screenPos;

        // マウスカーソルが存在する方向の角度を取得する
        var angle = Utils.GetAngle(Vector3.zero, direction);

        // プレイヤーがマウスカーソルの方向を見るようにする
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;
        */
        
    }

    // 弾を発射する関数
    private void Shoot()
    {
        var pos = transform.localPosition; // プレイヤーの位置
        var rot = transform.localRotation; // プレイヤーの向き
        pos.y += 0.4f;
        Debug.Log("check2");
        // 発射する弾を生成する
        var shot = Instantiate(m_shotPrefab, pos, rot);

            // 弾を発射する方向と速さを設定する
            shot.Init(transform.localEulerAngles.z, m_shotSpeed);
        shotLeft--;
    }
}