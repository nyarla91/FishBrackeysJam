using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Controls
{
    private static InputMap _inputMap;
    public static InputMap InputMap => _inputMap ?? (_inputMap = new InputMap());

    public static void Reset()
    {
        _inputMap = null;
    }
}
