using CRUD;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Azure.Documents.SystemFunctions;
using Microsoft.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace TTACRUD.Domain
{
    public class Model
    {
        public string id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public string Gender
        {
            get;
            set;
        }
        public string DOB
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string phone
        {
            get;
            set;
        }
        public string Age
        {
            get;
            set;
        }

    }
    public class Updatedoc
    {
        public string id { get; set; }
        public string name { get; set; }

        public string Gender { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string Age { get; set; }
        public string phone { get; set; }
    }
    public class Createdoc
    {
        public string id { get; set; }
        public string name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string Age { get; set; }
        public string phone { get; set; }
  

    }
    //public static class CustomValidators
    //{
    //    public static IRuleBuilderOptions<T, string> MatchPhoneNumberRule<T>(this IRuleBuilder<T, string> ruleBuilder)
    //    {
    //        return ruleBuilder.SetValidator(new RegularExpressionValidator(@\"^((?:[0-9]\\-?){6,14}[0-9])|((?:[0-9]\\x20?){6,14}[0-9])$\"\r\n));
    //    }
    //}
    public class ModelValidator : AbstractValidator<Model>
    {
        public ModelValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Please specify the name,Cannot create record without name.").Matches(new Regex ("^[A-Z][a-zA-Z]*$")).WithMessage("PhoneNumber not valid");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Please provide valid email");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required");
            //RuleFor(x => x.phone).NotEmpty().WithMessage("Phone number is required").Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("PhoneNumber not valid");
            RuleFor(x => x.phone).NotEmpty().WithMessage("Phone number is required").Matches(new Regex("^[0-9]+$")).WithMessage("PhoneNumber not valid");
            RuleFor(x => x.DOB).NotEmpty().WithMessage("DOB is required");


        }
    }

}
