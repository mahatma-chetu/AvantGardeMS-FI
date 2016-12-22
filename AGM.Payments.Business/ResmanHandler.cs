using AGM.Payments.Data;
using AGM.Payments.Model.DataModel;
using AGM.Payments.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.Payments.Business
{
    public class ResmanHandler
    {
        public static int AddUpdateEmployee(EmployeeDataModel employee)
        {
            Employee emp = new Employee();
            emp.TypeOfEmployee = "Employee";
            AvantGrandeMSEntities avantGrandeMSEntities = new AvantGrandeMSEntities();
            if (employee.EmployeeID == 0)
            {
                FillEmployeeObject(employee, emp);
                emp.CreateOn = DateTime.Now;
                emp.CreatedBy = "Admin";
                emp.Active = employee.Active;
                avantGrandeMSEntities.Employees.Add(emp);
            }
            else
            {
                emp.Active = employee.Active;
                emp = avantGrandeMSEntities.Employees.Where(x => x.EmployeeID == employee.EmployeeID).FirstOrDefault();
                if (emp.EmployeePropertyPermissions.Count > 0)
                {
                    avantGrandeMSEntities.EmployeePropertyPermissions.RemoveRange(emp.EmployeePropertyPermissions);
                }

                avantGrandeMSEntities.SaveChanges();
                FillEmployeeObject(employee, emp);
            }
            avantGrandeMSEntities.SaveChanges();
            return emp.EmployeeID;
        }

        public static int UpdateProfile(EmployeeDataModel employee)
        {
            AvantGrandeMSEntities avantGrandeMSEntities = new AvantGrandeMSEntities();
            Employee emp = avantGrandeMSEntities.Employees.Where(x => x.EmployeeID == employee.EmployeeID).FirstOrDefault();
            emp.Name = employee.Name;
            emp.Email = employee.Email;
            emp.Phone = employee.Phone;
            emp.Address = employee.Address;
            avantGrandeMSEntities.SaveChanges();
            return emp.EmployeeID;
        }

        public static int ChangePassword(int profileID, string password)
        {
            AvantGrandeMSEntities avantGrandeMSEntities = new AvantGrandeMSEntities();
            Employee emp = avantGrandeMSEntities.Employees.Where(x => x.EmployeeID == profileID).FirstOrDefault();
            emp.Password = password;
            avantGrandeMSEntities.SaveChanges();
            return emp.EmployeeID;
        }
        private static void FillEmployeeObject(EmployeeDataModel employee, Employee emp)
        {
            emp.EmployeeID = employee.EmployeeID;
            emp.Name = employee.Name;
            emp.Email = employee.Email;
            if (!string.IsNullOrEmpty(employee.Password))
            {
                emp.Password = Encryptor.Encrypt(employee.Password);
            }

            emp.Phone = employee.Phone;
            emp.Address = employee.Address;
            emp.Active = employee.Active;
            emp.EmployeePropertyPermissions = new List<EmployeePropertyPermission>();
            foreach (var prp in employee.Properties)
            {
                emp.EmployeePropertyPermissions.Add(
                new EmployeePropertyPermission
                { EmployeeID = employee.EmployeeID, PropertyID = prp.Value, PropertyName = prp.DisplayText });
            }
        }

        public static List<EmployeeDataModel> Employees()
        {
            List<EmployeeDataModel> emps = new List<EmployeeDataModel>();
            AvantGrandeMSEntities avantGrandeMSEntities = new AvantGrandeMSEntities();
            var employees = avantGrandeMSEntities.Employees.Include("EmployeePropertyPermissions").Where(x => x.TypeOfEmployee == "Employee").ToList();
            employees.ForEach(x =>
            {
                List<Model.Option> prp = new List<Model.Option>();
                foreach (var item in x.EmployeePropertyPermissions)
                {
                    prp.Add(new Model.Option
                    {
                        DisplayText = item.PropertyName,
                        Value = item.PropertyID,
                        Checked = true
                    });
                }
                emps.Add(new EmployeeDataModel
                {
                    EmployeeID = x.EmployeeID,
                    Active = x.Active,
                    Name = x.Name,
                    Email = x.Email,
                    Password = Encryptor.Decrypt(x.Password),
                    Phone = x.Phone,
                    Address = x.Address,
                    Properties = prp
                });

            });
            return emps.Where(x => x.Active == true).ToList();
        }

        private static bool IsExistsPaymentToProperty(int paymentID, string propertyID, bool isEditing)
        {
            AvantGrandeMSEntities avantGrandeMSEntities = new AvantGrandeMSEntities();
            var existsPay = avantGrandeMSEntities.PaymentToProperties.Where(x => x.AuthAccountID == paymentID).FirstOrDefault();
            var existsProperty = avantGrandeMSEntities.PaymentToProperties.Where(x => x.PropertyID == propertyID).FirstOrDefault();
            if (isEditing)
            {
                existsPay = avantGrandeMSEntities.PaymentToProperties.Where(x => x.PropertyID == propertyID && x.AuthAccountID == paymentID).FirstOrDefault();
                existsProperty = avantGrandeMSEntities.PaymentToProperties.Where(x => x.PropertyID == propertyID && x.AuthAccountID == paymentID).FirstOrDefault();
            }
            if (existsPay == null && existsProperty == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static int AddUpdatePaymentToProperty(PaymentToPropertyDataModel payment)
        {
            AvantGrandeMSEntities avantGrandeMSEntities = new AvantGrandeMSEntities();
            PaymentToProperty paymentToProperty = new PaymentToProperty();
            if (payment.PaymentID == 0)
            {
                //var exists = avantGrandeMSEntities.PaymentToProperties.Where(x => x.AuthAccountID == payment.PaymentID && x.PropertyID == payment.PropertyID).FirstOrDefault();
                if (!IsExistsPaymentToProperty(payment.AuthAccountID, payment.PropertyID, false))
                {
                    paymentToProperty.AuthAccountID = payment.AuthAccountID;
                    paymentToProperty.PropertyID = payment.PropertyID;
                    avantGrandeMSEntities.PaymentToProperties.Add(paymentToProperty);
                }
                else
                {
                    paymentToProperty.PaymentID = -1;
                }
            }
            else
            {
                if (!IsExistsPaymentToProperty(payment.AuthAccountID, payment.PropertyID, true))
                {
                    paymentToProperty = avantGrandeMSEntities.PaymentToProperties.Where(x => x.PaymentID == payment.PaymentID).FirstOrDefault();
                    if (paymentToProperty != null)
                    {
                        paymentToProperty.AuthAccountID = payment.AuthAccountID;
                        paymentToProperty.PropertyID = payment.PropertyID;
                    }
                }
                else
                {
                    paymentToProperty.PaymentID = -1;
                }

            }
            avantGrandeMSEntities.SaveChanges();
            return paymentToProperty.PaymentID;
        }
        public static List<PaymentToPropertyDataModel> PaymentToProperties()
        {
            AvantGrandeMSEntities avantGrandeMSEntities = new AvantGrandeMSEntities();
            List<PaymentToPropertyDataModel> paymentToProperties = new List<PaymentToPropertyDataModel>();
            var properties = PropertyHandler.GetProperties().Properties;
            var accounts = AccountHandler.AuthAccounts();
            avantGrandeMSEntities.PaymentToProperties.ToList().ForEach(x =>
            {
                string propertyName = properties.Where(p => p.PropertyID == x.PropertyID).First().Name;
                string accountName = accounts.Where(p => p.AuthAccountID == x.AuthAccountID).First().AccountName;
                paymentToProperties.Add(
                    new PaymentToPropertyDataModel
                    {
                        AuthAccountID = x.AuthAccountID,
                        AuthAccountName = accountName,
                        PaymentID = x.PaymentID,
                        PropertyID = x.PropertyID,
                        PropertyName = propertyName
                    });
            });
            return paymentToProperties;
        }
    }
}
