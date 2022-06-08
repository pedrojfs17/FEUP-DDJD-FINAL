using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameLogic : MonoBehaviour
{   
    public virtual void playerAction(GameObject player) {}

    public virtual void playerBlue(GameObject player) {}

    public virtual void playerOrange(GameObject player) {}

    public virtual void playerGreen(GameObject player) {}

    public virtual void playerYellow(GameObject player) {}
    
    public virtual void playerActionCanceled(GameObject player) {}
}
