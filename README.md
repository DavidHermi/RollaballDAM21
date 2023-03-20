# RollaballDAM21

# Unity rollaball

**Lo desglosaremos en dos partes el codigo y el proyecto de unity**


# Codigo

* Camara controller

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
```

**De este script cabria destacar sobre todo que los metodos estan hechos de tal manera que la main camara del proyecto va seguir al jugador**

* Player

```

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count ;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText ();

     
        winTextObject.SetActive(false);
        
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) 
    {
       
        if (other.gameObject.CompareTag ("PickUp"))
        {
            other.gameObject.SetActive (false);

         
            count = count + 1;

            SetCountText ();
        }
    }
    
    
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12) 
        {
            
            winTextObject.SetActive(true);
        }
    }
}
```

**Desglosando este metodo tendriamos unos cuantos metodos, el primero de ellos para iniciar el movimiento y ademas que salga el texto del score, el segundo seria para el movimiento con las variables del eje x y y, el tercero es un metodo para la velocidad del player, el cuarto es un trigger que le indica al player que cuando toca y absorba un collider, se sume uno en la puntuacion, y el ultimo es un metodo que le indica mediante un condicional que cuando llege a x puntuacion te indique que has ganado el juego**

* Rotator

```
using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    // Before rendering each frame..
    void Update () 
    {
        // Rotate the game object that this script is attached to by 15 in the X axis,
        // 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
        // rather than per frame.
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
    }
}
```

**Script simple que indica que los collider se mantengan en rotacion en el aire**

* Enemy

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private NavMeshAgent pathfinder;
    private Transform target;
    
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
    }
    void Update()
    {
        pathfinder.SetDestination(target.position);
    }
}
```
**Script para el enemigo que lo que hace es señalizar al player como un target, y mediante los dos metodos le indica al enemy que tiene que seguirlo**

* Puerta

```

namespace DefaultNamespace;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour {
    public Animator laPuerta ;

    private void OnTriggerEnter(Collider other)
    {

        lapuerta.play("abrir");
    }
    private void OnTriggerexit(Collider other)
    {

        lapuerta.play("salir");
    }
   
}
```
**Script que me diante dos triggers le indicamos que en caso de colision, con la puerta esta se mueva acorde al movimiento del player**

# Interfaz de unity

Ahora comenzaremos a desglosar el escenario hecho en unity no sin antes señalizar ciertas cosas y enumerar los principales componentes

** Lista de componentes principales de la escena**

![image](https://user-images.githubusercontent.com/91197896/226439528-e22150f0-0143-48c9-bb97-54c5d2006541.png)

**Materiales con los colores de cada componente**

![image](https://user-images.githubusercontent.com/91197896/226440205-4b086910-7960-432f-bd16-519b4ceb6689.png)

**Carpeta de scripts**


![image](https://user-images.githubusercontent.com/91197896/226440488-0b569529-cb8d-478b-9a38-eb40929e1b54.png)




**Assets**


![image](https://user-images.githubusercontent.com/91197896/226440607-ae2d0a79-5c25-4ae6-89d8-7e303f39f53c.png)


Lista de componentes a desglosar


1. Camara
2. Player
3. Colliders
4. Enemy
5. Puerta


* Camara. la camara esta desglosada de tal manera que esta orientada para que siga al jugador en todo momento y su vista esta enfocada de lleno al terreno del circuito, sin ella no podriamos ver como se desarolla la aplicacion, puesto que no veriamos nada.

**Componentes**

![image](https://user-images.githubusercontent.com/91197896/226441751-7b1df478-fc9b-4c95-baa3-0796a7dd7d0c.png)

* Player, el player consiste en la bola que vamos manejar con los controles del juego (las flechas) esta bola de color azul esta scripteada de tal manera que ca cumulando puntuacion cuando consigue recolectar los cuadrados en rotacion en el aire, y es perseguida por el enemy.


**Componenentes**


![image](https://user-images.githubusercontent.com/91197896/226442769-732c37b8-bf6d-4f9d-83e6-65723fe9dadf.png)



* Colliders, los colliders son objetos en movimiento que el jugador el player va a coleccionar de tal manera que cuando llege a x cantidad de colliders ganaras la partida, estos desapareceran en cuanto el player los pase por encima.

**Componentes**

![image](https://user-images.githubusercontent.com/91197896/226443421-b244608e-0468-4fe6-a669-f1186b9b2826.png)


* Enemy. el enemigo es una capsula de color rojo, su funcion es tener por target al player de tal manera que si lo toca se acabo la partida, este perseguira hasta la extenuacion al player, y tu objetivo sera despistarlo.


**Componentes**

![image](https://user-images.githubusercontent.com/91197896/226443886-3a891f52-20e0-4e85-b23e-0374aa0ff7b8.png)


* Puerta. la puerta el ultimo elemento importante se hizo mediante el animator de unity el cual, grabara dos secuencias una de abrir y otra de cerrar y mediante un Script, el player interectuara directamente con la puerta mediante el contacto y se aaccionaran los movimientos en un ratio de segundos

**Componentes**

![image](https://user-images.githubusercontent.com/91197896/226444855-fe2b6277-4a69-42d4-9ff1-524d0af90f75.png)








