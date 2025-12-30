using UnityEngine;

public class CarScript : MonoBehaviour
{
    [SerializeField] public CarConfig carConfig;
    //[SerializeField] private Vector3 finalPosition;

    private Vector3 initialPosition;
    private void Awake()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, transform.parent.position, 0.1f);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,180,0),0.1f);
    }

    private void OnDisable()
    {
        transform.position = initialPosition;
    }
}
