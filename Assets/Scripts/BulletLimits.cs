using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLimits : MonoBehaviour
{
    //Los limites de movimiento de la bala
    public float limitUp = 5.04f;//Limite arriba
    public float limitDown = -4.13f;//Limite abajo
    public float limitLeft = -9.51f;//Limite izquierda
    public float limitRight = 8.98f;//Limite derecha

    //Update is called once per frame
    void Update()
    {
        //Obtiene la posicion actual de la bala
        Vector2 position = transform.position;

        //Verificar si la bala sobrepasa los limites establecidos
        if (position.x > limitRight || position.x < limitLeft || position.y > limitUp || position.y < limitDown)
        {
            //Destruye la bala si se pasa de los limites establecidos
            Destroy(gameObject);
        }
    }
}