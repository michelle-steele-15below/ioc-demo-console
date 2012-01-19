using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace ioc_demo_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.InDirectory(new AssemblyFilter("")));

            var myThing = container.Resolve<IMyThing>();
            myThing.DoWork();

            Console.ReadLine();
        }
    }

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISomeOtherThing>().ImplementedBy<SomeOtherThing>());
            container.Register(Component.For<IMyThing>().ImplementedBy<MyThing>());
        }
    }

    interface IMyThing
    {
        void DoWork();
    }

    class MyThing : IMyThing
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

    interface ISomeOtherThing
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
