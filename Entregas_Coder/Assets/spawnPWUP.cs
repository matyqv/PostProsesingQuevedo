using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPWUP : MonoBehaviour
{
    [SerializeField] GameObject Botiquin;
    [SerializeField] GameObject Vision;
    [SerializeField] GameObject Veneno;


    [SerializeField] private float TR_Bo;
    [SerializeField] private float TR_Vi;
    [SerializeField] private float TR_Ve;



    // Start is called before the first frame update

    private void Awake()
    {
        InterfaceRecursos.GameOver += DestroyThis;
    }

    void Start()
    {
        TR_Bo = Random.Range(200, 1000);
        Invoke("I_bot", TR_Bo * Time.deltaTime);

        TR_Ve = Random.Range(200, 700);
        Invoke("I_vis", TR_Ve * Time.deltaTime);

        TR_Vi = Random.Range(200, 700);
        Invoke("I_ven", TR_Vi * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void I_bot()
    {               
        float X = Random.Range(-10, 10);
        Vector3 V = new Vector3(X, 0, 0);
        Instantiate(Botiquin, transform.position + V, transform.rotation);
      

        TR_Bo = Random.Range(200, 4000);
        Invoke("I_bot", TR_Bo * Time.deltaTime);
    }

    public void I_ven()
    {
        float X = Random.Range(-10, 10);
        Vector3 V = new Vector3(X, 0, 0);
        Instantiate(Veneno, transform.position + V, transform.rotation);


        TR_Ve = Random.Range(1000, 2000);
        Invoke("I_ven", TR_Ve * Time.deltaTime);
    }

    public void I_vis()
    {
        float X = Random.Range(-10, 10);
        Vector3 V = new Vector3(X, 0, 0);
        Instantiate(Vision, transform.position + V, transform.rotation);


        TR_Vi = Random.Range(1500, 2000);
        Invoke("I_vis", TR_Vi * Time.deltaTime);
    }

    private void OnDisable()
    {
        InterfaceRecursos.GameOver -= DestroyThis;
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
