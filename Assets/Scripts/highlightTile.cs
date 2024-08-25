using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using pieces;

public class highlightTile : MonoBehaviour
{
    public GameObject currPiece;
    private piece pieceRef;


    // Start is called before the first frame update
    void Awake()
    {
        pieceRef = currPiece.GetComponent<piece>();
    }

    void OnMouseDown()
    {
        Debug.Log("clicked");
    }

}
