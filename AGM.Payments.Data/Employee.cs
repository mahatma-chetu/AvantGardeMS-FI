//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AGM.Payments.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.EmployeePropertyPermissions = new HashSet<EmployeePropertyPermission>();
        }
    
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public System.DateTime CreateOn { get; set; }
        public string CreatedBy { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string TypeOfEmployee { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePropertyPermission> EmployeePropertyPermissions { get; set; }
    }
}
