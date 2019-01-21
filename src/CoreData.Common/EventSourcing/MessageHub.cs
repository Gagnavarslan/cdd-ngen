using System;
using System.Collections.Generic;
using System.Threading;

namespace CoreData.Common.EventSourcing
{
    public interface Handles<T>
    {
        void Handle(T message);
    }

    public interface ICommandSender
    {
        void Send<T>(T command) where T : ICommand;

    }
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : IEvent;
    }

    public class MessageHub : ICommandSender, IEventPublisher
    { // https://github.com/gregoryyoung/m-r/blob/master/SimpleCQRS/FakeBus.cs
        private readonly Dictionary<Type, List<Action<IEvent>>> _eventRoutes =
            new Dictionary<Type, List<Action<IEvent>>>();
        private readonly Dictionary<Type, Action<ICommand>> _commandHandlers =
            new Dictionary<Type, Action<ICommand>>();

        public void RegisterEventHandler<T>(Action<T> handler) where T : IEvent
        {
            if (!_eventRoutes.TryGetValue(typeof(T), out var handlers))
            {
                handlers = new List<Action<IEvent>>();
                _eventRoutes.Add(typeof(T), handlers);
            }

            handlers.Add(x => handler((T)x));
        }
        public void RegisterCommandHandler<T>(Action<T> handler) where T : ICommand
        {
            if (!_commandHandlers.TryGetValue(typeof(T), out var handlers))
            {
                _commandHandlers.Add(typeof(T), handlers);
            }
            else
            {
                _commandHandlers[typeof(T)] = x => handler((T)x);
            }
        }

        public void Send<T>(T command) where T : ICommand
        {
            if (_commandHandlers.TryGetValue(typeof(T), out var handler))
            {
                //if (handlers.Count != 1) throw new InvalidOperationException("cannot send to more than one handler");
                //handlers[0](command);
                handler(command);
            }
            else
            {
                throw new InvalidOperationException("no handler registered");
            }
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            if (!_eventRoutes.TryGetValue(@event.GetType(), out var handlers)) return;

            foreach (var handler in handlers)
            {
                //dispatch on thread pool for added awesomeness
                var handler1 = handler;
                ThreadPool.QueueUserWorkItem(x => handler1(@event));
            }
        }
    }
}
