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
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region DECLARE
        IDepartmentRepository _departmentRepository;
        #endregion

        #region Construct
        /// <summary>
        /// hàm khởi tạo cho department service
        /// </summary>
        /// <param name="departmentRepository"></param>
        /// CreatedBy: PQ Huy (06.07.2021)
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        #endregion 

        #region Method

        #endregion
    }
}
