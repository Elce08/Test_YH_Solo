﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data - Armor", menuName = "Scriptable Object/Item Data - Armor", order = 2)]
public class ItemData_Armor : ItemData
{
    public override WeaponType equipPart => WeaponType.Armor;

    public override void ItemStatus()
    {
        price = 1000 + (upgrade * 500);                // 아이템 가치

        beforeStr = (upgrade + 1) * 5;                 // 아이템의 강화 전 Str 수치
        beforeDef = (upgrade + 1) * 5;                 // 아이템의 강화 전 Def 수치
        beforeHP = (upgrade + 1) * 10;                 // 아이템의 강화 전 HP 수치
        beforeMP = (upgrade + 1) * 10;                 // 아이템의 강화 전 MP 수치
        beforeValue = upgrade;                         // 아이템의 강화 전 강화 수치

        afterStr = (upgrade + 2) * 5;                  // 아이템의 강화 후 Str 수치
        afterDef = (upgrade + 2) * 5;                  // 아이템의 강화 후 Def 수치
        afterHP = (upgrade + 2) * 10;                  // 아이템의 강화 후 HP 수치
        afterMP = (upgrade + 2) * 10;                  // 아이템의 강화 후 MP 수치
        afterValue = upgrade + 1;

        risingStr = afterStr - beforeStr;    // 아이템의 강화 시 상승 Str 수치
        risingDef = afterDef - beforeDef;    // 아이템의 강화 시 상승 Def 수치
        risingHP = afterHP - beforeHP;       // 아이템의 강화 시 상승 HP 수치
        risingMP = afterMP - beforeMP;       // 아이템의 강화 시 상승 MP 수치

        if(upgrade == 5)
        {
            cost = 0;                          // 아이템의 강화 시 소모 비용
        }
        cost = (upgrade + 1) * 500;                          // 아이템의 강화 시 소모 비용
    }
}
