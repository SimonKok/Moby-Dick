using UnityEngine;
using System.Collections;

namespace mobydick.model {
    public class Powerup : MonoBehaviour {
        private System.Random rand;
        public byte type;
        public byte lane;

        // Use this for initialization
        void Start() {
            rand = new System.Random();
            lane = (byte) rand.Next(0, 5);
            transform.Translate(new Vector3(40, 0, Constants.LANE_START + Constants.LANE_WIDTH * lane + .3f));
        }

        // Update is called once per frame
        void Update() {
            if (Engine.playing) {
                transform.Translate(-.05f * Engine.speed, 0, 0);
            }
        }
    }
}