// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GUIController.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once InconsistentNaming
public class GUIContoller : MonoBehaviour
{
    public static GUIContoller Instance;
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

    public bool UpdateCharacterOverview;

    public Citzen SelectedCitzen;
    private List<GameObject> _citzens = new List<GameObject>();
    private List<String> _jobList;

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
        ResourceUpdate();
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

    private void UpdateCitzenOverview()
    {
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

    private void ClearCitzenOverview()
    {
        foreach (Transform child in CitzenOverview.transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    private void ResourceUpdate()
    {
        if (GameController.Instance.City != null)
        {
            var tempStone = ResourcesPanel.transform.Find("StoneText").GetComponent<Text>();
            var tempWood = ResourcesPanel.transform.Find("WoodText").GetComponent<Text>();
            var tempFood = ResourcesPanel.transform.Find("FoodText").GetComponent<Text>();
            tempStone.text = GameController.Instance.City.CityResources.Stone.ToString();
            tempWood.text = GameController.Instance.City.CityResources.Wood.ToString();
            tempFood.text = GameController.Instance.City.CityResources.Food.ToString("####");
        }
    }
    
    public void SelectBuildingType(int x)
    {
        GameController.Instance.SelectedTypeToBuild = (TypeBuilding) x;
    }

    public void SelectBuilding(string x)
    {
        GameController.Instance.SelectedBuildingName = x;
    }

}