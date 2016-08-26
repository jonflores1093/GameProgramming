using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationPacManObserver
{
    class Program
    {
        static void Main(string[] args)
        {
            PacMan p = new PacMan();
            Ghost g = new Ghost(p);

            p.PacManState = PacManState.Moving;
            p.PacManState = PacManState.Stopped;

            p.PowerUp();

            Console.ReadKey();
        }
    }

    public interface ISubject
    {
        void Attach(IObserver o);
        void Deatach(IObserver o);
        void Notify();
    }

    public interface IObserver
    {
        void ObserverUpdate(Object sender, Object message);
    }

    public class PacMan : ISubject
    {
        PacManState pacManState;
        public PacManState PacManState
        {
            get { return pacManState; }
            set
            {
                if (!(pacManState == value))
                {
                    Console.WriteLine(this + " changing state to " + value);
                    pacManState = value;
                    Notify();       //Notify observers of change
                }
            }
        }

        public PacMan()
        {
            observers = new List<IObserver>();
        }

        internal void PowerUp()
        {
            this.Notify("PowerUP");

        }
        #region ISubject Members
        List<IObserver> observers;

        public void Attach(IObserver o)
        {

            this.observers.Add(o);
        }

        public void Deatach(IObserver o)
        {

            this.observers.Remove(o);
        }

        public void Notify()
        {
            foreach (IObserver o in observers)
            {
                o.ObserverUpdate(this, this.pacManState);
            }
        }

        public void Notify(string message)
        {
            foreach (IObserver o in observers)
            {
                o.ObserverUpdate(this, message);
            }
        }

        #endregion

    }

    public enum PacManState { Spawning, Stopped, Moving, Dying };

    public class Ghost : IObserver
    {
        public Ghost(PacMan p)
        {
            p.Attach(this);
        }

        public void ObserverUpdate(object sender, object message)
        {
            if (message is PacManState)
            {
                PacManState p = (PacManState)message;
                Console.WriteLine(this + " notified " + p + " from " + sender);


            }
            if (message is string)
            {
                string strMessage = (string)message;
                switch (strMessage)
                {
                    case "PowerUP":
                        Console.WriteLine(this + "PowerUp Message Received");
                        break;
                    case "PowerUP Elapsed":
                        Console.WriteLine(this + "PowerUp Elapsed Message Received");
                        break;
                }
            }
        }
    }
}
