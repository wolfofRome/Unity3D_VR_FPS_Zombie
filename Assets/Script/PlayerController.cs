using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public static PlayerController instant;

    public int m_nPlayerHP = 100;
    public int m_nBloodBag = 30;
  
    public int m_nGunPowerShot = 30;
    public int m_nKnifePower = 20;

    public float m_fHeadMultiple = 2f;
    public int m_nWeaponUpgradeAdd = 5;
    public int[] m_nWeaponUpgradeCoin = new int[2] {450,700};
    //public int[] m_nWeaponUpgradeGunBullet = new int[2] { 8, 9 };

    public float m_fCurHP;
    public int m_nCurWeaponLevel = 0;
    public int m_nCurCoin = 0;

    public float m_fNormalZombieHurt = 20;
    public float m_fSpecialZombieHurt = 35;
    public float m_fDogZombieHurt = 20;

    public int m_nNormalZombieCoin = 5;
    public int m_nSpecialZombieCoin = 10;
    public int m_nDogZombieCoin = 5;

    public int m_nSpecialZombieBullet = 30;

    public Transform m_headTransform;

    public int m_nGunStartBullet = 30;
    public int m_nGunCartridge = 15;

    public float m_fNearDeathHP = 20f;
    public GunController[] myGuns;

    public Transform m_tGetHitEffect;

  
    public AudioClip m_upMusic;
    public float m_fupVolume = 1f;
    public AudioClip m_healMusic;
    public float m_fhealVolume = 1f;
    public AudioClip m_GetHit1Music;
    public AudioClip m_GetHit2Music;
    public float m_fGethitVolume = 1f;
    public GameObject m_NearDeathVoiceObj;
    public AudioClip MachinegunReload;
    public float m_fMachinegunReloadVolume = 1f;

    public BleedBehavior m_NearDeathEffect;

    [HideInInspector]
    public int m_nCurBullet;

    void Awake()
    {
        instant = this;
    }

    // Use this for initialization
    void Start () {

    }

    public void init()
    {
        m_fCurHP = m_nPlayerHP;
        m_nCurWeaponLevel = 0;
        m_nCurCoin = 0;
        m_nCurBullet = m_nGunStartBullet;
        foreach (GunController gc in myGuns)
        {
            gc.init();
        }
        m_NearDeathVoiceObj.SetActive(false);
        m_NearDeathEffect.enabled = false;
    }

	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(m_headTransform.position.x,this.transform.position.y,m_headTransform.position.z);
        if (Input.GetMouseButtonDown(0))
        {
            RayZombie();
        }
    }


    private void RayZombie()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//从Camera发射射线到屏幕
        if (Physics.Raycast(ray, out hit))//射线碰撞检测
        {
           // Debug.DrawLine(m_shotTrans.position, hit.point, Color.red);
            if (hit.collider != null)
            {
                ZombieFindParent zparent = hit.collider.GetComponent<ZombieFindParent>();
                if (zparent)
                {
                    float cutBlood = PlayerController.instant.m_nGunPowerShot + PlayerController.instant.m_nWeaponUpgradeAdd * PlayerController.instant.m_nCurWeaponLevel;
                    zparent.m_MyParent.GetHit(zparent.m_bIsHead, cutBlood);
                }
                GameObject go = Instantiate(Resources.Load("xuepenjian")) as GameObject;
                go.transform.parent = hit.collider.transform.parent;
                go.transform.position = hit.point;
                go.transform.localScale = Vector3.one;
                go.transform.rotation = Quaternion.LookRotation(hit.normal);
            }
        }
    }

    public void GetHit(float cutblood)
    {
		if (GameStateController.instant.m_nCurState > 0) {
			if (m_fCurHP > 0) {
				//Debug.Log("cutblood:" + cutblood+ "   m_fCurHP:" + m_fCurHP);
				m_fCurHP -= cutblood;
                //Debug.Log("   m_fCurHP:" + m_fCurHP);
                hitEffect();
                playVoice(3);
                if (m_fCurHP <= 0)
                {
                    GameStateController.instant.GameOver();
                    m_NearDeathVoiceObj.SetActive(false);
                    m_NearDeathEffect.enabled = false;
                }
                else if (m_fCurHP<= m_fNearDeathHP)
                {
                    m_NearDeathVoiceObj.SetActive(true);
                    m_NearDeathEffect.enabled = true;
                }
			}
		}
    }

    private void hitEffect()
    {
		string effectStry = "zhuahen1";
        int rnum = Random.Range(0,2);
        if (rnum == 0)
        {
			effectStry = "zhuahen";
        }
        GameObject go = Instantiate(Resources.Load(effectStry)) as GameObject;
        go.transform.parent = m_tGetHitEffect;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localEulerAngles = Vector3.zero;
    }

    public void getCoin()
    {
        if (GameStateController.instant.m_nCurScore > m_nWeaponUpgradeCoin[1])
        {
            if (m_nCurWeaponLevel < 2)
            {
                m_nCurWeaponLevel = 2;
                UIController.instant.addLevelUp();
                playVoice(1);
            }
        }
        else if (GameStateController.instant.m_nCurScore > m_nWeaponUpgradeCoin[0])
        {
            if (m_nCurWeaponLevel < 1)
            {
                m_nCurWeaponLevel = 1;
                UIController.instant.addLevelUp();
                playVoice(1);
            }
        }
    }

    public void AddBlood()
    {
        m_fCurHP += m_nBloodBag;
        playVoice(2);
        if (m_fCurHP > m_fNearDeathHP)
        {
            m_NearDeathVoiceObj.SetActive(false);
            m_NearDeathEffect.enabled = false;
        }
    }

    public void AddBullet()
    {
        m_nCurBullet += m_nSpecialZombieBullet;
        playVoice(0);
    }
    /// <summary>
    /// 0 add bullet; 1 weaponUp ; 2 add Blood; 3 get hit
    /// </summary>
    /// <param name="state"></param>
    void playVoice(int state)
    {
        switch (state)
        {
            case 0:
                NGUITools.PlaySound(MachinegunReload, m_fMachinegunReloadVolume);
                break;
            case 1:
                NGUITools.PlaySound(m_upMusic, m_fupVolume);
                break;
            case 2:
                NGUITools.PlaySound(m_healMusic, m_fhealVolume);
                break;
            case 3:
                int num = Random.Range(0,2);
                if (num == 0)
                {
                    NGUITools.PlaySound(m_GetHit1Music, m_fGethitVolume);
                }
                else
                {
                    NGUITools.PlaySound(m_GetHit2Music, m_fGethitVolume);
                }
                break;
        }
    }
}
