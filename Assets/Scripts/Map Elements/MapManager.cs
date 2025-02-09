using System;
using System.Collections.Generic;

public class MapManager : Singleton<MapManager>
{
    public List<MapBlock> mapBlocks;

    private void Start()
    {
        SetUpInitialMap();
    }

    public void SetUpInitialMap()
    {
        throw new NotImplementedException();
    }
}
