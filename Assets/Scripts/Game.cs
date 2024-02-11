using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    [SerializeField] InputReader input;
    [SerializeField] Player player;
    public static InputReader InputReader => Instance.input;
    public static Player Player => Instance.player;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
}
