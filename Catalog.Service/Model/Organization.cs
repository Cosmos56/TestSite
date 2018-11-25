using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Service.Model
{
    public class Organization
    {
        public int Id { get; set; }

        public string OrganizationName { get; set; }

        public string TypeOwnership { get; set; }

        public DateTime StateDateRegistration { get; set; }

    }
}
