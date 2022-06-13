using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateController : MonoBehaviour {
    
    public static GameStateController instant;
    public int[] m_nGradePointList = new int[5] {100,150,200,250,300};
    /// <summary>
    /// 0,start ; 1.game_gate1 ; -1.game_End ; 2.CutDown time
    /// </summary>
    public int m_nCurState = 0;
    public int m_nCurScore = 0;

    public int m_nCurRang = 0;

    public AudioSource m_Audio;
    public AudioClip m_StartMusic;
    public float m_fStartVolume = 1f;
    public AudioClip m_GameMusic;
    public float m_fGameVolume = 1f;
    public AudioClip m_GameOverMusic;
    public float m_fGameOverVolume = 1f;
    public AudioClip m_GameWinMusic;
    public float m_fGameWinVolume = 1f;

    public AudioClip m_ElectricVoice;
    public float m_fElectricVolume = 0.2f;

    public float m_fCutDownBirthZombieTime = 9f;

    private float _fElecMiniTime = 10f;
    private float _fElecMaxTime = 50f;
    private float _fElecUseCutTime = 0f;
    private int _nZombiePlaceNum = 0;
    void Awake()
    {
        instant = this;
    }

    public void init()
    {
        m_nCurState = 0;
        m_nCurScore = 0;
       
    }

    // Use this for initialization
    void Start () {
        Init();
     //   PlayerController.instant.init();
     //  gamestart();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init()
    {
        m_nCurState = 0;
        m_nCurScore = 0;
        UIController.instant.init();
        PlayerController.instant.init();
        changeBackMusic();
    }

    public void ReInit()
    {
		Init();
        CameraController.instant.Restart();
    }

    public void GameStart()
    {
        UIController.instant.m_gStartUI.SetActive(false);
        UIController.instant.addCutDown();
        Invoke("gamestart", m_fCutDownBirthZombieTime);
    }

    void gamestart()
    {
        m_nCurState = 1;
        changeBackMusic();
    }

    public void AddScore(int scoreNum)
    {
        int exGrade = GetCurGrade();
        m_nCurScore += scoreNum;
        PlayerController.instant.m_nCurCoin = m_nCurScore;
        PlayerController.instant.getCoin();
        if (GetCurGrade() >= 5)
        {
            m_nCurState = -1;
            CameraController.instant.GameEnd();
            Invoke("showWinUI", CameraController.instant.m_fDurationTime + 0.2f);
        }
        else if (exGrade < GetCurGrade())
        {
			if (m_nCurState != 2) {
				m_nCurState = 2;
				_nZombiePlaceNum = 0; 
			}
        }
    }

    public void GameOver()
    {
        m_nCurState = -1;
        CameraController.instant.GameEnd();
        Invoke("showLoseUI", CameraController.instant.m_fDurationTime+0.2f);
    }

    void showLoseUI()
    {
        UIController.instant.ShowGameOver();
        changeBackMusic();
    }
    void showWinUI()
    {
        UIController.instant.ShowGameWin();
        changeBackMusic(true);
    }
    public int GetCurGrade()
    {
        for (int i = 0; i < m_nGradePointList.Length; i++)
        {
            if (m_nCurScore < m_nGradePointList[i])
            {
                return (i+1);
            }
        }
        return m_nGradePointList.Length;
    }

    void changeBackMusic(bool winflg = false)
    {
        switch (m_nCurState)
        {
            case 0:
                m_Audio.clip = m_StartMusic;
                m_Audio.volume = m_fStartVolume;
                m_Audio.loop = true;
                _fElecUseCutTime = _fElecMiniTime;
			m_Audio.Play ();
                break;
		case 1:
			m_Audio.clip = m_GameMusic;
			m_Audio.volume = m_fGameVolume;
			m_Audio.loop = true;
			m_Audio.Play ();
                break;
            case -1:
                if (winflg)
                {
                    m_Audio.clip = m_GameWinMusic;
                    m_Audio.volume = m_fGameWinVolume;
                    m_Audio.loop = false;
				m_Audio.Play ();
                }
                else
                {
                    m_Audio.clip = m_GameOverMusic;
                    m_Audio.volume = m_fGameOverVolume;
                    m_Audio.loop = false;
				m_Audio.Play ();
                }
                break;
        }
       
    }

    void backVoice()
    {
        if (m_nCurState > 0)
        {
            if (_fElecUseCutTime <= 0f)
            {
                _fElecUseCutTime = Random.Range(_fElecMiniTime,_fElecMaxTime);
                NGUITools.PlaySound(m_ElectricVoice,m_fElectricVolume);
            }
            _fElecUseCutTime -= Time.deltaTime;
        }
    }

    public void ZombiePlaceClearCall()
    {
        _nZombiePlaceNum += 1;
        Debug.Log("------------ZombiePlaceClearCall:"+ _nZombiePlaceNum);
        if (_nZombiePlaceNum >= 8)
        {
            UIController.instant.addCutDown();
            Invoke("GoOnBithZombie", m_fCutDownBirthZombieTime);
        }
    }

    void GoOnBithZombie()
    {
        m_nCurState = 1;
    }

    
}
