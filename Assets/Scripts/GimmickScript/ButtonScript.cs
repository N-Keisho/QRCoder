using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    PlayerScript playerscript;
    Animator btnAnimator;
    Animator rocketAnimator;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Astronaut");
        playerscript = obj.GetComponent<PlayerScript>();
        btnAnimator = GetComponent<Animator>();
        obj = GameObject.Find("AtomRocket");
        rocketAnimator = obj.GetComponent<Animator>();
    }

    void Update(){
        if (playerscript.ButtonPushed == true){
            btnAnimator.SetBool("isPushed", true);
            rocketAnimator.SetBool("ButtonPushed", true);
        }
        else {
            btnAnimator.SetBool("isPushed", false);
            rocketAnimator.SetBool("ButtonPushed", false);
        }
    }
}
