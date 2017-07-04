using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTileData{
    public string Name;
    public GameObject Prefab;
}

[CreateAssetMenu]
public class LevelTiles : ScriptableObject {
    public List<LevelTileData> Tiles = new List<LevelTileData>();
}