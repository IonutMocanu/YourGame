using UnityEngine;

public class UISceneManager : MonoBehaviour
{
    GameManager gameManager;

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
        return gameManager.CurrentUser.Money.ToString();
    }

    public void ShowMoney()
    {
        if(GetMoney() is null)
        {
            return;
        }
        string defaultMoney = "Money: ";
        gameManager.moneyText.text = defaultMoney + GetMoney();
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
