using UnityEngine;
using System.Collections;

namespace StrategyPacMan.weapons
{

    public class unityFoodWeapon : foodWeapon
    {


        SpriteRenderer spriteRenderer;
        public Color color;

        void Start()
        {
            Util.GetComponentIfNull<SpriteRenderer>(this, ref spriteRenderer);
            //color = Color.white;
            spriteRenderer.color = color;
            Debug.Log("unityFoodWeapon started");
        }

        public unityFoodWeapon()
        {
            
            
        }

        public override void OnCollisionEnter2D(Collision2D coll)
        {

            if (coll.gameObject.name == "PacMan")
            {
                ((ColorPlayer)coll.gameObject.GetComponent("ColorPlayer")).ChangeColor(this.color);
                
            }
            
            base.OnCollisionEnter2D(coll);
        }



    }
}
