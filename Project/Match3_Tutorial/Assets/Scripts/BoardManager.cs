using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public GameObject		backBrick;
	
	public GameObject       melonFruit;
	public GameObject       coconutFruit;
	public GameObject       grapeFruit;
	public GameObject       bananaFruit;
	public GameObject       kiwiFruit;
	public GameObject       lemonFruit;


	public const int 		rows = 10;
	public const int 		columns = 10;


	private GameManager 	gameScript;

	private Transform			brickHolder;
	private	GameObject[][] 		brickArray = new GameObject[rows][];

	public float        spawnSpeed = 400;
	private float       spawnTimer;
	private bool        boardOk;







	void Awake()
	{
		gameScript = GetComponent<GameManager>();

		brickHolder = gameObject.GetComponent<Transform>();
		spawnTimer = 1;
		boardOk = false;
		SetupLevel();
		boardOk = true;
	}



	void 	SetupLevel()
	{
		int x;
		int y;

		y = 0;
		while (y < rows)
		{
			brickArray[y] = new GameObject[columns];
			x = 0;
			while (x < columns)
			{
				brickArray[y][x] = Instantiate(backBrick, new Vector3 (x + brickHolder.position.x, y + brickHolder.position.y, 0f), Quaternion.identity, brickHolder);
				x++;
			}
			y++;
		}


	}






	private bool 	CheckSelections(int x, int y, int xPos, int yPos)
	{
		int 	xVec = x - xPos;
		int 	yVec = y - yPos;
		bool 	matchSucces = false;
		bool 	posSucces = false;
		string 	newTile;

		newTile = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileInside();
		if (0 <= yPos - 1 && yPos + 1 < rows)
		{
			if (newTile == brickArray[yPos + 1][xPos].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[yPos - 1][xPos].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
		}
		if (0 <= yPos - 2)
		{
			if (newTile == brickArray[yPos - 1][xPos].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[yPos - 2][xPos].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
		}
		if (yPos + 2 < rows)
		{
			if (newTile == brickArray[yPos + 1][xPos].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[yPos + 2][xPos].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
		}



		if (0 <= xPos - 1 && xPos + 1 < columns)
		{
			if (newTile == brickArray[yPos][xPos + 1].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[yPos][xPos - 1].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
		}
		if (0 <= xPos - 2)
		{
			if (newTile == brickArray[yPos][xPos - 1].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[yPos][xPos - 2].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
		}
		if (xPos + 2 < columns)
		{
			if (newTile == brickArray[yPos][xPos + 1].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[yPos][xPos + 2].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
		}







		newTile = brickArray[yPos][xPos].GetComponent<BackGroundBrick>().GetTileInside();
		if (0 <= y - 1 && y + 1 < rows)
		{
			if (newTile == brickArray[y + 1][x].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[y - 1][x].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
			
		}
		if (0 <= y - 2)
		{
			if (newTile == brickArray[y - 1][x].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[y - 2][x].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
		}
		if (y + 2 < rows)
		{
			if (newTile == brickArray[y + 1][x].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[y + 2][x].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
		}



		if (0 <= x - 1 && x + 1 < columns)
		{
			if (newTile == brickArray[y][x + 1].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[y][x - 1].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
		}
		if (0 <= x - 2)
		{
			if (newTile == brickArray[y][x - 1].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[y][x - 2].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
		}
		if (x + 2 < columns)
		{
			if (newTile == brickArray[y][x + 1].GetComponent<BackGroundBrick>().GetTileInside())
				if (newTile == brickArray[y][x + 2].GetComponent<BackGroundBrick>().GetTileInside())
					matchSucces = true;
		}
		

		if ((-1 <= xVec && xVec <= 1 && yVec == 0) || (-1 <= yVec && yVec <= 1 && xVec == 0))
			posSucces = true;

		if (posSucces == true && matchSucces == true)
			return true;
		return false;
	}








	public void 	SwapSelectedPositions()
	{
		int 	x = 0;
		int 	y = 0;
		int 	xPos = 0;
		int 	yPos = 0;
		bool	foundFirst = false;
		bool 	foundSecond = false;
		string 	selectedTile = "None";

		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				if (foundFirst == false)
				{
					GameObject tempFruit = brickArray[y][x].GetComponent<BackGroundBrick>().GetFruitInside();
					if (tempFruit == null)
					{
						x++;
						continue;
					}
					if (tempFruit.GetComponent<Fruit>().isSelected)
					{
						xPos = x;
						yPos = y;
						selectedTile = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileInside();
						foundFirst = true;
					}
				}
				else
				{
					GameObject tempFruit = brickArray[y][x].GetComponent<BackGroundBrick>().GetFruitInside();
					if (tempFruit == null)
					{
						x++;
						continue;
					}
					if (tempFruit.GetComponent<Fruit>().isSelected)
					{
						foundSecond = true;
						break;
					}
				}
				x++;
			}
			if (foundSecond == true)
				break;
			y++;
		}

		if (foundSecond == true && CheckSelections(x, y, xPos, yPos))
		{
				ResetSelection();
				
				GameObject fruitFirst = brickArray[y][x].GetComponent<BackGroundBrick>().GetFruitInside();
				GameObject fruitSecond = brickArray[yPos][xPos].GetComponent<BackGroundBrick>().GetFruitInside();
				Vector3		firstPos = fruitFirst.transform.position;
				Vector3 	secondPos = fruitSecond.transform.position;
				

				Instantiate(fruitSecond, firstPos, Quaternion.identity, brickHolder);
				Instantiate(fruitFirst, secondPos, Quaternion.identity, brickHolder);

				Destroy(brickArray[y][x].GetComponent<BackGroundBrick>().GetFruitInside());
				Destroy(brickArray[yPos][xPos].GetComponent<BackGroundBrick>().GetFruitInside());

				return ;
		}

		ResetSelection();
		return ;
	}

	void 	ResetSelection()
	{
		int x;
		int y;

		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				GameObject 	tempFruit = brickArray[y][x].GetComponent<BackGroundBrick>().GetFruitInside();
				if (tempFruit != null)
				{
					tempFruit.GetComponent<Fruit>().isSelected = false;
					tempFruit.GetComponent<Fruit>().isChanging = true;
				}
				x++;
			}
			y++;
		}
		gameScript.fruitsClicked = 0;
	}


















	public void     SpawnFruit(Vector3  blockPosition)
	{
		GameObject  toInstantiate;
		GameObject 	ClonedFruit;
		int         rand;

		rand = Random.Range(0, 6);
		if (rand == 0)
			toInstantiate = melonFruit;
		else if (rand == 1)
			toInstantiate = grapeFruit;
		else if (rand == 2)
			toInstantiate = coconutFruit;
		else if (rand == 3)
			toInstantiate = kiwiFruit;
		else if (rand == 4)
			toInstantiate = lemonFruit;
		else
			toInstantiate = bananaFruit;

		ClonedFruit = Instantiate(toInstantiate, new Vector3 (blockPosition.x, blockPosition.y + 1, -5f), Quaternion.identity, brickHolder);
		ClonedFruit.GetComponent<Fruit>().gameScript = gameScript;

	}

	public int    SpawnLine()
	{
		int x;
		int y;
		int spawned;

		spawned = 0;
		y = rows - 1;
		x = 0;
		while (x < columns)
		{
			if (!brickArray[y][x].GetComponent<BackGroundBrick>().SomethingInside())
			{
				SpawnFruit(brickArray[y][x].transform.position);
				spawned++;
			}
			x++;
		}

		return spawned;
	}


	public bool 	CheckTurn()
	{
		int x;
		int y;

		int spaces = 0;
		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				if(!brickArray[y][x].GetComponent<BackGroundBrick>().SomethingInside())
					spaces++;
				x++;
			}
			y++;
		}

		if (spaces == 0)
		{
			return true;
		}
		return false;
	}














	public int 	RemoveHorizontalFruits()
	{
		int x = 0;
		int y = 0;
		int xPos = 0;
		int fruitsOnLine = 0;
		int fruitsFound = 0;
		string 	fruitNow;

		y = 0;
		while (y < rows)
		{
			x = 0;
			fruitNow = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileInside();
			while (x < columns)
			{
				xPos = x + 1;
				fruitsOnLine = 1;
				fruitNow = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileInside();
				if (fruitNow == "None")
				{
					x++;
					continue;
				}
				while (xPos < columns)
				{
					if (fruitNow == brickArray[y][xPos].GetComponent<BackGroundBrick>().GetTileInside())
						fruitsOnLine++;
					else
						break;
					xPos++;
				}
				if (fruitsOnLine >= 3)
				{
					fruitsFound += fruitsOnLine;
					xPos = x;
					while (fruitsOnLine > 0)
					{
						brickArray[y][xPos].GetComponent<BackGroundBrick>().GetFruitInside().GetComponent<Fruit>().shouldDie = true;
						fruitsOnLine--;
						xPos++;
					}
					gameScript.playersTurn = false;
					gameScript.stablePosition = 0;
					ResetSelection();
				}
				x++;
			}
			y++;
		}
		return fruitsFound;
	}

	public int		RemoveVerticalFruits()
	{
		int x = 0;
		int y = 0;
		int yPos = 0;
		int fruitsOnLine = 0;
		int fruitsFound = 0;
		string 	fruitNow;

		x = 0;
		while (x < columns)
		{
			y = 0;
			fruitNow = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileInside();
			while (y < rows)
			{
				yPos = y + 1;
				fruitsOnLine = 1;
				fruitNow = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileInside();
				if (fruitNow == "None")
				{
					y++;
					continue;
				}
				while (yPos < rows)
				{
					if (fruitNow == brickArray[yPos][x].GetComponent<BackGroundBrick>().GetTileInside())
						fruitsOnLine++;
					else
						break;
					yPos++;
				}
				if (fruitsOnLine >= 3)
				{
					fruitsFound += fruitsOnLine;
					yPos = y;
					while (fruitsOnLine > 0)
					{
						brickArray[yPos][x].GetComponent<BackGroundBrick>().GetFruitInside().GetComponent<Fruit>().shouldDie = true;
						fruitsOnLine--;
						yPos++;
					}
					gameScript.playersTurn = false;
					gameScript.stablePosition = 0;
					ResetSelection();
				}
				y++;
			}
			x++;
		}
		return fruitsFound;
	}


















	void Update()
	{
		if (spawnTimer > 2 && boardOk == true)
		{
			spawnTimer = 0;
			SpawnLine();
		}
		else
			spawnTimer += spawnSpeed * Time.deltaTime;
	}
}
