using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeDetailsBase : ComponentBase
    {
        public Employee Employee { get; set; } = new Employee();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public string Coordinates { get; set; }
        public string ButtonText { get; set; } = "Hide Footer";
        public string CssClass { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            Id ??= "1";
            Employee = await EmployeeService.GetEmployee(int.Parse(Id));
        }

        // protected void Mouse_Move(MouseEventArgs e){
        //     Coordinates = $"x = {e.ClientX} - Y = {e.ClientX}";
        // }

        protected void Button_Click()
        {
            if (ButtonText == "Hide Footer")
            {
                ButtonText = "Show Footer";
                CssClass = "hideFooter";
            }
            else
            {
                {
                ButtonText = "Hide Footer";
                CssClass = null;
            }
            }
        }
    }
}