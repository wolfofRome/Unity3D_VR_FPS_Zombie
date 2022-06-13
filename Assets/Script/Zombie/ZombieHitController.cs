using UnityEngine;
using System.Collections;

public class ZombieHitController : MonoBehaviour {


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void AttackPlayr(AnimationEvent aevent)
    {
		//Debug.Log ("------AttackPlayr------");
        //Debug.Log("-----AnimationEvent-----" + aevent.data);
        int intdata = int.Parse(aevent.data);
        switch ((ZombieController.STYLE)intdata)
        {
            case ZombieController.STYLE.NOMARL:
                PlayerController.instant.GetHit(PlayerController.instant.m_fNormalZombieHurt);
                break;
            case ZombieController.STYLE.SPECIAL:
                PlayerController.instant.GetHit(PlayerController.instant.m_fSpecialZombieHurt);
                break;
            case ZombieController.STYLE.DOG:
                PlayerController.instant.GetHit(PlayerController.instant.m_fDogZombieHurt);
                break;
        }
    }
}
