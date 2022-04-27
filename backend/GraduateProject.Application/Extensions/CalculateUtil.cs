using GraduateProject.Application.Common.Dto;

namespace GraduateProject.Application.Extensions;

public static class CalculateUtil
{
    public static double Distance(Position x, Position y)
    {
        return Math.Sqrt(Math.Pow(x.Lat - y.Lat, 2) + Math.Pow(x.Lng - y.Lng, 2));
    }
}