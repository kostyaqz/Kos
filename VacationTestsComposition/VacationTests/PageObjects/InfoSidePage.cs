using System;
using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlFactories;
using VacationTests.Infrastructure.ControlServices;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageObjects
{
    public class InfoSidePage
    {
        private readonly ModalContextProvider modalContextProvider;
        private readonly Lazy<Label> headerLabel;
        private readonly Lazy<Button> crossButton;

        private readonly Lazy<Label> bodyLabel;
        private readonly Lazy<Button> agreeButton;
        private readonly Lazy<Button> notAgreeButton;
        private readonly Lazy<Button<LoginPage>> closeButton;
    
        public InfoSidePage(
            ControlFactory<Label> labelFactory,
            ControlFactory<Button> buttonFactory,
            ControlFactory controlFactory,
            ModalContextProvider modalContextProvider)
        {
            this.modalContextProvider = modalContextProvider;
            headerLabel = new(() => labelFactory.Create(GetModalContext(), new CssBy().WithTid("SidePageHeader")));
            crossButton = new(() => buttonFactory.Create(GetModalContext(), new CssBy().WithTid("SidePage__close")));

            bodyLabel = new(() => labelFactory.Create(GetModalContext(), new CssBy().WithTid("SidePageBody")));
            agreeButton = new(() => buttonFactory.Create(GetModalContext(), new CssBy().WithTid("AgreeButton")));
            notAgreeButton = new(() => buttonFactory.Create(GetModalContext(), new CssBy().WithTid("NotAgreeButton")));
            closeButton = new(() => controlFactory.Create<Button<LoginPage>>(GetModalContext(), new CssBy().WithTid("CloseButton")));
        }

        private IWebElement GetModalContext() => modalContextProvider.Get("InfoSidePage");

        public Label HeaderLabel => headerLabel.Value; 
        public Button CrossButton => crossButton.Value;

        public Label BodyLabel => bodyLabel.Value;
        public Button AgreeButton => agreeButton.Value;
        public Button NotAgreeButton => notAgreeButton.Value;
        public Button<LoginPage> CloseButton => closeButton.Value;
    }
}