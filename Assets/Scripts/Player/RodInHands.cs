using System;
using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class RodInHands : Transformer
{
    public static RodInHands instance;

    [SerializeField] private GameObject _slashPrefab;
    [SerializeField] private Transform _graphics;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _origin;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _lineQuality;

    public Transform Origin => _origin;
    public bool attacking;

    private void Awake()
    {
        instance = this;
    }

    public void ThrowStart(ProjectileLifecycle hook, float cooldown, Vector2 direction)
    {
        StartCoroutine(Throw(hook, cooldown, direction));
    }

    private IEnumerator Throw(ProjectileLifecycle hook, float cooldown, Vector2 direction)
    {
        attacking = true;
        _lineRenderer.enabled = true;
        direction.x = Mathf.Abs(direction.x);
        
        Vector2 hookLastPosition = Vector2.zero;
        hook.OnProjectileDestroy += () => CooldownStart(ref hookLastPosition, hook);
        BezierCurve lineCurve = new BezierCurve(new Vector3[3], _lineQuality);
        
        float rotation = VectorHelper.Vector2ToDegrees(direction);
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
        
        while (hookLastPosition.magnitude == 0)
        {
            const float yOffset = -2.5f;
            lineCurve.Quality = _lineQuality;
            lineCurve.SetPoint(0, _origin.position);
            lineCurve.SetPoint(1, Vector2.Lerp(_origin.position, hook.transform.position, 0.5f) + Vector2.up *yOffset);
            lineCurve.SetPoint(2, hook.transform.position);
            Vector3[] _linePoints = lineCurve.Path;
            for (int i = 0; i < _linePoints.Length; i++)
            {
                _linePoints[i].z = -1;
            }
            _lineRenderer.positionCount = _lineQuality;
            _lineRenderer.SetPositions(_linePoints);
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(0, 0, 60);
        BezierCurve returnCurve = new BezierCurve(new Vector3[3], _lineQuality);
        for (float i = 0; i < 1; i += Time.deltaTime / cooldown)
        {
            const float returnYOffst = 4;
            returnCurve.SetPoint(0, hookLastPosition);
            returnCurve.SetPoint(1, Vector2.Lerp(hookLastPosition, _origin.position, 0.7f) + Vector2.up * returnYOffst);
            returnCurve.SetPoint(2, _origin.position);
            Vector2 currentHookPoint = returnCurve.Evaluate(i);
            
            const float lineYOffset = -4;
            lineCurve.Quality = _lineQuality;
            lineCurve.SetPoint(0, _origin.position);
            lineCurve.SetPoint(1, Vector2.Lerp(_origin.position, currentHookPoint, 0.5f) + Vector2.up *lineYOffset);
            lineCurve.SetPoint(2, currentHookPoint);
            Vector3[] _linePoints = lineCurve.Path;
            for (int j = 0; j < _linePoints.Length; j++)
            {
                _linePoints[j].z = -1;
            }
            _lineRenderer.positionCount = _lineQuality;
            _lineRenderer.SetPositions(_linePoints);
            yield return null;
        }
        _lineRenderer.enabled = false;
        attacking = false;
    }

    private void CooldownStart(ref Vector2 hookLastPosition, ProjectileLifecycle hook)
    {
        hookLastPosition = hook.transform.position;
    }

    public void HitStart(float duration)
    {
        StartCoroutine(Hit(duration));
    }

    private IEnumerator Hit(float duration)
    {
        attacking = true;
        _graphics.localScale = Vector3.one * 1.5f;
        SpriteRenderer _slash = Instantiate(_slashPrefab, transform.position + Vector3.forward, Quaternion.identity).GetComponentInChildren<SpriteRenderer>();
        _slash.transform.parent = transform.parent;
        float angle = 60;
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            angle = Mathf.Lerp(angle, -60, 14 * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            float alpha = 1 - MathHelper.Evaluate(angle, -60, 60);
            _slash.color = new Color(1 ,1 , 1, alpha);
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(0, 0, 60);
        _graphics.localScale = Vector3.one;
        Destroy(_slash.gameObject);
        attacking = false;
    }

    public void Show() => _spriteRenderer.enabled = true;
    public void Hide() => _spriteRenderer.enabled = false;
}
