using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebAppTest.Models;

namespace WebAppTest.Authorization.Product_Requirements
{
    public class ProductOwnerHandler : AuthorizationHandler<ProductOwnerRequirement, Product>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            ProductOwnerRequirement requirement,
            Product product)
        {
            // Check if the user is authenticated
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            // Check if the user is the owner of the product
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (product.UserId == userId)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
