using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinoController : MonoBehaviour {
    

    public bool isSelected = true;

    [Header("Movimentação")]
    public float limiteY = 2f;
    public float velocidade = 2f;
    private Vector3 inicialPosition;
    //Chute
    private bool chutando = false;
    private float velocidadeChute = 30000f;

    void Awake() {
        inicialPosition = transform.position;
    }
    
    void Update()  {
        if (isSelected) {
            MoverPino();
            Chutar();
        }
    }

    /// <summary>Mover Pino</summary>
    private void MoverPino() {
        transform.Translate(Vector2.up * Input.GetAxis("Vertical") * velocidade * Time.deltaTime);
        if (transform.position.y > inicialPosition.y+limiteY)  transform.position = new Vector3(transform.position.x, inicialPosition.y+limiteY, transform.position.z);
        if (transform.position.y < inicialPosition.y-limiteY)  transform.position = new Vector3(transform.position.x, inicialPosition.y-limiteY, transform.position.z);
    }

    /// <summary>Realiza o chute</summary>
    private void Chutar() {
        if (Input.GetButton("Chutar")) {
            chutando = true;
            Vector3 rotationAtual = UnityEditor.TransformUtils.GetInspectorRotation(transform);
            if (Vector3.Distance(rotationAtual, new Vector3(0, -90, 0)) > 0.1f) {
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, -90, 0), 0.5f * Time.deltaTime);
                if (rotationAtual.y < -90f) transform.eulerAngles = new Vector3(0, -90, 0);
                Debug.Log(rotationAtual.y);
            }
        } else {
            if (chutando) {
                transform.Rotate(new Vector3(0, 360, 0), velocidadeChute * Time.deltaTime);
                Invoke("PararChute", 2f);
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    private void PararChute() {
        chutando = false;
    }
}
