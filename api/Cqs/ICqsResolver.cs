using System;

namespace Api.Cqs
{
    public interface ICqsResolver
    {
        TCommand ResolveCommand<TCommand>() where TCommand : ICommand<TCommand>;

        TQuery ResolveQuery<TQuery>() where TQuery : IQuery<TQuery>;
    }

    public class CqsResolver : ICqsResolver
    {
        private readonly Func<Type, dynamic> _getServiceByTypeFromContainer;

        public CqsResolver(Func<Type, dynamic> getServiceByTypeFromContainer)
        {
            _getServiceByTypeFromContainer = getServiceByTypeFromContainer;
        }

        public TCommand ResolveCommand<TCommand>() where TCommand : ICommand<TCommand>
        {
            return ResolveCqsObject<TCommand>();
        }

        public TQuery ResolveQuery<TQuery>() where TQuery : IQuery<TQuery>
        {
            return ResolveCqsObject<TQuery>();
        }

        private T ResolveCqsObject<T>()
        {
            Type cqsType = typeof(T);

            dynamic cqsObject = _getServiceByTypeFromContainer(cqsType)
                ?? throw new TypeLoadException($"Type not registered in container: {cqsType.Name}.");

            return Convert.ChangeType(cqsObject, cqsType);
        }
    }
}