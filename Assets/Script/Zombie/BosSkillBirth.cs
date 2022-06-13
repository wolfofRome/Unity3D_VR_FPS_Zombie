using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BosSkillBirth : MonoBehaviour {

    public int OnceDogNum = 5;
    private bool _bBirthFlg;
    private float _fUesCutTime;
    private float _fCurDogIntervalTime = 0.5f;

    private List<GameObject> MyBirthZombieList = new List<GameObject>();
    private int _numCut;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateController.instant.m_nCurState == 0)
        {
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
        else
        {
            ProduceMechine();
        }
    }

    public void StartBornDog()
    {
        _bBirthFlg = true;
    }

    void ProduceMechine()
    {
        if (_bBirthFlg)
        {
            _fUesCutTime += Time.deltaTime;
            if (_fUesCutTime >= _fCurDogIntervalTime)
            {
                if (_numCut >= 5)
                {
                    _bBirthFlg = false;
                    _numCut = 0;
                }
                else
                {
                    _fUesCutTime = 0f;
                    _numCut += 1;
                    ZombieBirth().init(ZombieController.STYLE.DOG);
                }
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
