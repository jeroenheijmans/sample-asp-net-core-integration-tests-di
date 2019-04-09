namespace Foo.Web.Tests
{
    public class DecoratedBarService : IBarService
    {
        private readonly IBarService innerService;

        public DecoratedBarService(IBarService innerService)
        {
            this.innerService = innerService;
        }

        public string GetValue() => $"{innerService.GetValue()} (decorated)";
    }
}
