using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.PacMan
{

    public enum GhostState { Chasing, Evading, Roving, Dead }

    class SimpleGhost : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        public Sprite EvadeTexture, NormalTexture;

        public GhostState State;

        void Start()
        {
            SetupGhost();
        }

        public void SetupGhost()
        {
            
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
            //Set Textures with script rather than UI
            NormalTexture = Resources.Load<Sprite>("Assets//RedGhost");
            EvadeTexture = Resources.Load<Sprite>("Assets//GhostHit");
        }

        protected virtual void ChangeGhostTextureToNormal()
        {
            //Change texture if Chasing
            //this.spriteRenderer.color = Color.white;
            this.spriteRenderer.sprite = NormalTexture;

        }

        protected virtual void ChangeGhostTectureToBlue()
        {
            //Change texture if evading
            this.spriteRenderer.sprite = EvadeTexture;
        }

        void Update()
        {
            
            switch (this.State)
            {
                case GhostState.Evading:
                    this.ChangeGhostTectureToBlue();
                    

                    break;

                case GhostState.Roving:
                    //set color to normal
                    this.ChangeGhostTextureToNormal();
                    
                    break;

                case GhostState.Chasing:
                    //set color to normal
                    this.ChangeGhostTextureToNormal();
                    

                    break;
                case GhostState.Dead:
                    //set color to normal
                    this.ChangeGhostTectureToBlue();


                    break;
            }
          
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "Player")
            {
                
                Debug.Log(string.Format("{0} triggerEnter with {1} change to dead", this, coll.ToString()));
                this.State = GhostState.Dead;
                
            }
        }

        void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "Player")
            {
                Debug.Log(string.Format("{0} triggerEnter with {1} change to dead", this, coll.ToString()));
                this.State = GhostState.Roving;
            }
        }
    }
}
