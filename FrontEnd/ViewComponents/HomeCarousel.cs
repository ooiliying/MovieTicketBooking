using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.ViewComponents {
    [ViewComponent( Name = "HomeCarousel" )]
    public class HomeCarousel : ViewComponent{
        public async Task<IViewComponentResult> InvokeAsync() {
            await Task.CompletedTask;
            return View( "Index" );
        }
    }
}
