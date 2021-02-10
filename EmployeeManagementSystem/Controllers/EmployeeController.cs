using AutoMapper;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.DTO;
using EmployeeManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves All Employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<EmployeeDTO>))]
        public IActionResult GetEmployees()
        {
            var objList = _employeeRepository.GetEmployees();
            var objDTO = new List<EmployeeDTO>();
            foreach(var obj in objList)
            {
                objDTO.Add(_mapper.Map<EmployeeDTO>(obj));
            }

            return Ok(objDTO);
        }

        /// <summary>
        /// Retrieves Employee By Id
        /// </summary>
        /// <param name="id">The Id of the employee</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name ="Get Employee")]
        [ProducesResponseType(200, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetEmployee(int id)
        {
            var obj = _employeeRepository.GetEmployee(id);
            if(obj == null)
            {
                return NotFound();
            }

            var objDTO = _mapper.Map<EmployeeDTO>(obj);
            return Ok(objDTO);
        }

        /// <summary>
        /// Creates New Employee
        /// </summary>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateEmployee([FromBody] EmployeeCreateDTO employeeDTO)
        {
            if(employeeDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_employeeRepository.EmployeeExists(employeeDTO.Name))
            {
                ModelState.AddModelError("", "Employee Exists!");
                return StatusCode(404, ModelState);
            }

            var employeeObj = _mapper.Map<Employee>(employeeDTO);

            if (!_employeeRepository.CreateEmployee(employeeObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {employeeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok(employeeObj);

        }

        /// <summary>
        /// Updates Existing Employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPatch("{id:int}", Name = "Update Employee")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.Id)
            {
                return BadRequest(ModelState);
            }

            var employeeObj = _mapper.Map<Employee>(employee);

            if (!_employeeRepository.UpdateEmployee(employeeObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {employeeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes Exisiting Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "Delete Employee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteEmployee(int id)
        {
            if (!_employeeRepository.EmployeeExists(id))
            {
                return NotFound();
            }

            var employeeObj = _employeeRepository.GetEmployee(id);

            if (!_employeeRepository.DeleteEmployee(employeeObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {employeeObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
