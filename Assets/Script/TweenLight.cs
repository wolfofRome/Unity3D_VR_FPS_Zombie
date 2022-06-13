using UnityEngine;
using System.Collections;

public class TweenLight : MonoBehaviour {
    public float m_fIntervalTime;
    public float m_fRate;
    public int m_nTwinkleTime;
	public float m_fMiniIntensity;

    private float _fMaxIntensity;
    private Light _MyLight;

    private float _useCutTime;
    // Use this for initialization
    void Start () {
        _MyLight = this.GetComponent<Light>();
        _fMaxIntensity = _MyLight.intensity;
    }
	
	// Update is called once per frame
	void Update () {
        if (_MyLight != null)
        {
            _useCutTime += Time.deltaTime;
            if (_useCutTime >= m_fIntervalTime)
            {
                _useCutTime = 0;
                StartCoroutine(twinkle());
            }
        }
	}

    IEnumerator twinkle()
    {
        for (int i = 0; i < m_nTwinkleTime; i++)
        {
			_MyLight.intensity = m_fMiniIntensity;
            yield return new WaitForSeconds(m_fRate);
            _MyLight.intensity = _fMaxIntensity;
            yield return new WaitForSeconds(m_fRate);
        }
    }
}
