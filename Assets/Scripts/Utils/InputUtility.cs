using UnityEngine;

public class InputUtility
{
    public static bool TryGetInputPosition(out Vector2 inputPosition)
    {
        inputPosition = Vector2.zero;
        if (Input.touchCount > 0)
        {
            // Get the first touch position
            Touch touch = Input.GetTouch(0);
            inputPosition = touch.position;
            return true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position
            inputPosition = Input.mousePosition;
            return true;
        }
        return false;
    }
}