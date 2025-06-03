using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class DiscountRepository : AppRepository<Discount>
    {
        public DiscountRepository(AppDbContext context) : base(context)
        {
        }

        public void ChangeDiscountStatus(int idDiscount, bool newStatus)
        {
            _context.Discount
                    .Where(d => d.Id == idDiscount)
                    .ExecuteUpdate(s => s.SetProperty(d => d.Status, newStatus));

            _context.SaveChanges();
        }

        public IList<Discount> GetAllDiscounts()
        {
            var dicountsList = _context.Discount
                               .Include(d => d.Game)
                               .ToList();

            return dicountsList;
        }
    }
}
