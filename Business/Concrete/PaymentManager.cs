using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private IPaymentDal _paymentDal;
        private ICreditCardService _creditCardService;

        public PaymentManager(IPaymentDal paymentDal, ICreditCardService creditCardService)
        {
            _paymentDal = paymentDal;
            _creditCardService = creditCardService;
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        public IResult Pay(CreditCard creditCard)
        {
            var result = BusinessRules.Run(ValidateCard(creditCard));

            if (result != null)
            {
                return new ErrorResult(Messages.PaymentDenied);
            }

            return new SuccessResult(Messages.PaymentSuccess);

        }

        private IResult ValidateCard(CreditCard creditCard)
        {
            if (creditCard.CardNumber.Length > 16)
            {
                return new ErrorResult(Messages.CreditCardNumberInvalid);
            }

            return new SuccessResult(Messages.PaymentSuccess);
        }
    }
}
