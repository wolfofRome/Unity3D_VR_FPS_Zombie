using UnityEngine;
using System.Collections;

public class PlayerUIController : MonoBehaviour {

    public UISprite m_spBloodNumHundred;
    public UISprite m_spBloodNumTen;
    public UISprite m_spBloodNum;

    public UISprite m_spCoinNumThousand;
    public UISprite m_spCoinNumHundred;
    public UISprite m_spCoinNumTen;
    public UISprite m_spCoinNum;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameStateController.instant.m_nCurState > 0)
        {
            if (!m_spBloodNumHundred.enabled)
            {
                m_spBloodNumHundred.enabled = true;
                m_spBloodNumTen.enabled = true;
                m_spBloodNum.enabled = true;

                m_spCoinNumThousand.enabled = true;
                m_spCoinNumHundred.enabled = true;
                m_spCoinNumTen.enabled = true;
                m_spCoinNum.enabled = true;
            }
            setNum();
        }
        else
        {
            m_spBloodNumHundred.enabled = false;
            m_spBloodNumTen.enabled = false;
            m_spBloodNum.enabled = false;

            m_spCoinNumThousand.enabled = false;
            m_spCoinNumHundred.enabled = false;
            m_spCoinNumTen.enabled = false;
            m_spCoinNum.enabled = false;
        }
    }

    void setNum()
    {
        if (PlayerController.instant.m_fCurHP >= 100)
        {
            m_spBloodNumHundred.spriteName = ((PlayerController.instant.m_fCurHP.ToString())[0]).ToString();
            m_spBloodNumTen.spriteName = ((PlayerController.instant.m_fCurHP.ToString())[1]).ToString();
            m_spBloodNum.spriteName = ((PlayerController.instant.m_fCurHP.ToString())[2]).ToString();
        }
        else if (PlayerController.instant.m_fCurHP < 100 && PlayerController.instant.m_fCurHP >= 10)
        {
            m_spBloodNumHundred.spriteName = "0";
            m_spBloodNumTen.spriteName = ((PlayerController.instant.m_fCurHP.ToString())[0]).ToString();
            m_spBloodNum.spriteName = ((PlayerController.instant.m_fCurHP.ToString())[1]).ToString();
        }
        else
        {
            m_spBloodNumHundred.spriteName = "0";
            m_spBloodNumTen.spriteName = "0";
            m_spBloodNum.spriteName = ((PlayerController.instant.m_fCurHP.ToString())[0]).ToString();
        }

        if (PlayerController.instant.m_nCurCoin >= 1000)
        {
            m_spCoinNumThousand.spriteName = ((PlayerController.instant.m_nCurCoin.ToString())[0]).ToString();
            m_spCoinNumHundred.spriteName = ((PlayerController.instant.m_nCurCoin.ToString())[1]).ToString();
            m_spCoinNumTen.spriteName = ((PlayerController.instant.m_nCurCoin.ToString())[2]).ToString();
            m_spCoinNum.spriteName = ((PlayerController.instant.m_nCurCoin.ToString())[3]).ToString();
        }
        else if (PlayerController.instant.m_nCurCoin < 1000 && PlayerController.instant.m_nCurCoin >= 100)
        {
            m_spCoinNumThousand.spriteName = "0";
            m_spCoinNumHundred.spriteName = ((PlayerController.instant.m_nCurCoin.ToString())[0]).ToString();
            m_spCoinNumTen.spriteName = ((PlayerController.instant.m_nCurCoin.ToString())[1]).ToString();
            m_spCoinNum.spriteName = ((PlayerController.instant.m_nCurCoin.ToString())[2]).ToString();
        }
        else if (PlayerController.instant.m_nCurCoin < 100 && PlayerController.instant.m_nCurCoin >= 10)
        {
            m_spCoinNumThousand.spriteName = "0";
            m_spCoinNumHundred.spriteName = "0";
            m_spCoinNumTen.spriteName = ((PlayerController.instant.m_nCurCoin.ToString())[0]).ToString();
            m_spCoinNum.spriteName = ((PlayerController.instant.m_nCurCoin.ToString())[1]).ToString();
        }
        else
        {
            m_spCoinNumThousand.spriteName = "0";
            m_spCoinNumHundred.spriteName = "0";
            m_spCoinNumTen.spriteName = "0";
            m_spCoinNum.spriteName = ((PlayerController.instant.m_nCurCoin.ToString())[0]).ToString();
        }
    }
}
