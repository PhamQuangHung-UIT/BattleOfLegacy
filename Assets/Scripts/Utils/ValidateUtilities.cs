using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ValidateUtilities
{
    public static void Assert(bool condition) => Debug.Assert(condition);

    public static void AssertNotNull(object obj, string objName)
    {
        Debug.Assert(obj != null, $"The {objName} is null");
    }

    public static void AssertEmptyList<T>(IEnumerable<T> l)
    {
        Debug.Assert(l != null || l.Count() == 0);
    }

    public static void AssertException(Action action, string message)
    {
        try
        {
            action.Invoke();
        }
        catch (Exception)
        {
            Debug.Log(message);
        }
    }
}