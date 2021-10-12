﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    
    private bool _move;
    private bool _dash;
    private bool _meleeAttack;
    private bool _hookAttack;

    private bool _hidden;
    
    private void Awake()
    {
        Controls.InputMap.Tutorial.Enable();
        Controls.InputMap.Tutorial.Move.performed += ctx => TriggerMove(out _move);
        Controls.InputMap.Tutorial.Dash.performed += ctx => TriggerMove(out _dash);
        Controls.InputMap.Tutorial.MeleeAttack.performed += ctx => TriggerMove(out _meleeAttack);
        Controls.InputMap.Tutorial.HookAttack.performed += ctx => TriggerMove(out _hookAttack);
    }

    private void TriggerMove(out bool action)
    {
        action = true;
        Check();
    }

    private void Check()
    {
        if (!_hidden &&_dash && _move && _meleeAttack && _hookAttack)
            StartCoroutine(BlendOut());
    }

    private IEnumerator BlendOut()
    {
        _hidden = true;
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            _canvasGroup.alpha = 1 - i;
            yield return null;
        }
    }

    private void OnDestroy()
    {
        Controls.InputMap.Tutorial.Enable();
        Controls.InputMap.Tutorial.Move.performed -= ctx => TriggerMove(out _move);
        Controls.InputMap.Tutorial.Dash.performed -= ctx => TriggerMove(out _dash);
        Controls.InputMap.Tutorial.MeleeAttack.performed -= ctx => TriggerMove(out _meleeAttack);
        Controls.InputMap.Tutorial.HookAttack.performed -= ctx => TriggerMove(out _hookAttack);
    }
}
