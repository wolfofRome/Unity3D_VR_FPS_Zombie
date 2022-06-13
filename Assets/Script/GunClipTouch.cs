using UnityEngine;
using System.Collections;

public class GunClipTouch : MonoBehaviour {
    public GunController m_MyParent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
		Debug.Log ("OnTriggerEnter:"+other.name);
        if (other.name.Contains("GunClip"))
        {
			Destroy(other.transform.parent.gameObject);
            m_MyParent.clipFullUp();
        }
    }
}
