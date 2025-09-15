namespace Project.Domain.Routes.BaseRouter
{
    public partial class Router
    {
        public class CategoryRouter : Router
        {
            private const string Prefix = Rule + "Categories";
            public const string Add = Prefix + "/";
            public const string Delete = Prefix + "/{id}";
            public const string Update = Prefix + "/{id}";
            public const string GetAll = Prefix + "/";
            public const string GetById = Prefix + "/{id}";
        }
    }
  
}
