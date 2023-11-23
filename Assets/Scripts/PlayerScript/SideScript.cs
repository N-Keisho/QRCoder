using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScript : MonoBehaviour
{
    public string SideName;
    PlayerScript playerscript;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Astronaut");
        playerscript = obj.GetComponent<PlayerScript>();
        animator = obj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            if (SideName == "front") playerscript.FrontSide = true;
            else if (SideName == "back") playerscript.BackSide = true;
            else if (SideName == "right") playerscript.RightSide = true;
            else if (SideName == "left") playerscript.LeftSide = true; 
            else if (SideName == "ground") playerscript.Grounded = true;
        }
        else if(other.tag == "Goal"){
            playerscript.GoalFlag = true;
            animator.SetBool("Goaled", true);
        }
        else if (other.tag == "Button"){
            playerscript.ButtonPushed = true;
            // if (SideName == "front") playerscript.FrontSide = true;
            // else if (SideName == "back") playerscript.BackSide = true;
            // else if (SideName == "right") playerscript.RightSide = true;
            // else if (SideName == "left") playerscript.LeftSide = true; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Button")
        {
            if (SideName == "front") playerscript.FrontSide = false;
            else if (SideName == "back") playerscript.BackSide = false;
            else if (SideName == "right") playerscript.RightSide = false;
            else if (SideName == "left") playerscript.LeftSide = false; 
            else if (SideName == "ground") playerscript.Grounded = false;
        }
    }
}
