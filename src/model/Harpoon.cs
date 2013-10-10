using UnityEngine;
using System.Collections;
using mobydick;

namespace mobydick.model {
    public class Harpoon : MonoBehaviour {
        public byte lane;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            transform.Translate(.5f + .5f * Engine.speed, 0, 0);
	    }
    }
}