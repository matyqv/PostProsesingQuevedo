using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PwUpMecanica : MonoBehaviour
{

    [SerializeField] float V_rot;
    [SerializeField] string Tipo;

    public static event Action RecibeVeneno;
    public static event Action RecibeVision;
    public static event Action RecibeBotiquin;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destruir", 7);
    }

    // Update is called once per frame
    void Update()
    {
        Rotar();
    }

    void Rotar()
    {
        transform.Rotate(0, V_rot * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool EsPlayer = other.CompareTag("Playerr");

        if (EsPlayer)
        {
            if (Tipo == "BO") { RecibeBotiquin?.Invoke(); }
            if (Tipo == "VE") { RecibeVeneno?.Invoke(); }
            if (Tipo == "VI") { RecibeVision?.Invoke(); }
            Destroy(gameObject);
        }
    }

    void destruir()
    { Destroy(gameObject); }
}
