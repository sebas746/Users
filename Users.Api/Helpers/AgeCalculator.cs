using AutoMapper;
using System;
using Users.Core.Dto;
using Users.Core.Entities;

namespace Users.Api.Helpers;

public class AgeCalculator : IValueResolver<User, UserDto, int>
{
    public int Resolve(User source, UserDto destination, int member, ResolutionContext context)
    {
        return CalculateAge(source.DateOfBirth);
    }

    private int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > today.AddYears(-age)) age--;
        return age;
    }
}
