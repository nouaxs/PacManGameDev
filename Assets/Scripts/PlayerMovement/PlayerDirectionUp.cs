using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirectionUp : IPlayerDirection
{
    public int getX()
    {
        return 0;
    }

    public int getY()
    {
        return 1;
    }
}
