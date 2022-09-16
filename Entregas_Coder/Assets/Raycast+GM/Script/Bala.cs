using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bala : MonoBehaviour
{

    [SerializeField] float DistanciaDetectar;
    [SerializeField] float speed;
    [SerializeField] ParticleSystem Boom;
    [SerializeField] ParticleSystem BoomMal;

    public float Speed { get => speed; set => speed = value; }

    public static event Action SumarScoreBala;
    public static event Action RestarScoreBala;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        Mov();
        DetectarEInteractuar();
    }
    private void OnDrawGizmos()
    {
        float MaxDist = DistanciaDetectar;
        RaycastHit hit;

        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, Quaternion.identity, MaxDist);
        if (isHit)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * MaxDist);
        }

    }
    void DetectarEInteractuar()
    {
        float MaxDist = DistanciaDetectar;
        RaycastHit hit;

        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale, transform.forward, out hit, Quaternion.identity, MaxDist);
        if (isHit)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                if (hit.transform.GetComponent<EnemyScript>().Human1)
                {
                    RestarScoreBala?.Invoke();
                    Instantiate(BoomMal, transform.position, Quaternion.identity);
                }
                if (!hit.transform.GetComponent<EnemyScript>().Human1)
                {
                    SumarScoreBala?.Invoke();
                    Instantiate(Boom, transform.position, Quaternion.identity);
                }

                Destroy(hit.transform.gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void Mov()
    {
        transform.Translate(transform.forward * Speed * Time.deltaTime);
    }
}
