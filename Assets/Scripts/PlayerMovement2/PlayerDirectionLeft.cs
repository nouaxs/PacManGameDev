using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirectionLeft : IPlayerDirection
{
    public int getX()
    {
        return -1;
    }

    public int getY()
    {
        return 0;
    }
}
