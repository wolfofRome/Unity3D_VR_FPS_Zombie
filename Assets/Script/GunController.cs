using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {
    public SteamVR_TrackedObject m_trackedObj;
    public Transform m_shotTrans;

    public GameObject m_gClip;
    
    public int m_nCurGunBullet;
    public Transform m_tGunEffect;
    public Animation m_MyAnimation;
    public GameObject m_MyRay;

   
    public AudioClip m_MachinegunFire;
    public float m_fMachinegunFireVolume = 1f;
    public AudioClip m_shootwideMusic;
    public float m_fshootwideVolume = 1f;
	public AudioClip MachinegunReload;
	public float m_fMachinegunReloadVolume = 1f;

    private TweenPosition _gunKickTweenP;
    private TweenRotation _gunkickTweenR;
    private TweenPosition _gunClipTweenP;

    private float _duration = 0.15f;

    /// <summary>
    /// 0.null 1.animationing 2.clipfinish 3.clipback
    /// </summary>
    private int _nIsReloading = 0;

    private HandActionCatch _HandCatch;

    // Use this for initialization
    void Start () {
        _HandCatch = HandActionCatch.instant(m_trackedObj.gameObject);
        _HandCatch.AddCatch( HandActionCatch.CATCH_STATE.HANDDOWN, clipBack);
		_HandCatch.m_bCatch = false;
    }
	
	// Update is called once per frame
	void Update () {
        getTrigger();
    }

    public void init()
    {
        m_nCurGunBullet = PlayerController.instant.m_nGunCartridge;
        PlayerController.instant.m_nCurBullet -= m_nCurGunBullet;
    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(Vector2.zero, new Vector2(100, 50)), "trigger"))
    //    {
    //        GunKick();

    //    }
    //}

    void getTrigger()
    {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)m_trackedObj.index);
        if (GameStateController.instant.m_nCurState > 0)
        {
			if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
				//Debug.Log ("-------m_nCurGunBullet---"+m_nCurGunBullet);
                if (m_nCurGunBullet > 0)
                {
                    m_nCurGunBullet -= 1;
                    GunEffect();
                    StartCoroutine(_MultiPulse(5));
                    RayZombie();
                    GunKick();
                    m_MyAnimation.Play("shot");
                }
            }
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
               // if (m_nCurGunBullet <= 0&&_nIsReloading==0)
                {
                    Reload();
                }
            }
            if (m_MyRay.activeSelf)
            {
                m_MyRay.SetActive(false);
            }
        }
        else
        {
            if (!m_MyRay.activeSelf)
            {
                m_MyRay.SetActive(true);
                m_MyRay.transform.localScale = new Vector3(0.01f,0.01f,2000f);
            }
            Vector3 fwd = m_shotTrans.TransformDirection(Vector3.forward);
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
				if (UIController.instant._CurUIChooseObj != null)
                {
					RayUI(UIController.instant._CurUIChooseObj);
                    ShotVoice(false);
                }
    //            RaycastHit hit;
    //            if (Physics.Raycast(m_shotTrans.position, fwd, out hit, 2000))
    //            {
    //                // Debug.DrawLine(m_shotTrans.position, hit.point, Color.red);
				//    if (hit.collider != null)
    //                {
				//	    if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("UI")) 
				//	    {
				//			RayUI (hit.collider.gameObject);
				//		}
    //                    ShotVoice(false);
    //                }
				//} 
            }
        }
    }

    private void GunEffect()
    {
		GameObject go = Instantiate(Resources.Load("kaihuo")) as GameObject;
        go.transform.parent = m_tGunEffect;
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.localScale = Vector3.one;
    }

    private void RayZombie()
    {
        Vector3 fwd = m_shotTrans.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(m_shotTrans.position, fwd, out hit, 2000))
        {
            Debug.DrawLine(m_shotTrans.position, hit.point, Color.red);
            if (hit.collider != null)
            {
				Debug.Log ("hit.collider:"+hit.collider.name);
                ZombieFindParent zparent = hit.collider.GetComponent<ZombieFindParent>();
				string texiaoName = "xuepenjian";
				if (zparent) {
					float cutBlood = PlayerController.instant.m_nGunPowerShot + PlayerController.instant.m_nWeaponUpgradeAdd * PlayerController.instant.m_nCurWeaponLevel;
					zparent.m_MyParent.GetHit (zparent.m_bIsHead, cutBlood);
					if (zparent.m_bIsHead) {
						texiaoName = "baotou";
					}
                    ShotVoice(true);
                } else {
					texiaoName = "shoujitongyong";
                    MedicalKitController mkit = hit.collider.GetComponent<MedicalKitController>();
                    if (mkit)
                    {
                        mkit.MyOnShot();
                        PlayerController.instant.AddBlood();
                    }
                    else
                    {
                        ShotVoice(false);
                    }
                }
				GameObject go = Instantiate(Resources.Load(texiaoName)) as GameObject;
                go.transform.parent = hit.collider.transform.parent;
                go.transform.position = hit.point;
                go.transform.localScale = Vector3.one;
                go.transform.rotation = Quaternion.LookRotation(hit.normal);
            }
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

    private void GunKick()
    {
        if (_gunKickTweenP != null)
        {
			this.gameObject.transform.localPosition = Vector3.zero;
			this.gameObject.transform.localEulerAngles =  new Vector3(0f, 0f, 0f);
            Destroy(_gunKickTweenP);
            Destroy(_gunkickTweenR);
        }
        Quaternion toQ = Quaternion.EulerRotation(-10f, 0f, 0f);
        _gunkickTweenR = TweenRotation.Begin(this.gameObject, _duration, toQ);
		_gunkickTweenR.from = new Vector3(0f, 0f, 0f);
        _gunkickTweenR.to = new Vector3(-10f, 0f, 0f);
        _gunKickTweenP = TweenPosition.Begin(this.gameObject, _duration, new Vector3(0f, 0f, -0.1f));
        _gunKickTweenP.from = Vector3.zero;
        _gunKickTweenP.to = new Vector3(0f, 0f, -0.1f);
        _gunKickTweenP.SetOnFinished(kickfinish);
    }

	public void kickfinish()
    {
        this.gameObject.transform.localPosition = Vector3.zero;
		this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        Destroy(_gunKickTweenP);
        Destroy(_gunkickTweenR);
    }

    private void RayUI(GameObject obj)
    {
		if (obj.name.Equals ("startButton")) {
			GameStateController.instant.GameStart ();
		} else if (obj.name.Equals ("RestartButton")) {
			GameStateController.instant.ReInit ();
		} else if (UIRenameController.instant) {
			if (UIRenameController.instant.gameObject.activeSelf)
			{
				if (obj.GetComponent<UISprite>())
				{
					UIRenameController.instant.ButtonOnClick(obj.GetComponent<UISprite>().spriteName);
				}
			}
		}
		UIController.instant._CurUIChooseObj = null;
    }

    

    void Reload()
    {
        _nIsReloading = 1;
        m_gClip.transform.localPosition = Vector3.zero;
        m_gClip.SetActive(true);
        _gunClipTweenP = TweenPosition.Begin(m_gClip, 0.1f, new Vector3(0f, 0f, 0f));
		_gunClipTweenP.from = Vector3.zero;
		_gunClipTweenP.to = new Vector3(0f, 0.08f, -0.3f);
		_gunClipTweenP.SetOnFinished(clipfinish);

    }
    public void clipfinish()
    {
       // Debug.Log("clipfinish");
		_HandCatch.m_bCatch = true;
        _nIsReloading = 2;
    }

    public void clipBack()
    {
       // Debug.Log("clipfinish");
        m_gClip.transform.localPosition = Vector3.zero;
        _HandCatch.m_bCatch = false;
        clipFullUp();
        _nIsReloading = 0;

    }

    public void clipFullUp()
    {
        int useBullet = 0;
        if (PlayerController.instant.m_nCurBullet > PlayerController.instant.m_nGunCartridge)
        {
            useBullet = PlayerController.instant.m_nGunCartridge - m_nCurGunBullet;
            m_nCurGunBullet = PlayerController.instant.m_nGunCartridge;
        }
        else
        {
            m_nCurGunBullet = PlayerController.instant.m_nCurBullet;
            useBullet = m_nCurGunBullet;
        }
        PlayerController.instant.m_nCurBullet -= useBullet;
		NGUITools.PlaySound(MachinegunReload, m_fMachinegunReloadVolume);
		//Debug.Log ("----m_nCurGunBullet-----"+m_nCurGunBullet);
    }

    void ShotVoice(bool zombieflg)
    {
        if (zombieflg)
        {
            NGUITools.PlaySound(m_MachinegunFire, m_fMachinegunFireVolume);
        }
        else
        {
            NGUITools.PlaySound(m_shootwideMusic, m_fshootwideVolume);
        }
    }
}
