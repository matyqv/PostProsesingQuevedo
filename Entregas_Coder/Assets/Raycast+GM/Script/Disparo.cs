using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{

    [SerializeField] float DistanciaDetectar;
    [SerializeField] GameObject Bala;
    [SerializeField] float speed;
    [SerializeField] bool CanShoot=true;
    [SerializeField] ParticleSystem Shoot;

    public KeyCode Disparar;
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Disparar)&& CanShoot)
        {
            Disparos();
        }
    }

    void Disparos()
    {
        Instantiate(Bala, transform.position, transform.rotation);
        CanShoot = false;
        Invoke("HabilitarDisparo", 20 * Time.deltaTime);
        Shoot.Play();
    }
    void HabilitarDisparo()
    {
        CanShoot = true;
    }
}
