using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopkeeperAnimation : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private float _period;

    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                _image.sprite = _sprites[i];
                yield return new WaitForSeconds(_period);
            }
        }
    }
}
