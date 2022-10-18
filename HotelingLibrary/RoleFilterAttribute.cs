using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelingLibrary
{
    public class RoleFilterAttribute : ActionFilterAttribute
    {
        private readonly UserRolesEnum _role;

        public RoleFilterAttribute(UserRolesEnum role)
        {
            _role = role;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var header = context.HttpContext.Request.Headers["Role"].ToString();
            if(String.IsNullOrEmpty(header))
            {
                throw new UnauthorizedAccessException("You don't have permission");
            }
            var role = Enum.Parse<UserRolesEnum>(header);

            //var role = context.HttpContext.Request.Headers["Role"];

            if (role != _role)
            {
                throw new UnauthorizedAccessException("You don't have permission");
            }

        }
    }
}
