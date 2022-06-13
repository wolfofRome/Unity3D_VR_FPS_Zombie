using UnityEngine;
using System.Collections;

public class UIRangOnetip : MonoBehaviour {
    public UISprite[] m_names;
    public UISprite[] m_scores;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setInfo(string pname,string score)
    {
        for (int i = 0; i < m_names.Length; i++)
        {
            if (i < pname.Length)
            {
                m_names[i].spriteName = pname[i].ToString();
            }
            else
            {
                m_names[i].spriteName = "";
            }
        }
        for (int i = 0; i < m_scores.Length; i++)
        {
            if (i < score.Length)
            {
                m_scores[i].spriteName = score[i].ToString();
            }
            else
            {
                m_scores[i].spriteName = "";
            }
        }
    }
}
