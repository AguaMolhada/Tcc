public interface IBuilding
{

    BuildingEventsHandler OnConstruction();
    /// <summary>
    /// Check if the building is overlaping anotherone
    /// </summary>
    /// <returns>if overlap return false</returns>
    bool CheckOverlap(int x,int y);

}

