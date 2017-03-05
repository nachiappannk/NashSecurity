using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace NashSecurity.Tests.ScenarioBasedTestingTools
{
    public static class ScenarioBasedTestingExtentions
    {
        public static void CopyScenarioDataFromType<T>(this Type scenarioType, T objectToGetData)
        {
            T scenario = scenarioType.CreateObject<T>();
            var mapper = CreateMapper<T, T>();
            mapper.Map(scenario, objectToGetData);
        }

        public static void CopyScenarioDataFromFactoryType<T>(this Type scenarioFactoryType, T objectToGetData)
        {
            IScenarioDataFactory<T> scenarioDataFactory = scenarioFactoryType.CreateObject<IScenarioDataFactory<T>>();
            var scenario = scenarioDataFactory.GetScenarioData();
            var mapper = CreateMapper<T, T>();
            mapper.Map(scenario, objectToGetData);
        }

        public static T CreateScenarioFromFactoryType<T>(this Type scenarioFactoryType)
        {
            IScenarioDataFactory<T> scenarioDataFactory = scenarioFactoryType.CreateObject<IScenarioDataFactory<T>>();
            var scenario = scenarioDataFactory.GetScenarioData();
            return scenario;
        }

        public static T CreateObject<T>(this Type objectType)
        {
            try
            {
                return (T)Activator.CreateInstance(objectType);
            }
            catch (Exception e)
            {
                throw new ObjectCreationIsNotPossible(e);
            }
            
        }


        public static void MoveToScenario<T>(this T scenario, T testFixture)
        {
            var mapper = CreateMapper<T,T>();
            mapper.Map(scenario, testFixture);
        }

        private static IMapper CreateMapper<TSource, TDestination>()
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TSource, TDestination>(); });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        public static void MoveToScenario<TFrom, TTo>(this TFrom scenario, TTo testFixture)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFrom, TTo>();
            });

            IMapper mapper = config.CreateMapper();
            mapper.Map(scenario, testFixture);
        }


        public static void CopyData<T>(this T sourceObject, T destinationObject)
        {
            var mapper = CreateMapper<T, T>();
            mapper.Map(sourceObject, destinationObject);
        }

    }

    public class ObjectCreationIsNotPossible : ApplicationException
    {
        public ObjectCreationIsNotPossible(Exception exception) : base("Object Createion Is Not Possible",exception)
        {
        }
    }
}
