using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPauseable
{
    void Pause(bool isDialoguePause = false);
    void Resume();
}
