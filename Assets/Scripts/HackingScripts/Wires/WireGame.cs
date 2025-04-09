using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class WireGame : MiniGame

{
    public Wire currentDrag;
    public Wire Hoverd;

    private int correctWires;


    private void Enable()
    {
        lives = 3;
        currentDrag = null;
        Hoverd = null;
    }
    public void CorrectWires()
    {
        correctWires++;
    }

    private new void Update()
    {
        base.Update();
        if(correctWires == 4)
        {
            Win();
        }
    }
}
