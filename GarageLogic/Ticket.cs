using System;

namespace Ex03.GarageLogic
{
    public class Ticket
    {
        public enum eCurrentStatus
        {
            InFixings,
            Fixed,
            Paid
        }

        private readonly Vehicle r_Vehicle;
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private eCurrentStatus m_CurrentStatus;

        public Ticket(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            if (i_Vehicle == null)
            {
                throw new ArgumentNullException(nameof(i_Vehicle));
            }

            if (string.IsNullOrEmpty(i_OwnerName))
            {
                throw new ArgumentException("Owner name cannot be empty.");
            }

            if (string.IsNullOrEmpty(i_OwnerPhoneNumber))
            {
                throw new ArgumentException("Owner phone number cannot be empty.");
            }

            r_Vehicle = i_Vehicle;
            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_CurrentStatus = eCurrentStatus.InFixings;
        }

        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }

        public string OwnerName
        {
            get
            {
                return r_OwnerName;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return r_OwnerPhoneNumber;
            }
        }

        public eCurrentStatus CurrentStatus
        {
            get
            {
                return m_CurrentStatus;
            }
            set
            {
                m_CurrentStatus = value;
            }
        }
    }
}