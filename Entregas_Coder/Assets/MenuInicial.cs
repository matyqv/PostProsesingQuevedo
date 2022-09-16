using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject PanelInicio;
    public GameObject GameOver;
    // Start is called before the first frame update
    void Awake()
    {
        InterfaceRecursos.GameOver += mostrarRestartMenu;
    }
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InicialElJuego()
    {
        Time.timeScale = 1;
        PanelInicio.SetActive(false);
    }

    public void Resetear()
    {
        string Namescene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(Namescene);
    }
    public void mostrarRestartMenu()
    {
        GameOver.SetActive(true);
    }
    private void OnDisable()
    {
        InterfaceRecursos.GameOver -= mostrarRestartMenu;
    }
}
