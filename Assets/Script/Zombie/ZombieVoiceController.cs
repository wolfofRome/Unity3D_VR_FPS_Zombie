using UnityEngine;
using System.Collections;

public class ZombieVoiceController : MonoBehaviour {

    public AudioSource m_MySource;
    public AudioClip m_groanVoice1;
    public AudioClip m_groanVoice2;
    public float m_fVolume = 1;
    public float m_fGroanInterval = 30f;
    
    private float m_fCutTime;
    private AudioClip _curPlayClip;
    // Use this for initialization
    void Start () {
        int rangnum = Random.Range(0,2);
        if (rangnum == 0)
        {
            _curPlayClip = m_groanVoice1;
        }
        else
        {
            _curPlayClip = m_groanVoice2;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (GameStateController.instant.m_nCurState >0)
        {
			m_fCutTime += Time.deltaTime;
            if (m_fCutTime >= m_fGroanInterval)
            {
                m_MySource.clip = _curPlayClip;
                m_MySource.volume = m_fVolume;
                m_MySource.loop = false;
                m_MySource.Play();
                m_fCutTime = 0f;
            }
        }
	}
}
