using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler : Notifiable,
                               IHandler<CreateTodoCommand>,
                               IHandler<UpdateTodoCommand>
    {

        private readonly ITodoRepository _repository;

        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateTodoCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, não é possível inserir sua tarefa!", command.Notifications);

            var model = new TodoItem(command.Title, command.Date, command.User);

            _repository.Create(model);

            return new GenericCommandResult(true, "Tarefa criada com sucesso!", model);
        }

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}