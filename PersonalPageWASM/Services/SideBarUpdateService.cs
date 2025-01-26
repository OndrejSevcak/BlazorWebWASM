namespace PersonalPageWASM.Services
{
    public class SideBarUpdateService
    {
        public event EventHandler<string> UpdateSidebarActiveItem;

        public void InvokeUpdateSidebar(string activeItem)
        {
            UpdateSidebarActiveItem.Invoke(this, activeItem);
        }
    }
}
