using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundBrick : MonoBehaviour
{
	private string 		tileName;
	private GameObject 	fruit;

	public 	GameObject 	GetFruitInside()
	{
		return fruit;
	}

	public	string 	GetTileInside()
	{
		return tileName;
	}

	public 	bool 	SomethingInside()
	{
		if (tileName == "None")
			return false;
		return true;
	}

	private void 	OnTriggerStay2D(Collider2D other)
	{
		tileName = other.gameObject.tag;
		fruit = other.gameObject;
	}

	private void	OnTriggerExit2D(Collider2D other)
	{
		tileName = "None";
	}

	void	Awake()
	{
		tileName = "None";
	}    

    // Update is called once per frame
    void Update()
    {
        
    }
}
