﻿namespace QuantLab.Shared.Infrastructure.Database
{
    public interface IUnitOfWork
    {
        Task<T> ExecuteAsync<T>(Func<Task<T>> action);
        Task ExecuteAsync(Func<Task> action);
    }
}
