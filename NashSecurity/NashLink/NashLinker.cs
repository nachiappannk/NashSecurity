using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;

namespace NashLink
{
    public class NashLinker
    {
        private Func<object> _stateCreator;
        private bool _shouldStateTraceBePrinted;
        private string[] _previousStatePropertyNames;
        private bool _isFixtureInitializationCheckNeeded;
        private Type _mappingStateType;
        private Type _mappingFixtureType;
        private Action<string> _stateNamePrinter;
        private string[] _ignoredPropertyNamesForInitializationCheck;

        public NashLinker UseState<T>(T state)
        {
            _stateCreator = () => state;
            return this;
        }

        public NashLinker CreateStateFromType(Type stateType)
        {
            _stateCreator = stateType.CreateFromType;
            return this;
        }

        public NashLinker CreateStateWithFactoryType(Type stateFactoryType, string createMethodName)
        {
            _stateCreator = () => stateFactoryType.CreateFromFactoryType<object>(createMethodName);
            return this;
        }

        public NashLinker EnableStateTrace(params string[] previousStatePropertyNames)
        {
            _previousStatePropertyNames = previousStatePropertyNames;
            _shouldStateTraceBePrinted = true;
            return this;
        }

        public NashLinker SetStatePrinter(Action<string> stateNamePrinter)
        {
            _stateNamePrinter = stateNamePrinter;
            return this;
        }

        public NashLinker ForMappingConsiderStateTypeAs(Type stateType)
        {
            _mappingStateType = stateType;
            return this;
        }

        public NashLinker ForMappingConsiderFixtureTypeAs(Type fixtureType)
        {
            _mappingFixtureType = fixtureType;
            return this;
        }

        public NashLinker EnableFixtureInitializationCheck(params string[] ignoredPropertyNames)
        {
            _isFixtureInitializationCheckNeeded = true;
            _ignoredPropertyNamesForInitializationCheck = ignoredPropertyNames;
            return this;
        }

        public void LinkStateToFixture(object fixture)
        {
            if (_stateCreator == null)
                throw new StateCreatorNotConfiguredException();
            object state = _stateCreator.Invoke();
            CopyPossibleProperties(state, fixture);
            if (_shouldStateTraceBePrinted)
                state.PrintStateHistory(_stateNamePrinter, _previousStatePropertyNames);
            if (_isFixtureInitializationCheckNeeded)
                AssetFixtureIsFullyInitialized(fixture, _ignoredPropertyNamesForInitializationCheck);            
        }

        private static void AssetFixtureIsFullyInitialized(object fixture, string[] ignoredPropertyNames)
        {
            var unsetPropertyNames = fixture.GetUnSetPropertNames();
            foreach (var ignoredName in ignoredPropertyNames)
            {
                unsetPropertyNames.Remove(ignoredName);
            }
            if (unsetPropertyNames.Count != 0)
            {
                throw new AllPropertiesAreNotSetException(unsetPropertyNames);
            }
        }

        public void CopyPossibleProperties(object source, object destination)
        {
            var sourceType = _mappingStateType ?? source.GetType();
            var destinationType = _mappingFixtureType?? destination.GetType();
            var mapper = CreateMapper(sourceType, destinationType);
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