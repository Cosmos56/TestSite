using System;
using System.ComponentModel.DataAnnotations;

namespace TestSite.Model
{
    public class CatalogItem
    {
        //Id
        public int Id { get; set; }

        [Display(Name = "Название организации")]
        public string OrganizationName { get; set; }

        [Display(Name = "Форма собственности")]
        public string TypeOwnership { get; set; }

        [Display(Name = "Дата гос. регистрации"), DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StateDateRegistration { get; set; }

        [Display(Name = "ОГРН")]
        public string Ogrn { get; set; }

        [Display(Name = "ИНН")]
        public string Inn { get; set; }

        [Display(Name = "КПП")]
        public string Kpp { get; set; }
    }
}
