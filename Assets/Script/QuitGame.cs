using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        Invoke("Quit",5f);
    }

    void Quit()
    {
        Debug.Log("Application Quit!");
        Application.Quit();
    }
}
