using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyAttributes", menuName = "Enemies/EnemyAttributes")]

public class EnemyScriptableObject : ScriptableObject
{
    public int damageDealt;
    public float speed;
}