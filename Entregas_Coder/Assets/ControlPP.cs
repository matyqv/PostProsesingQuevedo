using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System;

public class ControlPP : MonoBehaviour
{
    public Bloom bloom;
    [SerializeField] PostProcessVolume Ppv;

    [SerializeField]PostProcessProfile Vision;
    [SerializeField]PostProcessProfile Veneno;
    [SerializeField]PostProcessProfile Normal;

    [SerializeField] float Count_Ve;
    [SerializeField] float Count_Vi;

    [SerializeField] Color VHit;
    [SerializeField] Color Vnulo;
    [SerializeField] Color Vvisor;
    [SerializeField] Color Venvenenado;

    [SerializeField] Color ColorActual;


    public static event Action VisionNormal;
    // Start is called before the first frame update
    private void Awake()
    {
        PwUpMecanica.RecibeVeneno += Envenenado;
        PwUpMecanica.RecibeVision+= VisionNocturna;
        EnemyScript.restaVida += ColorearAtaqueTrigger;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Count_Ve > -1 || Count_Vi > -1)
        {
            Count_Vi -= 20 * Time.deltaTime;
            Count_Ve -= 10 * Time.deltaTime;
        }

        if (Count_Ve < 3 && Count_Vi < 3 )
        { VisionNula(); }
    }

    void Envenenado()
    {
        Count_Ve = 100;
        Ppv.profile = Veneno;
        ColorActual = Venvenenado;
    }
    void VisionNocturna()
    {
        Count_Vi = 100;
        Ppv.profile = Vision;
        ColorActual =Vvisor;
    }
        void VisionNula()
    {
        Ppv.profile = Normal;
        VisionNormal?.Invoke();
        ColorActual = Vnulo;
    }
    void ondamageFilter()
    {
        ColorGrading ColorFX;

        if (Ppv.profile.TryGetSettings(out ColorFX))
        {
            ColorFX.colorFilter.value = Color.red;
        }
    }


    private void OnDisable()
    {
        PwUpMecanica.RecibeVeneno -= Envenenado;
        PwUpMecanica.RecibeVision -= VisionNocturna;
        EnemyScript.restaVida -= ColorearAtaqueTrigger;
    }

    void ColorearAtaqueTrigger()
    {
        StartCoroutine(ColorearAtaque());
    }
    IEnumerator ColorearAtaque()
    {
        ColorGrading ColorFX;
        Ppv.profile.TryGetSettings(out ColorFX);

        Color ColorDirection = VHit - ColorFX.colorFilter.value;
        for (int i=0 ; i < 12; i++)
        {
            ColorFX.colorFilter.value += ColorDirection / 12;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        VolverAlColor();
    }

    void VolverAlColor()
    {
        ColorGrading ColorFX;
        Ppv.profile.TryGetSettings(out ColorFX);
        ColorFX.colorFilter.value = ColorActual;
    }

}
