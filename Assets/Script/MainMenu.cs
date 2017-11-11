// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainMenu.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject SeedField;

    public void StartGame()
    {
        if (SeedField.GetComponent<Text>().text == "")
        {
            GameController.Instance.seed = int.Parse(Ultility.GetRandomString(new System.Random(), 64));
        }
        else
        {
            GameController.Instance.seed = int.Parse(SeedField.GetComponent<Text>().text);
        }
        SceneManager.LoadScene("Teste");
    }



}
