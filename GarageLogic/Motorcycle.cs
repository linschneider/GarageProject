using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        string m_LicenseType;
        float m_EngineCapacity;


        private bool isValidLicenseType(string i_licenseType)
        {
            bool validLicense = false;

            switch (i_licenseType.ToLower())
            {
                case "a1":
                case "a2":
                case "aa":
                case "b":
                    validLicense = true;
                    break;
            }

            return validLicense;
        }
    }
}
