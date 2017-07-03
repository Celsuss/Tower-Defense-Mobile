using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTileData{
    public string Name;
    public GameObject Prefab;
}

//[CreateAssetMenu] creates an entry in the default Create menu of the ProjectView so you can easily create an instance of this ScriptableObject
[CreateAssetMenu]
public class LevelTiles : ScriptableObject {
    //This ScriptableObject simply stores a list of blocks. It kind of acts like a database in that it stores rows of data
    public List<LevelTileData> Tiles = new List<LevelTileData>();
}