using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GameDataEditable))]
public class GameDataEditor : Editor {

    private GameDataEditable data;

    public override void OnInspectorGUI()
    {
        data = (GameDataEditable)target;
        #region CustomInspector Controller
        data.ShowJobCustomInspector = EditorGUILayout.Toggle("Job Inspector", data.ShowJobCustomInspector);
        data.ShowSeedsCustomInspector = EditorGUILayout.Toggle("Seed Inspector", data.ShowSeedsCustomInspector);
        data.ShowSeasonCustomInspector = EditorGUILayout.Toggle("Season Inspector", data.ShowSeasonCustomInspector);
        data.ShowOrchardCustomInspector = EditorGUILayout.Toggle("Tree Inspector", data.ShowOrchardCustomInspector);
        data.ShowAnimalsCustomInspector = EditorGUILayout.Toggle("Animals Inspector", data.ShowAnimalsCustomInspector);
        data.ShowBuildingsCustomInspector = EditorGUILayout.Toggle("Building Inspector", data.ShowBuildingsCustomInspector);
        #endregion
        #region Job Custom Inspector
        if (data.ShowJobCustomInspector)
        {
            if (GUILayout.Button("Add Job"))
            {
                data.Jobs.Add(new Job());
            }
            if (data.Jobs.Count >= 1)
            {
                if (GUILayout.Button("Remove Job"))
                {
                    data.Jobs.RemoveAt(data.Jobs.Count - 1);
                }
            }
            foreach (var job in data.Jobs)
            {
                EditorGUILayout.Space();
                EditorGUIUtility.labelWidth = 30f;
                job.JobName = EditorGUILayout.TextField("Job", job.JobName);
                EditorGUI.indentLevel += 1;
                EditorGUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 29f;
                EditorGUILayout.LabelField("Can have:", GUILayout.ExpandWidth(false));
                job.Female = EditorGUILayout.Toggle("F", job.Female, GUILayout.ExpandWidth(false));
                job.Male = EditorGUILayout.Toggle("M", job.Male, GUILayout.ExpandWidth(false));
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel -= 1;
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
        }
        #endregion
        #region Building Custom Inspector //TODO
        if (data.ShowBuildingsCustomInspector) {
            if (GUILayout.Button("New Building"))
            {
                data.Buildings.Add(null);
            }
            if (GUILayout.Button("Remove Buildin"))
            {
                data.Buildings.RemoveAt(data.Buildings.Count - 1);
            }
            foreach (var build in data.Buildings)
            {
          //TODO 
            }
        }
        #endregion
        #region Season Custom Inspector
        if (data.ShowSeasonCustomInspector)
        {
            foreach (var season in data.Seasons)
            {
                EditorGUILayout.Space(); EditorGUILayout.Space();
                season.SeasonName = EditorGUILayout.TextField(season.SeasonName);
                season.Days = EditorGUILayout.IntField("Season Days", season.Days);
                EditorGUILayout.LabelField("Min Temp:" + season.MinTemp.ToString("n1") + "|Max Temp:" + season.MaxTemp.ToString("n1"));
                EditorGUILayout.MinMaxSlider(ref season.MinTemp, ref season.MaxTemp, -20, 38);
                season.MinTemp = Mathf.Round(season.MinTemp * 100) / 100;
                season.MaxTemp = Mathf.Round(season.MaxTemp * 100) / 100;
                EditorGUILayout.Space();
            }
        }
        #endregion
        #region Tree Custom Inspector
        if (data.ShowOrchardCustomInspector)
        {
            if (GUILayout.Button("New Fruit Tree"))
            {
                data.TreeTypes.Add(new OrchardTrees());
            }
            if (data.TreeTypes.Count >= 1)
            {
                if (GUILayout.Button("Remove Fruit Tree"))
                {
                    data.TreeTypes.RemoveAt(data.TreeTypes.Count - 1);
                }
            }
            foreach (var tree in data.TreeTypes)
            {
                EditorGUILayout.Space();
                tree.TreeName = EditorGUILayout.TextField("Tree Name", tree.TreeName);
                tree.AgeToProduce = EditorGUILayout.IntField("Age to start production", tree.AgeToProduce);
                tree.TimeHarvest = EditorGUILayout.IntField("Time spent in days to harverst", tree.TimeHarvest);
                tree.AmmoutFood = EditorGUILayout.IntField("Food given after harverst", tree.AmmoutFood);

                EditorGUILayout.Space();
            }
        }

        #endregion
        #region Animal Custom Inspector //TODO
        if (data.ShowAnimalsCustomInspector)
        {
            if (GUILayout.Button("New Animal"))
            {
                data.Animals.Add(new Animal());
            }
            if (data.Animals.Count >= 1)
            {
                if (GUILayout.Button("Remove Fruit Tree"))
                {
                    data.Animals.RemoveAt(data.Animals.Count - 1);
                }
            }
            foreach (var animal in data.Animals)
            {
                EditorGUILayout.Space();
                animal.AnimalName = EditorGUILayout.TextField("Animal Name", animal.AnimalName);
                EditorGUILayout.Space();
            }
        }
        #endregion
        #region Seed Custom Inspector
        if (data.ShowSeedsCustomInspector)
        {
            if (GUILayout.Button("New Seed"))
            {
                data.Seeds.Add(new PlantationSeeds());
            }
            if (data.Seeds.Count >= 1)
            {
                if (GUILayout.Button("Remove last Seed"))
                {
                    data.Seeds.RemoveAt(data.Seeds.Count - 1);
                }
            }
            foreach (var seed in data.Seeds)
            {
                EditorGUILayout.Space();
                seed.SeedName = EditorGUILayout.TextField("Seed Name", seed.SeedName);
                seed.DaysToPlant = EditorGUILayout.IntSlider("Days spent on planting", seed.DaysToPlant,2,6);
                seed.DaysToGrow = EditorGUILayout.IntSlider("Days to grow adult form", seed.DaysToGrow,40,60);
                seed.DaysToHarvest = EditorGUILayout.IntSlider("Days spent harversting", seed.DaysToHarvest,8,24);
                seed.AmmountFood = EditorGUILayout.IntField("Ammout food given", seed.AmmountFood);
                seed.MinTemperatureResistence = EditorGUILayout.IntSlider("Min temp", seed.MinTemperatureResistence,-10,10);

                EditorGUILayout.Space();
            }
        }
        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();
        #endregion
        if (GUILayout.Button("Reoder All things"))
        {
            data.SortList();
        }
        if (GUILayout.Button("Save Things"))
        {
            EditorUtility.SetDirty(data);
        }   
    }
}
