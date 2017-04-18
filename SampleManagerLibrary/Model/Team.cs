using System;
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
}