using Microsoft.AspNetCore.Mvc.Filters;

namespace BookingApi.Attributes
{
    public class RoleFilterAttribute : ActionFilterAttribute
    {
        private readonly string _role;

        public RoleFilterAttribute(string role)
        {
            _role = role;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var role = context.HttpContext.Request.Headers["Role"];

            if (role != _role)
            {
                throw new UnauthorizedAccessException("You don't have permission");
            }

        }
    }
}
