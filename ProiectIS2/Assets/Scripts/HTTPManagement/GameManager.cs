using UnityEngine;
using APINet;
using _Scripts;
using APINet.Shared.Database.Models;
using APINet.Shared.DataTransferObjects;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private UISceneManager _uiSceneManager;
    private ScManager _scManager;

    public UserRecord CurrentUser { get; set; }

    [SerializeField]
    private UserAddRecord _record = new UserAddRecord();

    private const string BaseUrl = "http://localhost:7106/api";

    public TMPro.TextMeshProUGUI moneyText;
    public TMPro.TextMeshProUGUI nameText;

    private CarManager _carManager;

    private bool isProcessingTransaction = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //async - pt test
    void Start()
    {
        //test
        //try
        //{
        //    var car = await HttpClient.Get<Car>($"{BaseUrl}/Car/2");
        //    if (car != null) Debug.Log($"Marca {car.Manufacturer}");
        //}
        //catch (Exception ex)
        //{
        //    Debug.LogError(ex.Message);
        //}

        _uiSceneManager = this.GetComponent<UISceneManager>();
        _scManager = this.GetComponent<ScManager>();
    }

    public async void GetUser(string email)
    {
        try
        {
            string url = $"{BaseUrl}/User/{email}";
            CurrentUser = await HttpClient.Get<UserRecord>(url);

            //if (CurrentUser != null)
            //{
            //    Debug.Log($"ID: {CurrentUser.Id}");
            //    Debug.Log($"First name: {CurrentUser.FirstName}");
            //    Debug.Log($"Last name: {CurrentUser.LastName}");
            //}
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public void SetInputFirstName(string name)
    {
        _record.FirstName = name;
    }

    public void SetInputLastName(string name)
    {
        _record.LastName = name;
    }

    public void SetInputEmail(string name)
    {
        _record.Email = name;
    }

    public async void AddUser()
    {
        try
        {
            string url = $"{BaseUrl}/User";

            //test
            Debug.Log($"Nume: {_record.FirstName}");

            var createdUser = await HttpClient.Post<User>(url, _record);

            //if (createdUser != null)
            //{
            //    url = $"{BaseUrl}/User/{createdUser.Email}";
            //    CurrentUser = await HttpClient.Get<User>(url);
            //    Debug.Log($"Id actual: {CurrentUser.Id}");
            //}
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public async void BuyCar()
    {
        if (isProcessingTransaction) return;

        isProcessingTransaction = true;
        _carManager = GameObject.FindGameObjectWithTag("carmanager").GetComponent<CarManager>();
        _carManager.buyButton.interactable = false;

        try
        {
            Debug.Log("test, merge butonul");
            //carmanager
            //https://localhost:7106/api/Car/buy/3 - exemplu

            string url = $"{BaseUrl}/Car/buy/{CurrentUser.Id}";

            if (CurrentUser.Money >= _carManager.currentCarConfig.price)
            {
                CarAddRecord record = new CarAddRecord();

                record.Manufacturer = _carManager.currentCarConfig.manufacturer;
                record.Model = _carManager.currentCarConfig.model;
                record.Year = _carManager.currentCarConfig.year;
                record.Speed = _carManager.currentCarConfig.speed;
                record.Price = _carManager.currentCarConfig.price;
                record.UserId = CurrentUser.Id;

                int? newAmountOfMoney = CurrentUser.Money - _carManager.currentCarConfig.price;

                UserMoneyUpdateRecord _userMoneyUpdateRecord = new UserMoneyUpdateRecord();
                _userMoneyUpdateRecord.Id = CurrentUser.Id;
                _userMoneyUpdateRecord.Money = newAmountOfMoney;

                CarAddRecord carAddRecord = await HttpClient.Post<CarAddRecord>(url, record);

                UserMoneyUpdateRecord addMoneyRecord = await HttpClient.Put<UserMoneyUpdateRecord>($"{BaseUrl}/User/add-money", _userMoneyUpdateRecord);

                CurrentUser.Money = addMoneyRecord.Money;

                _uiSceneManager.ShowMoney(addMoneyRecord.Money);

                _carManager.playButton.interactable = true;

                //Debug.Log(CurrentUser.Money);

                //if (newAmountOfMoney <= _carManager.currentCarConfig.price)
                //{
                //    _carManager.buyButton.interactable = false;
                //}
                //else
                //{
                //    _carManager.buyButton.interactable = true;
                //}
            }
            else
            {
                Debug.Log("Nu ai suficienti bani");
                //_scManager.buyButton.interactable = false;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            _carManager.buyButton.interactable = true;
        }
        finally
        {
            isProcessingTransaction = false;
        }
    }
}