using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {
    public Block m_blockPrefab;
	// Use this for initialization
	void Start () {
        int i, j;
        Debug.Log("check");
        for (i = 0; i < 6; i++) {
            for (j = 0; j < 13; j++)
            {
                var pos = new Vector3( (float)(1.1*(-6+j)),(float)(4.5-0.6*i),0);
                var rot = transform.localRotation;
                var block = Instantiate(m_blockPrefab, pos, rot);
                var p = Random.Range(0.0f, 1.0f);
                if (p < 0.5) block.InIt(1);
                else if (p < 0.8) block.InIt(2);
                else block.InIt(3);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
