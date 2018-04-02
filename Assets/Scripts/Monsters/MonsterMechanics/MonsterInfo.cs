using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MonsterInfo {
    public GV.MonsterTypes monsterType;
    public System.Type monsterScriptType;
    public float hp, speed, dmg;

	public MonsterInfo(GV.MonsterTypes _monsterType ,System.Type _monsterScriptType, float _hp, float _speed, float _dmg)
    {
        monsterType = _monsterType;
        monsterScriptType = _monsterScriptType;
        hp = _hp;
        speed = _speed;
        dmg = _dmg;
    }
}
