using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace ioc_demo_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<ISomeOtherThing>().ImplementedBy<SomeOtherThing>());
            container.Register(Component.For<IMyThing>().ImplementedBy<MyThing>());

            var myThing = container.Resolve<IMyThing>();
            myThing.DoWork();

            Console.ReadLine();
        }
    }

    internal interface IMyThing
    {
        void DoWork();
    }

    internal class MyThing : IMyThing
    {
        private readonly ISomeOtherThing otherThing;

        public MyThing(ISomeOtherThing otherThing)
        {
            this.otherThing = otherThing;
        }

        public void DoWork()
        {
            Console.WriteLine("Doing some work" + otherThing.GetCustomText());
        }
    }

    internal interface ISomeOtherThing
    {
        string GetCustomText();
    }

    class SomeOtherThing : ISomeOtherThing
    {
        public string GetCustomText()
        {
            return " and more work";
        }
    }
}
