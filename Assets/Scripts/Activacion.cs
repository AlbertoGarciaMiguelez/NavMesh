using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activacion : MonoBehaviour
{
    public GameObject perseguidor1;
    public GameObject perseguidor2;
    public GameObject perseguidor3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void whenButtonClicked1(){
        if(perseguidor1.activeSelf == true){
            perseguidor1.SetActive(false);
        }else{
            perseguidor1.SetActive(true);
        }
    }
    public void whenButtonClicked2(){
        if(perseguidor2.activeSelf == true){
            perseguidor2.SetActive(false);
        }else{
            perseguidor2.SetActive(true);
        }
    }
    public void whenButtonClicked3(){
        if(perseguidor3.activeSelf == true){
            perseguidor3.SetActive(false);
        }else{
            perseguidor3.SetActive(true);
        }
    }
}
