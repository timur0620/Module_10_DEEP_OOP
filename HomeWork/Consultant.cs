using Bogus;
using Bogus.DataSets;
using Module_10_DEEP_OOP.HomeWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Module_10_Deep_OOP.HomeWork
{  
    class Consultant: Manager
    {
        public Consultant ChangeNumberPhone(string oldNumber, string newNumber)
        {
            List<Manager> allClients = GetAllClient();

            Consultant consultant = new Consultant();

            if (newNumber == null || newNumber.Length == 0 || newNumber.Equals(""))
            {
                return consultant;
            }
            foreach (Consultant client in allClients)
            {
                if (client.phoneNumber.Equals(oldNumber))
                {
                    client.phoneNumber = newNumber;

                    consultant = client;

                    return client;
                }
            }
            return consultant;
        }
    }
}

