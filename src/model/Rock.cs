using UnityEngine;
using System.Collections;
using mobydick;

namespace mobydick.model {
    public class Rock : MonoBehaviour {
        private System.Random rand;
        public byte lane;
        public bool sinking, sunk;
        private float sinkSpeed = -.1f;

        // Use this for initialization
        void Start() {
            rand = new System.Random();
            lane = (byte) rand.Next(0, 5);
            transform.Translate(new Vector3(40, 0.4f, Constants.LANE_START + Constants.LANE_WIDTH * lane + 0.34f));
        }

        // Update is called once per frame
        void Update() {
            if (Engine.playing) {
                transform.Translate(-.05f * Engine.speed, 0, 0);
            }

            if (sinking) {
                transform.Translate(0, sinkSpeed, 0);
                sinkSpeed -= .05f;
                if (transform.position.y <= -1.5f)
                    sunk = true;
            }
        }
    }
}