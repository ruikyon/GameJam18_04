using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public int m_score;
    public double barRate;
    public double contRate;
    public static ScoreManager sm;
    public int m_disInterval;
    public int counter=0;
    public int m_dec=0;
	// Use this for initialization
	void Start () {
        sm = this;
        m_score = 0;
        barRate = 1;
        contRate = 1;
	}
	
	// Update is called once per frame
	void Update () {
        counter++;
        if (counter > m_disInterval)
        {
            m_score -= m_dec;
            counter = 0;
        }
	}
}
