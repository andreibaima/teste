using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public CreateProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // vai tratar as informações e realizar efetimanete as informações no banco de dados, fazer como nosso serviço ta fazendo
        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);

            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            //async -> quando se faz uma requisição no banco de datao a thread fica esperando a resposta, fica inativa
            //quando usa o await você delega essa operação E/S de acesso a banco de dados a trhead fica limpara para fazer outras coisas

            return project.Id;
        }
    }
}
