using UnityEngine;
using UnityEngine.UI;

public class DropDownExtensionScript : MonoBehaviour
{
    private GameObject _attCitzen;
    private Dropdown _mySelfDropdown;

    public void Init(GameObject c)
    {
        
        _attCitzen = c;
        _mySelfDropdown = gameObject.GetComponent<Dropdown>();
    }

    public void UpdateCitzenJob()
    {
        _attCitzen.GetComponent<Citzen>().UpdateJob(_mySelfDropdown.options[_mySelfDropdown.value].text);
        GUIContoller.Instance.UpdateCharacterOverview = true;
    }
}