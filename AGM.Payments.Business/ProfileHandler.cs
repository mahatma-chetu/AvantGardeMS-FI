using AGM.Payments.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.Payments.Business
{
    public class ProfileHandler
    {
        public static ProfileDataModel Profile(int profileID)
        {
            Data.AvantGrandeMSEntities avant = new Data.AvantGrandeMSEntities();
            var profileFromDb = avant.Employees.Where(x => x.EmployeeID==profileID).FirstOrDefault();
            ProfileDataModel profileDataModel = new ProfileDataModel();
            if (profileFromDb!=null)
            {
                profileDataModel.ProfileID = profileFromDb.EmployeeID;
                profileDataModel.Name = profileFromDb.Name;
                profileDataModel.Address = profileFromDb.Address;
                profileDataModel.Email = profileFromDb.Email;
                profileDataModel.Phone = profileFromDb.Phone;
            }
            return profileDataModel;
        }
    }
}
