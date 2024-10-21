using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using ShoppingSystem.Discount.Grpc.Data;
using ShoppingSystem.Discount.Grpc.Models;

namespace ShoppingSystem.Discount.Grpc.Services
{
    public class DiscountService(DiscountContext _context, ILogger<DiscountService> _logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon == null)
                coupon = new Models.Coupon
                {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = "No Discount description"
                };

            _logger.LogInformation($"Discount is retrieved for ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Models.Coupon>();

            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

            _context.Coupons.Add(coupon);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Discount is successfully created with ProductName: {coupon.ProductName}");

            var couponModel = coupon?.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Models.Coupon>();

            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

            _context.Coupons.Update(coupon);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Discount is successfully updated with ProductName: {coupon.ProductName}");

            var couponModel = coupon?.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Product Name {request.ProductName} is not found!"));

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Discount is successfully deleted with ProductName: {request.ProductName}");

            return new DeleteDiscountResponse
            {
                Success = true,
            };
        }
    }
}
