using Organization.Service.Validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Extensions.Options;
using Organization.Service.Services;

namespace Organization.Service.Model
{
    public class OrganizationDetails
    {
        #region Public members

        public int Id { get; set; }

        [EmptyOrIdentity,
         RegularExpression(@"^[А-Яа-я ]*$", ErrorMessage = "Название организации может содержать только русские буквы"),
         Display(Name = "Название организации")]
        public string OrganizationName { get; set; }

        [Required(ErrorMessage = "Будте любезны, укажите форму собственности"),
         Display(Name = "Форма собственности")]
        public string TypeOwnership { get; set; }

        [Required(ErrorMessage = "Дата регистрации не может быть пустой"),
         Display(Name = "Дата гос. регистрации"), DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StateDateRegistration { get; set; }

        [LengthDigitalString("ОГРН", 13),
         EmptyOrIdentity,
         Display(Name = "ОГРН")]
        public string Ogrn { get; set; }

        [LengthDigitalString("ИНН", 10),
         EmptyOrIdentity,
         Display(Name = "ИНН")]
        public string Inn { get; set; }

        [LengthDigitalString("КПП", 9),
         EmptyOrIdentity,
         Display(Name = "КПП")]
        public string Kpp { get; set; }

        #endregion

        #region Private members



        #endregion

        #region Constructors

        

        #endregion
    }
}
