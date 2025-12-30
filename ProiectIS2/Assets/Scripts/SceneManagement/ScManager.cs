using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScManager : MonoBehaviour
{
    UISceneManager uISceneManager;
    GameManager gameManager;
    //private void Start()
    //{
    //    if (SceneManager.GetActiveScene().buildIndex == 1)
    //    {
    //        this.GetComponent<GameManager>().moneyText = GameObject.FindGameObjectWithTag("money");
    //    }
    //}

    private void Awake()
    {
        uISceneManager = this.GetComponent<UISceneManager>();
        gameManager = this.GetComponent<GameManager>();
        if (uISceneManager == null) Debug.LogError("Lipsește UISceneManager!");
        if (gameManager == null) Debug.LogError("Lipsește GameManager!");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //Debug.Log("Salut");
            gameManager.moneyText = GameObject.FindGameObjectWithTag("money").GetComponent<TMPro.TextMeshProUGUI>();
            uISceneManager.ShowMoney();
            Debug.Log(gameManager.moneyText.text);

            gameManager.nameText = GameObject.FindGameObjectWithTag("name").GetComponent<TMPro.TextMeshProUGUI>();
            uISceneManager.ShownName();
            Debug.Log(gameManager.moneyText.text);
        }
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

}
