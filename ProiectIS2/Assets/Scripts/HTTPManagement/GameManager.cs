using UnityEngine;
using APINet;
using _Scripts;
using APINet.Shared.Database.Models;
using APINet.Shared.DataTransferObjects;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public User CurrentUser { get; private set; }

    [SerializeField]
    private UserAddRecord _record = new UserAddRecord();

    private const string BaseUrl = "https://localhost:7106/api";

    public TMPro.TextMeshProUGUI moneyText;
    public TMPro.TextMeshProUGUI nameText;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    async void Start()
    {
        //test
        try
        {
            var car = await HttpClient.Get<Car>($"{BaseUrl}/Car/2");
            if (car != null) Debug.Log($"Marca {car.Manufacturer}");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public async void GetUser(string email)
    {
        try
        {
            string url = $"{BaseUrl}/User/{email}";
            CurrentUser = await HttpClient.Get<User>(url);

            if (CurrentUser != null)
            {
                Debug.Log($"ID: {CurrentUser.Id}");
                Debug.Log($"First name: {CurrentUser.FirstName}");
                Debug.Log($"Last name: {CurrentUser.LastName}");
            }
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
}