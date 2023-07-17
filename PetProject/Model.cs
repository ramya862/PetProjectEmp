using CRUD;
using FluentValidation;
using Microsoft.Net.Http.Headers;

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
    public class ModelValidator : AbstractValidator<Model>
    {
        public ModelValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Please specify the name,Cannot create record without name.");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.phone).NotEmpty().WithMessage("Phone is required");
            RuleFor(x => x.Age).NotEmpty().WithMessage("DOB is required");

        }
    }

}
