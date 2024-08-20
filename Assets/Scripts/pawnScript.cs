using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using pieces;
using grid;

public class pawnScript : pieceClass
{

    private List<Vector2> positionsList = new List<Vector2>();


    public GameObject piece;
    private pieceClass pawn;

    public GameObject gridObj;
    private gridScript gridRef;

    private Vector2 thisCurrPos = new Vector2(1,1);
    private static Vector2[] moveDir1 = new Vector2[2] {new Vector2(1,0), new Vector2(2,0)};
    private Vector2[][] availMoveRaw = new Vector2[1][] {moveDir1};

    // Start is called before the first frame update
    void Awake()
    {
        pawn = piece.GetComponent<pieceClass>();
        gridRef = gridObj.GetComponent<gridScript>();
        
        currPos = thisCurrPos;
        //pawn.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        pawn.checkMove(currPos, availMoveRaw, positionsList, false);
    }
}
