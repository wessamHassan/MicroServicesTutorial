using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Models
{
    public class ProductValidator: AbstractValidator<Product>
    {
        private static IFormatProvider ciHijriFormat1;
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty();//.WithMessage("{PropertyName} should be not empty.");
            RuleFor(p => p.Id).NotNull().NotEqual(5).WithMessage("{PropertyName} should be not 5.");

            RuleFor(p => p.TestingEnum).IsInEnum().WithMessage("Value is not exist");

            RuleFor(x => x.Name).Must(IsValidHijriDate).WithMessage("date must be hijri formate");

        }

        public bool IsValidHijriDate(string pHijri)
        {
            //Int64 tempObject;
            //if (!Int64.TryParse(pHijri, out tempObject))
            //{
            //    return false;
            //}

            bool _result = false;
            //DateTime dt = new DateTime();
            //DateTime.TryParseExact(pHijri, "yyyyMMdd", ciHijriFormat1, DateTimeStyles.None, out dt);
            //try
            //{
            //    string str = dt.ToString("yyyyMMdd", ciHijriFormat1);
            //}
            //catch
            //{
            //    dt = DateTime.MinValue;
            //}
            //_result = dt != DateTime.MinValue;

            //there was validating error for date > 1448
            
            if (pHijri.Length == 8)
            {
                if ((Convert.ToInt32(pHijri.Substring(0, 4)) > 1440) & (Convert.ToInt32(pHijri.Substring(0, 4)) < 1580))
                {
                    _result = true;
                }
                else
                {
                    _result = false;
                }
            }
            
            return _result;
        }
    }

}

   

