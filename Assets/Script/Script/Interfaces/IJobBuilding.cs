using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJobBuilding {

    int ShowProgress();
    void AddResources(GameResources x);
    bool AssignWorker();
    
}
