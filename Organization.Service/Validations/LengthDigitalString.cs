using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Organization.Service.Validations
{
    public class LengthDigitalString : ValidationAttribute
    {
        private readonly string _nameField;
        private readonly int _lenth;

        public LengthDigitalString(string nameField, int lenth)
        {
            _nameField = nameField;
            _lenth = lenth;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                ErrorMessage = _nameField + " не может быть пустым";
                return false;
            }

            if (value.ToString().Length != _lenth)
            {
                ErrorMessage = "Длинна " + _nameField + " должна составлять " + _lenth + " цифр!";
                return false;
            }

            if (Regex.IsMatch(value.ToString(), @"^[\d]*$")) return true;

            ErrorMessage = _nameField + " может содержать только цыфры!";
            
            return false;

        }
    }
}
