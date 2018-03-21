using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleClass : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
        SingletonExample.Instance.TestFuncA();
        Debug.Log("moo|");   
	}

}
