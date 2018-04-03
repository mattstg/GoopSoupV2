using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;


public class ParticleManager
    {
        public enum ModuleType { Main, Emission }

        static Dictionary<ModuleType, string> moduleNameByEnum = new Dictionary<ModuleType, string>()
        {
        {ModuleType.Main, "main" },
        {ModuleType.Emission, "emission" }
        };

        public class ModuleGetter
        {
            public PropertyInfo pI;
            public Type moduleType;
            object getModuleDelegate;
            //Type getModuleDelegateType;
            MethodInfo invokeGetterMethodInfo;

            public ModuleGetter(PropertyInfo _pI)
            {
                pI = _pI;                                                                   //Store the property info
                moduleType = pI.PropertyType;                                               //Retrieve the methods return type                           

                MethodInfo mi = typeof(ModuleGetter).GetMethod("FunkyMagic");               //Retrieve using reflection the MethodInfo of FunkyMagic
                mi = mi.MakeGenericMethod(moduleType);                                      //So we fill the generic T in FunkyMagic with moduleType

                //Type genericFunc = typeof(Func<,>);                                         //We declare a type of Func that has two generic arguements
                //Type[] typeArgs = { typeof(ParticleSystem), moduleType };                   //Make an array of types, ParticleSystem and moduleType
                //getModuleDelegateType = genericFunc.MakeGenericType(typeArgs);              //We make a solid type, by giving a generic type an array of types.

                getModuleDelegate = mi.Invoke(this, new object[] { pI.GetGetMethod() });    //We invoke the method info (mi, which is FunkyMagic) and pass it the method info of the property getter 

                //Setup caster method
                invokeGetterMethodInfo = typeof(ModuleGetter).GetMethod("InvokeGetter");         //Save the Caster method info   
                invokeGetterMethodInfo = invokeGetterMethodInfo.MakeGenericMethod(moduleType);   //fill generic T in InvokeGetter
            }

            public Func<ParticleSystem, T> FunkyMagic<T>(MethodInfo mi)
            {
                return (Func<ParticleSystem, T>)Delegate.CreateDelegate(typeof(Func<ParticleSystem, T>), mi);        //We create a delegate where the types now have the correct T
            }

            public object GetModule(ParticleSystem ps) //the function that gets called externally and returns the actual module object
            {
                return invokeGetterMethodInfo.Invoke(this, new object[] { ps });
            }

            public T InvokeGetter<T>(ParticleSystem ps)
            {
                return ((Func<ParticleSystem, T>)getModuleDelegate).Invoke(ps);
            }
        }

        public class ValueSetter
        {

            PropertyInfo pI;
            Type variableType;
            Type moduleType;
            MethodInfo getSetMethodInfo;

            public ValueSetter(PropertyInfo _pI, Type _moduleType)
            {
                pI = _pI;
                variableType = pI.PropertyType;
                moduleType = _moduleType;

                if (pI.GetSetMethod() != null)
                    getSetMethodInfo = pI.GetSetMethod();
            } 

            public void SetValue(object module, object newValue)
            {
                getSetMethodInfo.Invoke(module, new object[] { newValue });
            }
        }


        Dictionary<string, PropertyInfo> modulePropertyInfoByName = new Dictionary<string, PropertyInfo>();
        Dictionary<ModuleType, ModuleGetter> moduleGetterByEnum = new Dictionary<ModuleType, ModuleGetter>();
        Dictionary<ModuleGetter, Dictionary<string, ValueSetter>> valueSettersByModuleGetter = new Dictionary<ModuleGetter, Dictionary<string, ValueSetter>>();

        public void Setup()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (PropertyInfo pI in typeof(ParticleSystem).GetProperties()) //caches PropertyInfo for each property in ParticleSystemClass
            {
                modulePropertyInfoByName.Add(pI.Name, pI);
            }
            foreach (KeyValuePair<ModuleType, string> kv in moduleNameByEnum) //associates our module enums to our PropertyInfos by name, and makes a ModuleGetter for each one
            {
                ModuleGetter mGetter = new ModuleGetter(modulePropertyInfoByName[kv.Value]);
                moduleGetterByEnum.Add(kv.Key, mGetter);
            }
            foreach (KeyValuePair<ModuleType, ModuleGetter> kv in moduleGetterByEnum)
            {
                Dictionary<string, ValueSetter> valueSetterByName = new Dictionary<string, ValueSetter>();
                foreach (PropertyInfo pI in kv.Value.moduleType.GetProperties())
                {
                    ValueSetter vS = new ValueSetter(pI, kv.Value.moduleType);
                    valueSetterByName.Add(pI.Name, vS);
                }
                valueSettersByModuleGetter.Add(kv.Value, valueSetterByName);
            }

        }

        public void SetSystemValues(ParticleSystem system, ModuleType moduleType, string propertyName, object newValue)
        {
            ModuleGetter mG = moduleGetterByEnum[moduleType];
            object module = mG.GetModule(system);
            valueSettersByModuleGetter[mG][propertyName].SetValue(module, newValue);
        }
    }
