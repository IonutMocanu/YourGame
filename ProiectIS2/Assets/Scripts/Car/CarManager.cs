using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;

    [SerializeField] private TMPro.TextMeshProUGUI infoText;

    private int currentcar;
    //private CarConfig currentCarConfig;

    private void Awake()
    {
        SelectCar(0);
    }

    private void SelectCar(int index)
    {
        backButton.interactable = (index!=0);
        nextButton.interactable = (index!= transform.childCount-1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }

        CarConfig currentCarConfig = transform.GetChild(index).GetComponent<CarScript>().carConfig;

        string s = $"Manufacturere: {currentCarConfig.manufacturer} Model: {currentCarConfig.model} Year: {currentCarConfig.year} Top Speed: {currentCarConfig.speed} Price: {currentCarConfig.price}$";
        infoText.text = s;
    }

    public void ChangeCar(int index)
    {
        currentcar += index;
        SelectCar(currentcar);
    }
}
