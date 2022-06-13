using UnityEngine;
using System.Collections;

public class GunUIController : MonoBehaviour {
    public GunController m_Myparent;

    public UISprite m_spLocalBulletNumTen;
    public UISprite m_spLocalBulletNum;

    public UISprite m_spAllBulletNumHundred;
    public UISprite m_spAllBulletNumTen;
    public UISprite m_spAllBulletNum;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameStateController.instant.m_nCurState > 0)
        {
            if (!m_spLocalBulletNumTen.enabled)
            {
                m_spLocalBulletNumTen.enabled = true;
                m_spLocalBulletNum.enabled = true;
                m_spAllBulletNumHundred.enabled = true;
                m_spAllBulletNumTen.enabled = true;
                m_spAllBulletNum.enabled = true;
            }
            setNum();
        }
        else
        {
            m_spLocalBulletNumTen.enabled = false;
            m_spLocalBulletNum.enabled = false;

            m_spAllBulletNumHundred.enabled = false;
            m_spAllBulletNumTen.enabled = false;
            m_spAllBulletNum.enabled = false;

        }
	}

    void setNum()
    {
        if (m_Myparent.m_nCurGunBullet >=10)
        {
            m_spLocalBulletNumTen.spriteName = ((m_Myparent.m_nCurGunBullet.ToString())[0]).ToString();
            m_spLocalBulletNum.spriteName = ((m_Myparent.m_nCurGunBullet.ToString())[1]).ToString();
        }
        else
        {
            m_spLocalBulletNumTen.spriteName = "0";
            m_spLocalBulletNum.spriteName = ((m_Myparent.m_nCurGunBullet.ToString())[0]).ToString();
        }

        if (PlayerController.instant.m_nCurBullet >= 100)
        {
            m_spAllBulletNumHundred.spriteName = ((PlayerController.instant.m_nCurBullet.ToString())[0]).ToString();
            m_spAllBulletNumTen.spriteName = ((PlayerController.instant.m_nCurBullet.ToString())[1]).ToString();
            m_spAllBulletNum.spriteName = ((PlayerController.instant.m_nCurBullet.ToString())[2]).ToString();
        }
        else if (PlayerController.instant.m_nCurBullet < 100&& PlayerController.instant.m_nCurBullet >=10 )
        {
            m_spAllBulletNumHundred.spriteName = "0";
            m_spAllBulletNumTen.spriteName = ((PlayerController.instant.m_nCurBullet.ToString())[0]).ToString();
            m_spAllBulletNum.spriteName = ((PlayerController.instant.m_nCurBullet.ToString())[1]).ToString();
        }
        else
        {
            m_spAllBulletNumHundred.spriteName = "0";
            m_spAllBulletNumTen.spriteName = "0";
            m_spAllBulletNum.spriteName = ((PlayerController.instant.m_nCurBullet.ToString())[0]).ToString();
        }
    }

}
