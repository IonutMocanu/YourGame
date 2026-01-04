using UnityEngine;
using UnityEngine.UI;

public class UISceneManager : MonoBehaviour
{


    private GameManager gameManager;

    private void Awake()
    {
        gameManager = this.gameObject.GetComponent<GameManager>();
    }

    private string GetMoney()
    {
        if(gameManager.CurrentUser is null)
        {
            Debug.Log("Nu avem utilizator");
            return null;
        }
        gameManager.GetUser(gameManager.CurrentUser.Email);
        return gameManager.CurrentUser.Money.ToString();
    }

    public void ShowMoney(int? money)
    {
        if(GetMoney() is null)
        {
            return;
        }
        string defaultMoney = "Money: ";
        gameManager.moneyText.text = defaultMoney + money.ToString();
    }

    private string GetPlayerName()
    {
        if (gameManager.CurrentUser is null)
        {
            Debug.Log("Nu avem utilizator");
            return null;
        }
        return gameManager.CurrentUser.FirstName.ToString() + " " + gameManager.CurrentUser.LastName.ToString();
    }

    public void ShownName()
    {
        if (GetPlayerName() is null)
        {
            return;
        }
        string defaultName = "Player: ";
        gameManager.nameText.text = defaultName + GetPlayerName();
    }
}
