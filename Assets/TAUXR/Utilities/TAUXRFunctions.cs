using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TAUXRFunctions
{
    public static string GetFormattedDateTime(bool includeTime = false)
    {
        DateTime now = DateTime.Now;
        if (includeTime)
        {
            return now.ToString("yy.MM.dd_HH-mm");
        }
        else
        {
            return now.ToString("yyyy.MM.dd");
        }
    }
}
