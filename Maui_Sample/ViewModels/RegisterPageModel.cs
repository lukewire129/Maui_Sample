using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Maui_Sample.ViewModels.ValueChangedMessage;
using System.Collections.ObjectModel;

namespace Maui_Sample.ViewModels
{
        public enum TERSTTYPE
        {
                ALL,
                SERVICE,
                PERSONAL,
                THIRDPARTIES,
                MARKETINGS
        }

        public partial class TermsViewModel :ObservableObject
        {
                private TERSTTYPE tersValue;
                [ObservableProperty]
                private string titleText;

                private bool isChecked;

                public bool IsChecked
                {
                        get => isChecked;
                        set
                        {
                                SetProperty(ref isChecked, value);
                                WeakReferenceMessenger.Default.Send(new CheckStateChangedMessage(new CheckStateMesage()
                                {
                                        type = tersValue,
                                        state = value
                                }));
                        }
                }

                [ObservableProperty]
                private bool isSubPage =false;

                public TermsViewModel SetType(TERSTTYPE type)
                {
                        this.tersValue = type;
                        return this;
                }

                public TermsViewModel SetTtitle(string titleText)
                {
                        this.TitleText = titleText;
                        return this;
                }

                public TermsViewModel SetSubTitleEnabled()
                {
                        this.IsSubPage = true;
                        return this;
                }

                public TERSTTYPE GetTersType() => this.tersValue; 
                public void CheckStateChange(bool state) =>this.IsChecked = state;

                public bool GetEssentialTerm() => this.IsSubPage == true;

                public bool IsCheckTrue() => this.IsChecked == true;

                [RelayCommand]
                private void Area()
                {
                        this.IsChecked = !this.IsChecked;
                        WeakReferenceMessenger.Default.Send(new CheckStateChangedMessage(new CheckStateMesage()
                        {
                                type = tersValue,
                                state = this.IsChecked
                        }));
                }
        }

        public partial class RegisterPageModel : ObservableObject
        {
                [ObservableProperty]
                private ObservableCollection<TermsViewModel> termsViewModels = new ObservableCollection<TermsViewModel>();

                [ObservableProperty]
                private bool nextButtonEnable  =false;

                public RegisterPageModel()
                {
                        TermsViewModels.Add(new TermsViewModel()
                                                .SetType(TERSTTYPE.ALL)
                                                .SetTtitle("약관 전체 동의"));
                        TermsViewModels.Add(new TermsViewModel()
                                                .SetType(TERSTTYPE.SERVICE)
                                                .SetTtitle("서비스 이용약관 (필수)")
                                                .SetSubTitleEnabled());
                        TermsViewModels.Add(new TermsViewModel()
                                                .SetType(TERSTTYPE.PERSONAL)
                                                .SetTtitle("개인정보 수집/이용 동의 (필수)")
                                                .SetSubTitleEnabled());
                        TermsViewModels.Add(new TermsViewModel()
                                                .SetType(TERSTTYPE.THIRDPARTIES)
                                                .SetTtitle("개인정보 제3자 정보제공 동의 (필수)")
                                                .SetSubTitleEnabled());
                        TermsViewModels.Add(new TermsViewModel()
                                                .SetType(TERSTTYPE.MARKETINGS)
                                                .SetTtitle("마케팅 및 이용정보 수신 동의 (선택)"));

                        // Register a message in some module
                        WeakReferenceMessenger.Default.Register<CheckStateChangedMessage>(this, (r, m) =>
                        {
                                var value = m.Value;
                                if (value.type == TERSTTYPE.MARKETINGS)
                                        return;

                                if (value.type == TERSTTYPE.ALL)
                                {
                                        foreach(var termsVieModel in this.TermsViewModels.Where(x=>x.GetTersType() != TERSTTYPE.ALL))
                                        {
                                                termsVieModel.CheckStateChange(value.state);
                                        }
                                        return;
                                }

                                NextButtonEnable = this.TermsViewModels.Where(x => x.GetEssentialTerm() == true)
                                                                                            .All(x => x.IsCheckTrue());
                        });
                }
        }
}
