namespace CoreData.Common.EventSourcing
{
    // https://abdullin.com/post/event-sourcing-a-la-lokad/
    // https://abdullin.com/post/event-sourcing-aggregates/

    public interface IEvent { }
    public interface ICommand { }
    public interface IAggregateState { void Apply(IEvent e); }
    public interface IAggregateRoot { void Execute(ICommand c); }
}
