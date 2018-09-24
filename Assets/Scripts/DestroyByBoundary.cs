using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	//Este método destruye el objeto al salir del Boundary
	void OnTriggerExit (Collider other) 
	{
		Destroy(other.gameObject);
	}
}
