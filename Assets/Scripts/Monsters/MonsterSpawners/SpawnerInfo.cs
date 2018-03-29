using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnerInfo  {
    public GV.MonsterTypes monsterType;
    public float hp, spawnRate, movespeed, damage;

    public SpawnerInfo(GV.MonsterTypes _monsterType, float _hp, float _spawnRate, float _moveSpeed, float _dmg)
    {
        monsterType = _monsterType;
        hp = _hp;
        spawnRate = _spawnRate;
        movespeed = _moveSpeed;
        damage = _dmg;
    }
}
