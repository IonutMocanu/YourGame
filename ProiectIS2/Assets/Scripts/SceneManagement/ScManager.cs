using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScManager : MonoBehaviour
{
    //public Button buyButton;
    private UISceneManager uISceneManager;
    private GameManager gameManager;
    //private CarManager _carManager;
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
            //_carManager = GameObject.FindGameObjectWithTag("carmanager").GetComponent<CarManager>();



            //Debug.Log("Salut");
            gameManager.moneyText = GameObject.FindGameObjectWithTag("money").GetComponent<TMPro.TextMeshProUGUI>();
            uISceneManager.ShowMoney(gameManager.CurrentUser.Money);
            //Debug.Log(gameManager.moneyText.text);

            gameManager.nameText = GameObject.FindGameObjectWithTag("name").GetComponent<TMPro.TextMeshProUGUI>();
            uISceneManager.ShownName();
            //Debug.Log(gameManager.moneyText.text);
        }
    }

    //private void Update()
    //{

    //}

    public void ApplicationQuit()
    {
        Application.Quit();
    }

}
