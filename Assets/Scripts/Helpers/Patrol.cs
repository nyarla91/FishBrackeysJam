using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NyarlaEssentials
{
public class Patrol : Transformer
{
    [SerializeField] private Vector3[] _path;
    [SerializeField] private float _speed;
    [SerializeField] private float _nextPointDelay;
    public enum LoopType
    {
        Return,
        Lap
    }
    [SerializeField] private LoopType _loopType;
    private Vector3 origin;

    private void Awake()
    {
        origin = transform.position;
    }

    private void Start()
    {
        StartCoroutine(Launch());
    }

    private IEnumerator Launch()
    {
        while (true)
        {
            foreach (var point in _path)
            {
                yield return StartCoroutine(GoToNextPoint(point));
            }
            if (_loopType.Equals(LoopType.Return))
            {
                for (int i = _path.Length - 2; i >= 0; i--){
                    yield return StartCoroutine(GoToNextPoint(_path[i]));
                }
            }
            yield return StartCoroutine(GoToNextPoint(Vector3.zero));
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < _path.Length; i++)
        {
            DrawCube(i);
            DrawLine(i);
        }

        void DrawCube(int i)
        {
            float cubeSize = 4;
            Gizmos.color = Color.green;
            Gizmos.DrawCube(_path[i] + gameObject.transform.position, new Vector3(cubeSize, cubeSize, cubeSize));
        }

        void DrawLine(int i)
        {
            Vector3 origin;
            if (i == 0)
                origin = Vector3.zero;
            else
                origin = _path[i - 1];

            Gizmos.color = Color.green;
            Gizmos.DrawLine(_path[i] + gameObject.transform.position, origin + gameObject.transform.position);
        }
    }

    private IEnumerator GoToNextPoint(Vector3 point)
    {
        point += origin;
        while (!transform.position.Equals(point)){
            transform.position = Vector3.MoveTowards(transform.position, point, _speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(_nextPointDelay);
    }
}

}