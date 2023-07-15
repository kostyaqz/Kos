using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlFactories;
using VacationTests.Infrastructure.ControlServices;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class ClaimLightbox
    {
        private readonly ControlFactory<Label> labelFactory;
        private readonly ControlFactory controlFactory;
        private readonly ModalContextProvider modalContextProvider;

        public ClaimLightbox(
            ControlFactory<Label> labelFactory,
            ControlFactory controlFactory,
            ModalContextProvider modalContextProvider)
        {
            this.labelFactory = labelFactory;
            this.controlFactory = controlFactory;
            this.modalContextProvider = modalContextProvider;
        }

        private IWebElement GetModalContext() => modalContextProvider.Get("ClaimModal");

        public Label ModalHeaderLabel => labelFactory.Create(GetModalContext(), new CssBy().WithTid("ModalHeader"));
        public Button CrossButton => controlFactory.Create<Button>(GetModalContext(), new CssBy().WithTid("modal-close"));
        public Label StatusLabel => labelFactory.Create(GetModalContext(), new CssBy().WithTid("StatusLabel"));
        public Label ClaimTypeLabel => labelFactory.Create(GetModalContext(), new CssBy().WithTid("ClaimTypeLabel"));
        public Label ChildAgeLabel => labelFactory.Create(GetModalContext(), new CssBy().WithTid("ChildAgeLabel"));
        public Label PeriodLabel => labelFactory.Create(GetModalContext(), new CssBy().WithTid("PeriodLabel"));

        public Label AvailableDaysMessageLabel => labelFactory.Create(GetModalContext(), new CssBy().WithTid("AvailableDaysMessageLabel"));
        public Label AvailableDaysLabel => labelFactory.Create(GetModalContext(), new CssBy().WithTid("AvailableDaysLabel"));
        public Checkbox PayNowCheckbox => controlFactory.Create<Checkbox>(GetModalContext(), new CssBy().WithTid("PayNowCheckbox"));
        public Label DirectorFioLabel => labelFactory.Create(GetModalContext(), new CssBy().WithTid("DirectorFioLabel"));

        public ClaimLightboxFooter Footer =>
            controlFactory.Create<ClaimLightboxFooter>(GetModalContext(), new CssBy().WithTid("ModalFooter"));
    }
}