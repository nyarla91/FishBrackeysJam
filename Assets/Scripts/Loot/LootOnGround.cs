using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using NyarlaEssentials.Sound;
using UnityEngine;

public class LootOnGround : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Collider2D _collider;

    private Vector2 _targetPoint;    
    private FishInfo _loot;
    public FishInfo Loot => _loot;

    public delegate void LandHandler();
    public LandHandler OnLanded;


    public void Init(FishInfo loot, Vector2 targetPoint)
    {
        _loot = loot;
        _spriteRenderer.sprite = _loot.Sprite;
        _targetPoint = targetPoint;
        StartCoroutine(Fly());
    }

    private IEnumerator Fly()
    {
        const float alignYOffset = 6;
        const float flightDuration = 1f;

        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        
        BezierCurve curve = new BezierCurve(new Vector3[3]);
        curve.SetPoint(0, transform.position);
        curve.SetPoint(1, Vector2.Lerp(transform.position, _targetPoint, 0.5f) + Vector2.up * alignYOffset);
        curve.SetPoint(2, _targetPoint);

        for (float i = 0; i < 1; i += Time.deltaTime / flightDuration)
        {
            Vector3 newPoint = curve.Evaluate(i);
            newPoint.z = -1;
            transform.position = newPoint;
            yield return null;
        }

        if (OnLanded != null)
            OnLanded();
        SoundPlayer.Play("fishDrop", 1);
        _particleSystem.enableEmission = false;
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }
}
