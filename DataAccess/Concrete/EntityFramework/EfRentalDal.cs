using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalsDetailDto> GetRentalDetail()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars on r.CarId equals c.Id
                             join b in context.Brands on c.BrandId equals b.Id
                             join cus in context.Customers on r.CustomerId equals cus.Id
                             join u in context.Users on cus.UserId equals u.Id

                             select new RentalsDetailDto
                             {
                                 BrandName = b.Name,
                                 FullName = u.FirstName + ' ' + u.LastName
                             };

                return result.ToList();
            }
        }
    }
}
