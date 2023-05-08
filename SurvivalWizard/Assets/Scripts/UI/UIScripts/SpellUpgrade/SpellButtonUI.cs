using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalWizard.UI.UIScripts
{
    public class SpellButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _TextMeshProUGUI;

        private Button _buttonChooseSpell;
        private Image _image;

        public Image Image { get => _image ??= GetComponent<Image>(); }
        public Button ButtonChooseSpell { get => _buttonChooseSpell ??= GetComponent<Button>(); }
        public TextMeshProUGUI Text { get => _TextMeshProUGUI ?? throw new UnityException("TextMeshProUGUI is null"); }
    }
}
