﻿using FasTnT.Domain.Commands.Setup;
using FasTnT.Domain.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FasTnT.Domain.Handlers.DatabaseSetup
{
    public class MigrateDatabaseHandler : IRequestHandler<MigrateDatabaseRequest>
    {
        private readonly IDatabaseMigrator _migrator;

        public MigrateDatabaseHandler(IDatabaseMigrator migrator)
        {
            _migrator = migrator;
        }

        public async Task<Unit> Handle(MigrateDatabaseRequest request, CancellationToken cancellationToken)
        {
            await _migrator.Migrate();

            return Unit.Value;
        }
    }
}
