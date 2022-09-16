using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float DistanciaDetectar;
    [Range(0,30)]
    [SerializeField] float Speed;
    [SerializeField] CharacterController CC;
    [SerializeField] SkinnedMeshRenderer MR;

    [SerializeField] Material Piel1;
    [SerializeField] Material Piel2;

    public float Speed1 { get => Speed; set => Speed = value; }
    public bool Human1 { get => Human; set => Human = value; }

    [SerializeField] bool Human;


    public static event Action SumaScore;
    public static event Action restaVida;

    public bool Revelado;
    // Start is called before the first frame update
    private void Awake()
    {
        PwUpMecanica.RecibeVeneno += Render1;
        PwUpMecanica.RecibeVision += Render2;
        ControlPP.VisionNormal += Render1;
    }

    void Start()
    {
        CC = GetComponent<CharacterController>();
        if (Revelado)
        {
            Render2();
        }
        if (!Revelado)
        {
            Render1();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MOV();

        DetectarEInteractuar();
    }

    public void Render1()
    {
        MR.material = Piel1;
    }
    public void Render2()
    {
        MR.material = Piel2;
    }
    private void OnDrawGizmos()
    {
        float MaxDist = DistanciaDetectar;
        RaycastHit hit;

        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, Quaternion.identity, MaxDist);
        if (isHit)
        {
            Gizmos.color = Color.red;
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

        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, Quaternion.identity, MaxDist);
        if (isHit)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Player"))
            {
                if (Human1)
                {
                    SumaScore?.Invoke();
                    Destroy(gameObject);
                }
                if (!Human1)
                {
                    restaVida?.Invoke();
                    Destroy(gameObject);
                }

            }
        }
    }
    private void MOV()
    {
        CC.Move(transform.forward * Speed1 * Time.deltaTime);
        CC.Move(transform.up * -Speed1 *4* Time.deltaTime); //gravedad
    }
    private void OnDisable()
    {
        PwUpMecanica.RecibeVeneno -= Render1;
        PwUpMecanica.RecibeVision -= Render2;
        ControlPP.VisionNormal -= Render2;
    }

}
