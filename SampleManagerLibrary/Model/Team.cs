using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SampleManagerLibrary
{
    [Flags]
    public enum Team
    {
        Administrator = 1,
        FoundryEngineering = 2,
        MetallurgicalEngineering = 4,
        QualityEngineering = 8,
        IndustrialEngineering =16,
        ProductionControl = 32,
        CoreDepartment = 64,
        MoldDepartment = 128,
        MeltDepartment = 256,
        CleanDepartment = 512,
        QualityDepartment = 1024,
        ShippingDepartment = 2048
    }

    public static class ProductFlow
    {
        private static readonly ReadOnlyCollection<string> order = new ReadOnlyCollection<string>(new[]
            {
                "MetallurgicalEngineering",
                "ProductionControl",
                "CoreDepartment",
                "MoldDepartment",
                "MeltDepartment",
                "CleanDepartment",
                "FinishingDepartment",
                "QualityDepartment",
                "ShippingDepartment"
            });

        public static ReadOnlyCollection<string> Order { get => order; }
    }
}