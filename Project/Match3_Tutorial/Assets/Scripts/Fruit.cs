using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
	public 	bool 	isSelected = false;
	public 	bool 	isChanging;
	public 	bool 	shouldDie = false;

	public 	Sprite 	selectedSprite;
	public 	Sprite 	simpleSprite;

	private SpriteRenderer 		thisSprite;
	public 	GameManager 		gameScript;


	void OnMouseDown()
	{
		if (isChanging == false && gameScript.playersTurn)
		{
			isChanging = true;
			isSelected = true;
		}
	}


	void Start()
	{
		isChanging = true;

		thisSprite = GetComponent<SpriteRenderer>();
		// gameScript = gameManObj.GetComponent<GameManager>();
	}



	void Update()
	{
		if (isSelected)
		{
			if (isChanging)
			{
				isChanging = false;
				thisSprite.sprite = selectedSprite;
				gameScript.fruitsClicked++;
			}
		}

		if (!isSelected)
		{
			if (isChanging)
			{
				isChanging = false;
				thisSprite.sprite = simpleSprite;
			}
		}

		if (shouldDie)
		{
			// Debug.Log("Destroyed Fruit");
			if (gameObject != null)
				Destroy(gameObject);

		}
		
	}
}
