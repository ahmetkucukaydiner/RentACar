using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
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

        public IResult Pay(Payment payment, CreditCard creditCard)
        {
            var result = BusinessRules.Run(ValidateCard(creditCard));

            if (result != null)
            {
                payment.PaymentDate = DateTime.Now;
                return result;
            }

            payment.PaymentDate = DateTime.Now;
            _paymentDal.Add(payment);
            return new SuccessResult(result.Message);

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
