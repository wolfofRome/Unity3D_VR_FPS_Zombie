using UnityEngine;
using System.Collections;

public class DestoryMySelf : MonoBehaviour {

    public float m_fMyLifeTime = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        m_fMyLifeTime -= Time.deltaTime;
        if (m_fMyLifeTime <= 0f)
        {
            Destroy(this.gameObject);
        }

    }
}
