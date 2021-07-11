var Resource = Resource || {};

/**
 * Data type
 */
Resource.DataTypeColumn = {
    Number: "Number",
    Date: "Date",
    Enum: "Enum",
    Email: "Email",
    Name: "Name",
    DateForm: "DateForm"
}

/**
 * Gender
 */
Resource.Gender = {
    Male: "Nam",
    Female: "Nữ",
    Other: "Khác"
}

/**
 * Work status
 */
Resource.WorkStatus = {
    Active: "Đang làm việc",
    Intern: "Đang thử việc",
    Retired: "Đã nghỉ việc"
}

/**
 * Form type
 */
Resource.FormType = {
    Add: "Add",
    Edit: "Edit",
    Delete: "Delete",
    Refresh: "Refresh"
}

/**
 * State date
 */
Resource.StateDate = {
    InValid: 'Invalid Date'
}

/**
 * Duplicate type
 */
Resource.DuplicateType = {
    EmployeeCode: "employeeCode",
    Email: "Email"
}

/**
 * Change page size
 */
Resource.pageChange = {
    Previous: 'Previous',
    Behind: 'Behind'
}

/**
 * Lable
 */
Resource.FormatLable = {
    Address: 'address',
    BankAccountNumber: 'bankAccountNumber',
    BankBranchName: 'bankBranchName',
    BankName: 'bankName',
    BankProvinceName: 'bankProvinceName',
    CreatedBy: 'breatedBy',
    CreatedDate: 'breatedDate',
    DateOfBirth: 'dateOfBirth',
    DepartmentId: 'departmentId',
    DepartmentName: 'departmentName',
    Email: 'email',
    EmployeeCode: 'employeeCode',
    EmployeeId: 'employeeId',
    EmployeeName: 'employeeName',
    EmployeePosition: 'employeePosition',
    Gender: 'gender',
    IdentityDate: 'identityDate',
    IdentityNumber: 'identityNumber',
    IdentityPlace: 'identityPlace',
    ModifedBy: 'modifedBy',
    ModifedDate: 'modifedDate',
    PhoneNumber: 'phoneNumber',
    TelephoneNumber: 'telephoneNumber'
}

Resource.LableName = {
    Address: 'ĐỊA CHỈ',
    BankAccountNumber: 'TÀI KHOẢN NGÂN HÀNG',
    BankBranchName: 'TÊN CHI NHÁNH',
    BankName: 'TÊN NGÂN HÀNG',
    BankProvinceName: 'TÊN TỈNH NGÂN HÀNG',
    CreatedBy: 'TẠO BỞI',
    CreatedDate: 'NGÀY TẠO',
    DateOfBirth: 'NGÀY SINH',
    DepartmentId: 'MÃ PHÒNG BAN',
    DepartmentName: 'TÊN PHÒNG BAN',
    Email: 'EMAIL',
    EmployeeCode: 'MÃ NHÂN VIÊN',
    EmployeeId: 'ID NHÂN VIÊN',
    EmployeeName: 'TÊN NHÂN VIÊN',
    EmployeePosition: 'CHỨC VỤ',
    Gender: 'GIỚI TÍNH',
    IdentityDate: 'NGÀY CẤP',
    IdentityNumber: 'MÃ CMND',
    IdentityPlace: 'NƠI CẤP CMND',
    ModifedDate: 'NGÀY SỬA',
    ModifedBy: 'SỬA BỞI',
    PhoneNumber: 'ĐIỆN THOẠI DI ĐỘNG',
    TelephoneNumber: 'ĐIỆN THOẠI CỐ ĐỊNH'
}

export default Resource;