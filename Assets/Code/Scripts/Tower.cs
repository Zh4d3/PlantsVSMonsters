using System;
using UnityEngine;

[Serializable]
public class Tower {

    public string name;
    public int cost;
    public GameObject prefab;
    public string towerType;

    public Tower (string _name, int _cost, GameObject _prefab, string _towerType) {
        name = _name;
        cost = _cost;
        prefab = _prefab;
        towerType = _towerType;
    }

}
