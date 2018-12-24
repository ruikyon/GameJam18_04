using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public Item[] iPrefabs;
    public int m_scoreBase;
    public int m_hp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InIt(int i)
    {
        m_hp = i;
        switch (i) {
            case 1:
                GetComponent<Renderer>().material.color = Color.white;
                break;
            case 2:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
            default:
                GetComponent<Renderer>().material.color = Color.green;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        ScoreManager sm_instance = ScoreManager.sm;
        sm_instance.m_score += (int)(m_scoreBase * sm_instance.barRate * sm_instance.contRate);
        ScoreManager.sm.contRate *= 1.05;
        var shot_i = collision.gameObject.GetComponent<Shot>();
        if (shot_i.type) m_hp = 0;
        else m_hp--;

        if (m_hp == 0)
        {
            float p = Random.Range(0.0f, 1.0f);
            var pos = transform.localPosition; // プレイヤーの位置
            var rot = transform.localRotation; // プレイヤーの向き
                                               //Debug.Log(p);
            if (p < 0.1)
            {
                var item = Instantiate(iPrefabs[0], pos, rot);
            }
            else if (p < 0.15)
            {
                var item = Instantiate(iPrefabs[1], pos, rot);
            }
            else if (p < 0.3)
            {
                var item = Instantiate(iPrefabs[2], pos, rot);
            }
            else if (p < 0.4)
            {
                var item = Instantiate(iPrefabs[3], pos, rot);
            }

            Destroy(gameObject);
        }
        else {
            switch (m_hp)
            {
                case 1:
                    GetComponent<Renderer>().material.color = Color.white;
                    break;
                case 2:
                    GetComponent<Renderer>().material.color = Color.blue;
                    break;
                default:
                    GetComponent<Renderer>().material.color = Color.green;
                    break;
            }
        }
    }
}
