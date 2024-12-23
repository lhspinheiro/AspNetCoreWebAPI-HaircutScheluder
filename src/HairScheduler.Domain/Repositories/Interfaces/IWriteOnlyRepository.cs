﻿using HairScheduler.Domain.Entities;

namespace HairScheduler.Domain.Repositories.Interfaces;
public interface IWriteOnlyRepository
{
    public Task Add(Schedule schedule);

    public Task<bool> ExistData(DateTime date);


}
