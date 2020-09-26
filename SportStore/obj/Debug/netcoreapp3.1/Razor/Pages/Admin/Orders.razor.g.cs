#pragma checksum "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\Orders.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "05e72cb6992a7dce9441ab3bdd43f12042894fca"
// <auto-generated/>
#pragma warning disable 1591
namespace SportStore.Pages.Admin
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
#nullable restore
#line 1 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\_Imports.razor"
using Microsoft.AspNetCore.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\_Imports.razor"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\_Imports.razor"
using SportStore.Models;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/admin/orders")]
    public partial class Orders : OwningComponentBase<IOrderRepository>
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<SportStore.Pages.Admin.OrderTable>(0);
            __builder.AddAttribute(1, "TableTitle", "Unshipped Orders");
            __builder.AddAttribute(2, "ButtonLabel", "Ship");
            __builder.AddAttribute(3, "Orders", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.IEnumerable<SportStore.Models.Order>>(
#nullable restore
#line 5 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\Orders.razor"
                                                                     UnshippedOrders

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "OrderSelected", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Int32>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Int32>(this, 
#nullable restore
#line 5 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\Orders.razor"
                                                                                                     ShipOrder

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
            __builder.AddMarkupContent(5, "\r\n");
            __builder.OpenComponent<SportStore.Pages.Admin.OrderTable>(6);
            __builder.AddAttribute(7, "TableTitle", "Shipped Orders");
            __builder.AddAttribute(8, "ButtonLabel", "Reset");
            __builder.AddAttribute(9, "Orders", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.IEnumerable<SportStore.Models.Order>>(
#nullable restore
#line 6 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\Orders.razor"
                                                                    ShippedOrders

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(10, "OrderSelected", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Int32>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Int32>(this, 
#nullable restore
#line 6 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\Orders.razor"
                                                                                                  ResetOrder

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
            __builder.AddMarkupContent(11, "\r\n");
            __builder.OpenElement(12, "button");
            __builder.AddAttribute(13, "class", "btn btn-info");
            __builder.AddAttribute(14, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 7 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\Orders.razor"
                                         e=> UpdateData()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(15, "Refresh Data");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 10 "D:\Projects\.netbook\SportStores\SportStore\Pages\Admin\Orders.razor"
      
	public IOrderRepository Repository => Service;
	public IEnumerable<Order> AllOrders { get; set; }
	public IEnumerable<Order> UnshippedOrders { get; set; }
	public IEnumerable<Order> ShippedOrders { get; set; }

	protected async override Task OnInitializedAsync()
	{
		await UpdateData();
	}

	public async Task UpdateData()
	{
		AllOrders = await Repository.Orders.ToListAsync();
		UnshippedOrders = AllOrders.Where(o => !o.Shipped);
		ShippedOrders = AllOrders.Where(o => o.Shipped);
	}

	public void ShipOrder(int id) => UpdateOrder(id, true);
	public void ResetOrder(int id) => UpdateOrder(id, false);

	public void UpdateOrder(int id, bool shipValue)
	{
		Order o = Repository.Orders.FirstOrDefault(o => o.OrderId == id);
		o.Shipped = shipValue;
		Repository.SaveOrder(o);
	}

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
