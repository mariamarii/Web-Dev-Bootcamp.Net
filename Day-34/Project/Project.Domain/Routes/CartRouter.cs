namespace Project.Domain.Routes.BaseRouter
{
    public partial class Router
    {
        public class CartRouter : Router
        {
            private const string Prefix = Rule + "Carts";
            public const string Add = Prefix + "/";
            public const string Delete = Prefix + "/{id}";
            public const string GetById = Prefix + "/{id}";
            public const string GetByUserId = Prefix + "/user/{userId}";
            public const string AddItem = Prefix + "/{cartId}/items";
            public const string UpdateItem = Prefix + "/items/{cartItemId}";
            public const string RemoveItem = Prefix + "/items/{cartItemId}";
        }
    }
}