using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class obstacle : MonoBehaviour
{
    public GameObject canvas;
    

    private void Start()
    {
        canvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("carslot"))
        {
            canvas.SetActive(true);
        }
    }
}
