// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GUIController.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once InconsistentNaming
public class GUIContoller : MonoBehaviour
{
    public static GUIContoller Instance;

    /// <summary>
    /// Seed choice dropdown menu.
    /// </summary>
   // public GameObject SeedChoiceDropdown;
    /// <summary>
    /// Season Clock Hand.
    /// </summary>
    public GameObject SeasonClockHand;
    /// <summary>
    /// Game Speed display.
    /// </summary>
    public GameObject GameSpeedObj;
    /// <summary>
    /// Resources Panel.
    /// </summary>
    public GameObject ResourcesPanel;
    /// <summary>
    /// Citzen Overview Panel
    /// </summary>
    public GameObject CitzenOverview;
    /// <summary>
    /// Citzen list item.
    /// </summary>
    public GameObject CitzenListItem;
    /// <summary>
    /// If will update the character overview window.
    /// </summary>
    public bool UpdateCharacterOverview;
    /// <summary>
    /// If will update the seed choice.
    /// </summary>
    private bool UpdateSeed;
    /// <summary>
    /// Selected thing.
    /// </summary>
    public GameObject SelectedThing;
    /// <summary>
    /// private list with all citzens.
    /// </summary>
    private List<GameObject> _citzens = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        _citzens = GameController.Instance.City.CityHabitants;
        if (UpdateCharacterOverview)
        {
            ClearCitzenOverview();
            if (CitzenOverview.transform.childCount == 0)
            {
                UpdateCharacterOverview = false;
            }
        }
        UpdateCitzenOverview();
        UpdateAllGUI();
  //      UpdateSeedChoice();
    }
    /// <summary>
    /// Activate and deactivate the current menu.
    /// </summary>
    /// <param name="target">Target to activate and deactivate</param>
    public void ActivateMenu(GameObject target)
    {
        target.SetActive(!target.activeSelf);
    }

    /// <summary>
    /// Set the game speed.
    /// </summary>
    /// <param name="x"></param>
    public void SetGameSpeed(int x)
    {
        if (x < 0)
        {
            GameController.Instance.City.Time.Speed -= x;
        }
        else if (x > 1)
        {
            GameController.Instance.City.Time.Speed += x;
        }
        else
        {
            GameController.Instance.City.Time.Speed = x;
        }
        if (GameController.Instance.City.Time.Speed <= 0)
        {
            GameController.Instance.City.Time.Speed = 0;
        }
    }

    /// <summary>
    /// CLock hand rotation.
    /// </summary>
    /// <param name="seasonIndex">Current season Index</param>
    public void ClockController(int seasonIndex)
    {
        var day = GameController.Instance.City.Time.CurrentDay;
        var seasonDay = GameController.Instance.City.Time.Seasons[seasonIndex].Days;
        var ammout = (float)day / seasonDay;
        var rotateInitial = 0;
        if (seasonIndex == 0)
        {
            rotateInitial = -180;
        }
        if(seasonIndex == 1)
        {
            rotateInitial = -270;
        }
        if(seasonIndex == 2)
        {
            rotateInitial = 0;
        }
        if(seasonIndex == 3)
        {
            rotateInitial = -90;
        }
        Debug.Log(seasonIndex);

        SeasonClockHand.transform.rotation = Quaternion.Euler(0, 0, (rotateInitial + 45) - (90 * ammout));
    }

    /// <summary>
    /// This will display the Character Overview menu.
    /// </summary>
    /// <param name="self">The panel to activate.</param>
    public void CharacterOverview(GameObject self)
    {
        self.SetActive(!self.activeSelf);
        if (self.activeSelf)
        {
            UpdateCitzenOverview();
            if (CitzenOverview.transform.childCount != 0 && CitzenOverview.transform.childCount != _citzens.Count)
            {
                ClearCitzenOverview();
            }
            CitzenOverview.GetComponent<AutomaticSize>().AdjustSize();
        }
    }

    /// <summary>
    /// Update the overviewGUI.
    /// </summary>
    private void UpdateCitzenOverview()
    {
        List<string> _jobList;
        if (CitzenOverview.transform.childCount == 0)
        {
            foreach (var citzen in _citzens)
            {
                if (citzen != null)
                {
                    var citzenScript = citzen.GetComponent<Citzen>();

                    var temp = Instantiate(CitzenListItem, Vector3.zero, Quaternion.identity);
                    temp.transform.SetParent(CitzenOverview.transform);

                    var tempCitzenName = temp.transform.Find("Citzen Name").GetComponent<Text>();
                    var tempCitzenGenere = temp.transform.Find("Gender").GetComponent<Text>();
                    var tempCitzenAge = temp.transform.Find("Age").GetComponent<Text>();
                    var tempJob = temp.transform.Find("Current Job").GetComponent<Text>();
                    var tempDropDown = temp.transform.Find("Job Dropdown").GetComponent<Dropdown>();

                    tempCitzenName.text = citzenScript.Name;
                    tempCitzenGenere.text = citzenScript.NpcGenere.ToString("G").First().ToString();
                    tempCitzenAge.text = citzenScript.Age.ToString();
                    tempJob.text = citzenScript.Profession.JobName;

                    _jobList = new List<string>();
                    foreach (var skill in citzenScript.Skills)
                    {
                        _jobList.Add(skill.SkillName);
                    }
                    tempDropDown.AddOptions(_jobList);
                    tempDropDown.GetComponent<DropDownExtensionScript>().Init(citzen);

                }
            }
        }
    }

    /*
    /// <summary>
    /// Update the seed selection dropdown menu.
    /// </summary>
    private void UpdateSeedChoice()
    {
        if (!UpdateSeed)
        {
            List<string> seedList = new List<string>();
            var tempData = GameController.Instance.GameData.Seeds;
            foreach (var seed in tempData)
            {
                seedList.Add(seed.SeedName);
            }
            var temp = SeedChoiceDropdown.GetComponent<Dropdown>();
            temp.AddOptions(seedList);
            UpdateSeed = true;
        }
    }
    */
    /// <summary>
    /// Clear the overviewGUI.
    /// </summary>
    private void ClearCitzenOverview()
    {
        foreach (Transform child in CitzenOverview.transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    /// <summary>
    /// Resource PanelGUI updater.
    /// </summary>    
    // ReSharper disable once InconsistentNaming
    private void UpdateAllGUI()
    {
        if (GameController.Instance.City != null)
        {
            GameSpeedObj.GetComponent<Text>().text = GameController.Instance.City.Time.Speed.ToString();
            var tempStone = ResourcesPanel.transform.Find("StoneText").GetComponent<Text>();
            var tempWood = ResourcesPanel.transform.Find("WoodText").GetComponent<Text>();
            var tempFood = ResourcesPanel.transform.Find("FoodText").GetComponent<Text>();
            tempStone.text = GameController.Instance.City.CityResources.Stone.ToString();
            tempWood.text = GameController.Instance.City.CityResources.Wood.ToString();
            tempFood.text = GameController.Instance.City.CityResources.Food.ToString("####");
        }
    }

    // ReSharper disable once InconsistentNaming
    private void SelectThingGUI()
    {
        
    }

    public void SelectBuildingType(int x)
    {
        BuildingController.Instance.SelectedTypeToBuild = (TypeBuilding) x;
    }

    public void SelectBuilding(string x)
    {
        BuildingController.Instance.SelectedBuildingName = x;
    }

}