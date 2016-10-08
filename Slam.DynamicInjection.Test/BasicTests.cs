﻿using System;
using System.Diagnostics;
using System.Reflection;

using Hobbisoft.Slam.DynamicInjection.UnitTests.Classes;


#if UNITTEST
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Reflection;

#endif
namespace Hobbisoft.Slam.DynamicInjection.Test
{
#if UNITTEST
    [TestClass]
#endif
    public class BasicTests
    {



#if UNITTEST
        [TestMethod]
#endif
        public void RunPartialClassReplacement()
        {
            //  given a class with 3 functions
            //  replace 2 of them


        }

#if UNITTEST
        [TestMethod]
#endif
        public void RunUI()
        {
            DefaultInjectors.UpdateLogger("Message1");
        }

#if UNITTEST
        [TestMethod]
#endif
        public void RunUI2()
        {
            //int MID = 10650712;

            //AccountContext.SetCurrent(MID, MID, 0);
            //var principal = AccountContext.Current.ToETPrincipal();
            //List<SqlParameter> Params = new List<SqlParameter>();

            //var p = new SqlParameter("@CustomerId", SqlDbType.Int);
            //p.SqlValue = 1;
            //Params.Add(p);
            //SqlParameter[] parms = Params.ToArray<SqlParameter>();

            //Injector.SlamClass(typeof(Hobbisoft.Slam.Core.DBCommand), typeof(Hobbisoft.Slam.DynamicInjection.DBCommand));
            //Hobbisoft.Slam.Core.DBConnection.ExecuteNonQuery(principal, DatabaseType.Member, "dbo.Dev_Testing", parms, 300, 0);
        }

#if UNITTEST
        [TestMethod]
#endif
        public void RunAll()
        {
            //RunBasics();
            //RunSealed();
            //RunSquirt();
            RunUI();

        }

#if UNITTEST
        [TestMethod]
#endif
        public void RunBasics()
        {
            Log.Output(@"Current Value of MySourceClass.PublicStaticWhatsTheTime() = " +
                            MySourceClass.PublicStaticWhatsTheTime());
            Injector.SlamClass(typeof(MySourceClass), typeof(MyMockClass));

            Log.Output(" New Value of MySourceClass.PublicStaticWhatsTheTime() = " +
                            MySourceClass.PublicStaticWhatsTheTime());

        }
#if UNITTEST
        [TestMethod]
#endif
        public void RunSquirt()
        {

            Injector.GetILForMethod(typeof(Squirter), "MyFunction");
            Injector.GetILForMethod(typeof(Squirtee), "MyFunction");
            ////Injector.GetILForMethod(typeof(FinalSquirtExample), "MyFunction");


            Squirtee.MyFunction();
            Injector.SlamClass(typeof(Squirtee), typeof(Squirter));
            Squirtee.MyFunction();


        }


#if UNITTEST
        [TestMethod]
#endif
        public void RunSealed()
        {
            // Below, we can't create a new MyFactoryFactory because the class is an interface based internal (sealed)
            // Try uncommenting the lines to see 

            //Hobbisoft.Slam.DynamicInjection.UnitTests.Classes.MyFactoryFactory sf =
            //    new Hobbisoft.Slam.DynamicInjection.UnitTests.Classes.MyFactoryFactory();

            // and so is the static function we want to call

            //Hobbisoft.Slam.DynamicInjection.UnitTests.Classes.MyFactoryFactory.MySealedFunction(true,
            //    "Wish I could use you in my unit tests :(");


            // object will be the result of the function call (see more below)
            object result = new object();
            var methodParameterData = new object[] { true, "I'm calling a sealed method!" };

            // with a small adjustment to injector, we can pass this method in to be slammed/bonded/injected
            MethodInfo methodInfo = InjectionHelper.GetSealedMethod(@"C:\Users\Patrick\Documents\GitHub\Slam\bin\Slam.UnitTests.Classes.dll",
                "Hobbisoft.Slam.DynamicInjection.UnitTests.Classes.MyFactoryFactory", "MySealedFunction", methodParameterData,

                // but if we pass true to InvokeMethod and ref Object, it will call the method and return the result to your object
                true, ref result);

            // if result were typed you could simply cast ie
            //MyFactory typedResult = (MyFactory)result;

            Log.Output("Result of running sealed method is " + Convert.ToBoolean(result).ToString());

        }

    }  
}
