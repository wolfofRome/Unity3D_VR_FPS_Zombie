using UnityEngine;
using System.Collections;

public class FlyToPlayer : MonoBehaviour {

    public float m_fStayTime = 5f;
    public float m_fSpeed = 2f;
    public float m_fDeletTime = 10f;
    private float _fCutTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _fCutTime += Time.deltaTime;
       
        if (_fCutTime >=m_fStayTime)
        {
            //this.transform.position = Vector3.MoveTowards(this.transform.position, PlayerController.instant.gameObject.transform.position, Time.deltaTime * m_fSpeed);
            //if (Mathf.Abs(Vector3.Distance(this.transform.position, PlayerController.instant.gameObject.transform.position))<=0.7f)
            //{
            //    Destroy(this.gameObject);
            //}
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+Time.deltaTime* m_fSpeed, this.transform.position.z);
            if (_fCutTime >= m_fDeletTime)
            {
                Destroy(this.gameObject);
            }
        }
       
	}
}
