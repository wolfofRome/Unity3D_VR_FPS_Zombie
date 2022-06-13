using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIRangController : MonoBehaviour {

    public UIRangOnetip[] m_rangs;
    private List<string> showList = new List<string>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        init();
    }

    void init()
    {
        showList.Clear();
        for (int i = 0; i < 9; i++)
        {
			Debug.Log ("--UIRangController---:"+PlayerPrefs.GetString(i.ToString(), "-1"));
            showList.Add(PlayerPrefs.GetString(i.ToString(), "-1"));
        }
       // showList.Sort(ComperaTo);
        MaoPao();
        for (int i = 0; i < m_rangs.Length; i++)
        {
            if (showList[i].Equals("-1"))
            {
                m_rangs[i].setInfo("   ","   "); 
            }
            else
            {
                string[] infos = showList[i].Split('_');
                m_rangs[i].setInfo(infos[0],infos[1]);
                PlayerPrefs.SetString(i.ToString(), showList[i]);
            }
           
        }
    }

    int ComperaTo(string str1,string str2)
    {
        int num = 0;
        if (str1.Equals("-1") && str2.Equals("-1"))
        {
            num = 0;
            return num;
        }
        else if (str1.Equals("-1"))
        {
            num = -1;
            return num;
        }
        else if (str2.Equals("-1"))
        {
            num = 1;
            return num;
        }
        else
        {
            string[] infos1 = str1.Split('_');
            string[] infos2 = str2.Split('_');
            int num1 = 0;
            int num2 = 0;
            int.TryParse(infos1[1], out num1);
            int.TryParse(infos2[1], out num2);
            if (num1 < num2)
            {
                num = -1;
                return num;
            }
            else
            {
                num = 1;
                return num;
            }
        }
        return num;
    }

    void MaoPao()
    {
        for (int i = 0; i <= showList.Count - 1; i++)
        {

            for (int j = i + 1; j < showList.Count; j++)
            {
                string str1 = showList[i];
                string str2 = showList[j];
				if (str1.Equals ("-1") && str2.Equals ("-1")) {
                   
				} else if (str1.Equals ("-1")) {
					string temp1 = showList [i];

					showList [i] = showList [j];

					showList [j] = temp1;
				}
				else if (str2.Equals ("-1"))
				{
					
				}
                else
                {
                    string[] infos1 = str1.Split('_');
                    string[] infos2 = str2.Split('_');
                    int num1 = 0;
                    int num2 = 0;
                    int.TryParse(infos1[1], out num1);
                    int.TryParse(infos2[1], out num2);
                    if (num1 < num2)
                    {
                        string temp2 = showList[i];

                        showList[i] = showList[j];

                        showList[j] = temp2;
                    }
                }
            }

        }
    }
}
