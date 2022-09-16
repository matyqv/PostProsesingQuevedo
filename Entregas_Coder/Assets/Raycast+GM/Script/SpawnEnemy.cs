using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [SerializeField]private GameObject EnemySpawn;
    [SerializeField]private GameObject HumanSpawn;
    [SerializeField] private float speed;
    [SerializeField] private float TiempoDeRepeticion;
    [SerializeField] bool Revelar;
    // Start is called before the first frame update
    void Start()
    {
        Render1();
        speed = 3;
        Invoke("CrearEnemigo", TiempoDeRepeticion * Time.deltaTime);
    }
    private void Awake()
    {
        PwUpMecanica.RecibeVeneno += Render1;
        PwUpMecanica.RecibeVision += Render2;
        ControlPP.VisionNormal += Render1;
        InterfaceRecursos.GameOver += DestroyThis;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CrearEnemigo()
    {
        speed+=0.4f;
        float SPD = Random.Range(speed/2, speed);
        EnemySpawn.GetComponent<EnemyScript>().Speed1 = SPD;
        EnemySpawn.GetComponent<EnemyScript>().Revelado = Revelar;
        float X = Random.Range(-4, 4);
        float Z = Random.Range(-1, 1);
        Vector3 V = new Vector3(X, 0, Z);
        float I= Random.Range(0,100);

        if (I > 50)
        {
            GameObject Spawmeado= Instantiate(EnemySpawn, transform.position + V, transform.rotation);        
        }
        if (I <= 50)
        {
            GameObject Spawmeado = Instantiate(HumanSpawn, transform.position + V, transform.rotation);
        }

        TiempoDeRepeticion = Random.Range(50, 250);
        Invoke("CrearEnemigo", TiempoDeRepeticion * Time.deltaTime);

    }

    void Render1()
    {
        Revelar = false;
    }
    void Render2()
    {
        Revelar = true;
    }
    private void OnDisable()
    {
        PwUpMecanica.RecibeVeneno -= Render1;
        PwUpMecanica.RecibeVision -= Render2;
        ControlPP.VisionNormal -= Render2;
        InterfaceRecursos.GameOver -= DestroyThis;
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
