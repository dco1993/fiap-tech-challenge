using Domain.Entity;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappers
{
    internal static class DiscountMapper
    {
        internal static Discount ToEntity(DiscountCreateDTO discount)
        {
            var discountEntity = new Discount
            {
                IdGame = discount.IdGame,
                PercentOff = discount.DiscountPercentage
            };

            discountEntity.SetDiscountPeriod(discount.StartDiscount, discount.EndDiscount);

            return discountEntity;
        }

        internal static DiscountDTO ToDTO(Discount discount)
        {
            var discountDTO = new DiscountDTO
            {
                StartDiscount = discount.StartDiscount,
                EndDiscount = discount.EndDiscount,
                DiscountPercentage = discount.PercentOff,
                DiscountedPrice = discount.DiscountedPrice
            };

            if (discount.Game != null)
            {
                discount.Game.Discounts = null;
                discountDTO.Game = GameMapper.ToDTO(discount.Game);
            }

            return discountDTO;
        }
    }
}
