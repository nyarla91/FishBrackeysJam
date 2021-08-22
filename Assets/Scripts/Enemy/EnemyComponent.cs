using System.Collections;
using System.Collections.Generic;
using NyarlaEssentials;
using UnityEngine;

public class EnemyComponent : Transformer
{
    private Enemy _enemy;
    protected Enemy Enemy => _enemy ?? (_enemy = GetComponent<Enemy>());
}
