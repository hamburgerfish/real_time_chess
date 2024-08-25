using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using pieces;


namespace grid
{
    public class gridScript : MonoBehaviour
    {

        public float _time;
        private float fixedDeltaTime;
        public bool active = true;
        public static float activeTime = 10.0f;
        public static float inactiveTime = 5.0f;

        private GameObject currPiece;
        private piece pieceRef;

        public List<string> immuneList = new List<string>();
        public Dictionary<string, Vector2> positionsDict = new Dictionary<string, Vector2>(){ 
            {"WP1", new Vector2(1,2)},
            {"WP2", new Vector2(2,2)},
            {"WP3", new Vector2(3,2)},
            {"WP4", new Vector2(4,2)},
            {"WP5", new Vector2(5,2)},
            {"WP6", new Vector2(6,2)},
            {"WP7", new Vector2(7,2)},
            {"WP8", new Vector2(8,2)},
            {"WR1", new Vector2(1,1)},
            {"WR2", new Vector2(8,1)},
            {"WN1", new Vector2(2,1)},
            {"WN2", new Vector2(7,1)},
            {"WB1", new Vector2(3,1)},
            {"WB2", new Vector2(6,1)},
            {"WK1", new Vector2(5,1)},
            {"WQ1", new Vector2(4,1)},
            {"BP1", new Vector2(1,7)},
            {"BP2", new Vector2(2,7)},
            {"BP3", new Vector2(3,7)},
            {"BP4", new Vector2(4,7)},
            {"BP5", new Vector2(5,7)},
            {"BP6", new Vector2(6,7)},
            {"BP7", new Vector2(7,7)},
            {"BP8", new Vector2(8,7)},
            {"BR1", new Vector2(1,8)},
            {"BR2", new Vector2(8,8)},
            {"BN1", new Vector2(2,8)},
            {"BN2", new Vector2(7,8)},
            {"BB1", new Vector2(3,8)},
            {"BB2", new Vector2(6,8)},
            {"BK1", new Vector2(5,8)},
            {"BQ1", new Vector2(4,8)}
            };


        // Start is called before the first frame update
        void Awake()
        {
            this.fixedDeltaTime = Time.fixedDeltaTime;



            //piece = GameObject.Find("/Grid/Piece");
            //pieceRef = piece.GetComponent<pieceClass>();
            executeChanges();


        }

        // Update is called once per frame
        void Update()
        {
            _time += Time.deltaTime;
        }

        private float timelastActive = -(activeTime + inactiveTime);
        void FixedUpdate()
        {
            if (_time - timelastActive >= activeTime + inactiveTime || active)
            {
                if (!active)
                {
                    timelastActive = _time;
                    active = true;
                }
                executeChanges();
                if (_time - timelastActive >= activeTime) active = false;
            }
        }



        private string currTag;
        private int currCompX;
        private int currCompY;
        private void highlight(List<Vector2> availMoveCurr) //old
        {
            foreach (Vector2 availMoveInst in availMoveCurr)
            {
                currCompX = (int)Mathf.Round(availMoveInst.x);
                currCompY = (int)Mathf.Round(availMoveInst.y);
                currTag = currCompX.ToString() + currCompY.ToString();
            }
        }

        private void getChanges()
        {
            active = true;
        }

        private string[] removeArray;
        private void executeChanges()
        {
            removeArray = positionsDict.GroupBy(i => i.Value).Where(x => x.Count() > 1).Select(x => positionsDict.FirstOrDefault(i => i.Value == x.Key).Key).ToArray();
            foreach(string pieceName in removeArray)
            {
                if (!immuneList.Contains(pieceName))
                {
                    currPiece = GameObject.Find("/Grid/" + pieceName);
                    pieceRef = currPiece.GetComponent<piece>();
                }
            }
        }

    }
}

