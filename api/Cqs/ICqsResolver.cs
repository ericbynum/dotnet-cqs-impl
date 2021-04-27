using System;

namespace Api.Cqs
{
    public interface ICqsResolver
    {
        TCommand ResolveCommand<TCommand>() where TCommand : ICommand;

        TQuery ResolveQuery<TQuery>() where TQuery : IQuery;
    }

    public class CqsResolver : ICqsResolver
    {
        private readonly Func<Type, dynamic> _getServiceByTypeFromContainer;

        public CqsResolver(Func<Type, dynamic> getServiceByTypeFromContainer)
        {
            _getServiceByTypeFromContainer = getServiceByTypeFromContainer;
        }

        public TCommand ResolveCommand<TCommand>() where TCommand : ICommand
        {
            return ResolveCqsObject<TCommand>();
        }

        public TQuery ResolveQuery<TQuery>() where TQuery : IQuery
        {
            return ResolveCqsObject<TQuery>();
        }

        private T ResolveCqsObject<T>()
        {
            Type cqsType = typeof(T);

            return _getServiceByTypeFromContainer(cqsType)
                ?? throw new TypeLoadException($"Type not registered in container: {cqsType.Name}.");
        }
    }
}