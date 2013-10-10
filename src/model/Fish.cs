using UnityEngine;
using System.Collections;
using mobydick;

namespace mobydick.model {
    public class Fish : MonoBehaviour {
        private System.Random rand;
        public byte spawnType;
        public byte lane;
        public byte type;

        // Use this for initialization
        void Start() {
            rand = new System.Random();
            spawnType = (byte) (type != 0 ? rand.Next(0, 3) : rand.Next(0, 2));
            lane = (byte) rand.Next(0, 5);
            switch (spawnType) {
                case 0: //left to right
                    transform.Translate(new Vector3(-40, 0, Constants.LANE_START + Constants.LANE_WIDTH * lane + .3f));
                    foreach (Transform t in transform) {
                        switch (type) {
                            case 0:
                                t.Rotate(0, 90, 0);
                                break;
                            case 1:
                                t.Rotate(0, -180, 0);
                                break;
                            /*case 2:
                                t.Rotate(0, 180, 0);
                                break;*/
                            case 3: //previously jellyfish
                                t.Rotate(0, 90, 0);
                                break;
                            case 2:
                            case 4:
                                t.Rotate(0, -180, 0);
                                break;
                        }
                    }
                    break;
                case 1: //right to left
                    transform.Translate(new Vector3(40, 0, Constants.LANE_START + Constants.LANE_WIDTH * lane + .3f));
                    if (type == 0 || type == 3) {
                        foreach (Transform t in transform) {
                            t.Rotate(0, -90, 0);
                        }
                    }
                    break;
                case 2: //fixed
                    transform.Translate(new Vector3(rand.Next(-40, 40), 0, Constants.LANE_START + Constants.LANE_WIDTH * lane + .3f));
                    foreach (Transform t in transform) {
                        switch (type) {
                            case 0:
                                t.Rotate(0, 90, 0);
                                break;
                            case 1:
                                t.Rotate(0, -180, 0);
                                break;
                            case 2:
                                t.Rotate(0, 180, 0);
                                break;
                            case 3: //previously jellyfish
                                t.Rotate(0, 90, 0);
                                break;
                            case 4:
                                t.Rotate(0, -180, 0);
                                break;
                        }
                    }
                    break;
            }

        }

        // Update is called once per frame
        void Update() {
            if (Engine.playing) {
                switch (spawnType) {
                    case 0: //left to right
                        transform.Translate(.03f * Engine.speed, 0, 0);
                        break;
                    case 1: //right to left
                        transform.Translate(-.05f * Engine.speed, 0, 0);
                        break;
                }
            }
        }
    }
}