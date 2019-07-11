using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntroObserver
{
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
}
