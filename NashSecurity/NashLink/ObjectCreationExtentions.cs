using System;
using AutoMapper;

namespace NashLink
{
    public static class ObjectCreationExtentions
    {
        public static T CreateFromType<T>(this Type objectType)
        {
            try
            {
                return (T)Activator.CreateInstance(objectType);
            }
            catch (Exception e)
            {
                throw new ObjectCreationIsNotPossibleException(e);
            }
        }

        public static object CreateFromType(this Type objectType)
        {
            return objectType.CreateFromType<object>();
        }

        internal static T CreateFromFactoryType<T>(this Type factoryType, string createMethodName)
        {
            var methodInfo = factoryType.GetMethod(createMethodName);
            var obj = (T)methodInfo.Invoke(factoryType.CreateFromType(), null);
            return obj;
        }

        public static void CopyPossibleProperties(this object source, object destination)
        {
            var mapper = CreateMapper(source.GetType(), destination.GetType());
            mapper.Map(source, destination);
        }

        private static IMapper CreateMapper(Type sourceType, Type destinationType)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap(sourceType, destinationType); });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}