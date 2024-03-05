using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_EndIndicator : MonoBehaviour
{
    [SerializeField] Image _image;

    public void UpdateEndIndicator(bool endReached)
    {
        if (endReached)
        {
            _image.color = Color.red;
        }
        else
        {
            _image.color = Color.green;
        }
    }
}
