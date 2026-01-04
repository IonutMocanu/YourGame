using APINet.Shared.Database.Models;
using APINet.Shared.DataTransferObjects;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;
    [SerializeField] public Button buyButton;
    [SerializeField] public Button playButton;

    [SerializeField] private TMPro.TextMeshProUGUI infoText;

    private int currentcar;
    public CarConfig currentCarConfig;
    //private CarConfig currentCarConfig;

    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.FindAnyObjectByType(typeof(GameManager)).GetComponent<GameManager>();
        SelectCar(0);
    }

    private void Start()
    {
        //buyButton = GameObject.FindGameObjectWithTag("buybutton").GetComponent<Button>();
        buyButton.onClick.AddListener(gameManager.BuyCar);
        Debug.Log("A inceput nivelul 2");
            
    }

    private void SelectCar(int index)
    {
        backButton.interactable = (index!=0);
        nextButton.interactable = (index!= transform.childCount-1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }

        currentCarConfig = transform.GetChild(index).GetComponent<CarScript>().carConfig;


        string s = $"Manufacturere: {currentCarConfig.manufacturer} Model: {currentCarConfig.model} Year: {currentCarConfig.year} Top Speed: {currentCarConfig.speed} Price: {currentCarConfig.price}$";
        infoText.text = s;

        if (gameManager == null) Debug.Log("e gol");

        //Debug.Log($"Index:{index}, bani: {currentCarConfig.price}, bani player: {gameManager.CurrentUser.Money}");
        if (AvemMasina(currentCarConfig))
        {
            playButton.interactable = true;
            buyButton.interactable = false;
            return;
        }
        else
        {
            playButton.interactable = false;
            buyButton.interactable = true;
        }
        if (gameManager.CurrentUser.Money <= currentCarConfig.price)
        {
            buyButton.interactable = false;
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.interactable = true;
        }
        //Debug.Log($"Avem masina: {AvemMasina(currentCarConfig)}");
    }

    public void ChangeCar(int index)
    {
        currentcar += index;
        SelectCar(currentcar);
    }

    //trebuie sa vedem daca avem masina deja in garaj

    private bool AvemMasina(CarConfig currentCarConfig)
    {
        bool avemMasina = false;

        List<CarRecord> userCar = gameManager.CurrentUser.Garage;

        foreach (CarRecord car in userCar)
        {
            if(car.Manufacturer == currentCarConfig.manufacturer && car.Model == currentCarConfig.model)
            {
                avemMasina = true;
            }
        }

        return avemMasina;
    }
}
