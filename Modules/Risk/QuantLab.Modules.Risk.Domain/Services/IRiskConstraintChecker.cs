using QuantLab.Modules.Risk.Domain.Model;
using QuantLab.Modules.Risk.Domain.Model.Constraints;

namespace QuantLab.Modules.Risk.Domain.Services
{
    internal interface IRiskConstraintChecker
    {

        ConstraintCheckResult Check(WhatIfPosition whatIfPosition);
    }
}
