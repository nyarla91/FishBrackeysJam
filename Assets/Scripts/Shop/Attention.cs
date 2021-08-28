using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attention : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void Show() => _image.enabled = true;
    public void Hide() => _image.enabled = false;
}
