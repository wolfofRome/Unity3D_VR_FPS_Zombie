using UnityEngine;
using System.Collections;

public class MedicalKitController : MonoBehaviour {

    public float m_fShowTime = 50f;
    public float m_fIntervalTime = 30f;

    public GameObject m_gMyChild;
    public BoxCollider m_cMyCollider;
    private bool _bCurShowflg = false;
    public float _fCutTime;
	// Use this for initialization
	void Start () {
        disable();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameStateController.instant.m_nCurState == 1)
        {
            _fCutTime -= Time.deltaTime;
            if (_fCutTime <= 0)
            {
                if (_bCurShowflg)
                {
                    disable();
                }
                else
                {
                    show();
                }
            }
        }
	}

    public void MyOnShot()
    {
        m_gMyChild.SetActive(false);
        m_cMyCollider.enabled = false;
    }

    void show()
    {
        _fCutTime = m_fShowTime;
        _bCurShowflg = true;
        m_gMyChild.SetActive(true);
        m_cMyCollider.enabled = true;
    }

    void disable()
    {
        _fCutTime = m_fIntervalTime;
        _bCurShowflg = false;
        m_gMyChild.SetActive(false);
        m_cMyCollider.enabled = false;
    }
}
