using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region DECLARE

        #endregion

        #region Contructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm lấy ra code lớn nhất
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PQ Huy(05.07.2021)
        public string GetMaxEmployeeCode()
        {
            // Set store
            var storeName = $"Proc_GetMaxEmployeeCode";

            // Query new code
            var maxCode = _dbConnection.QueryFirstOrDefault<string>("Proc_GetMaxEmployeeCode", commandType: CommandType.StoredProcedure);

            // Return a max code
            return maxCode;
        }

        /// <summary>
        /// Hàm kiểm tra mã code tồn tại không
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy(05.07.2021)
        public bool CheckEmployeeCodeExits(string code)
        {
            // create store name
            var storeName = $"Proc_CheckEmployeeCodeExist";

            // add paramer
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@EmployeeCode", code);

            // query database
            var res = _dbConnection.QueryFirstOrDefault<bool>(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure);

            // return boolen
            return res;
        }

        /// <summary>
        /// Hàm lấy ra nhân viên theo email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy(05.07.2021)
        public IEnumerable<Employee> GetByEmail(string email)
        {
            //khởi tạo các commandText
            var parameterId = new DynamicParameters();
            parameterId.Add("Email", email);

            var data = _dbConnection.Query<Employee>($"Proc_GetEmployeeByEmail", parameterId, commandType: CommandType.StoredProcedure);

            //Trả về dữ liệu
            return data;
        }

        /// <summary>
        /// Hàm lấy ra tổng bản ghi
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PQ Huy(05.07.2021)
        public int GetEmployeeTotal()
        {
            // Store name
            var storeName = $"Proc_GetEmployeeTotal";

            // Query database
            var resTotal = _dbConnection.QueryFirstOrDefault<int>(storeName, commandType: CommandType.StoredProcedure);

            // Return value
            return resTotal;
        }

        /// <summary>
        /// Hàm lấy ra bản ghi theo paging
        /// </summary>
        /// <param name="pageIndex">index page</param>
        /// <param name="pageSize">size page</param>
        /// CreatedBy: PQ Huy(05.07.2021)
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeePaging(int pageIndex, int pageSize)
        {
            // Store name
            var storeName = $"Proc_GetEmployeePaging";

            // Add paramer
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@pageIndex", pageIndex);
            dynamicParameters.Add($"@pageSize", pageSize);

            // Query database
            var resultValue = _dbConnection.Query<Employee>(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            
            // Return value
            return resultValue;
        }

        /// <summary>
        /// Hàm lọc ra nhân viên theo paging
        /// </summary>
        /// <param name="pageIndex">index page</param>
        /// <param name="pageSize">size page</param>
        /// <param name="employeeFilter">value filter</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy(05.07.2021)
        public IEnumerable<Employee> GetEmployeeFilter(int pageIndex, int pageSize, string employeeFilter)
        {
            // Store name
            var storeName = $"Proc_GetEmployeeFilter";

            // Add paramer
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@pageIndex", pageIndex);
            dynamicParameters.Add($"@pageSize", pageSize);
            dynamicParameters.Add($"@employeeFilter", employeeFilter);

            // Query database
            var resultFilter = _dbConnection.Query<Employee>(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure);

            // Return value
            return resultFilter;
        }

        /// <summary>
        /// Hàm lấy ra tổng bản ghi filter
        /// </summary>
        /// <param name="employeeFilter">value filter</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy(05.07.2021)
        public IEnumerable<Employee> GetEmployeeFilterAll(string employeeFilter)
        {
            // Store name
            var storeName = $"Proc_GetEmployeeFilterAll";

            // Add paramer
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@employeeFilter", employeeFilter);

            // Query database
            var resultFilter = _dbConnection.Query<Employee>(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure);

            // Return value
            return resultFilter;
        }

        /// <summary>
        /// Hàm lọc ra tổng nhân viên theo điều kiện
        /// </summary>
        /// <param name="employeeFilter">giá tị lọc</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy(05.07.2021)
        public int GetTotalFilter(string employeeFilter)
        {
            // Store name
            var storeName = $"Proc_EmployeeCountFilter";

            // Add paramer
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@employeeFilter", employeeFilter);

            // Query database
            var resTotal = _dbConnection.QueryFirstOrDefault<int>(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure);

            // Return value
            return resTotal;
        }

        #endregion
    }
}
