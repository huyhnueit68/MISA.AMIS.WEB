using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Service;
using MISA.CukCuk.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.AMIS.Web.Controllers
{
    /// <summary>
    /// Api Danh mục khác hàng
    /// CreatedBy: PQ Huy (05.07.2021)
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseEntityController<Employee>
    {
        #region DECLARE
        IEmployeeService _employeeService;
        #endregion

        #region Contructor
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Method

        /// <summary>
        /// Lấy mã nhân viên mới nhất
        /// </summary>
        /// <returns>Trả về mã nhân viên mới</returns>
        /// CreatedBy: PQ Huy (07.07.2021)
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            var newCode = _employeeService.GetNewEmployeeCode();

            // return a new code
            return Ok(newCode);
        }

        /// <summary>
        /// Kiểm tra mã nhân viên tồn tại trong hệ thống
        /// </summary>
        /// <param name="code">Mã nhân viên</param>
        /// <returns>Trả về true/false khi có mã nhân viên</returns>
        /// CreatedBy: PQ Huy (07.07.2021)
        [HttpGet("CheckEmployeeCodeExist")]
        public IActionResult CheckEmployeeCodeExist(string code)
        {
            var isExist = _employeeService.CheckEmployeeCodeExits(code);

            // return value
            return Ok(isExist);
        }

        /// <summary>
        /// Lấy dữ liệu bản ghi theo id
        /// </summary>
        /// <returns>Trả về bản ghi với id tương ứng</returns>
        /// CreatedBy: PQ Huy (05.07.2021)
        [HttpGet("Email")]
        public IActionResult GetEmployeeByEmail(string email)
        {
            // gọi function lấy dữ liệu
            var data = _employeeService.GetByEmail(email);

            //trả về dữ liệu
            return Ok(data);
        }

        /// <summary>
        /// Lọc ra danh sách nhân viên
        /// </summary>
        /// <param name="pageIndex">Index trang hiện tại</param>
        /// <param name="pageSize">Tổng số bản ghi trên một trang</param>
        /// <param name="employeeFilter">value filter</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (08.07.2021)
        [HttpGet("EmployeeFilter")]
        public IActionResult GetEmployeeFilter(int pageIndex, int pageSize, string employeeFilter)
        {
            // Get service
            var resultFilter = _employeeService.GetEmployeeFilter(pageIndex, pageSize, employeeFilter);

            // Return value
            return Ok(resultFilter);
        }

        /// <summary>
        /// Lấy ra tất cả dánh sách nhân viên đã lọc
        /// </summary>
        /// <param name="employeeFilter">value filter</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (08.07.2021)
        [HttpGet("EmployeeFilterAll")]
        public IActionResult GetEmployeeFilterAll(string employeeFilter)
        {
            // Get service
            var resultFilter = _employeeService.GetEmployeeFilterAll(employeeFilter);

            // Return value
            return Ok(resultFilter);
        }

        /// <summary>
        /// Phân trang dữ liệu
        /// </summary>
        /// <param name="pageIndex">Index của trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (08.07.2021)
        [HttpGet("EmployeePaging")]
        public IActionResult GetEmployeePaging(int pageIndex, int pageSize)
        {
            // Get service
            var resultPaging = _employeeService.GetEmployeePaging(pageIndex, pageSize);

            // Return value
            return Ok(resultPaging);
        }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// <returns>Trả về tổng số bản ghi hiện tại</returns>
        /// CreatedBy: PQ Huy (08.07.2021)
        [HttpGet("EmployeeTotal")]
        public IActionResult GetEmployeeTotal( )
        {
            // Get service
            var total = _employeeService.GetEmployeeTotal();

            // Return value
            return Ok(total);
        }

        #endregion
    }
}
