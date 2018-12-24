using UnityEngine;
using UnityEngine.UI;

// 情報表示用の UI を制御するコンポーネント
public class Hud : MonoBehaviour
{
    public Text m_leftText;// レベルのテキスト
    public Text m_scoreText;
    public GameObject m_gameOverText; // ゲームオーバーのテキスト
    public GameObject m_gameReadyText;
    public static Hud hud;
    private bool goFlag = false, startFlag=false;

    private void Start()
    {
        hud = this;
    }

    // 毎フレーム呼び出される関数
    private void Update()
    {
        if (!startFlag && Input.GetMouseButtonDown(2))
        {
            startFlag = true;
            ScoreManager.sm.m_dec = 50;
        }
        m_gameReadyText.SetActive(!startFlag);
        // レベルのテキストの表示を更新する
        m_leftText.text = Player.m_instance.shotLeft.ToString();
        m_scoreText.text = ScoreManager.sm.m_score.ToString();
        m_gameOverText.SetActive(goFlag);
        
    }

    public void GameOver() {
        Debug.Log("gameover");
        goFlag = true;
        ScoreManager.sm.m_dec = 0;
    }
}