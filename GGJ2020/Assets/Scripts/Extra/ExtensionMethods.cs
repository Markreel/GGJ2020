using UnityEngine;

public static class ExtensionMethods
{
    public static bool BoxCast(this Physics physics, BoxCast boxCast)
    {
        return Physics.BoxCast(boxCast.center, boxCast.halfExtents, boxCast.direction);
    }

    public static bool BoxCast(this Physics physics, BoxCast boxCast, out RaycastHit hitInfo)
    {
        return Physics.BoxCast(boxCast.center, boxCast.halfExtents, boxCast.direction, out hitInfo);
    }

    public static bool CheckBox(this Physics physics, BoxCast boxCast)
    {
        return Physics.CheckBox(boxCast.center, boxCast.halfExtents);
    }
}
