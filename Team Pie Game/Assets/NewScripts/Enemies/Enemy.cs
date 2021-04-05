using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy", menuName ="ScritableObjects/Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public int hp;
    public int damage;
    public int expValue;
    public float speed;

    public int numOfIngredientsToDrop;

    public GameObject ingridentPrefab;
    public GameObject deathEffect;

}
