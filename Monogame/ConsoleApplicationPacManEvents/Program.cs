using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationPacManEvents
{

    // A delegate type for hooking up change notifications.
    public delegate void FoodHitEventHandler(object sender, EventArgs e);

    //a big change
    
    class Program
    {
        static void Main(string[] args)
        {
            PacMan p = new PacMan();
            Ghost g = new Ghost();
            Food f = new Food();
            f.Hit += new FoodHitEventHandler(p.OnFoodHit);
            f.Hit += new FoodHitEventHandler(g.OnFoodHit);

            f.CallHit();

            Console.ReadKey();
        }
    }

    public class Food
    {
        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event FoodHitEventHandler Hit;

        // Invoke the Hit event; called whenever list changes
        protected virtual void OnHit(EventArgs e)
        {
            if (Hit != null)
                Hit(this, e);
        }

        public void CallHit()
        {
            this.OnHit(EventArgs.Empty);
        }

    }

    public class Ghost
    {
        public void OnFoodHit(object sender, EventArgs e)
        {
            Console.WriteLine(this + "GhostHit");
        }
    }

    public class PacMan
    {
        public void OnFoodHit(object sender, EventArgs e)
        {
            Console.WriteLine(this + "GhostHit");
        }
    }
}
