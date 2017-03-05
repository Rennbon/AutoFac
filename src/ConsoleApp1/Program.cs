using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.OwnedInstances;
using ConsoleApp1.PropertyTest;
using ConsoleApp1.ResolveTest;
using Autofac.Core;
using ConsoleApp1.TypedP;
using ConsoleApp1.OwnTest;

namespace ConsoleApp1
{

    public class Program
    {

        private static IContainer Container { get; set; }
        public static void Main(string[] args)
        {


            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            //属性方法注入
            builder.RegisterType<Property2>();
            //不注册p2编译不会报错，运行时在实例化对象时抛异常
            //builder.Register(c => new Property1 { P2 = c.Resolve<Property2>() });
            //不注册p2编译不报错，在启动时直接抛异常
            //builder.Register(c => new Property1()).OnActivated(e => e.Instance.P2= e.Context.Resolve<Property2>());
            //初始化对象时会将属性赋默认值null
            //builder.RegisterType<Property1>().PropertiesAutowired();
            //初始化对象指定属性赋值
            //builder.RegisterType<Property1>().WithProperty("S2", "");

            //builder.Register(c => {
            //    var result = new Property1();
            //    var dep = c.Resolve<Property2>();
            //    result.Method(dep);
            //    return result;
            //});
            //builder.RegisterType<Property1>()
            //  .OnActivating(e => {
            //      var dep = e.Context.Resolve<Property2>();
            //      e.Instance.Method(dep);
            //  });


            //resolve
            //builder.RegisterType<Resolve1>().As<IResolve>();
            //typeparam
            builder.RegisterType<Rule1>().As<IRule>();
            builder.RegisterType<Rule2>().As<IRule>();
            builder.RegisterType<Foo>();


            builder.RegisterType<B>();
            builder.RegisterType<C>().InstancePerOwned<B>();

            builder.RegisterType<Ooo>().InstancePerLifetimeScope();
            builder.RegisterType<Xxx>().InstancePerLifetimeScope();

            builder.Register<A>((c, p) =>
            {
                return new A(p.Named<string>("name"));
            }).InstancePerRequest();
            builder.RegisterType<GoldCard>();
            //builder.Register<CreditCard>(
            //  (c, p) =>
            //  {
            //      var accountId = p.Named<string>("accountId");
            //      if (accountId.StartsWith("9"))
            //      {
            //          return new GoldCard(accountId);
            //      }
            //      else
            //      {
            //          return new StandardCard(accountId);
            //      }
            //  });

           
            
            Container = builder.Build();
            TestA();
            //GetCard();
            //WriteDate();
            //TestPP();
            //GetCard();
            //TestResolve();
            //TypeP();
            Console.ReadKey();
        }
        public static void TestResolve()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //var service = scope.Resolve<IResolve>();
                //var service = scope.ResolveOptional<IResolve>();
                //IResolve r1 = null;
                //if (scope.TryResolve<IResolve>(out r1))
                //{
                //    // Do something with the resolved provider value.
                //}       
            }
        }
        public static void TypeP()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //var foo1 = scope.Resolve<Foo>();
                //Console.WriteLine(foo1.GetNumberOfRules());

                // resolve with custom rules, ok¡
                var rules = new List<IRule>();
                rules.Add(new Rule1());
                TypedParameter param = new TypedParameter(typeof(IEnumerable<IRule>), rules);
                var foo2 = scope.Resolve<Foo>(param);
                Console.WriteLine(foo2.GetNumberOfRules());
            }
        }
        public static void TestPP()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var w1 = scope.Resolve<Property1>();
            }

        }
        public static void TestA()
        {
            //var root = Container.Resolve<A>(new NamedParameter("name","root"));
            ////SingleInstance
            //using (var scope1 = Container.BeginLifetimeScope())
            //{
            //    for (var i = 0; i < 100; i++)
            //    {
            //        var w1 = scope1.Resolve<A>(new NamedParameter("name", i.ToString()));
            //        //w1.Write();
            //        using (var scope2 = scope1.BeginLifetimeScope())
            //        {
            //            var w2 = scope2.Resolve<A>(new NamedParameter("name", i.ToString()));
            //            w2.Write();
            //        }
            //    }
            //}

            //InstancePerLifetimeScope
            //using (var scope1 = Container.BeginLifetimeScope())
            //{
            //    for (var i = 0; i < 100; i++)
            //    {
            //        // Every time you resolve this from within this
            //        // scope you'll get the same instance.
            //        var w1 = scope1.Resolve<A>(new NamedParameter("name", i.ToString()));
            //        w1.Write();
            //    }
            //}

            //using (var scope2 = Container.BeginLifetimeScope())
            //{
            //    for (var i = 100; i > 0; i--)
            //    {
            //        // Every time you resolve this from within this
            //        // scope you'll get the same instance, but this
            //        // instance is DIFFERENT than the one that was
            //        // used in the above scope. New scope = new instance.
            //        var w2 = scope2.Resolve<A>(new NamedParameter("name", i.ToString()));
            //        w2.Write();
            //    }
            //}


            //InstancePerMatchingLifetimeScope("myrequest")
            //using (var scope1 = Container.BeginLifetimeScope("myrequest"))
            //{
            //    for (var i = 0; i < 100; i++)
            //    {
            //        var w1 = scope1.Resolve<A>(new NamedParameter("name", "w1" + i.ToString()));
            //        w1.Write();
            //        using (var scope2 = scope1.BeginLifetimeScope())
            //        {
            //            var w2 = scope2.Resolve<A>(new NamedParameter("name", "w2" + i.ToString()));
            //            w2.Write();
            //            // w1 and w2 are always the same object
            //            // instance because the component is per-matching-lifetime-scope,
            //            // so it's effectively a singleton within the
            //            // named scope.
            //        }
            //    }
            //}

            //// Create another lifetime scope using the tag.
            //using (var scope3 = Container.BeginLifetimeScope("myrequest"))
            //{
            //    for (var i = 0; i < 100; i++)
            //    {
            //        // w3 will be DIFFERENT than the worker resolved in the
            //        // earlier tagged lifetime scope.
            //        var w3 = scope3.Resolve<A>(new NamedParameter("name", "w3" + i.ToString()));
            //        w3.Write();
            //        using (var scope4 = scope3.BeginLifetimeScope())
            //        {
            //            var w4 = scope4.Resolve<A>(new NamedParameter("name", "w4" + i.ToString()));
            //            w4.Write();
            //            // w3 and w4 are always the same object because
            //            // they're in the same tagged scope, but they are
            //            // NOT the same as the earlier workers (w1, w2).
            //        }
            //    }
            //}
            //using (var noTagScope = Container.BeginLifetimeScope())
            //{
            //    // This throws an exception because this scope doesn't
            //    // have the expected tag and neither does any parent scope!
            //    var fail = noTagScope.Resolve<A>(new NamedParameter("name", "ERROR"));
            //}

            //.InstancePerOwned<ConsoleOutput>(
            using (var scope = Container.BeginLifetimeScope())
            {
                var h1 = scope.Resolve<Owned<B>>();
                h1.Dispose();
            }
        }
        public static void GetCard()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                //var card = scope.Resolve<CreditCard>(new NamedParameter("accountId", "12345"));
                string name = "accountId";
                string accountId = Guid.NewGuid().ToString();
                //var card = scope.Resolve<CreditCard>(new NamedParameter(name, accountId));
                var card2 = scope.Resolve<GoldCard>(new TypedParameter(typeof(string), accountId));
                //card.WhatIsIt();
                card2.WhatIsIt();
            }
        }

        public static void WriteDate()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }
    }

    public interface IOutput
    {
        void Write(string content);
    }
    public class ConsoleOutput : IOutput
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }
    public interface IDateWriter
    {
        void WriteDate();
    }
    public class TodayWriter : IDateWriter
    {
        private IOutput _output;
        public TodayWriter(IOutput output)
        {
            this._output = output;
        }

        public void WriteDate()
        {
            this._output.Write(DateTime.Today.ToString());
        }
    }
}
