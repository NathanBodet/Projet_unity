using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Datas
{
    public string name;
    public float x;
    public float y;
    public int strength;
    public int endurance;
    public int agility;
    public float switchCooldown;
    public bool typeArmeEquipee;
    public float currentHealth;
    public string nameScene;
    public int i = 0;
    public int j = 0;
    public int[][] map;
    public bool[][] roomsFinies;
    public int[][] roomsSpec;
    public float timerStart;
}
