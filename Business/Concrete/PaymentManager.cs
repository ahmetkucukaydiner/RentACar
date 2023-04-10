using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private IPaymentDal _paymentDal;

        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        [ValidationAspect(typeof(PaymentValidator))]
        public IResult Pay(Payment payment)
        {
            var result = _paymentDal.Get(p =>
                p.FullName == payment.FullName &&
                p.CardNumber == payment.CardNumber &&
                p.Cvv == payment.Cvv &&
                p.Month == payment.Month &&
                p.Year == payment.Year
                );

            if (result != null)
            {
                return new SuccessResult(Messages.PayIsSuccessfull);
            }

            return new ErrorResult(Messages.CardInformationIsIncorrect);
        }
    }
}
