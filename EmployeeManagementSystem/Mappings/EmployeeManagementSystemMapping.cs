using AutoMapper;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Mappings
{
    public class EmployeeManagementSystemMapping : Profile
    {
        public EmployeeManagementSystemMapping()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();
        }
    }

}
