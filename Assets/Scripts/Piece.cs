using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using grid;

namespace pieces
{
        public class piece : MonoBehaviour
    {
        public GameObject highlight;
        public GameObject selector;
        private SpriteRenderer selectorSpriteRenderer;

        public bool canMove = false;

        public Vector2 currPos;

        public float scale = 0.16f;
        public Vector2 axisOffset = new Vector2(0.08f, 0.08f);

        public Vector2[] availPos;

        public bool white;

        public bool selected = false;
        public Vector2 destination;


        public gridScript gridRef;
        private Vector2 startPos = new Vector2(1,2);
        public static Vector2[] moveDir1 = new Vector2[2] {new Vector2(0,1), new Vector2(0,2)};
        private static Vector2[] moveDir2 = new Vector2[2] {new Vector2(1,0), new Vector2(2,0)};
        private static Vector2[] moveDir3 = new Vector2[2] {new Vector2(-1,0), new Vector2(-2,0)};
        private Vector2[][] availMoveRaw = new Vector2[3][] {moveDir1, moveDir2, moveDir3};



        

        // Start is called before the first frame update
        void Start()
        {
            selector = this.transform.Find("Selector").gameObject;
            selectorSpriteRenderer = selector.GetComponent<SpriteRenderer>();
        }



        // Update is called once per frame
        void Update()
        {
            if (!gridRef.active && selected) unHighlight();
        }

        void FixedUpdate()
        {

        }

        void OnMouseDown()
        {
            if (gridRef.active) act(currPos, availMoveRaw, gridRef.positionsDict, false);
        }


        public List<Vector2> availMoveCurr = new List<Vector2>();
        private Vector2 availMoveInstance;
        private bool breakFlag;
        public void checkMove(Vector2 currPos, Vector2[][] availMoveRaw, Dictionary<string, Vector2> positionsDict, bool passThrough)
        {
            selectorSpriteRenderer.enabled = true;
            availMoveCurr.Clear();
            for (int i = 0; i < availMoveRaw.Length; i++)
            {
                breakFlag = false;
                for (int j = 0; j < availMoveRaw[i].Length; j++)
                {
                    availMoveInstance = availMoveRaw[i][j] + currPos;
                    if (!(availMoveInstance.x > 8 || availMoveInstance.y > 8 || availMoveInstance.x < 1 || availMoveInstance.y < 1))
                    {
                        if (!passThrough)
                        {
                            foreach (KeyValuePair<string, Vector2> k in positionsDict)
                            {
                                if (k.Value == availMoveInstance)
                                {
                                    breakFlag = true;
                                    break;
                                }
                            }
                            availMoveCurr.Add(availMoveInstance);
                            Instantiate(highlight, availMoveInstance*scale - axisOffset, Quaternion.identity, this.transform);
                            if (breakFlag) break;
                        }
                        else
                        {
                            availMoveCurr.Add(availMoveInstance);
                            Instantiate(highlight, availMoveInstance*scale - axisOffset, Quaternion.identity, this.transform);
                        }
                    }
                }
            }
        }


        public void kill(Dictionary<string, Vector2> positionsDict)
        {
            positionsDict.Remove(this.name);
            Destroy(this.gameObject);
        }

        private void move(Vector2 destination, Dictionary<string, Vector2> positionsDict, List<string> immuneList)
        {
            immuneList.Add(this.name);
            positionsDict[this.name] = destination;
        }

        public void act(Vector2 currPos, Vector2[][] availMoveRaw, Dictionary<string, Vector2> positionsDict, bool passThrough)
        {
            selected = !selected;
            if (selected)
            {
                checkMove(currPos, availMoveRaw, positionsDict, passThrough);
            }
            else
            {
                unHighlight();
            }
        }

        public void unHighlight()
        {
            selectorSpriteRenderer.enabled = false;
            Transform tr = this.transform;
            for (int i=0; i < tr.childCount; i++)
            {
                if (tr.GetChild(i).gameObject.CompareTag("highlight")) Destroy(tr.GetChild(i).gameObject);
            }
        }
    }

}
