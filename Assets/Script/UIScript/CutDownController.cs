using UnityEngine;
using System.Collections;

public class CutDownController : MonoBehaviour {

    public UISprite m_boSprite;
    public float m_fcutTime = 4f;
	// Use this for initialization
	void Start () {
		m_boSprite.spriteName = "bo_"+(GameStateController.instant.GetCurGrade()).ToString();
    }
	
	// Update is called once per frame
	void Update () {
        m_fcutTime -= Time.deltaTime;
        if (m_fcutTime <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    
}
