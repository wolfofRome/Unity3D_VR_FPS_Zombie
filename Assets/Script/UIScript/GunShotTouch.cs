using UnityEngine;
using System.Collections;

public class GunShotTouch : MonoBehaviour {

    public GunController m_MyParent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
		Debug.Log ("other:"+other.gameObject.name);
       // m_MyParent.setChooseUIObj(other.gameObject,true);
    }

    void OnTriggerExit(Collider other)
    {
       // m_MyParent.setChooseUIObj(other.gameObject, false);
    }
}
