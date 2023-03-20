using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult Add(Rental rental);

        IDataResult<List<RentalsDetailDto>> GetRentalDetail();
    }
}
