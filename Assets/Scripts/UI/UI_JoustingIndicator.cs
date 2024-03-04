using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_JoustingIndicator : MonoBehaviour
{
    [SerializeField] TMP_Text distance;
    [SerializeField] Image _image;

    public void UpdateDistanceIndicator(float distance)
    { 
        this.distance.text = distance.ToString("F0");
    }

    public void UpdateStrikingDistanceIndicator(bool inDistance)
    {
        if (inDistance)
        {
            _image.color = Color.green;
        }
        else
        {
            _image.color = Color.red;
        }
    }
}
