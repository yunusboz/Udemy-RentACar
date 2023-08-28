﻿using Core.Persistence.Repository;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IFuelRepository : IAsyncRepository<Fuel, Guid>, IRepository<Fuel, Guid>
{

}
