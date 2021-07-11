using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Service
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region DECLARE
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Construct
        /// <summary>
        /// hàm khởi tạo cho employee service
        /// </summary>
        /// <param name="employeeRepository"></param>
        /// CreatedBy: PQ Huy (05.07.2021)
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion

        #region Method
        /// <summary>
        /// Hàm lấy ra mã nhân viên mới nhất
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (07.07.2021)
        public string GetNewEmployeeCode()
        {
            // Init new code and string for char
            var maxCode = _employeeRepository.GetMaxEmployeeCode();
            string letters = string.Empty;
            string numbers = string.Empty;

            // Plus each character
            foreach (char c in maxCode)
            {
                if (Char.IsLetter(c))
                {
                    letters += c;
                }
                if (Char.IsNumber(c))
                {
                    numbers += c;
                }
            }

            var newCode = $"NV-{Int32.Parse(numbers) + 1}";

            // return a string with new code
            return newCode;
        }

        /// <summary>
        /// Hàm kiểm tra mã nhân viên có tồn tại trong
        /// </summary>
        /// <param name="code">mã nhân viên</param>
        /// <returns></returns>
        public bool CheckEmployeeCodeExits(string code)
        {
            // Gọi đến repository
            var rowAffect = _employeeRepository.CheckEmployeeCodeExits(code);

            // Trả về boolen
            return rowAffect;
        }

        /// <summary>
        /// Hàm lấy ra nhân viên theo email
        /// </summary>
        /// <param name="email">email nhân viên</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (10.07.2021)
        public IEnumerable<Employee> GetByEmail(string email)
        {
            // gọi respository
            var employeeData = _employeeRepository.GetByEmail(email);

            // return value
            return employeeData;
        }

        /// <summary>
        /// Hàm tính tổng nhân viên
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (10.07.2021)
        public int GetEmployeeTotal()
        {
            // Get repositori total employee
            var count = _employeeRepository.GetEmployeeTotal();

            // Return value
            return count;
        }

        /// <summary>
        /// Hàm pagding
        /// </summary>
        /// <param name="pageIndex">chỉ mục page</param>
        /// <param name="pageSize">kích thước page</param>
        /// <returns>Trả về tổng bản ghi và data pagding</returns>
        /// CreatedBy: PQ Huy (10.07.2021)
        public ServiceResult GetEmployeePaging(int pageIndex, int pageSize)
        {
            ServiceResult serviceResult = new ServiceResult();

            // Call repository
            var count = _employeeRepository.GetEmployeeTotal();

            // Get paging
            var resultEmployee = _employeeRepository.GetEmployeePaging(pageIndex, pageSize);

            // Push data
            serviceResult.Data = resultEmployee;
            serviceResult.Total = count;

            // Return value
            return serviceResult;
        }

        /// <summary>
        /// Hàm lọc ra nhân viên theo paging
        /// </summary>
        /// <param name="pageIndex">indexx</param>
        /// <param name="pageSize">size</param>
        /// <param name="employeeFilter">giá trị lọc</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (10.07.2021)
        public ServiceResult GetEmployeeFilter(int pageIndex, int pageSize, string employeeFilter)
        {
            ServiceResult serviceResult = new ServiceResult();

            // Call repository
            var count = _employeeRepository.GetTotalFilter(employeeFilter);

            // Get paging
            var resultFilter = _employeeRepository.GetEmployeeFilter(pageIndex, pageSize, employeeFilter);

            // Push data
            serviceResult.Data = resultFilter;
            serviceResult.Total = count;

            // Return value
            return serviceResult;
        }

        /// <summary>
        /// Lấy ra toàn bộ bản ghi được lọc
        /// </summary>
        /// <param name="employeeFilter">giá trị lọc</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (10.07.2021)
        public ServiceResult GetEmployeeFilterAll(string employeeFilter)
        {
            ServiceResult serviceResult = new ServiceResult();

            // Call repository
            var count = _employeeRepository.GetTotalFilter(employeeFilter);

            // Get paging
            var resultFilter = _employeeRepository.GetEmployeeFilterAll(employeeFilter);

            // Push data
            serviceResult.Data = resultFilter;
            serviceResult.Total = count;

            // Return value
            return serviceResult;
        }

        /// <summary>
        /// Đếm ra tổng bản ghi được lọc
        /// </summary>
        /// <param name="employeeFilter"> giá trị lọc</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (10.07.2021)
        public int GetTotalFilter(string employeeFilter)
        {
            // Get repositori total employee
            var count = _employeeRepository.GetTotalFilter(employeeFilter);

            // Return value
            return count;
        }



        #endregion
    }
}
