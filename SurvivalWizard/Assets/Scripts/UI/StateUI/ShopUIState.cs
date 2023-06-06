
using SurvivalWizard.UI.UIScripts;

namespace SurvivalWizard.Assets.Scripts.UI.StateUI
{
    public class ShopUIState : IStateUI
    {
        private LoaderUI _loaderUI;
        private ShopUI _shopUI;

        public ShopUIState(LoaderUI loaderUI, ShopUI shopUI)
        {
            _loaderUI = loaderUI;
            _shopUI = shopUI;
        }
        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}
