using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace grid
{
    public class gridScript : MonoBehaviour
    {

        private float scale = 0.16f;

        public float _time;
        private float fixedDeltaTime;
        public List<Vector2> positionsList = new List<Vector2>();
        public Vector2[] nextPositions = new Vector2[2];


        // Start is called before the first frame update
        void Awake()
        {
            this.fixedDeltaTime = Time.fixedDeltaTime;
        }

        // Update is called once per frame
        void Update()
        {
            _time += Time.deltaTime;
        }


        private string currTag;
        private int currCompX;
        private int currCompY;
        public void highlight(List<Vector2> availMoveCurr)
        {
            foreach (Vector2 availMoveInst in availMoveCurr)
            {
                currCompX = (int)Mathf.Round(availMoveInst.x);
                currCompY = (int)Mathf.Round(availMoveInst.y);
                currTag = currCompX.ToString() + currCompY.ToString();
            }
        }

    }
}

