using Infra.Data.Repository;
using Services.DTO;
using Services.Mappers;

namespace Services
{
    public class DiscountService
    {
        private DiscountRepository _discountRepository;
        private GameRepository _gameRepository;

        public DiscountService(DiscountRepository discountReposiyory, GameRepository gameRepository)
        {
            _discountRepository = discountReposiyory;
            _gameRepository = gameRepository;
        }

        public List<DiscountDTO> GetAllDiscounts()
        {
            var discounts = _discountRepository.GetAllDiscounts();

            var discountsDTO = discounts.Select(d => DiscountMapper.ToDTO(d)).ToList();

            return discountsDTO;
        }

        public GameDTO CreateDiscount(DiscountCreateDTO newDiscount)
        {
            var discountCreated = _discountRepository.Add(DiscountMapper.ToEntity(newDiscount));

            var gameDiscounted = GameMapper.ToDTO(_gameRepository.GetById(newDiscount.IdGame));

            gameDiscounted.Discount = DiscountMapper.ToDTO(discountCreated);

            return gameDiscounted;
        }

        public void ChangeDiscountStatus(int idDiscount, bool newStatus)
        {
            var discount = _discountRepository.GetById(idDiscount);

            if (discount is null)
                throw new ArgumentException("Discount not found.");

            _discountRepository.ChangeDiscountStatus(idDiscount, newStatus);
        }
    }
}
