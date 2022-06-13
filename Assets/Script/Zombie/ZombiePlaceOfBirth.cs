using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombiePlaceOfBirth : MonoBehaviour {

    public bool m_bBirthNormalZombie;
    public float[] m_fNormalIntervalTimeMin = new float[5] {10f,10f,10f,10f,10f};
    public float[] m_fNormalIntervalTimeMax = new float[5] {12f,12f,12f,12f,12f};
    public bool m_bBirthSpecialZombie;
    public float[] m_fSpecialIntervalTimeMin = new float[5] { 10f, 10f, 10f, 10f, 10f };
    public float[] m_fSpecialIntervalTimeMax = new float[5] { 12f, 12f, 12f, 12f, 12f };
    public bool m_bBirthDogZombie;
    public float[] m_fDogIntervalTimeMin = new float[5] { 10f, 10f, 10f, 10f, 10f };
    public float[] m_fDogIntervalTimeMax = new float[5] { 12f, 12f, 12f, 12f, 12f };
    public bool m_bStartBirthNormal = false;
    public bool m_bStartBirthSpecial = false;
    public bool m_bStartBirthDog = false;

    private float _fCurNormalIntervalTime
    {
        get
        {
            int curGrade = GameStateController.instant.GetCurGrade();
            return Random.Range(m_fNormalIntervalTimeMin[curGrade], m_fNormalIntervalTimeMax[curGrade]);
        }
    }
    private float _fCurSpecialIntervalTime
    {
        get
        {
            int curGrade = GameStateController.instant.GetCurGrade();
            return Random.Range(m_fSpecialIntervalTimeMin[curGrade], m_fSpecialIntervalTimeMax[curGrade]);
        }
    }
    private float _fCurDogIntervalTime
    {
        get
        {
            int curGrade = GameStateController.instant.GetCurGrade();
            return Random.Range(m_fDogIntervalTimeMin[curGrade], m_fDogIntervalTimeMax[curGrade]);
        }
    }

    private float _fUesCutTime;
    private float _fUseCutTime2;
    private float _fUseCutTime3;
    private bool _bCallGameState;
	private bool _bBithFistflg = false;
    private List<GameObject> MyBirthZombieList = new List<GameObject>();
    // Use this for initialization
    void Start () {
        Invoke("birthFirst", 0.5f);
	}

    void birthFirst()
    {
		if (!_bBithFistflg) {
			if (m_bStartBirthNormal)
			{
				ZombieBirth().init(ZombieController.STYLE.NOMARL);
			}
			if (m_bStartBirthSpecial)
			{
				ZombieBirth().init(ZombieController.STYLE.SPECIAL);
			}
			if (m_bStartBirthDog)
			{
				ZombieBirth().init(ZombieController.STYLE.DOG);
			}
			_bBithFistflg = true;
		}

    }
	// Update is called once per frame
	void Update () {
        if (GameStateController.instant.m_nCurState ==1)
        {
			_bCallGameState = false;
			birthFirst ();
            int curGrade = GameStateController.instant.GetCurGrade();
            if (curGrade < m_fNormalIntervalTimeMin.Length)
            {
                ProduceMechine();
            }
        }
        else
        {
			_bBithFistflg = false;
            if (GameStateController.instant.m_nCurState == 0)
            {
				
                _bCallGameState = false;
                if (MyBirthZombieList.Count > 0)
                {
                    for (int i = 0; i < MyBirthZombieList.Count; i++)
                    {
                        if (MyBirthZombieList[i])
                        {
                            Destroy(MyBirthZombieList[i]);
                        }
                    }
                    MyBirthZombieList.Clear();
                }
            }
            else if (GameStateController.instant.m_nCurState == 2)
            {
                if (!_bCallGameState)
                {
                    bool canAdd = true;
                    if (MyBirthZombieList.Count > 0)
                    {
                        for (int i = 0; i < MyBirthZombieList.Count; i++)
                        {
                            if (MyBirthZombieList[i])
                            {
                                canAdd = false;
                            }
                        }
                    }
                    if (canAdd)
                    {
                        _bCallGameState = true;
                        MyBirthZombieList.Clear();
                        GameStateController.instant.ZombiePlaceClearCall();
                    }
                }
            }
        }
    }

    void ProduceMechine()
    {
        if (m_bBirthNormalZombie)
        {
            _fUesCutTime += Time.deltaTime;
            if (_fUesCutTime >= _fCurNormalIntervalTime)
            {
                _fUesCutTime = 0f;
                ZombieBirth().init( ZombieController.STYLE.NOMARL);
            }
        }
        if (m_bBirthSpecialZombie)
        {
            _fUseCutTime2 += Time.deltaTime;
            if (_fUseCutTime2 >= _fCurSpecialIntervalTime)
            {
                _fUseCutTime2 = 0f;
                ZombieBirth().init(ZombieController.STYLE.SPECIAL);
            }
        }
        if (m_bBirthDogZombie)
        {
            _fUseCutTime3 += Time.deltaTime;
            if (_fUseCutTime3 >= _fCurDogIntervalTime)
            {
                _fUseCutTime3 = 0f;
                ZombieBirth().init(ZombieController.STYLE.DOG);
            }
        }
    }

    ZombieController ZombieBirth()
    {
        GameObject go = Instantiate(Resources.Load("AllZombieController")) as GameObject;
        go.transform.parent = this.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.localScale = Vector3.one;
        MyBirthZombieList.Add(go);
        ZombieController newZombie = go.GetComponent<ZombieController>();
        return newZombie;
    }

}
