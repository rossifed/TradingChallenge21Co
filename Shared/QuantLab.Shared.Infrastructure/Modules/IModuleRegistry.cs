﻿namespace QuantLab.Shared.Infrastructure.Modules
{
    internal interface IModuleRegistry
    {
        IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key);
        ModuleRequestRegistration GetRequestRegistration(string path);
        void AddBroadcastAction(Type requestType, Func<object, CancellationToken, Task> action);

        void AddRequestAction(string path, Type requestType, Type responseType,
            Func<object, CancellationToken, Task<object>> action);
    }
}
