// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Citzen.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RandomNameGeneratorLibrary;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Class for all NPC's
/// </summary>
public class Citzen : MonoBehaviour
{
    /// <summary>
    /// NPC name.
    /// </summary>
    public string Name { get; protected set; }
    /// <summary>
    /// NPC Age.
    /// </summary>
    public int Age { get; protected set; }
    /// <summary>
    /// NPC NpcGenere.
    /// </summary>
    public Genere NpcGenere { get; protected set; }
    /// <summary>
    /// NPC Death Change, more old more chance to die.
    /// </summary>
    public float DeathChance { get; protected set; }
    /// <summary>
    /// NPC Hunger value. If stay 0 for 1 day he will die doesn't matter the death chance.
    /// </summary>
    public float Saturation { get; protected set; }
    /// <summary>
    /// NPC Happiness, more happier more efficient on the job.
    /// </summary>
    public float Happiness { get; protected set; }
    /// <summary>
    /// NPC Job, children will transform in studendt if a chuch is avaliable and have 1 professor.
    /// </summary>
    public Job Profession;
    /// <summary>
    /// Citzen Skills;
    /// </summary>
    public List<Skill> Skills;
    /// <summary>
    /// NPC House.
    /// </summary>
    public GameObject NpcHouse;
    /// <summary>
    /// NPC job Location, children doesn't work and student will "work" on the chuch to learn more things. (you'll be able to "Graduate" the children).
    /// </summary>
    public GameObject JobLocation;
    /// <summary>
    /// Corroutine is running.
    /// </summary>
    private bool _cbRunning;
    /// <summary>
    /// Reference to this game object.
    /// </summary>
    private GameObject _mySelf;
    /// <summary>
    /// If the citzen is working.
    /// </summary>
    private bool _isWorking;
    /// <summary>
    /// If is time to work.
    /// </summary>
    private bool _workingTime;
    /// <summary>
    /// Method to initialize a random NPC;
    /// </summary>
    public void Init(System.Random rnd)
    {
        NpcGenere = (Genere) Random.Range(0, 2);
        Age = Random.Range(20, 25);
        InitializeSkills();
        UpdateJob("");
        var namegen = new PersonNameGenerator(rnd);
        switch (NpcGenere)
        {
            case Genere.Male:
                Name = namegen.GenerateRandomMaleFirstAndLastName();
                break;
            case Genere.Female:
                Name = namegen.GenerateRandomFemaleFirstAndLastName();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        Saturation = 100;
        Happiness = 60;
        _mySelf = gameObject;
        StartCoroutine("SearchHouse");
        StartCoroutine("SaturationController");
        HappyBirthday();
    }

    /// <summary>
    /// Initialize citzen skills.
    /// </summary>
    private void InitializeSkills()
    {
        foreach (var dataJob in GameController.Instance.GameData.Jobs)
        {
            var temp = new Skill();
            if (dataJob.Female && NpcGenere == Genere.Female)
            {
                temp.SkillName = dataJob.JobName;
                temp.Efficiency = Random.Range(1, 100)/1000f;
                Skills.Add(temp);
            }
            if (dataJob.Male && NpcGenere == Genere.Male)
            {
                temp.SkillName = dataJob.JobName;
                temp.Efficiency = Random.Range(1, 100)/1000f;
                Skills.Add(temp);
            }
        }
        var sortedList = Skills.OrderByDescending(a => a.Efficiency).ToList();
        Skill tempToRemove;
        if (Age > 18)
        {
            tempToRemove = sortedList.Find(a => a.SkillName.Contains("Child"));
            sortedList.Remove(tempToRemove);
        }
        tempToRemove = sortedList.Find(a => a.SkillName.Contains("Nomad"));
        sortedList.Remove(tempToRemove);

        Skills = sortedList;
    }

    public void UpdateJob(string jobName)
    {
        if (Age < 18)
        {
            Profession = GameController.Instance.GameData.Jobs.Find(a => a.JobName.Contains("Child"));
        }
        else if( jobName == "")
        {
            Profession = GameController.Instance.GameData.Jobs.Find(a => a.JobName.Contains(Skills[0].SkillName));
        }
        else
        {
            Profession = GameController.Instance.GameData.Jobs.Find(a => a.JobName.Contains(jobName));
        }
    }
    /// <summary>
    /// Each year the age will increment (O RLY?!) and the death chance will adjust automaticaly.
    /// </summary>
    public void HappyBirthday()
    {
        Age++;
        CalculateDeathChance();
        if (Age > 18)
        {
            var tempChildToRemove = Skills.Find(a => a.SkillName.Contains("Child"));
            if (tempChildToRemove != null)
            {
                Skills.Remove(tempChildToRemove);
            }
            UpdateJob("");
        }
        if (Age == 25)
        {
            NpcHouse = null;
            StartCoroutine("SearchHouse");
        }
    }

    private void CalculateDeathChance()
    {
        var x = 0f;
        if (Age > 20)
        {
            x = 0.4f;
        }
        else if (Age >= 20 && Age < 60)
        {
            x = 0.5f;
        }
        else if (Age >= 60)
        {
            x = 0.8f;
        }
        DeathChance = 0.01f * Mathf.Pow(1, 2) + Age * x;
    }

    private void Update()
    {
        if (JobLocation != null)
        {
            _workingTime = Ultility.Btween(GameController.Instance.City.Time.Hour, JobLocation.GetComponent<GenericJobBuilding>().HoursToWork[0], JobLocation.GetComponent<GenericJobBuilding>().HoursToWork[1], true);
        }
        if (!_isWorking && _workingTime)
        {
            StartCoroutine("WorkMachine");
        }
    }
    /// <summary>
    /// Controls the saturation decreasing and increasing.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SaturationController()
    {
        var going = false;
        while (true)
        {
            Saturation -= _isWorking ? 15 * GameController.Instance.City.Time.Speed : 5 * GameController.Instance.City.Time.Speed;

            if (Saturation <= 25)
            {
                Debug.Log("Fome indo comer");
                yield return new WaitForSeconds(1);
                if (NpcHouse != null)
                {
                    if (Vector3.Distance(transform.position, NpcHouse.transform.position) < 1)
                    {
                        if (GameController.Instance.City.CityResources.Food > 1)
                        {
                            GameController.Instance.City.CityResources.Food -= 1;
                            Saturation = 100;
                        }
                        yield return new WaitForSeconds(1);
                    }
                }
                else if( Saturation <= 0)
                {
                    Saturation = 0;
                    DeathChance += DeathChance/2+5;
                    if (DeathChance >= 100)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            yield return new WaitForSeconds(5);
        }
    }

    /// <summary>
    /// Search if have an avaliable house to live.
    /// </summary>
    private IEnumerator SearchHouse()
    {
        if (_cbRunning)
        {
            yield break;
        }
        _cbRunning = true;
        while (NpcHouse == null)
        {
            if (GameController.Instance.CityBuildings.Any())
            {
                var avaliableBuildings = GameObject.FindGameObjectsWithTag("Building");
                if (avaliableBuildings.Any())
                {
                    foreach (var building in avaliableBuildings)
                    {
                        if (building.GetComponent<GenericBuilding>().Type == TypeBuilding.House)
                        {
                            if (building.GetComponent<House>().Habitants.Count < building.GetComponent<GenericBuilding>().MaxCitzenInside)
                            {
                                var test = building.GetComponent<House>().RegisterPeopleInHouse(_mySelf);
                                if (test == HouseEventsHandler.Sucess)
                                {
                                    NpcHouse = building;
                                    break;
                                }
                            }
                        }
                    }
                    yield return new WaitForSeconds(5);
                }
                else
                {
                    Debug.Log(Name + " homeless trying again in 5 seconds.");
                    yield return new WaitForSeconds(5);
                }
            }
            else
            {
                Debug.Log("Dont have any houses constructed");
                yield return new WaitForSeconds(10);
            }
        }
    }
    /// <summary>
    /// Simple state machine to move the citzen to the job location or to the house.
    /// </summary>
    private IEnumerator WorkMachine()
    {
        bool going;
        if (JobLocation != null)
        {
            going = true;
            _isWorking = true;
            while (_workingTime)
            {
                if (going && Saturation > 20)
                {
                    transform.position = Vector3.MoveTowards(transform.position, JobLocation.transform.position,
                        Profession.Speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, JobLocation.transform.position) < 1)
                    {
                        JobLocation.GetComponent<GenericJobBuilding>().AssignWorker(_mySelf);
                        going = false;
                        yield return new WaitForSeconds(5);
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, NpcHouse.transform.position, Profession.Speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, NpcHouse.transform.position) < 1)
                    {
                        yield return new WaitForSeconds(1);
                    }
                }
                yield return new WaitForSeconds(1);
            }
            JobLocation.GetComponent<GenericJobBuilding>().AssignWorker(_mySelf);
            _isWorking = false;
        }
        yield return new WaitForSeconds(1);
        if (NpcHouse != null)
        {
            going = true;
            while (going)
            {
                transform.position = Vector3.MoveTowards(transform.position, NpcHouse.transform.position, Profession.Speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, NpcHouse.transform.position) < 1)
                {
                    going = false;
                    yield return new WaitForSeconds(1);
                }
            }
        }
    }

}
