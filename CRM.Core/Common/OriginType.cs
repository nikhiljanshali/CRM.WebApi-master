namespace CRM.Core.Common {
    public enum OriginType {

        /// <summary>
        /// Full access. all user rights granted and all apps menu can access.
        /// Mostly applied for devloper.
        /// </summary>
        SuperAdmin,

        /// <summary>
        /// Special role for top level managemant. department and user criteria change rights granted.
        /// </summary>
        Administrator,

        /// <summary>
        /// A parent company is a company that owns enough voting stock in another firm.
        /// </summary>
        Parent_Company,

        /// <summary>
        /// is a company that is completely or partly owned and partly or wholly controlled by another company 
        /// </summary>
        Subsidiary,

        /// <summary>
        /// Hiring worker and manage company's workforce requirmant. content managemant right provided.
        /// Can access any employee information but not granted any work or payscale history.
        /// </summary>
        HumanResource,

        /// <summary>
        /// Manage company's verious account and payroll. Employee work and payroll history access right granted.
        /// Content managemant and accounting module available. workforce managemant not allowed.
        /// </summary>
        Accountant,

        /// <summary>
        /// Can add bill and check entry. can access company inventory (exclude diamond). 
        /// Maintan inventory and sort goods to employee.
        /// </summary>
        Maintanance,

        /// <summary>
        /// Specially dedicated to Software eng. granted superadmin rights.
        /// </summary>
        SoftWare_Eng,

        /// <summary>
        /// Roll for network eng. only shcedule permission granted.
        /// </summary>
        Network_Eng,

        /// <summary>
        ///<P> packet code printing and number generating rights.
        /// Can connect with balance scale and masure it. also can create child packet.</P> 
        /// </summary>
        Weight_Scale,

        GIA,

        IGI,

        HRD,

        SalesMen,

        SalesManager,

        /// <summary>
        /// Can transfer packet one to other department. also can employee entry in company.
        /// if given can obtain also other charge respact to admin orders.
        /// </summary>
        DataEntry,

        /// <summary>
        /// Kitchen staff.can use inventory related to thair pentry use. 
        /// also able to see there schedule. 
        /// </summary>
        Pentry,

        /// <summary>
        /// Liable for securing company. Shelve log of visitor and employee.
        /// can access employee database and has permit to capture visitor picture.
        /// </summary>
        Security,

        /// <summary>
        /// Purchage department can buy item and make thair bill.
        /// Cant pay the bill accounting module were absent.
        /// </summary>
        Purchage,

        /// <summary>
        /// Sales department can sell item and negoriat about price and range.
        /// Cant accept chaque becouse accounting module were absent.
        /// </summary>
        Sales,

        /// <summary>
        /// Vendor for verious goods. connected to maintance department.
        /// has content managemant for shelving bill and other documant.
        /// </summary>
        Vendor,

        /// <summary>
        /// Customer account. can buy product once company has produced. 
        /// or even place requiremant for product.
        /// </summary>
        Customer,

        /// <summary>
        /// Liable for running operation for full branch. this account very close to administrator.
        /// Has access to full company stock. can change department and employee criteria.
        /// </summary>
        BranchHead,

        /// <summary>
        /// Department's head. can request to change employee critera. has not permission to change that.
        /// Has access to thair departmant's stock.
        /// </summary>
        Manager,

        /// <summary>
        /// Employee account. they can see there past work and payroll.
        /// They also see there schedule and there criteria.
        /// </summary>
        Employee,

        /// <summary>
        /// No in any use. assign if you have not to use origintype.
        /// </summary>
        Empty,

        /// <summary>
        /// Director of Company
        /// </summary>
        Director,

        /// <summary>
        /// For Support Staff
        /// </summary>
        Support,

        Seller,
    }
}