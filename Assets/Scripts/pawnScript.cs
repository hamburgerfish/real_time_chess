using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using piecesFake;
using grid;

public class pawnScript : pieceClass
{
    public GameObject piece;
    private pieceClass pawn;
    public gridScript gridRef;


    private Vector2 startPos = new Vector2(1,2);
    private static Vector2[] moveDir1 = new Vector2[2] {new Vector2(0,1), new Vector2(0,2)};
    private static Vector2[] moveDir2 = new Vector2[2] {new Vector2(1,0), new Vector2(2,0)};
    private static Vector2[] moveDir3 = new Vector2[2] {new Vector2(-1,0), new Vector2(-2,0)};
    private Vector2[][] availMoveRaw = new Vector2[3][] {moveDir1, moveDir2, moveDir3};


    // Start is called before the first frame update
    void Awake()
    {
        pawn = piece.GetComponent<pieceClass>();        
        currPos = gridRef.positionsDict[this.name];
    }

    void Update()
    {
        if (!gridRef.active && pawn.selected) pawn.unHighlight();
    }

    
    void OnMouseDown()
    {
        if (gridRef.active) pawn.act(currPos, availMoveRaw, gridRef.positionsDict, false);
    }
}
