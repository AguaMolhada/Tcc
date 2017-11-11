using UnityEngine;
using UnityEngine.UI;

public class DropDownExtensionScript : MonoBehaviour
{
    private GameObject _attObj;
    private Dropdown _mySelfDropdown;

    public void Init(GameObject o)
    {
        _attObj = o;
        _mySelfDropdown = gameObject.GetComponent<Dropdown>();
    }

    public void UpdateCitzenJob()
    {
        _attObj.GetComponent<Citzen>().UpdateJob(_mySelfDropdown.options[_mySelfDropdown.value].text);
        GUIContoller.Instance.UpdateCharacterOverview = true;
    }

    public void UpdateSeedFarm()
    {
        _attObj.GetComponent<Farm>().StartFarm(_mySelfDropdown.options[_mySelfDropdown.value].text);
    }
}