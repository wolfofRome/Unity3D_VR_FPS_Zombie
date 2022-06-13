using UnityEngine;
using System.Collections;

public class KnifeController : MonoBehaviour {
    public SteamVR_TrackedObject m_trackedObj;
    public Transform m_knifeBloodTrans;

    public AudioClip m_Hit;
    public float m_fHitVolume = 1f;
    public AudioClip m_Meleeswing;
    public float m_fMeleeswingVolume = 1f;

    private float m_fcutTime;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        ZombieFindParent zparent = other.GetComponent<ZombieFindParent>();
        if (zparent)
        {
            float cutBlood = PlayerController.instant.m_nGunPowerShot + PlayerController.instant.m_nWeaponUpgradeAdd * PlayerController.instant.m_nCurWeaponLevel;
            zparent.m_MyParent.GetHit(zparent.m_bIsHead, cutBlood);
            m_fcutTime = 0.5f;
            ShotVoice(true);
        }
        StartCoroutine(_MultiPulse(5));
    }

    void OnTriggerStay(Collider other)
    {
        ZombieFindParent zparent = other.GetComponent<ZombieFindParent>();
        if (zparent)
        {
            var system = Valve.VR.OpenVR.System;
            system.TriggerHapticPulse((uint)m_trackedObj.index, 0, (char)3999);
            if (m_fcutTime >= 0.5f)
            {
                m_fcutTime = 0f;
				GameObject go = Instantiate(Resources.Load("daoshang")) as GameObject;
                go.transform.parent = other.transform.parent;
                go.transform.position = m_knifeBloodTrans.position;
                go.transform.localScale = Vector3.one;
                go.transform.LookAt(PlayerController.instant.transform.position);
            }
            m_fcutTime += Time.deltaTime;
        }
    }

    private IEnumerator _MultiPulse(int numPulses)
    {
        while (numPulses-- > 0)
        {
            var system = Valve.VR.OpenVR.System;
            system.TriggerHapticPulse((uint)m_trackedObj.index, 0, (char)3999);
            yield return new WaitForEndOfFrame();
        }
    }

    void ShotVoice(bool zombieflg)
    {
        if (zombieflg)
        {
            NGUITools.PlaySound(m_Hit, m_fHitVolume);
        }
        else
        {
            NGUITools.PlaySound(m_Meleeswing, m_fMeleeswingVolume);
        }
    }
}
