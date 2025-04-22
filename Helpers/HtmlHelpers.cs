using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Threading.Tasks;

public static class RazorViewToStringRenderer
{
    public static async Task<string> RenderViewToStringAsync(PageModel page, string viewName, object model)
    {
        var httpContext = page.HttpContext;
        var actionContext = new ActionContext(httpContext, page.RouteData, page.PageContext.ActionDescriptor);

        var serviceProvider = httpContext.RequestServices;
        var engine = (ICompositeViewEngine)serviceProvider.GetService(typeof(ICompositeViewEngine));
        var tempDataProvider = (ITempDataProvider)serviceProvider.GetService(typeof(ITempDataProvider));
        var view = engine.FindView(actionContext, viewName, false).View;

        using var output = new StringWriter();
        var viewContext = new ViewContext(
            actionContext,
            view,
            new ViewDataDictionary(new EmptyModelMetadataProvider(), page.ModelState)
            {
                Model = model
            },
            new TempDataDictionary(httpContext, tempDataProvider),
            output,
            new HtmlHelperOptions()
        );

        await view.RenderAsync(viewContext);
        return output.ToString();
    }
}
